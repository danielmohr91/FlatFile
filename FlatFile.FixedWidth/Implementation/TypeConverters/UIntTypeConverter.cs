using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class UIntTypeConverter : TypeConverterBase<uint>, ITypeConverter<uint>
    {
        public override uint ConvertFromString(string stringValue)
        {
            return uint.Parse(stringValue.Trim());
        }
    }
}