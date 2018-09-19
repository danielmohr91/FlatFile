using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class BooleanTypeConverter : TypeConverterBase<bool>, ITypeConverter<bool>
    {
        public override bool ConvertFromString(string stringValue)
        {
            return bool.Parse(stringValue.Trim());
        }
    }
}