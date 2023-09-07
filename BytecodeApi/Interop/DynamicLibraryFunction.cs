using BytecodeApi.Extensions;
using System.Diagnostics;
using System.Reflection;

namespace BytecodeApi.Interop;

/// <summary>
/// Represents the function of a native DLL file that does not return a value.
/// </summary>
[DebuggerDisplay($"{nameof(DynamicLibraryFunction)}: {{Name,nq}}({{DebuggerDisplayParameters,nq}})")]
public sealed class DynamicLibraryFunction
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private string DebuggerDisplayParameters => Method.GetParameters().Select(parameter => parameter.ParameterType.ToCSharpName()).AsString(", ");
	private readonly MethodInfo Method;
	/// <summary>
	/// Gets the <see cref="DynamicLibrary" /> that was used to create this <see cref="DynamicLibraryFunction" />.
	/// </summary>
	public DynamicLibrary Library { get; private init; }
	/// <summary>
	/// Gets the name of the entry point in the DLL.
	/// </summary>
	public string Name => Method.Name;

	internal DynamicLibraryFunction(DynamicLibrary library, MethodInfo method)
	{
		Library = library;
		Method = method;
	}

	/// <summary>
	/// Calls the function with the specified parameters.
	/// </summary>
	/// <param name="parameters">A collection of parameters. The number of parameters must match the number of parameter types upon creation.</param>
	public void Call(params object?[]? parameters)
	{
		Check.TargetParameterCount(parameters?.Length ?? 0, Method.GetParameters().Length);

		Method.Invoke(null, parameters);
	}

	/// <summary>
	/// Returns the name of this <see cref="DynamicLibraryFunction" />.
	/// </summary>
	/// <returns>
	/// The name of this <see cref="DynamicLibraryFunction" />.
	/// </returns>
	public override string ToString()
	{
		return Name;
	}
}

/// <summary>
/// Represents the function of a native DLL file that returns a value of the specified type.
/// </summary>
/// <typeparam name="T">The function's return type.</typeparam>
[DebuggerDisplay($"{nameof(DynamicLibraryFunction<T>)}: {{Name,nq}}({{DebuggerDisplayParameters,nq}})")]
public sealed class DynamicLibraryFunction<T>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private string DebuggerDisplayParameters => Method.GetParameters().Select(parameter => parameter.ParameterType.ToCSharpName()).AsString(", ");
	private readonly MethodInfo Method;
	/// <summary>
	/// Gets the <see cref="DynamicLibrary" /> that was used to create this <see cref="DynamicLibraryFunction{T}" />.
	/// </summary>
	public DynamicLibrary Library { get; private set; }
	/// <summary>
	/// Gets the name of the entry point in the DLL.
	/// </summary>
	public string Name => Method.Name;

	internal DynamicLibraryFunction(DynamicLibrary library, MethodInfo method)
	{
		Library = library;
		Method = method;
	}

	/// <summary>
	/// Calls the function with the specified parameters and returns a value.
	/// </summary>
	/// <param name="parameters">A collection of parameters. The number of parameters must match the number of parameter types upon creation.</param>
	/// <returns>
	/// The value that the native function returned.
	/// </returns>
	public T? Call(params object?[]? parameters)
	{
		Check.TargetParameterCount(parameters?.Length ?? 0, Method.GetParameters().Length);

		return Method.Invoke<T>(null, parameters);
	}

	/// <summary>
	/// Returns the name of this <see cref="DynamicLibraryFunction{T}" />.
	/// </summary>
	/// <returns>
	/// The name of this <see cref="DynamicLibraryFunction{T}" />.
	/// </returns>
	public override string ToString()
	{
		return Name;
	}
}