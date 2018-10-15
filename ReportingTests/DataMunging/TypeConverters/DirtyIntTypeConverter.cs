using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatFile.FixedWidth.Implementation.TypeConverters;

namespace DataMunging.Reporting.TypeConverters
{
   public  class DirtyIntTypeConverter: TypeConverter<int>
    {
        public override int ConvertFromString(string stringValue)
        {
            var digitsOnly = new string(stringValue.Where(char.IsDigit).ToArray());
            return int.Parse(digitsOnly.Trim());
        }
    }
}
