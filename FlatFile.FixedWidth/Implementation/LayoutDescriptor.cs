using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FlatFile.FixedWidth.Implementation.TypeConverters;
using FlatFile.FixedWidth.Interfaces;
using FlatFile.FixedWidth.Models;

namespace FlatFile.FixedWidth.Implementation
{
    /// <summary>
    ///     Describes a single row of type TTarget.
    ///     Code review comments:
    ///     - Follows open / closed principle. If model changes, nothing in here changes since generic was used.
    ///     - If ascii encoded file changes to unicode file, nothing in here changes, etc...
    /// </summary>
    /// <typeparam name="TTarget">Type of target model</typeparam>
    public class LayoutDescriptor<TTarget> : IFlatFileLayoutDescriptor<TTarget>
    {
        private readonly IDictionary<int, IFixedFieldSetting<TTarget>> fields;

        // TODO: Make a static factory to get the type converter. Changed ITypeConverter<object> to just object for now
        //private readonly IDictionary<Type, ITypeConverter<TTarget>> typeConverters;

        private int currentPosition;
        private ICollection<IFixedFieldSetting<TTarget>> orderedFields;

        public LayoutDescriptor()
        {
            fields = new Dictionary<int, IFixedFieldSetting<TTarget>>();
            // typeConverters = GetTypeConverters();
        }

        /// <summary>
        ///     Implements IFlatFileLayoutDescriptor.
        ///     Note that this could throw key not found exception. Perhaps wrap this...
        /// </summary>
        public IFixedFieldSetting<TTarget> GetField(int key)
        {
            return fields[key];
        }

        /// <inheritdoc />
        public ICollection<IFixedFieldSetting<TTarget>> GetOrderedFields()
        {
            // @Lee - Is there a best practice for ToList vs. casting to a collection? 

            //      - ToList creates a copy (https://msdn.microsoft.com/en-us/library/bb342261(v=vs.110).aspx)
            //      - The ToList<TSource>(IEnumerable<TSource>) method forces immediate query evaluation and returns a List<T> that contains the query results. You can append this method to your query in order to obtain a cached copy of the query results.

            //      - Casting to ICollection (instead of calling Enumerable.ToList()) so no cached copy is created.
            // NOPE - Unable to cast object of type 'WhereSelectEnumerableIterator`2[System.Collections.Generic.KeyValuePair`2[System.Int32,FlatFile.FixedWidth.Interfaces.IFixedFieldSetting],FlatFile.FixedWidth.Interfaces.IFixedFieldSetting]' to type 
            // This cast generated an invalid cast exception, details above. Using ToList instead. 

            if (orderedFields == null)
                orderedFields = fields
                    .OrderBy(x => x.Key)
                    .Select(x => x.Value)
                    .ToList();

            return orderedFields;
        }

        /// <summary>
        ///     Implements IFlatFileLayoutDescriptor.
        ///     Positions are managed internally in an auto ordered settings container.
        /// </summary>
        // public IFlatFileLayoutDescriptor<TTarget> AppendField<TProperty>(Expression<Func<TTarget, TProperty>> expression, int fieldLength)
        public IFlatFileLayoutDescriptor<TTarget> AppendField<TProperty>(Expression<Func<TTarget, TProperty>> expression, int fieldLength)
        {
            var propertyInfo = GetMemberExpression(expression.Body).Member as PropertyInfo;
            var typeConverter = GetTypeConverter(propertyInfo.PropertyType);

            if (propertyInfo != null && typeConverter != null)
            {
                //var stronglyTyped = (ITypeConverter<dynamic>)typeConverter;
                // Generics are really aimed at static typing rather than types only known at execution time
                // Using dynamic for now, probably the most specific I can get above "object"

                Add(fieldLength, propertyInfo, typeConverter);
                return this;
            }

            throw new ArgumentException($"No default type converter defined for object type: {propertyInfo?.PropertyType}. Please explicitly define a TypeConverter.");
        }

