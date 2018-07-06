using System;
using System.Reflection;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    // TODO: Make this a factory that chooses an ITypeConverter subclass for the conversion
    public class GenericTypeConverter : IGenericTypeConverter
    {
        /// <summary>
        ///     Gets converted value. Strings are right trimmed. Types with built in Parse method (e.g. primitive types, and some others)
        ///     are converted if possible using defaults.
        /// </summary>
        /// <param name="stringValue">String value to convert</param>
        /// <param name="propertyInfo">Property info of target field in model</param>
        /// <returns>Populated model</returns>
        public object GetConvertedValue(string stringValue, PropertyInfo propertyInfo)
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

            var parseMethod = propertyInfo.PropertyType.GetMethod("Parse", new[] { typeof(string) });

            if (parseMethod == null)
            {
                return stringValue.Trim();
            }

            try
            {
                return parseMethod.Invoke(null, new object[] {stringValue.Trim()});
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    // The inner exception is preferred (e.g. System.FormatException) 
                    // over the wrapped reflection exception (TargetInvocationException)
                    throw e.InnerException;
                }

                throw;
            }
        }
    }
}
