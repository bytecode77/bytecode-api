using BytecodeApi;
using BytecodeApi.Mathematics;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Inline try/catch
		Console.WriteLine("GetFirstName() = " + CSharp.Try(() => GetFirstName(), "my_default_value"));

		// Retry a function for 20 times
		Console.WriteLine("GetLastName() = " + CSharp.Retry(() => GetLastName(), 20));

		// Convert from one object to another using reflection
		// Very useful if you have classes where most properties are equal (e.g. conversion of database model to entities)
		ClassA objA = new ClassA
		{
			FirstName = "Bill",
			LastName = "Smith"
		};
		ClassB objB = CSharp.ConvertObject<ClassB>(objA);

		Console.ReadKey();
	}

	private static string GetFirstName()
	{
		// This method has a bug and throws:
		throw new Exception();
	}
	private static string GetLastName()
	{
		// This method connects to an unstable web service and sometimes throws:

		if (MathEx.Random.Next(3) == 0)
		{
			return "Smith";
		}
		else
		{
			throw new Exception();
		}
	}
}

public class ClassA
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
}

public class ClassB
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
}