        private void Add(int length, PropertyInfo property, ITypeConverter<TTarget> typeConverter)
        {
            Add(length, property);
            fields[currentPosition].TypeConverter = typeConverter;
        }

        /// <summary>
        ///     Adds Property Info into next available position. Positions are managed internally, and Length is calculated base on
        ///     last position and last field length.
        /// </summary>
        /// <param name="length">Length in characters of the field</param>
        /// <param name="property">Property Info of the field</param>
        private void Add(int length, PropertyInfo property)
        {
            var startPosition = 0;
            IFixedFieldSetting<TTarget> key;
            if (fields.TryGetValue(currentPosition, out key))
            {
                currentPosition++;
                startPosition = key.StartPosition + key.Length;
            }

            var setting = new FixedFieldSetting<TTarget>
            {
                StartPosition = startPosition,
                Length = length,
                PropertyInfo = property
            };

            fields[currentPosition] = setting;

            orderedFields = null; // Ordered fields are now dirty, clear cache
        }

        public IFlatFileLayoutDescriptor<TTarget> AppendField<TProperty>(Expression<Func<TTarget, TProperty>> expression, int fieldLength, ITypeConverter<TTarget> typeConverter)
        {
            var propertyInfo = GetMemberExpression(expression.Body).Member as PropertyInfo;

            Add(fieldLength, propertyInfo, typeConverter);
            return this;
        }


        /// <summary>
        ///     Gets the member expression. Only Member Access is currently implemented.
        /// </summary>
        /// <param name="body">Expression to be casted</param>
        /// <returns>Member Expression for provided expression</returns>
        private MemberExpression GetMemberExpression(Expression body)
        {
            // Must treat MemberAccess expressions differently from lambda expressions, et al.
            if (body.NodeType == ExpressionType.MemberAccess) return body as MemberExpression;
            return null;
        }

        private ITypeConverter<TTarget> GetTypeConverter(Type propertyInfoPropertyType)
        {
            if (typeof(TTarget) == typeof(bool))
                return (ITypeConverter<TTarget>) new BooleanTypeConverter();
            return (ITypeConverter<TTarget>) new IntTypeConverter(); // TODO: finish these}
        }

// Make this a static factory instead
//private IDictionary<Type, ITypeConverter<TTarget>> GetTypeConverters()
//{

//    // Should ITypeConverter take a generic? Makes use cases like this cumbersome.
//    // https://stackoverflow.com/questions/353126/c-sharp-multiple-generic-types-in-one-list
//    var  boolConverter = new BooleanTypeConverter();

//    //var x = new BooleanTypeConverter(); // some arbitrary expression for an example.
//    //Type T = x.GetType(); // or set T however you wish.

//    //Type objectType = typeof(BooleanTypeConverter);
//    //var genericType = objectType.MakeGenericType(T);
//    //var instance = Activator.CreateInstance(genericType);


//    return new Dictionary<Type, ITypeConverter<dynamic>>
//    {
//        {
//            typeof(bool),
//            boolConverter
//        }//,
//        //{
//        //    typeof(decimal),
//        //    new DecimalTypeConverter()
//        //},
//        //{
//        //    typeof(double),
//        //    new DoubleTypeConverter()
//        //},
//        //{
//        //    typeof(float),
//        //    new FloatTypeConverter()
//        //},
//        //{
//        //    typeof(int),
//        //    new IntTypeConverter()
//        //},
//        //{
//        //    typeof(uint),
//        //    new UIntTypeConverter()
//        //},
//        //{
//        //    typeof(long),
//        //    new LongTypeConverter()
//        //},
//        //{
//        //    typeof(ulong),
//        //    new ULongTypeConverter()
//        //},
//        //{
//        //    typeof(short),
//        //    new ShortTypeConverter()
//        //},
//        //{
//        //    typeof(ushort),
//        //    new UShortTypeConverter()
//        //},
//        //{
//        //    typeof(string),
//        //    new StringTypeConverter()
//        //}
//    };
    }
}