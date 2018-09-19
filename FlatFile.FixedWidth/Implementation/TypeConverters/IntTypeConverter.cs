using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class IntTypeConverter : TypeConverterBase<int>, ITypeConverter<int>
    {
        public override int ConvertFromString(string stringValue)
        {
            return int.Parse(stringValue.Trim());
        }
    }
}