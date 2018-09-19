namespace FlatFile.FixedWidth.Interfaces
{
    public interface ITypeConverterBase
    {
        dynamic ConvertFromString(string stringValue);
    }
}