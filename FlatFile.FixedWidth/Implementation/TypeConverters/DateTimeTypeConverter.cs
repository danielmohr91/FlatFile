using System;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class DateTimeTypeConverter : ITypeConverter<DateTime>
    {
        public DateTime ConvertFromString(string stringValue)
        {
            return DateTime.Parse(stringValue.Trim());
        }
    }
}