namespace FlatFile.FixedWidth.Interfaces
{
    public interface IPrimitiveTypeConverter
    {
        string GetConvertedString(string unparsedString);
    }
}