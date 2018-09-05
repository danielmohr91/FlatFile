using System;
using System.Reflection;
using FlatFile.FixedWidth.Implementation.TypeConverters;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Models
{
    public class FixedFieldSetting<TProperty> : IFixedFieldSetting<TProperty>
    {
        public int Length { get; set; }

        public int StartPosition { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public ITypeConverter<TProperty> TypeConverter { get; set; }

        public static explicit operator FixedFieldSetting<TProperty>(IntTypeConverter v)
        {
            // Could provide an explicit cast behavior for IFixedFieldSetting<object>?
            // Feels kludgy... 
            throw new NotImplementedException();
        }
    }
}