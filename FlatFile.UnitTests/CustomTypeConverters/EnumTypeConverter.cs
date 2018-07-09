using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace FlatFileParserUnitTests.CustomTypeConverters
{
    public enum Day
    {
        Sun,
        Mon,
        Tues,
        Wed,
        Thu,
        Fri,
        Sat
    }

    // Documentation: https://msdn.microsoft.com/en-us/library/ayybcxe5.aspx
    public class EnumTypeConverter : TypeConverter
    {
        private readonly IDictionary<string, Day> conversions;
        private readonly ICollection supportedValues;

        public EnumTypeConverter()
        {
            conversions = new Dictionary<string, Day>
            {
                {"SUN", Day.Sun},
                {"SUNDAY", Day.Sun},
                {"M", Day.Mon},
                {"MON", Day.Mon},
                {"MONDAY", Day.Mon},
                {"T", Day.Tues},
                {"TUES", Day.Tues},
                {"TUESDAY", Day.Tues},
                {"W", Day.Wed},
                {"WED", Day.Wed},
                {"WEDNESDAY", Day.Wed},
                {"TH", Day.Thu},
                {"THURS", Day.Thu},
                {"THURSDAY", Day.Thu},
                {"F", Day.Fri},
                {"FR", Day.Fri},
                {"FRI", Day.Fri},
                {"FRIDAY", Day.Fri},
                {"SAT", Day.Sat},
                {"SATURDAY", Day.Sat}
            };
            supportedValues = (ICollection) conversions.Keys;
        }

 public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string) && conversions.TryGetValue(value.ToString().ToUpper(), out var day))
            {
                return day;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }


        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            // return a TypeConverter.StandardValuesCollection containing the standard values for the property type.
            // The standard values for a property must be of the same type as the property itself
            return new StandardValuesCollection(supportedValues);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return base.IsValid(context, value);
        }
    }
}