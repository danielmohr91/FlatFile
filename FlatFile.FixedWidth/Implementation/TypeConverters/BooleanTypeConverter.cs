using System;
using System.Collections.Generic;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class BooleanTypeConverter : ITypeConverter<bool>
    {
        private readonly Dictionary<string, string> conversions;

        public BooleanTypeConverter()
        {
            conversions = GetConversions();
        }

        public bool ConvertFromString(string stringValue)
        {
            if (conversions.TryGetValue(GetSanitizedString(stringValue), out var normalizedBoolString))
            {
                return bool.Parse(normalizedBoolString);
            }

            throw new ArgumentException("Input must be true / false, or 1 / 0.", nameof(stringValue));
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

            return conversions.TryGetValue(unparsedString, out var value)
                ? value
                : unparsedString;
        }
    }
}