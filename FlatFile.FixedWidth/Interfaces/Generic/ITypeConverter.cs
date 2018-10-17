namespace FlatFile.FixedWidth.Interfaces.Generic
{
    public interface ITypeConverter<T> : ITypeConverter
    {
        T ConvertFromString(string stringValue);
    }
}