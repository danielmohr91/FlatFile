using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class ShortTypeConverter : TypeConverterBase<short>, ITypeConverter<short>
    {
        public override short ConvertFromString(string stringValue)
        {
            return short.Parse(stringValue.Trim());
        }
    }
}