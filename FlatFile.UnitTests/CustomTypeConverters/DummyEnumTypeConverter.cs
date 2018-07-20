using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Enum;

namespace FlatFileParserUnitTests.CustomTypeConverters
{
    // Documentation: https://msdn.microsoft.com/en-us/library/ayybcxe5.aspx
    public class DummyEnumTypeConverter : TypeConverter, ITypeConverter
    {
        private readonly IDictionary<string, Day> conversions;
        private readonly ICollection supportedValues;

        public DummyEnumTypeConverter()
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
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string) && conversions.TryGetValue(value.ToString().Trim().ToUpper(), out var day))
            {
                return day;
            }

            return base.ConvertFrom(context, culture, value);
        }

        // ITypeConverter uses PropertyInfo. .NET library does not. Wrapping .NET call for now.
        public object ConvertFromString(string stringValue, PropertyInfo propertyInfo)
        {
            return ConvertFromString(stringValue.Trim());
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            // return a PrimitiveTypeConverter.StandardValuesCollection containing the standard values for the property type.
            // The standard values for a property must be of the same type as the property itself
            return new StandardValuesCollection(supportedValues);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}