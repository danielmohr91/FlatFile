namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class UShortTypeConverter : TypeConverter<ushort>
    {
        public override ushort ConvertFromString(string stringValue)
        {
            return ushort.Parse(stringValue.Trim());
        }
    }
}