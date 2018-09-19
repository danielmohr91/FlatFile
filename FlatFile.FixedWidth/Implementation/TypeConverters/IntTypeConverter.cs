namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class IntTypeConverter : TypeConverter<int>
    {
        public override int ConvertFromString(string stringValue)
        {
            return int.Parse(stringValue.Trim());
        }
    }
}