using System.Linq;
using FlatFile.FixedWidth.Implementation.TypeConverters;

namespace DataMunging.Reporting.TypeConverters
{
    public class DirtyIntTypeConverter : TypeConverter<int>
    {
        public override int ConvertFromString(string stringValue)
        {
            return int.Parse(stringValue.Trim('*', ' '));
        }
    }
}