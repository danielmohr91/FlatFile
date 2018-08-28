<Query Kind="Program" />

void Main()
{
	var test = new Test();

	test.CastingToGenericOnlyKnownAtRuntime();
	
	// Unable to cast object of type 'BoolFoo' to type 'IFoo`1[System.Object]'.
	var bar1 = test.CollectionWithMixedGenerics(1);
	var baz1 = bar1.Bar();

	// Runtime error: // Unable to cast object of type 'IntFoo' to type 'IFoo`1[System.Object]'.
	var bar2 = test.StaticFactoryWithDynamic(1);
	var baz2 = bar2.Bar();

	// Runtime error: // Unable to cast object of type 'IntFoo' to type 'IFoo`1[System.Object]'.
	var bar3 = test.StaticFactoryWithObject(1);
	var baz3 = bar3.Bar();
}

public class Test
{
	public IFoo<dynamic> CollectionWithMixedGenerics(int key)
	{
		// Could potentially use 'object', but can't later cast to IFoo<T> where T is only known at runtime. 
		// Also, I'd like to depend on the interface, not just 'object'
		var objectCollection = new Dictionary<int, object> {
			{0, new BoolFoo()},
			{1, new IntFoo()},
			{2, new StringFoo()}
		};

		var typedCollection = new Dictionary<int, IFoo<dynamic>> {
			{0, (IFoo<dynamic>) new BoolFoo()},
			{1, (IFoo<dynamic>) new IntFoo()},
			{2, (IFoo<dynamic>) new StringFoo()}
		};
		// If no cast, compiler errors: 
		//    Cannot convert BoolFoo to IFoo<dynamic>
		//    Cannot convert IntFoo to IFoo<dynamic>
		//    Cannot convert StringFoo to IFoo<dynamic>
		// 
		// If cast to (IFoo<dynamic>), runtime error:
		//    Unable to cast object of type 'BoolFoo' to type 'IFoo`1[System.Object]'.

		return typedCollection[key];
	}


	public IFoo<dynamic> StaticFactoryWithDynamic(int key)
	{
		// Unable to cast object of type 'IntFoo' to type 'IFoo`1[System.Object]'.
		switch (key)
		{
			case 0:
				return (IFoo<dynamic>)new BoolFoo(); // Also tried casting to IFoo<bool> 
			case 1:
				return (IFoo<dynamic>)new IntFoo();
			default:
				return (IFoo<dynamic>)new StringFoo();
		}
	}

	public IFoo<object> StaticFactoryWithObject(int key)
	{
		// Unable to cast object of type 'IntFoo' to type 'IFoo`1[System.Object]'.
		switch (key)
		{
			case 0:
				return (IFoo<object>)new BoolFoo(); // Also tried casting to IFoo<bool> 
			case 1:
				return (IFoo<object>)new IntFoo();
			default:
				return (IFoo<object>)new StringFoo();
		}
	}

	public void CastingToGenericOnlyKnownAtRuntime()
	{
		// Could use dictionary of objects, but I can't can't object to IFoo<T> later on if T is only known at runtime.
		// For example:
		object compileTimeCast = new BoolFoo();
		((IFoo<bool>)compileTimeCast).Bar(); // Works fine when type (e.g. bool) is known at compile time.

		object runtimeCast = new BoolFoo();

		bool b = false; // suppose type here (used bool as example) is only known at runtime

		var type1 = typeof(IFoo<>).MakeGenericType(b.GetType());
		var thing1 = Activator.CreateInstance(type1); // Cannot create an instance of an interface.
		((IFoo<dynamic>)thing1).Bar();

		var type2 = typeof(BoolFoo).MakeGenericType(b.GetType()); // BoolFoo is not a GenericTypeDefinition. MakeGenericType may only be called on a type for which Type.IsGenericTypeDefinition is true
		var thing2 = Activator.CreateInstance(type2);
		((BoolFoo)thing2).Bar();
	}
}


public interface IFoo<T>
{
	T Bar();
}

public class BoolFoo : IFoo<bool>
{
	public bool Bar()
	{
		return true;
	}
}

public class IntFoo : IFoo<int>
{
	public int Bar()
	{
		return 1;
	}
}

public class StringFoo : IFoo<string>
{
	public string Bar()
	{
		return "hello world";
	}
}