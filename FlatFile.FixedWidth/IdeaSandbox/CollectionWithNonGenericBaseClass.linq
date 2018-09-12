<Query Kind="Program" />

void Main()
{
	var intConverter = new TypeConverter<int>();
	var boolConverter = new TypeConverter<bool>();

	var collection = new TypeConverterCollection();
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

public class TypeConverter<T> : TypeConverter
{
	public override Type Type => typeof(T);

	public T Value { get; set; }
}

public class TypeConverterWithMethod<T> : TypeConverter where T : new()
{
	public override Type Type => typeof(T);

	public T GetValueFromString()
	{
		return new T();
	}
}

public class TypeConverterCollection
{
	private Dictionary<Type, TypeConverter> lookups;

	public TypeConverterCollection()
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
