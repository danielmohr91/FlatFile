using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class DoubleTypeConverter : ITypeConverter<object>
    {
        public object ConvertFromString(string stringValue)
        {
            return double.Parse(stringValue.Trim());
        }
    }
}