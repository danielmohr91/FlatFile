using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class ULongTypeConverter : ITypeConverter<object>
    {
        public object ConvertFromString(string stringValue)
        {
            return ulong.Parse(stringValue.Trim());
        }
    }
}