using System.ComponentModel;
using System.Globalization;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    internal class DecimalTypeConverter : NumericTypeConverterBase
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                return decimal.Parse(value.ToString().Trim());
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}