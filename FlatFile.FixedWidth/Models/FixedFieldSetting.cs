using System.Reflection;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Models
{
    public class FixedFieldSetting<T> : IFixedFieldSetting<T>
    {
        public int Length { get; set; }

        public int StartPosition { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public ITypeConverter<T> TypeConverter { get; set; }
    }
}