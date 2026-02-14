using System.Diagnostics;
using System.Reflection;

// Implements an entry point that invokes all methods marked with the ExecuteAttribute.

MethodInfo[] methods = typeof(Playground).Assembly
	.GetTypes()
	.SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.Public))
	.Where(method => method.GetCustomAttribute<ExecuteAttribute>() != null)
	.ToArray();

foreach (MethodInfo method in methods)
{
	object?[]? parameters = method.GetParameters() switch
	{
		ParameterInfo[] p when p.Length == 0 => null,
		ParameterInfo[] p when p.Length == 1 && p[0].ParameterType == typeof(string[]) => new object[] { args },
		_ => throw new InvalidOperationException("Method must have either no parameters, or a string[] parameter.")
	};

	Stopwatch stopwatch = Stopwatch.StartNew();

	if (method.Invoke(null, parameters) is Task task)
	{
		await task;
	}

	stopwatch.Stop();
	Console.WriteLine($"{method.Name} executed in {stopwatch.ElapsedMilliseconds} ms");
}

Console.ReadKey();