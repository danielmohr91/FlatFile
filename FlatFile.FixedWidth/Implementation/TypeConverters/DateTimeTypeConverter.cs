using System;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class DateTimeTypeConverter : TypeConverter<DateTime>
    {
        public override DateTime ConvertFromString(string stringValue)
        {
            return DateTime.Parse(stringValue.Trim());
        }
    }
}