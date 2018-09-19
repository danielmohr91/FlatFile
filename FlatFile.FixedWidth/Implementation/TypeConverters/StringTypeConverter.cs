using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class StringTypeConverter : ITypeConverter<string>
    {
        private readonly bool trim;

        public StringTypeConverter(bool trim = true)
        {
            this.trim = trim;
        }

        public string ConvertFromString(string stringValue)
        {
            return trim ? stringValue.Trim() : stringValue;
        }

        dynamic ITypeConverterBase.ConvertFromString(string stringValue)
        {
            return ConvertFromString(stringValue);
        }
    }
}