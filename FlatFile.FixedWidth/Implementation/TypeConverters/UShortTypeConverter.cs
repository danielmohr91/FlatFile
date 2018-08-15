using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class UShortTypeConverter : ITypeConverter<ushort>
    {
        public ushort ConvertFromString(string stringValue)
        {
            return ushort.Parse(stringValue.Trim());
        }
    }
}