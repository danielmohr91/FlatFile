namespace FlatFile.FixedWidth.Interfaces
{
    public interface ITypeConverter<T> : ITypeConverterBase
    {
        T ConvertFromString(string stringValue);
    }

    // Need a base class / interface to use this in a list with mixed generics. 
    // Is this overcomplicating things? 
    //  // https://www.roelvanlisdonk.nl/2010/08/31/how-to-mix-generic-classes-in-a-generic-list-in-c/
    // The base class doesn't doo anything
    // Tried using "ITypeConverter<object>", but can't mix and match in a collection
    public interface ITypeConverterBase
    {
    //    object ConvertFromString(string stringValue);
    }
}

