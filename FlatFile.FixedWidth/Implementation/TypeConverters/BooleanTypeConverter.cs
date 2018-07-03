using System.Collections.Generic;
using FlatFileParserUnitTests.TypeConverters.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    internal class BooleanTypeConverter : IPrimitiveTypeConverter
    {
        private readonly Dictionary<string, string> conversions;

        public BooleanTypeConverter()
        {
            conversions = GetConversions();
        }

        public string GetConvertedString(string unparsedString)
        {
            unparsedString = unparsedString
                .Trim()
                .ToLower();

            string value; 
            return conversions.TryGetValue(unparsedString, out value) 
                ? value 
                : unparsedString;
        }

        private Dictionary<string, string> GetConversions()
        {
            return new Dictionary<string, string>
            {
                {"1", "true"},
                {"0", "false"}
            };
        }
    }
}