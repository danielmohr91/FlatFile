using System.Collections.Generic;

namespace FlatFileParserUnitTests.Models
{
    public class PrimitiveTypesModel
    {
        public bool boolTest { get; set; }
        public char charTest { get; set; }
        public decimal decimalTest { get; set; }
        public double doubleTest { get; set; }
        public float floatTest { get; set; }
        public int id { get; set; }
        public int intTest { get; set; }
        public long longTest { get; set; }
        public short shortTest { get; set; }
        public string stringTest { get; set; }
        public uint uintTest { get; set; }
        public ulong ulongTest { get; set; }
        public ushort ushortTest { get; set; }

        public override bool Equals(object obj)
        {
            var model = obj as PrimitiveTypesModel;
            return model != null &&
                   boolTest == model.boolTest &&
                   charTest == model.charTest &&
                   decimalTest == model.decimalTest &&
                   doubleTest == model.doubleTest &&
                   floatTest == model.floatTest &&
                   id == model.id &&
                   longTest == model.longTest &&
                   shortTest == model.shortTest &&
                   stringTest == model.stringTest &&
                   uintTest == model.uintTest &&
                   ulongTest == model.ulongTest &&
                   ushortTest == model.ushortTest;
        }

        public override int GetHashCode()
        {
            var hashCode = -422371933;
            hashCode = hashCode * -1521134295 + boolTest.GetHashCode();
            hashCode = hashCode * -1521134295 + charTest.GetHashCode();
            hashCode = hashCode * -1521134295 + decimalTest.GetHashCode();
            hashCode = hashCode * -1521134295 + doubleTest.GetHashCode();
            hashCode = hashCode * -1521134295 + floatTest.GetHashCode();
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + longTest.GetHashCode();
            hashCode = hashCode * -1521134295 + shortTest.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(stringTest);
            hashCode = hashCode * -1521134295 + uintTest.GetHashCode();
            hashCode = hashCode * -1521134295 + ulongTest.GetHashCode();
            hashCode = hashCode * -1521134295 + ushortTest.GetHashCode();
            return hashCode;
        }
    }
}