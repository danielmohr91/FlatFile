using System.Reflection;

namespace FlatFile.FixedWidth.Interfaces
{
    public interface ITypeConverter
    {
        object ConvertFromString(string stringValue, PropertyInfo propertyInfo);
    }
}