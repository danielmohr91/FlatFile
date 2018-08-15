using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class LongTypeConverter : ITypeConverter<object>
    {
        public object ConvertFromString(string stringValue)
        {
            return long.Parse(stringValue.Trim());
        }
    }
}