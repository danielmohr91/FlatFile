<Query Kind="Program" />

void Main()
{
	// Pattern 1 - Collection of Mixed Generics

	// Naive solution, use a collection of object
	var collectionTest = new CollectionWithMixedGenerics();
	collectionTest.InvokeOnListOfObjects();

	// Naive solution, use a collection of IFoo<object> - Would need to the Foo classes to implement IFoo<object> though, can't mix generic types. 
	// collectionTest.InvokeOnListOfWeaklyTypedThings(); // RUNTIME EXCEPTION - Unable to cast object of type 'BoolFoo' to type 'IFoo`1[System.Object]'

	
	// Pattern 2 - Static Factory
	var factoryTest = new StaticFactoryTest();
	factoryTest.InvokeBar();
	

}

public class CollectionWithMixedGenerics
{
	public void InvokeOnListOfObjects()
	{
		// Arrange collection of objects
		var weaklyTypedThings = new List<object> {
		new BoolFoo(),
		new IntFoo(),
		new StringFoo()
	};

		// Invoke method
		// Iddeally would use IFoo<object> or IFoo<dynamic>. Can ONLY seem to do this is the generic type is already known.
		var thing = (IFoo<bool>)weaklyTypedThings.FirstOrDefault(); // Only works if cast to the specific type (e.g. bool for the first, int for second, and string for third in this example)
		thing.Bar();

		// I could track the generic type easily enough, but can't use anything dynamic - generic must be a compile time constant
		// There are some ideas here: https://stackoverflow.com/questions/4101784/calling-a-generic-method-with-a-dynamic-type, but wouldn't work with an interface, only concrete types.
	}

	public void InvokeOnListOfWeaklyTypedThings()
	{
		// Test the cast - RUNTIME EXCEPTION HERE
		IFoo<object> test = (IFoo<object>)new BoolFoo(); // Unable to cast object of type 'BoolFoo' to type 'IFoo`1[System.Object]'.

		// Arrange weakly typed list of things - This throws runtime error with exception noted above
		var weaklyTypedThings = new List<IFoo<object>> {
		(IFoo<object>) new BoolFoo(),
		(IFoo<object>) new IntFoo(),
		(IFoo<object>) new StringFoo()
	};

		// Invoke method
		var thing = weaklyTypedThings.FirstOrDefault();
		thing.Bar();
	}
}

public class StaticFactoryTest
{

	public void InvokeBar()
	{
		var foo = GetFoo(1);
		foo.Bar(); //  This works, but like above, can't perform cast.
		
		// Can't cast though using a generic, this would fail:
		// var test = (IFoo<dynamic>) foo;		
	}

	private dynamic GetFoo(int key)
	{
		switch (key)
		{
			case 0:
				return new BoolFoo();
			case 1:
				return new IntFoo();
			default:
				return new StringFoo();
		}
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
		return "Hello World";
	}
}