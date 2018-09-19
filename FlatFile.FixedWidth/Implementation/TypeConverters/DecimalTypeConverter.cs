using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class DecimalTypeConverter : ITypeConverter<decimal>
    {
        public decimal ConvertFromString(string stringValue)
        {
            return decimal.Parse(stringValue.Trim());
        }

        dynamic ITypeConverterBase.ConvertFromString(string stringValue)
        {
            return ConvertFromString(stringValue);
        }
    }
}