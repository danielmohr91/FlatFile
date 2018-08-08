namespace FlatFile.FixedWidth.Interfaces
{
    public interface ITypeConverter<T>
    {
        T ConvertFromString(string stringValue);
    }
}