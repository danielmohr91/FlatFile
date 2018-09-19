using System;

namespace FlatFileParserUnitTests.Models
{
    public class DateTimeModel
    {
        public DateTime DateTime1 { get; set; }
        public DateTime DateTime2 { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DateTimeModel model &&
                   DateTime1 == model.DateTime1 &&
                   DateTime2 == model.DateTime2;
        }

        public override int GetHashCode()
        {
            var hashCode = -2098635407;
            hashCode = hashCode * -1521134295 + DateTime1.GetHashCode();
            hashCode = hashCode * -1521134295 + DateTime2.GetHashCode();
            return hashCode;
        }
    }
}