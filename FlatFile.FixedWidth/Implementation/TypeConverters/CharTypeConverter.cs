using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class CharTypeConverter : ITypeConverter<char>
    {
        public char ConvertFromString(string stringValue)
        {
            return char.Parse(stringValue.Trim());
        }
    }
}