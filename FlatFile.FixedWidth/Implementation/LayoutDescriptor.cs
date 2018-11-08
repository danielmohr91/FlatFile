using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FlatFile.FixedWidth.Implementation.TypeConverters;
using FlatFile.FixedWidth.Interfaces;
using FlatFile.FixedWidth.Interfaces.Generic;
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
        private readonly IDictionary<int, IFixedFieldSetting> fields; // Generic type for fixed field setting represents the property, not the whole model (TTarget). Since the properties may be assorted, using object for now.

        private int currentPosition;
        private ICollection<IFixedFieldSetting> orderedFields;
        private List<ITestForSkip> skipDefinitions;

        public LayoutDescriptor()
        {
            fields = new Dictionary<int, IFixedFieldSetting>();
            skipDefinitions = new List<ITestForSkip>();
        }


        public IFlatFileLayoutDescriptor<TTarget> AppendIgnoredField(int fieldLength)
        {
            Add(fieldLength, null, true);
            return this;
        }

        /// <summary>
        ///     Implements IFlatFileLayoutDescriptor.
        ///     Note that this could throw key not found exception. Perhaps wrap this...
        /// </summary>
        public IFixedFieldSetting GetField(int key)
        {
            return fields[key];
        }

        /// <inheritdoc />
        public ICollection<IFixedFieldSetting> GetOrderedFields()
        {
            return orderedFields ?? (orderedFields = fields
                       .OrderBy(x => x.Key)
                       .Select(x => x.Value)
                       .ToList());
        }

        public ICollection<ITestForSkip> GetSkipDefinitions()
        {
            return skipDefinitions;
        }

        public IFlatFileLayoutDescriptor<TTarget> WithSkipDefinition(ITestForSkip skipDefinition)
        {
            skipDefinitions.Add(skipDefinition);
            return this;
        }

        /// <summary>
        ///     Implements IFlatFileLayoutDescriptor.
        ///     Positions are managed internally in an auto ordered settings container.
        /// </summary>
        public IFlatFileLayoutDescriptor<TTarget> AppendField<TProperty>(
            Expression<Func<TTarget, TProperty>> expression,
            int fieldLength,
            bool shouldSkip = false)
        {
            var propertyInfo = GetMemberExpression(expression.Body).Member as PropertyInfo;
            var typeConverter = GetTypeConverter(typeof(TProperty));

            if (propertyInfo != null && typeConverter != null)
            {
                Add(fieldLength, propertyInfo, typeConverter);
                return this;
            }

            throw new ArgumentException($"No default type converter defined for object type: {propertyInfo?.PropertyType}. Please explicitly define a TypeConverter.");
        }

        public IFlatFileLayoutDescriptor<TTarget> AppendField<TProperty>(
            Expression<Func<TTarget, TProperty>> expression,
            int fieldLength,
            ITypeConverter<TProperty> typeConverter)
        {
            var propertyInfo = GetMemberExpression(expression.Body).Member as PropertyInfo;

            Add(fieldLength, propertyInfo, typeConverter);
            return this;
        }

        private void Add(int length, PropertyInfo property, ITypeConverter typeConverter)
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
        /// <param name="shouldSkip">True if field should be skipped</param>
        private void Add(int length, PropertyInfo property, bool shouldSkip = false)
        {
            var startPosition = 0;

            if (fields.TryGetValue(currentPosition, out var key))
            {
                currentPosition++;
                startPosition = key.StartPosition + key.Length;
            }

            var setting = new FixedFieldSetting
            {
                StartPosition = startPosition,
                Length = length,
                PropertyInfo = property,
                ShouldSkip = shouldSkip
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

        private ITypeConverter GetTypeConverter(Type fieldType)
        {
            if (fieldType == typeof(bool))
            {
                return new BooleanTypeConverter();
            }

            if (fieldType == typeof(DateTime))
            {
                return new DateTimeTypeConverter();
            }

            if (fieldType == typeof(decimal))
            {
                return new DecimalTypeConverter();
            }

            if (fieldType == typeof(double))
            {
                return new DoubleTypeConverter();
            }

            if (fieldType == typeof(float))
            {
                return new FloatTypeConverter();
            }

            if (fieldType == typeof(int))
            {
                return new IntTypeConverter();
            }

            if (fieldType == typeof(long))
            {
                return new LongTypeConverter();
            }

            if (fieldType == typeof(short))
            {
                return new ShortTypeConverter();
            }

            if (fieldType == typeof(string))
            {
                return new StringTypeConverter();
            }

            if (fieldType == typeof(uint))
            {
                return new UIntTypeConverter();
            }

            if (fieldType == typeof(ulong))
            {
                return new ULongTypeConverter();
            }

            if (fieldType == typeof(ushort))
            {
                return new UShortTypeConverter();
            }

            return null;
        }
    }
}