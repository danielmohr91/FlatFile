namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class ULongTypeConverter : TypeConverter<ulong>
    {
        public override ulong ConvertFromString(string stringValue)
        {
            return ulong.Parse(stringValue.Trim());
        }
    }
}