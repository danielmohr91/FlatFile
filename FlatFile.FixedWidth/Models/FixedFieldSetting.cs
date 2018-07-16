using System.Reflection;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Models
{
    public class FixedFieldSetting : IFixedFieldSetting
    {
        public int Length { get; set; }

        public int StartPosition { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public ITypeConverter TypeConverter { get; set; }
    }
}