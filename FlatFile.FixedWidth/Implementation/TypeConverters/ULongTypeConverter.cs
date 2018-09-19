using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class ULongTypeConverter : TypeConverterBase<ulong>, ITypeConverter<ulong>
    {
        public override ulong ConvertFromString(string stringValue)
        {
            return ulong.Parse(stringValue.Trim());
        }
    }
}