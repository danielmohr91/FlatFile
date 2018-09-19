namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class LongTypeConverter : TypeConverter<long>
    {
        public override long ConvertFromString(string stringValue)
        {
            return long.Parse(stringValue.Trim());
        }
    }
}