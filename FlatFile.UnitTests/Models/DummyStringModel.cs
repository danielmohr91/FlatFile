namespace FlatFileParserUnitTests.Models
{
    public class DummyStringModel
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

            // C# 7 Pattern matching
            // This breaks in VS 2015 / C#6 compiler
            // https://www.danielcrabtree.com/blog/152/c-sharp-7-is-operator-patterns-you-wont-need-as-as-often
            
            // Option 1
            //var model = obj as DummyStringModel;
            //if (model is null)
            //{
            //    // model is null, is NOT equivalent to model == null
            //    // uses Object.Equals(model, null);
            //    return false;
            //}

            // Option 2 (cleaner): 
            //if (!(obj is DummyStringModel model))
            //{
            //    return false;
            //}

            // C# 6 polyfill
            var model = (DummyStringModel) obj;
            if ( object.Equals(model, null))
            {
                return false;
            }

            if (Id != model.Id ||
                Field1 != model.Field1 ||
                Field2 != model.Field2 ||
                Field3 != model.Field3 ||
                Field4 != model.Field4 ||
                Field5 != model.Field5 ||
                Field6 != model.Field6 ||
                Field7 != model.Field7 ||
                Field8 != model.Field8 ||
                Field9 != model.Field9 ||
                Field10 != model.Field10 ||
                Field11 != model.Field11 ||
                Field12 != model.Field12
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