using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public abstract class TypeConverterBase<T> : ITypeConverterBase
    {
        dynamic ITypeConverterBase.ConvertFromString(string stringValue)
        {
            return ConvertFromString(stringValue);
        }

        public abstract T ConvertFromString(string stringValue);
    }
}