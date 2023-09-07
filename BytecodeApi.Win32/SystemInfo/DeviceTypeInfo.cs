using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Represents a device type, containing an array of <see cref="DeviceInfo" />, retrieved by <see cref="DeviceManager" />.
/// </summary>
[DebuggerDisplay($"{nameof(DeviceTypeInfo)}: ClassName = {{ClassName}}, Devices: {{Devices.Count}}")]
public sealed class DeviceTypeInfo
{
	/// <summary>
	/// Gets the class GUID of this device type.
	/// </summary>
	public string ClassGuid { get; internal set; } = null!;
	/// <summary>
	/// Gets the class name of this device type.
	/// </summary>
	public string? ClassName { get; internal set; }
	/// <summary>
	/// Gets the path to the resource DLL that contains the icon associated with this device type.
	/// </summary>
	public string? IconPath { get; internal set; }
	/// <summary>
	/// Gets the icon index (resource name) within the resource DLL located in <see cref="IconPath" />.
	/// </summary>
	public int? IconIndex { get; internal set; }
	/// <summary>
	/// Gets all <see cref="DeviceInfo" /> objects of this device type.
	/// </summary>
	public ReadOnlyCollection<DeviceInfo> Devices { get; internal set; } = null!;

	internal DeviceTypeInfo()
	{
	}

	/// <summary>
	/// Returns the class name of this <see cref="DeviceTypeInfo" />.
	/// </summary>
	/// <returns>
	/// The name of this <see cref="DeviceTypeInfo" />.
	/// </returns>
	public override string ToString()
	{
		return ClassName ?? "";
	}
}