using FlatFile.FixedWidth.Interfaces.Generic;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public abstract class TypeConverter<T> : ITypeConverter<T>
    {
        public object GetConvertedValue(string stringValue)
        {
            return ConvertFromString(stringValue);
        }

        public abstract T ConvertFromString(string stringValue);
    }
}