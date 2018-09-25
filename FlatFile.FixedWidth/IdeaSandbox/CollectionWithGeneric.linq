<Query Kind="Program" />

void Main()
{
	List<ITypeConverter> converters = new List<ITypeConverter> {
		new BoolTypeConverter(),
		new TypeConverter<string>(),
		new TypeConverter<int> (),
		new IntTypeConverter()
	};

	var testConverter = converters.Last();
	testConverter.Foo.Dump();

	testConverter = converters.First();
	testConverter.Foo = true;
	testConverter.Foo.Dump();
//	testConverter.GetValueFromString("test").Dump();  // calls method in TypeConverter	

}


public interface ITypeConverter
{
	object Foo { get; set; }

	object GetValueFromString(string s);
}

public class TypeConverter<T> : ITypeConverter
{
	public T Foo { get; set; }

	object ITypeConverter.Foo
	{
		get { return Foo; }
		set { Foo = (T)Foo; }
	}

	public object GetValueFromString(string s)
	{
		throw new System.NotImplementedException();
	}
}

public class BoolTypeConverter : TypeConverter<bool>, ITypeConverter
{

	object ITypeConverter.Foo
	{
		get { return Foo; } // POC
		set { Foo = (bool)Foo; }
	}

	public new bool GetValueFromString(string s)
	{
		return bool.Parse(s);
	}
}

public class IntTypeConverter : TypeConverter<bool>, ITypeConverter
{

	object ITypeConverter.Foo
	{
		get { return 42; }
		set { Foo = (bool)Foo; }
	}
}