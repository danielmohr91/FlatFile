using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class LongTypeConverter : TypeConverterBase<long>, ITypeConverter<long>
    {
        public override long ConvertFromString(string stringValue)
        {
            return long.Parse(stringValue.Trim());
        }
    }
}