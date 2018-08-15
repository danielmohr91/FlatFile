using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class FloatTypeConverter : ITypeConverter<object>
    {
        public object ConvertFromString(string stringValue)
        {
            return float.Parse(stringValue.Trim());
        }
    }
}