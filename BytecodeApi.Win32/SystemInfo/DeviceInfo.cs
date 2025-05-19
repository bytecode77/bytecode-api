using BytecodeApi.Extensions;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Represents device information, retrieved by <see cref="DeviceManager" />.
/// </summary>
[DebuggerDisplay($"{nameof(DeviceInfo)}: Name = {{Name}}, Description = {{Description}}, Attributes: {{Attributes.Count}}")]
public sealed class DeviceInfo
{
	/// <summary>
	/// Gets the collection of attributes associated with this device.
	/// </summary>
	public ReadOnlyDictionary<string, object> Attributes { get; private init; }
	/// <summary>
	/// Gets the name associated with this device.
	/// </summary>
	public string? Name { get; private init; }
	/// <summary>
	/// Gets the description associated with this device.
	/// </summary>
	public string? Description { get; private init; }

	internal DeviceInfo(IDictionary<string, object> attributes)
	{
		Attributes = attributes.ToReadOnlyDictionary();
		Name = Attributes.GetValueOrDefault("Name") as string;
		Description = Attributes.GetValueOrDefault("Description") as string;
	}

	/// <summary>
	/// Returns the name of this <see cref="DeviceInfo" />.
	/// </summary>
	/// <returns>
	/// The name of this <see cref="DeviceInfo" />.
	/// </returns>
	public override string ToString()
	{
		return Name ?? "";
	}
}