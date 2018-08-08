using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class DecimalTypeConverter : ITypeConverter<object>
    {
        public object ConvertFromString(string stringValue)
        {
            return decimal.Parse(stringValue.Trim());
        }
    }
}