using System.Reflection;

namespace FlatFile.FixedWidth.Interfaces
{
    public interface IFixedFieldSetting<TProperty>
    {
        /// <summary>
        ///     Length of the field
        /// </summary>
        int Length { get; set; }

        /// <summary>
        ///     Zero based start position from the left
        /// </summary>
        int StartPosition { get; set; }

        /// <summary>
        ///     Property Info for target field
        /// </summary>
        PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        ///     Custom Type Converter (overrides default converter)
        /// </summary>
        ITypeConverter<TProperty> TypeConverter { get; set; }
    }
}