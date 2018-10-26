using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FlatFile.FixedWidth.Interfaces.Generic;

namespace FlatFile.FixedWidth.Interfaces
{
    /// <summary>
    ///     Interface for Flat File settings.
    ///     Since fieldLength is specific to a flat file, this interface was named
    ///     IFlatFileLayoutDescriptor, not ILayoutDescriptor
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    public interface IFlatFileLayoutDescriptor<TTarget>
    {
        /// <summary>
        ///     Appends field into next position. Order is important.
        ///     Order added must match column order in flat file.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression">Expression for target model and field</param>
        /// <param name="fieldLength">Length of the field in characters</param>
        /// <param name="shouldSkip">True if column should be skipped</param>
        /// <returns></returns>
        IFlatFileLayoutDescriptor<TTarget> AppendField<TProperty>(Expression<Func<TTarget, TProperty>> expression, int fieldLength, bool shouldSkip = false);

        /// <summary>
        ///     Appends field into next position. Order is important.
        ///     Order added must match column order in flat file.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression">Expression for target model and field</param>
        /// <param name="fieldLength">Length of the field in characters</param>
        /// ///
        /// <param name="typeConverter">Custom type converter (overrides default)</param>
        /// <returns></returns>
        IFlatFileLayoutDescriptor<TTarget> AppendField<TProperty>(Expression<Func<TTarget, TProperty>> expression, int fieldLength, ITypeConverter<TProperty> typeConverter);

        /// <summary>
        ///     Returns field for specified key. Returns null if not found.
        /// </summary>
        /// <param name="key">Index of column</param>
        /// <returns>Field, if found. Else, throws exception</returns>
        IFixedFieldSetting GetField(int key);

        /// <summary>
        ///     Returns all fields, ordered by key. This corresponds to the Left to Right position of each column in the flat file.
        /// </summary>
        /// <param name="key">Index of column</param>
        /// <returns>Field, if found. Else, throws exception</returns>
        ICollection<IFixedFieldSetting> GetOrderedFields();
    }
}