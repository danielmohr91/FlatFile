namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class ShortTypeConverter : TypeConverter<short>
    {
        public override short ConvertFromString(string stringValue)
        {
            return short.Parse(stringValue.Trim());
        }
    }
}