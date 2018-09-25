namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class DecimalTypeConverter : TypeConverter<decimal>
    {
        public override decimal ConvertFromString(string stringValue)
        {
            return decimal.Parse(stringValue.Trim());
        }
    }
}