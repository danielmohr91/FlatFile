using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class DecimalTypeConverter : TypeConverterBase<decimal>, ITypeConverter<decimal>
    {
        public override decimal ConvertFromString(string stringValue)
        {
            return decimal.Parse(stringValue.Trim());
        }
    }
}