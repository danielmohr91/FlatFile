namespace FlatFile.FixedWidth.Interfaces
{
    public interface ITypeConverterBase
    {
        object GetConvertedValue(string stringValue);
    }
}