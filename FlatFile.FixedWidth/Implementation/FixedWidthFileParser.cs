using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlatFile.FixedWidth.Interfaces;

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

            if (layout.GetOrderedFields()
                .Where(x => !x.ShouldSkip)
                .Any(x => x.TypeConverter == null))
            {
                throw new ArgumentException("Missing TypeConverter for one or more fields", nameof(layout));
            }
        }

        public ICollection<T> ParseFile(bool ignoreFirstRow = false, bool ignoreBlankRows = false)
        {
            return ParseFileHelper(ignoreFirstRow, ignoreBlankRows, null);
        }

        private ICollection<T> ParseFileHelper(bool ignoreFirstRow, bool ignoreBlankRows, ITestForSkip testForSkip)
        {
            var i = 0;
            var rows = new List<T>();
            using (var reader = new StreamReader(filePath))
            {
                string row;
                while ((row = reader.ReadLine()) != null)
                {
                    if (i++ == 0 && ignoreFirstRow)
                    {
                        continue;
                    }

                    if (ignoreBlankRows && string.IsNullOrWhiteSpace(row))
                    {
                        continue;
                    }

                    if (testForSkip != null && testForSkip.ShouldSkip(row))
                    {
                        continue;
                    }

                    rows.Add(GetModelFromLine(row));
                }
            }

            return rows;
        }

        public ICollection<T> ParseFile(ITestForSkip testForSkip, bool ignoreFirstRow = false, bool ignoreBlankRows = false)
        {
            return ParseFileHelper(ignoreFirstRow, ignoreBlankRows, testForSkip);
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
                if (field.ShouldSkip)
                {
                    continue;
                }

                // This could throw ambigous match exception if inheritance is used on the model incorrectly (e.g. new 
                // keyword missing, and hiding a parent property)
                // This could throw argument null exception if Field or PropertyInfo or Name are null
                // Should check for these conditions eventually. 
                var modelProperty = typeof(T).GetProperty(field.PropertyInfo.Name);

                if (modelProperty != null)
                {
                    var stringToConvert = row.Substring(field.StartPosition, field.Length);
                    var typeConverter = field.TypeConverter;
                    var convertedValue = typeConverter.GetConvertedValue(stringToConvert);

                    modelProperty.SetValue(
                        model,
                        convertedValue);
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
    }
}