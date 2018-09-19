namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class FloatTypeConverter : TypeConverter<float>
    {
        public override float ConvertFromString(string stringValue)
        {
            return float.Parse(stringValue.Trim());
        }
    }
}