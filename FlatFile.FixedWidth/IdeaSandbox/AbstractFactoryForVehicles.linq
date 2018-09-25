<Query Kind="Program" />

void Main()
{

	var carFactory = new VehicleFactory<Car>();
	var carProduct = carFactory.Create<Toyota>();
	
	var planeFactory = new VehicleFactory<Plane>(); 
	var planeProduct = planeFactory.Create<Boing>();

	// How about product-specific methods not exposed by interface without casting? 
	carProduct.ToyotaOnlyOperation(); 
	planeProduct.BoingOnlyOperation();
}


// Abstract Factory
public interface IVehicleFactory<TFactory>
{
	// Using generics to get type specific behavior without downcasting
	TProduct Build<TProduct>() where TProduct : IProduct<TFactory>, new();
}



// Concrete Factories
public class Car : IVehicleFactory<Car>
{
	public TProduct Build<TProduct>() where TProduct : IProduct<Car>, new()
	{
		Console.WriteLine("Creating Car");
		return new TProduct();
	}
}

public class Plane : IVehicleFactory<Plane>
{
	public TProduct Build<TProduct>() where TProduct : IProduct<Plane>, new()
	{
		Console.WriteLine("Creating Plane");
		return new TProduct();
	}
}

public class Boat : IVehicleFactory<Boat>
{
	public TProduct Build<TProduct>() where TProduct : IProduct<Boat>, new()
	{
		Console.WriteLine("Creating Boat");
		return new TProduct();
	}
}




// Abstract Product
public interface IProduct<TProduct>
{
	void Operate(); //  Drive, Fly, etc...
}




// Concrete Products 
class Toyota : IProduct<Car>
{
	public void Operate()
	{
		Console.WriteLine("Driving Toyota");
	}

	public void ToyotaOnlyOperation()
	{
		Console.WriteLine("Toyota specific operation");
	}
}

class Honda : IProduct<Car>
{
	public void Operate()
	{
		Console.WriteLine("Driving Honda");
	}

	public void HondaOnlyOperation()
	{
		Console.WriteLine("Honda specific operation");
	}
}

class Boing : IProduct<Plane>
{
	public void Operate()
	{
		Console.WriteLine("Flying Boing");
	}

	public void BoingOnlyOperation()
	{
		Console.WriteLine("Boing specific operation");
	}
}




// Concrete Products using dependent type
public class Engine<TFactory> : IProduct<TFactory>
{
	public void Operate()
	{
		Console.WriteLine("Operating Engine");
	}

	public void EngineSpecificOperation()
	{
		Console.WriteLine("Engine Specific Operation");
	}
}


public class Frame<TFactory> : IProduct<Car>, IProduct<Plane>
{
	public void Operate()
	{
		Console.WriteLine("Operating Frame");
	}

	public void FrameSpecificOperation()
	{
		Console.WriteLine("Frame Specific Operation");
	}
}

public class Rudder<TFactory> : IProduct<Plane>, IProduct<Boat>
{
	public void Operate()
	{
		Console.WriteLine("Operating Rudder");
	}

	public void RudderSpecificOperation()
	{
		Console.WriteLine("Rudder Specific Operation");
	}
}



// Client Factory
class VehicleFactory<TFactory> where TFactory : IVehicleFactory<TFactory>, new()
{
	public TProduct Create<TProduct>() where TProduct : IProduct<TFactory>, new()
	{
		return new TFactory().Build<TProduct>();
	}
}