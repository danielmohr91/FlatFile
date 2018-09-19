using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class UShortTypeConverter : TypeConverterBase<ushort>, ITypeConverter<ushort>
    {
        public override ushort ConvertFromString(string stringValue)
        {
            return ushort.Parse(stringValue.Trim());
        }
    }
}