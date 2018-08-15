using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class ULongTypeConverter : ITypeConverter<ulong>
    {
        public ulong ConvertFromString(string stringValue)
        {
            return ulong.Parse(stringValue.Trim());
        }
    }
}