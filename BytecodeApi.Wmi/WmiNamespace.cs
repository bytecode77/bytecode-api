using BytecodeApi.Extensions;
using System.Diagnostics;
using System.Management;

namespace BytecodeApi.Wmi;

/// <summary>
/// Represents a WMI namespace.
/// </summary>
[DebuggerDisplay($"{nameof(WmiNamespace)}: Path = {{Path}}")]
public sealed class WmiNamespace
{
	/// <summary>
	/// Gets the path of this <see cref="WmiNamespace" />.
	/// </summary>
	public string Path { get; private init; }
	internal string FullPath => System.IO.Path.Combine(@"\\", Environment.MachineName, "ROOT", Path);

	internal WmiNamespace(string path)
	{
		Path = path;
	}

	/// <summary>
	/// Retrieves all WMI namespaces that are within this <see cref="WmiNamespace" />.
	/// </summary>
	/// <returns>
	/// A <see cref="WmiNamespace" />[] with all WMI namespaces that are within this <see cref="WmiNamespace" />.
	/// </returns>
	public WmiNamespace[] GetNamespaces()
	{
		using ManagementClass managementClass = new(FullPath, "__namespace", new ObjectGetOptions());
		using ManagementObjectCollection objectCollection = managementClass.GetInstances();

		return objectCollection
			.Cast<ManagementObject>()
			.AsDisposable()
			.Select(obj => new WmiNamespace(System.IO.Path.Combine(Path, obj["Name"].ToString() ?? throw new ManagementException("Namespace name is empty."))))
			.ToArray();
	}
	/// <summary>
	/// Gets a <see cref="WmiNamespace" /> identified by a path.
	/// </summary>
	/// <param name="path">The path that identifies the <see cref="WmiNamespace" />.</param>
	/// <returns>
	/// A new <see cref="WmiNamespace" /> object.
	/// </returns>
	public WmiNamespace GetNamespace(string path)
	{
		Check.ArgumentNull(path);

		return new(System.IO.Path.Combine(Path, path));
	}
	/// <summary>
	/// Retrieves all WMI classes from this <see cref="WmiNamespace" />.
	/// </summary>
	/// <returns>
	/// A <see cref="WmiClass" />[] with all WMI classes from this <see cref="WmiNamespace" />.
	/// </returns>
	public WmiClass[] GetClasses()
	{
		using ManagementObjectSearcher searcher = new(FullPath, "SELECT * FROM meta_class");
		using ManagementObjectCollection objects = searcher.Get();

		return objects
			.Cast<ManagementObject>()
			.AsDisposable()
			.Select(obj => new WmiClass(this, obj.ClassPath.ClassName))
			.ToArray();
	}
	/// <summary>
	/// Gets a <see cref="WmiClass" /> identified by a name.
	/// </summary>
	/// <param name="name">The name that identifies the <see cref="WmiClass" />.</param>
	/// <returns>
	/// A new <see cref="WmiClass" /> object.
	/// </returns>
	public WmiClass GetClass(string name)
	{
		Check.ArgumentNull(name);

		return new(this, name);
	}
}