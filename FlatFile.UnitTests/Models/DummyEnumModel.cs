using System.Collections.Generic;
using FlatFileParserUnitTests.Enum;

namespace FlatFileParserUnitTests.Models
{
    public class DummyEnumModel
    {
        public int Id { get; set; }
        public string StringTest { get; set; }
        public Day DayTest { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DummyEnumModel model &&
                   Id == model.Id &&
                   StringTest == model.StringTest &&
                   DayTest == model.DayTest;
        }

        public override int GetHashCode()
        {
            var hashCode = 1609819705;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(StringTest);
            hashCode = hashCode * -1521134295 + DayTest.GetHashCode();
            return hashCode;
        }
    }
}