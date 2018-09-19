namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class StringTypeConverter : TypeConverter<string>
    {
        private readonly bool trim;

        public StringTypeConverter(bool trim = true)
        {
            this.trim = trim;
        }

        public override string ConvertFromString(string stringValue)
        {
            return trim ? stringValue.Trim() : stringValue;
        }
    }
}