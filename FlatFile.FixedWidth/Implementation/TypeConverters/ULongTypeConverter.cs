using System.ComponentModel;
using System.Globalization;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    internal class ULongTypeConverter : NumericTypeConverterBase
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                return ulong.Parse(value.ToString().Trim());
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}