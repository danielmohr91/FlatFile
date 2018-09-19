namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class UIntTypeConverter : TypeConverter<uint>
    {
        public override uint ConvertFromString(string stringValue)
        {
            return uint.Parse(stringValue.Trim());
        }
    }
}