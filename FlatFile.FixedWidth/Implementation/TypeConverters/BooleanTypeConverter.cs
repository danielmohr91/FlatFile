using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class BooleanTypeConverter : ITypeConverter<bool>
    {
        public bool ConvertFromString(string stringValue)
        {
            return bool.Parse(stringValue.Trim());
        }
    }
}