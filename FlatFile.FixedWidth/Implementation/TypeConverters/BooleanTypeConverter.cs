using System;
using System.Collections.Generic;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class BooleanTypeConverter : ITypeConverter<object>
    {
        private readonly Dictionary<string, string> conversions;

        public BooleanTypeConverter()
        {
            conversions = GetConversions();
        }

        public object ConvertFromString(string stringValue)
        {
            try
            {
                var sanitizedString = GetSanitizedString(stringValue);
                conversions.TryGetValue(sanitizedString, out var convertedString);

                return bool.Parse(convertedString ?? sanitizedString);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Input must be true / false, or 1 / 0.", nameof(stringValue), e);
            }
        }

        private Dictionary<string, string> GetConversions()
        {
            return new Dictionary<string, string>
            {
                {"1", "true"},
                {"0", "false"}
            };
        }

        private string GetSanitizedString(string unparsedString)
        {
            unparsedString = unparsedString
                .Trim()
                .ToLower();


            var sanitizedString = conversions.TryGetValue(unparsedString, out var value)
                ? value
                : unparsedString;

            return sanitizedString;
        }
    }
}