namespace FlatFile.FixedWidth.Interfaces
{
    public interface ITypeConverter<T> : ITypeConverterBase
    {
        T ConvertFromString(string stringValue);
    }
}