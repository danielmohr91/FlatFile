<Query Kind="Program" />

void Main()
{
	List<ITypeConverter> converters = new List<ITypeConverter> {
		new BoolTypeConverter(),
		new IntTypeConverter()
	};
	
	var boolConverter = converters.First();
	boolConverter.SourceValue = "true";
	boolConverter.ConvertedValue.Dump();

	var intConverter = converters.Last();
	intConverter.SourceValue = "42";
	intConverter.ConvertedValue.Dump();
}


public interface ITypeConverter
{
	object ConvertedValue { get; set; }
	string SourceValue { get; set; }
}

public class TypeConverter<T> : ITypeConverter
{
	public T ConvertedValue { get; set; }

	object ITypeConverter.ConvertedValue
	{
		get { return ConvertedValue; }
		set { ConvertedValue = (T)ConvertedValue; }
	}

	public string SourceValue { get; set; }
}

public class BoolTypeConverter : TypeConverter<bool>, ITypeConverter
{

	object ITypeConverter.ConvertedValue
	{
		get { return bool.Parse(SourceValue); }
		set { ConvertedValue = (bool)ConvertedValue; }
	}
}

public class IntTypeConverter : TypeConverter<bool>, ITypeConverter
{

	object ITypeConverter.ConvertedValue
	{
		get { return int.Parse(SourceValue); }
		set { ConvertedValue = (bool)ConvertedValue; }
	}
}