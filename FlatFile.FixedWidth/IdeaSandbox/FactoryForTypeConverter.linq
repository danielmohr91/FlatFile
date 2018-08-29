<Query Kind="Program" />

void Main()
{

	var typeConverterFactory = new TypeConverterFactory<PrimitiveTypeConverter>();

	var intTypeConverter = typeConverterFactory.Create<IntTypeConverter>();
	var boolTypeConverter = typeConverterFactory.Create<BoolTypeConverter>();

	intTypeConverter.ConvertFromString().Dump();
	boolTypeConverter.ConvertFromString().Dump();
}


// Abstract Factory
public interface ITypeConverterFactory<TFactory>
{
	TTypeConverter Build<TTypeConverter>() where TTypeConverter : ITypeConverter<TFactory>, new();
}


// Concrete Factory
public class PrimitiveTypeConverter : ITypeConverterFactory<PrimitiveTypeConverter>
{
	public TTypeConverter Build<TTypeConverter>() where TTypeConverter : ITypeConverter<PrimitiveTypeConverter>, new()
	{
		return new TTypeConverter();
	}
}


// Abstract Type Converter
public interface ITypeConverter<TTypeConverter>
{
	bool CanConvertFromString(string s); // Can't use convert from string here yet, without another generic 
}


// Concrete Type Converters 
public class IntTypeConverter : ITypeConverter<PrimitiveTypeConverter>
{
	public bool CanConvertFromString(string s)
	{
		return true;
	}

	public int ConvertFromString()
	{
		return 42;
	}
}

public class BoolTypeConverter : ITypeConverter<PrimitiveTypeConverter>
{
	public bool CanConvertFromString(string s)
	{
		return true;
	}

	public bool ConvertFromString()
	{
		return true;
	}
}


// Type Converter Factory
class TypeConverterFactory<TFactory> where TFactory : ITypeConverterFactory<TFactory>, new()
{
	public TTypeConverter Create<TTypeConverter>() where TTypeConverter : ITypeConverter<TFactory>, new()
	{
		return new TFactory().Build<TTypeConverter>();
	}
}