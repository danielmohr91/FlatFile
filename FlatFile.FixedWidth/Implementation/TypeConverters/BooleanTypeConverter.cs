using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    internal class BooleanTypeConverter : TypeConverter
    {
        private readonly Dictionary<string, string> conversions;

        public BooleanTypeConverter()
        {
            conversions = GetConversions();
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var sanitizedString = GetSanitizedString(value.ToString());

            if (conversions.TryGetValue(sanitizedString, out var normalizedBoolString))
            {
                return bool.Parse(normalizedBoolString);
            }

            return base.ConvertFrom(context, culture, sanitizedString);
        }

        public string GetSanitizedString(string unparsedString)
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
                {"0", "false"},
                {"true", "true"},
                {"false", "false"}
            };
        }
    }
}