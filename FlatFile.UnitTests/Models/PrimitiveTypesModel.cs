using System.Collections.Generic;

namespace FlatFileParserUnitTests.Models
{
    public class PrimitiveTypesModel
    {
        public int id { get; set; }
        public bool boolTest { get; set; }
        public string stringTest { get; set; }
        public double doubleTest { get; set; }
        public char charTest { get; set; }


        public override bool Equals(object obj)
        {
            return obj is PrimitiveTypesModel types &&
                   id == types.id &&
                   boolTest == types.boolTest &&
                   stringTest == types.stringTest &&
                   doubleTest == types.doubleTest &&
                   charTest == types.charTest;
        }

        // Numeric value used to insert and identify PrimitiveTypesModel object in a hash-based collection such as the Dictionary<TKey, TValue> class, 
        // the Hashtable class, or a type derived from the DictionaryBase class. The GetHashCode method provides this hash code for algorithms 
        // that need quick checks of object equality.

        public override int GetHashCode()
        {
            var hashCode = -393617894;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + boolTest.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(stringTest);
            hashCode = hashCode * -1521134295 + doubleTest.GetHashCode();
            hashCode = hashCode * -1521134295 + charTest.GetHashCode();
            return hashCode;
        }
    }
}