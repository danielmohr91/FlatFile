using System;
using System.Collections.Generic;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Enum;

namespace FlatFileParserUnitTests.CustomTypeConverters
{
    // Documentation: https://msdn.microsoft.com/en-us/library/ayybcxe5.aspx
    public class DummyEnumTypeConverter : ITypeConverter<Day>
    {
        private readonly IDictionary<string, Day> conversions;

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
        }

        public Day ConvertFromString(string stringValue)
        {
            if (conversions.TryGetValue(stringValue.Trim().ToUpper(), out var day)) return day;

            throw new ArgumentException("Input must be a day of the week (full name or abbreviated), case insensitive.", nameof(stringValue));
        }

        dynamic ITypeConverterBase.GetConvertedValue(string stringValue)
        {
            return ConvertFromString(stringValue);
        }
    }
}