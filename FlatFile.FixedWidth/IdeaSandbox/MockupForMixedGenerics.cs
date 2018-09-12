using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFile.FixedWidth.IdeaSandbox
{
    public class IntTypeConverter : Interfaces.ITypeConverter<int>
    {
        public int ConvertFromString(string s)
        {
            return 42;
        }
    }
    public class BoolTypeConverter : Interfaces.ITypeConverter<bool>
    {
        public bool ConvertFromString(string s)
        {
            return false;
        }
    }

    public abstract class Field
    {
        public abstract Type Type { get; }
    }

    public class Field<T> : Field
    {
        public Interfaces.ITypeConverter<T> TypeConverter { get; set; }
        public override Type Type => typeof(T);
    }

    public class Settings
    {
        List<Field> fields;
        
        public Settings()
        {
            // mockup a quick list
            var intField = new Field<int>();
            intField.TypeConverter = new IntTypeConverter();

            var boolField = new Field<bool>();
            boolField.TypeConverter = new BoolTypeConverter();

            fields.Add(intField);
            fields.Add(boolField);

            var fieldTest = fields.FirstOrDefault();
             var stronglyTypedField = (Field<fieldTest.Type>) fieldTest; // Can't cast on the fly - fieldTest.Type is a variable, can't be used as a type
            // If this can be case here, this may be a solid solition (using an base class w/o generic or second interface w/o generic, and instead tracking the type). 

        }

        //private Field<T> GetField<T>()
        //{
        //    return 
        //}

    }

}
