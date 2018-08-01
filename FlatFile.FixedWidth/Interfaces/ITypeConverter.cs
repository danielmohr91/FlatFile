namespace FlatFile.FixedWidth.Interfaces
{
    public interface ITypeConverter<T> : ITypeConverterBase
    {
       new  T ConvertFromString(string stringValue);
    }

    // Need a base class / interface to use this in a list with mixed generics. 
    // Is this overcomplicating things? 
    //  // https://www.roelvanlisdonk.nl/2010/08/31/how-to-mix-generic-classes-in-a-generic-list-in-c/
    // The base class doesn't doo anything
    // Tried using "ITypeConverter<object>", but can't mix and match in a collection
    // This would be easier with an abstract class, and abstract method for ConvertFromString. Equivilent for interface? 
    public interface ITypeConverterBase
    {
        // dynamic ConvertFromString(string stringValue);
    }
}

