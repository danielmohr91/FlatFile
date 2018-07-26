using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class UIntTypeConverter : ITypeConverter<uint>
    {
        public uint ConvertFromString(string stringValue)
        {
            return uint.Parse(stringValue.Trim());
        }
    }
}