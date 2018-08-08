using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class StringTypeConverter : ITypeConverter<object>
    {
        private readonly bool trim;

        public StringTypeConverter(bool trim = true)
        {
            this.trim = trim;
        }

        public object ConvertFromString(string stringValue)
        {
            return trim ? stringValue.Trim() : stringValue;
        }
    }
}