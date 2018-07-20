namespace FlatFile.FixedWidth.Interfaces
{
    public interface ITypeConverter
    {
        object ConvertFromString(string stringValue);
        //object ConvertFromString(string stringValue, PropertyInfo propertyInfo);
    }
}