namespace FlatFile.FixedWidth.Interfaces
{
    public interface ITypeConverter<T> : ITypeConverterBase
    {
        new T ConvertFromString(string stringValue);
    }
}