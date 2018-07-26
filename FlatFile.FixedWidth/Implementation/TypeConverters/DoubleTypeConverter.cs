using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class DoubleTypeConverter : ITypeConverter<double>
    {
        public double ConvertFromString(string stringValue)
        {
            return double.Parse(stringValue.Trim());
        }
    }
}