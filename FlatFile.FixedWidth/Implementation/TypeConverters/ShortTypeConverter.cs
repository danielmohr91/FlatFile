using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class ShortTypeConverter : ITypeConverter<short>
    {
        public short ConvertFromString(string stringValue)
        {
            return short.Parse(stringValue.Trim());
        }

        dynamic ITypeConverterBase.ConvertFromString(string stringValue)
        {
            return ConvertFromString(stringValue);
        }
    }
}