using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation
{
    /// <summary>
    ///     Quick and dirty file writer to generate test files
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FixedWidthFileWriter<T>
    {
        private readonly string filePath;
        private readonly IFlatFileLayoutDescriptor<T> layout;

        public FixedWidthFileWriter(IFlatFileLayoutDescriptor<T> layout, string filePath)
        {
            this.layout = layout;
            this.filePath = filePath;

            if (layout.GetOrderedFields()
                .Any(x => x.TypeConverter == null))
            {
                throw new ArgumentException("Missing TypeConverter for one or more fields", nameof(layout));
            }
        }

        public void WriteFile(ICollection<T> rows)
        {
            // Warning: This will overwrite previous file contents
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var row in rows)
                {
                    var line = GetLineFromModel(row);
                    writer.WriteLine(line);
                }

                writer.Flush();
            }
        }

        private string GetLineFromModel(T row)
        {
            var line = string.Empty;

            foreach (var field in layout.GetOrderedFields())
            {
                var modelProperty = typeof(T).GetProperty(field.PropertyInfo.Name);

                if (modelProperty != null)
                {
                    var modelValue = modelProperty.GetValue(row).ToString();

                    // Throw exception on truncation. Early failure preferable to incorrect data.
                    // e.g. 1.234567890E+100 would truncate to 1.234 @ 5 char field length. 
                    // TODO: Accept a type converter here for writes
                    if (field.Length < modelValue.Length)
                    {
                        throw new Exception($"Field is too short for value. '{field.PropertyInfo.Name}' field length of {field.Length} " +
                                            $"is less than required length of {modelValue.Length} for value {modelValue}");
                    }

                    var valueForFile = modelValue
                        .Substring(0, Math.Min(field.Length, modelValue.Length))
                        .PadRight(field.Length);

                    line += valueForFile;
                }
                else
                {
                    throw new Exception($"Model property with name {field.PropertyInfo.Name} was not found.");
                }
            }

            return line;
        }
    }
}