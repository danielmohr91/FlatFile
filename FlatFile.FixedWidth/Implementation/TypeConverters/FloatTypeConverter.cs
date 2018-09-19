using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class FloatTypeConverter : ITypeConverter<float>
    {
        public float ConvertFromString(string stringValue)
        {
            return float.Parse(stringValue.Trim());
        }

        dynamic ITypeConverterBase.ConvertFromString(string stringValue)
        {
            return ConvertFromString(stringValue);
        }
    }
}