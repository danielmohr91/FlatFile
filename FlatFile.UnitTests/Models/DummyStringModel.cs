namespace FlatFileParserUnitTests.Models
{
    internal class DummyStringModel
    {
        public string Id { get; set; } // TODO: Make this an int, and support field.TypeConverter
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }
        public string Field7 { get; set; }
        public string Field8 { get; set; }
        public string Field9 { get; set; }
        public string Field10 { get; set; }
        public string Field11 { get; set; }
        public string Field12 { get; set; }

        public override bool Equals(object obj)
        {
            // Pattern matching
            var other = obj as DummyStringModel;

            // This compiles in VS 2015 / C#6, but does not work as expected
            // if (other == null) { return false; }

            // This breaks in VS 2015 / C#6 compiler
            // https://www.danielcrabtree.com/blog/152/c-sharp-7-is-operator-patterns-you-wont-need-as-as-often
            if (other is null)
            {
                // other is null, is NOT equivalent to other == null
                // uses Object.Equals(other, null);
                return false;
            }

            if (Id != other.Id ||
                Field1 != other.Field1 ||
                Field2 != other.Field2 ||
                Field3 != other.Field3 ||
                Field4 != other.Field4 ||
                Field5 != other.Field5 ||
                Field6 != other.Field6 ||
                Field7 != other.Field7 ||
                Field8 != other.Field8 ||
                Field9 != other.Field9 ||
                Field10 != other.Field10 ||
                Field11 != other.Field11 ||
                Field12 != other.Field12
            )
                return false;

            return true;
        }


        public static bool operator ==(DummyStringModel x, DummyStringModel y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(DummyStringModel x, DummyStringModel y)
        {
            return !(x == y);
        }
    }
}