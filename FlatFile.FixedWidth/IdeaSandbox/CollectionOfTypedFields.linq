<Query Kind="Program" />

void Main()
{
	var intConverter = new IntTypeConverter();
	var boolConverter = new BoolTypeConverter();

	var collection = new TypeConverterFactory();
	collection.Put(intConverter);
	collection.Put(boolConverter);

	var x = collection.Get<int>(); // strongly typed as TypeConverter<int>
	var y = collection.Get<bool>(); // strongly typed as TypeConverter<bool>

	x.Dump();
	y.Dump();
}

public abstract class TypeConverter
{
	public abstract Type Type { get; }
}

public abstract class TypeConverter<T> : TypeConverter // where T : new()
{
	public override Type Type => typeof(T);

	public abstract T GetValueFromString(string s);
}

public class IntTypeConverter : TypeConverter<int> {
	public override int GetValueFromString(string s) {
		return 42;
	}
}

public class BoolTypeConverter : TypeConverter<bool>
{
	public override bool GetValueFromString(string s)
	{
		return false;
	}
}

public class Field<T> {
	TypeConverter<T> typeConverter {get;set;}
}

public class TypeConverterFactory
{
	private Dictionary<Type, TypeConverter> lookups;

	public TypeConverterFactory()
	{
		lookups = new Dictionary<Type, TypeConverter>();

	}
	
	public void Put<T>(TypeConverter<T> item)
	{
		lookups[typeof(T)] = item;
	}
	
	public TypeConverter<T> Get<T>()
	{
		return lookups[typeof(T)] as TypeConverter<T>;
	}
}
