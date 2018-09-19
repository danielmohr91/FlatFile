using System;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class DateTimeTypeConverter : TypeConverterBase<DateTime>, ITypeConverter<DateTime>
    {
        public override DateTime ConvertFromString(string stringValue)
        {
            return DateTime.Parse(stringValue.Trim());
        }
    }
}