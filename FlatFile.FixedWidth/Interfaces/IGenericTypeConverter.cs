using System.Reflection;

namespace FlatFile.FixedWidth.Interfaces
{
    public interface IGenericTypeConverter
    {
        object GetConvertedValue(string stringValue, PropertyInfo propertyInfo);
    }
}