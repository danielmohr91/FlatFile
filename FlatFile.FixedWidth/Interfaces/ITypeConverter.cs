namespace FlatFile.FixedWidth.Interfaces
{
    public interface ITypeConverter
    {
        object GetConvertedValue(string stringValue);
    }
}