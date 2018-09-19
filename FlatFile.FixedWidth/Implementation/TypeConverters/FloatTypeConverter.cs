using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class FloatTypeConverter : TypeConverterBase<float>, ITypeConverter<float>
    {
        public override float ConvertFromString(string stringValue)
        {
            return float.Parse(stringValue.Trim());
        }
    }
}