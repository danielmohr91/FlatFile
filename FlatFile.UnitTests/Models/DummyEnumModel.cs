using System.Collections.Generic;
using FlatFileParserUnitTests.Enum;

namespace FlatFileParserUnitTests.Models
{
    public class DummyEnumModel
    {
        public int Id { get; set; } // Test default type converter for primitive types
        public string StringTest { get; set; } // Test no type converter
        public Day DayTest { get; set; } // Test custom type converter

        public override bool Equals(object obj)
        {
            var model = obj as DummyEnumModel;
            return model != null &&
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