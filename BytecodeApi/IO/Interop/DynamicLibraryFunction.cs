using BytecodeApi.Extensions;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BytecodeApi.IO.Interop
{
	/// <summary>
	/// Represents the function of a native DLL file that does not return a value.
	/// </summary>
	public sealed class DynamicLibraryFunction
	{
		private readonly MethodInfo Method;
		/// <summary>
		/// Gets the <see cref="DynamicLibrary" /> that was used to create this <see cref="DynamicLibraryFunction" />.
		/// </summary>
		public DynamicLibrary Library { get; private set; }
		/// <summary>
		/// Gets the name of the name of the entry point in the DLL.
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
		public void Call(params object[] parameters)
		{
			Check.TargetParameterCount(parameters.Length == Method.GetParameters().Length);

			Method.Invoke(null, parameters);
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Path.GetFileName(Library.DllName) + ", " + Name + ", Parameters: " + Method.GetParameters().Select(parameter => parameter.ParameterType.ToCSharpName(TypeNaming.CSharp)).AsString(", ") + "]";
		}
	}

	/// <summary>
	/// Represents the function of a native DLL file that returns a value of the specified type.
	/// </summary>
	/// <typeparam name="T">The function's return type.</typeparam>
	public sealed class DynamicLibraryFunction<T>
	{
		private readonly MethodInfo Method;
		/// <summary>
		/// Gets the <see cref="DynamicLibrary" /> that was used to create this <see cref="DynamicLibraryFunction{T}" />.
		/// </summary>
		public DynamicLibrary Library { get; private set; }
		/// <summary>
		/// Gets the name of the name of the entry point in the DLL.
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
		public T Call(params object[] parameters)
		{
			Check.TargetParameterCount(parameters.Length == Method.GetParameters().Length);

			return (T)Method.Invoke(null, parameters);
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Path.GetFileName(Library.DllName) + ", " + Name + ", Parameters: " + Method.GetParameters().Select(parameter => parameter.ParameterType.ToCSharpName(TypeNaming.CSharp)).AsString(", ") + "]";
		}
	}
}