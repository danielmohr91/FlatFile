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
        private readonly IDictionary<int, IFixedFieldSetting> fields;

        // Make a factory to get the type converter, or change ITypeConverter<object> to just object
        private readonly IDictionary<Type, object> typeConverters;



        private int currentPosition;
        private ICollection<IFixedFieldSetting> orderedFields;

        public LayoutDescriptor()
        {
            fields = new Dictionary<int, IFixedFieldSetting>();
            typeConverters = GetTypeConverters();
        }

        /// <summary>
        ///     Implements IFlatFileLayoutDescriptor.
        ///     Note that this could throw key not found exception. Perhaps wrap this...
        /// </summary>
        public IFixedFieldSetting GetField(int key) => fields[key];

        /// <inheritdoc />
        public ICollection<IFixedFieldSetting> GetOrderedFields()
        {
            // @Lee - Is there a best practice for ToList vs. casting to a collection? 

            //      - ToList creates a copy (https://msdn.microsoft.com/en-us/library/bb342261(v=vs.110).aspx)
            //      - The ToList<TSource>(IEnumerable<TSource>) method forces immediate query evaluation and returns a List<T> that contains the query results. You can append this method to your query in order to obtain a cached copy of the query results.

            //      - Casting to ICollection (instead of calling Enumerable.ToList()) so no cached copy is created.
            // NOPE - Unable to cast object of type 'WhereSelectEnumerableIterator`2[System.Collections.Generic.KeyValuePair`2[System.Int32,FlatFile.FixedWidth.Interfaces.IFixedFieldSetting],FlatFile.FixedWidth.Interfaces.IFixedFieldSetting]' to type 
            // This cast generated an invalid cast exception, details above. Using ToList instead. 

            if (orderedFields == null)
            {
                orderedFields = fields
                    .OrderBy(x => x.Key)
                    .Select(x => x.Value)
                    .ToList();
            }

            return orderedFields;
        }

        /// <summary>
        ///     Implements IFlatFileLayoutDescriptor.
        ///     Positions are managed internally in an auto ordered settings container.
        /// </summary>
        public IFlatFileLayoutDescriptor<TTarget> AppendField<TProperty>(Expression<Func<TTarget, TProperty>> expression, int fieldLength)
        {
            var propertyInfo = GetMemberExpression(expression.Body).Member as PropertyInfo;

            if (propertyInfo != null && typeConverters.TryGetValue(propertyInfo.PropertyType, out var converter))
            {
                Add(fieldLength, propertyInfo, converter);
                return this;
            }

            throw new ArgumentException($"No default type converter defined for object type: {propertyInfo?.PropertyType}. Please explicitly define a TypeConverter.");
        }

        public IFlatFileLayoutDescriptor<TTarget> AppendField<TProperty>(Expression<Func<TTarget, TProperty>> expression, int fieldLength, object typeConverter)
        {
            var propertyInfo = GetMemberExpression(expression.Body).Member as PropertyInfo;

            Add(fieldLength, propertyInfo, typeConverter);
            return this;
        }

        private void Add(int length, PropertyInfo property, object typeConverter)
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
            int startPosition = 0;
            IFixedFieldSetting key;
            if (fields.TryGetValue(currentPosition, out key))
            {
                currentPosition++;
                startPosition = key.StartPosition + key.Length;
            }

            var setting = new FixedFieldSetting
            {
                StartPosition = startPosition,
                Length = length,
                PropertyInfo = property
            };

            fields[currentPosition] = setting;

            orderedFields = null; // Ordered fields are now dirty, clear cache
        }


        /// <summary>
        ///     Gets the member expression. Only Member Access is currently implemented.
        /// </summary>
        /// <param name="body">Expression to be casted</param>
        /// <returns>Member Expression for provided expression</returns>
        private MemberExpression GetMemberExpression(Expression body)
        {
            // Must treat MemberAccess expressions differently from lambda expressions, et al.
            if (body.NodeType == ExpressionType.MemberAccess)
            {
                return body as MemberExpression;
            }

            return null;
        }

        private IDictionary<Type, object> GetTypeConverters()
        {
 
            // Should ITypeConverter take a generic? Makes use cases like this cumbersome.
            // https://stackoverflow.com/questions/353126/c-sharp-multiple-generic-types-in-one-list

            return new Dictionary<Type, object>
            {
                {
                    typeof(bool),
                    new BooleanTypeConverter()
                },
                {
                    typeof(decimal),
                    new DecimalTypeConverter()
                },
                {
                    typeof(double),
                    new DoubleTypeConverter()
                },
                {
                    typeof(float),
                    new FloatTypeConverter()
                },
                {
                    typeof(int),
                    new IntTypeConverter()
                },
                {
                    typeof(uint),
                    new UIntTypeConverter()
                },
                {
                    typeof(long),
                    new LongTypeConverter()
                },
                {
                    typeof(ulong),
                    new ULongTypeConverter()
                },
                {
                    typeof(short),
                    new ShortTypeConverter()
                },
                {
                    typeof(ushort),
                    new UShortTypeConverter()
                },
                {
                    typeof(string),
                    new StringTypeConverter()
                }
            };
        }
    }
}