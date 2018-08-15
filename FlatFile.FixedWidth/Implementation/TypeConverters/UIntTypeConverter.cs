using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class UIntTypeConverter : ITypeConverter<object>
    {
        public object ConvertFromString(string stringValue)
        {
            return uint.Parse(stringValue.Trim());
        }
    }
}