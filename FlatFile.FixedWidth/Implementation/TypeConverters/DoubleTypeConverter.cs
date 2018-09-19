using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class DoubleTypeConverter : TypeConverterBase<double>, ITypeConverter<double>
    {
        public override double ConvertFromString(string stringValue)
        {
            // This currently fails for double.MaxValue.
            // Can reproduce here: double.Parse(double.MaxValue.ToString());
            // Seems strange that the documented maximum value throws OverflowException: Value was either too large or too small for a Double
            return double.Parse(stringValue.Trim());
        }
    }
}