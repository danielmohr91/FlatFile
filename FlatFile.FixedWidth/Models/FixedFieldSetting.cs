using System.Reflection;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Models
{
    public class FixedFieldSetting<TProperty> : IFixedFieldSetting
    {
        public int Length { get; set; }

        public int StartPosition { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public ITypeConverterBase TypeConverter { get; set; }

        //public static explicit operator FixedFieldSetting<TProperty>(IntTypeConverter v)
        //{
        //    // Could provide an explicit cast behavior for IFixedFieldSetting<object>?
        //    // Feels kludgy... 
        //    throw new NotImplementedException();
        //}
    }
}