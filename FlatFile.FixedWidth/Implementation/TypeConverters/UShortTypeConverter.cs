using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class UShortTypeConverter : ITypeConverter<object>
    {
        public object ConvertFromString(string stringValue)
        {
            return ushort.Parse(stringValue.Trim());
        }
    }
}