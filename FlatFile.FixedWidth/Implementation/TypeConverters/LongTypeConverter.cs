using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class LongTypeConverter : ITypeConverter<long>
    {
        public long ConvertFromString(string stringValue)
        {
            return long.Parse(stringValue.Trim());
        }

        dynamic ITypeConverterBase.ConvertFromString(string stringValue)
        {
            return ConvertFromString(stringValue);
        }
    }
}