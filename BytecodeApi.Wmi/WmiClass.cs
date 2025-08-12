using System.Diagnostics;
using System.Management;

namespace BytecodeApi.Wmi;

/// <summary>
/// Represents a WMI class.
/// </summary>
[DebuggerDisplay($"{nameof(WmiClass)}: Name = {{Name}}, Namespace = {{Namespace.FullPath}}")]
public sealed class WmiClass : IWmiQueryable
{
	/// <summary>
	/// Gets the <see cref="WmiNamespace" /> associated with this <see cref="WmiClass" />.
	/// </summary>
	public WmiNamespace Namespace { get; private init; }
	/// <summary>
	/// Gets the name of this <see cref="WmiClass" />.
	/// </summary>
	public string Name { get; private init; }

	internal WmiClass(WmiNamespace @namespace, string name)
	{
		Namespace = @namespace;
		Name = name;
	}

	/// <summary>
	/// Specifies what columns to read from the WMI class. By default, all columns are read.
	/// </summary>
	/// <param name="columns">A <see cref="string" />[] specifying what columns to read from the WMI class.</param>
	/// <returns>
	/// The query with the additional SELECT statement.
	/// </returns>
	public IWmiQueryable Select(params string[] columns)
	{
		return new WmiQueryBuilder(this).Select(columns);
	}
	/// <summary>
	/// Specifies a filter condition for the WMI WHERE clause.
	/// </summary>
	/// <param name="condition">A statement for the WMI WHERE clause.</param>
	/// <returns>
	/// The query with the additional WHERE statement.
	/// </returns>
	public IWmiQueryable Where(string condition)
	{
		return new WmiQueryBuilder(this).Where(condition);
	}
	/// <summary>
	/// Executes this query and reads the contents from the WMI class.
	/// </summary>
	/// <returns>
	/// A new <see cref="WmiObject" />[] with the contents from the WMI class.
	/// </returns>
	public WmiObject[] ToArray()
	{
		return new WmiQueryBuilder(this).ToArray();
	}
	/// <summary>
	/// Executes this query and reads the first element from the WMI class.
	/// </summary>
	/// <returns>
	/// A new <see cref="WmiObject" /> with the first element from the WMI class.
	/// </returns>
	public WmiObject First()
	{
		return new WmiQueryBuilder(this).First();
	}
	/// <summary>
	/// Executes this query and reads the first element from the WMI class, and returns <see langword="null" />, if the query returned no elements.
	/// </summary>
	/// <returns>
	/// A new <see cref="WmiObject" /> with the first element from the WMI class, or <see langword="null" />, if the query returned no elements.
	/// </returns>
	public WmiObject? FirstOrDefault()
	{
		return new WmiQueryBuilder(this).FirstOrDefault();
	}

	/// <summary>
	/// Invokes a method on this <see cref="WmiClass" />.
	/// </summary>
	/// <param name="methodName">The name of the method to invoke.</param>
	/// <param name="args">An array containing parameter values.</param>
	/// <returns>
	/// The <see cref="object" /> value returned by the method.
	/// </returns>
	public object InvokeMethod(string methodName, params object[]? args)
	{
		Check.ArgumentNull(methodName);

		using ManagementClass managementClass = new(Namespace.FullPath, Name, new ObjectGetOptions());
		return managementClass.InvokeMethod(methodName, args ?? []);
	}
	/// <summary>
	/// Invokes a method on this <see cref="WmiClass" />.
	/// </summary>
	/// <param name="methodName">The name of the method to invoke.</param>
	/// <param name="args">An array containing parameter values.</param>
	/// <typeparam name="T">The type of the returned value.</typeparam>
	/// <returns>
	/// The value returned by the method, casted to <typeparamref name="T" />.
	/// </returns>
	public T InvokeMethod<T>(string methodName, params object[]? args)
	{
		return (T)InvokeMethod(methodName, args);
	}
}