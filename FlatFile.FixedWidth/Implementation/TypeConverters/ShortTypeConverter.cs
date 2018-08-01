using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class ShortTypeConverter : ITypeConverter<object>
    {
        public object ConvertFromString(string stringValue)
        {
            return short.Parse(stringValue.Trim());
        }
    }
}