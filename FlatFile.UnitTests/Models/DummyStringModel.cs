using System.Collections.Generic;

namespace FlatFileParserUnitTests.Models
{
    public class DummyStringModel
    {
        public string Id { get; set; } // TODO: Make this an int, and support field.PrimitiveTypeConverter
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
            if (Equals(model, null))
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
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = -1292043955;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field1);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field2);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field3);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field4);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field5);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field6);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field7);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field8);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field9);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field10);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field11);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Field12);
            return hashCode;
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