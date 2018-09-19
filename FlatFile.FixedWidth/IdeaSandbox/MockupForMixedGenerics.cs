using System;
using System.Collections.Generic;
using System.Linq;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.IdeaSandbox
{
    public class IntTypeConverter : ITypeConverter<int>
    {
        public int ConvertFromString(string s)
        {
            return 42;
        }
    }

    public class BoolTypeConverter : ITypeConverter<bool>
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
        public ITypeConverter<T> TypeConverter { get; set; }
        public override Type Type => typeof(T);
    }

    public class Settings
    {
        private List<Field> fields;

        public Settings()
        {
            CreateList();

            var lookups = new Dictionary<int, Field>();
            lookups.Add(0, new Field<int> {TypeConverter = new IntTypeConverter()});
            lookups.Add(1, new Field<bool> {TypeConverter = new BoolTypeConverter()});

            var fieldTest = lookups[0];
            // Resume here pulling from this mockup: C:\Projects\FlatFile\FlatFile.FixedWidth\IdeaSandbox\CollectionOfTypedFields.linq
            //fieldTest.TypeConverter;
        }

        private void CreateList()
        {
// mockup a quick list
            var intField = new Field<int>
            {
                TypeConverter = new IntTypeConverter()
            };

            var boolField = new Field<bool>
            {
                TypeConverter = new BoolTypeConverter()
            };

            fields.Add(intField);
            fields.Add(boolField);


            var fieldTest = fields.FirstOrDefault();

            var type = typeof(Field<>).MakeGenericType(fieldTest.Type);
            var typedField = Activator.CreateInstance(type);
            // Can't do this as a list. Will use a dictionary. 

            ////var typedField = Convert.ChangeType(fieldTest, Field<fieldTest.Type>);
            //var typed= fieldTest.GetType().GetProperty()
            // var stronglyTypedField = (Field<test>) fieldTest; // Can't cast on the fly - fieldTest.Type is a variable, can't be used as a type
            //// If this can be case here, this may be a solid solition (using an base class w/o generic or second interface w/o generic, and instead tracking the type). 
        }

        //private Field<T> GetField<T>()
        //{
        //    return 
        //}
    }
}