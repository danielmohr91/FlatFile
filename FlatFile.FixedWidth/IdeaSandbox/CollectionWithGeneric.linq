<Query Kind="Program" />

void Main()
{
	var fooList = new List<Foo<object>>(); // Somewhere
	var boolFoo = new Foo<bool>(); 
	var intFoo = new Foo<int>();
	
	// Can't mix the types here
	fooList.Add(boolFoo);
	fooList.Add(intFoo);
	
}

public class Foo<T> where T : new()
{
	T Bar()
	{
		return new T();
	}
}