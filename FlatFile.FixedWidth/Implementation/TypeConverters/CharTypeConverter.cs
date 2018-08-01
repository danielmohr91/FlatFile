using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class CharTypeConverter : ITypeConverter<object>
    {
        public object ConvertFromString(string stringValue)
        {
            return char.Parse(stringValue.Trim());
        }
    }
}