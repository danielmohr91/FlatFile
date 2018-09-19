namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class BooleanTypeConverter : TypeConverter<bool>
    {
        public override bool ConvertFromString(string stringValue)
        {
            return bool.Parse(stringValue.Trim());
        }
    }
}