using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public abstract class TypeConverter<T> : ITypeConverter<T>
    {
        dynamic ITypeConverterBase.ConvertFromString(string stringValue)
        {
            return ConvertFromString(stringValue);
        }

        public abstract T ConvertFromString(string stringValue);
    }
}