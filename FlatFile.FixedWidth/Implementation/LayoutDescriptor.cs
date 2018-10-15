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

        public LayoutDescriptor()
        {
            fields = new Dictionary<int, IFixedFieldSetting>();
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

        /// <summary>
        ///     Implements IFlatFileLayoutDescriptor.
        ///     Positions are managed internally in an auto ordered settings container.
        /// </summary>
        public IFlatFileLayoutDescriptor<TTarget> AppendField<TProperty>(
            Expression<Func<TTarget, TProperty>> expression,
            int fieldLength)
        {
            var propertyInfo = GetMemberExpression(expression.Body).Member as PropertyInfo;
            var typeConverter = GetTypeConverter<TProperty>();

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

        private void Add<TProperty>(int length, PropertyInfo property, ITypeConverter<TProperty> typeConverter)
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

            if (fields.TryGetValue(currentPosition, out var key))
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

        private ITypeConverter<T> GetTypeConverter<T>()
        {
            if (typeof(T) == typeof(bool))
            {
                return (ITypeConverter<T>) new BooleanTypeConverter();
            }

            if (typeof(T) == typeof(DateTime))
            {
                return (ITypeConverter<T>) new DateTimeTypeConverter();
            }

            if (typeof(T) == typeof(decimal))
            {
                return (ITypeConverter<T>) new DecimalTypeConverter();
            }

            if (typeof(T) == typeof(double))
            {
                return (ITypeConverter<T>) new DoubleTypeConverter();
            }

            if (typeof(T) == typeof(float))
            {
                return (ITypeConverter<T>) new FloatTypeConverter();
            }

            if (typeof(T) == typeof(int))
            {
                return (ITypeConverter<T>) new IntTypeConverter();
            }

            if (typeof(T) == typeof(long))
            {
                return (ITypeConverter<T>) new LongTypeConverter();
            }

            if (typeof(T) == typeof(short))
            {
                return (ITypeConverter<T>) new ShortTypeConverter();
            }

            if (typeof(T) == typeof(string))
            {
                return (ITypeConverter<T>) new StringTypeConverter();
            }

            if (typeof(T) == typeof(uint))
            {
                return (ITypeConverter<T>) new UIntTypeConverter();
            }

            if (typeof(T) == typeof(ulong))
            {
                return (ITypeConverter<T>) new ULongTypeConverter();
            }

            if (typeof(T) == typeof(ushort))
            {
                return (ITypeConverter<T>) new UShortTypeConverter();
            }

            return null;
        }
    }
}