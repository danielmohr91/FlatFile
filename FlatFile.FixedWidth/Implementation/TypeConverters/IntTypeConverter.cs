using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class IntTypeConverter : ITypeConverter<object>
    {
        public object ConvertFromString(string stringValue)
        {
            return int.Parse(stringValue.Trim());
        }
    }
}