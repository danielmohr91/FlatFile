using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.TypeConverters;

namespace FlatFile.FixedWidth.Implementation
{
    public class FixedWidthFileParser<T> :
        IFixedWidthFileParser<T> 
        where T : new() 
    {
        private readonly string filePath;
        private readonly IFlatFileLayoutDescriptor<T> layout;

        public FixedWidthFileParser(IFlatFileLayoutDescriptor<T> layout, string filePath)
        {
            this.layout = layout;
            this.filePath = filePath;
        }

        public ICollection<T> ParseFile()
        {
            var rows = new List<T>();
            using (var reader = new StreamReader(filePath))
            {
                string row;
                while ((row = reader.ReadLine()) != null)
                {
                    rows.Add(GetModelFromLine(row));
                }
            }

            return rows;
        }

        /// <summary>
        ///     For each field in layout, the field is extracted from row and added to model (TEntity)
        /// </summary>
        /// <exception cref="T:Exception">Property name is null, not unique, or not found in TEntity.</exception>
        /// <param name="row"></param>
        /// <returns></returns>
        private T GetModelFromLine(string row)
        {
            var model = new T();
            foreach (var field in layout.GetOrderedFields())
            {
                // This could throw ambigous match exception if inheritance is used on the model incorrectly (e.g. new 
                // keyword missing, and hiding a parent property)
                // This could throw argument null exception if Field or PropertyInfo or Name are null
                // Should check for these conditions eventually. 
                var modelProperty = typeof(T).GetProperty(field.PropertyInfo.Name);

                if (modelProperty != null)
                {
                    modelProperty.SetValue(
                        model,
                        GetConvertedValue(row.Substring(field.StartPosition, field.Length), modelProperty));
                }
                else
                {
                    /* Model type (TEntity) of LayoutDescriptor and FixedWidthFileParser must match, so
                     * short of user monkeying around with the PropertyInfo, the layout descriptor's
                     * field.PropertyInfo.Name should always be found in the model */
                    throw new Exception($"Model property with name {field.PropertyInfo.Name} was not found.");
                }
            }
            
            return model;
        }

        /// <summary>
        /// Gets converted value. Strings are right trimmed. Primitive types are converted if possible using defaults.
        /// </summary>
        /// <param name="stringValue">String value to convert</param>
        /// <param name="propertyInfo">Property info of target field in model</param>
        /// <returns>Populated model</returns>
        private object GetConvertedValue(string stringValue, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(string))
            {
                // TODO: Trim behavior here should be configurable. Trimming trailing whitespace by default for the time being. 
                return stringValue.TrimEnd();
            }

            // TODO: Allow TypeConverter classes to overwrite default Parse method below. Hardcoding example for bool below for now.
            if (propertyInfo.PropertyType == typeof(bool))
            {
                var converter = new BooleanTypeConverter();
                stringValue = converter.GetConvertedString(stringValue);
            }

            var parseMethod = propertyInfo.PropertyType.GetMethod("Parse", new Type[] {typeof(string)});
            return parseMethod.Invoke(null, new object[] { stringValue.Trim() });
        }
    }
}