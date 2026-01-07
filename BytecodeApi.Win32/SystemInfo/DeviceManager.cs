using BytecodeApi.Extensions;
using BytecodeApi.Wmi;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Provides a snapshot of all devices as shown in the Windows Device Manager.
/// </summary>
public sealed class DeviceManager
{
	/// <summary>
	/// Gets all <see cref="DeviceInfo" /> objects, categorized by device types.
	/// </summary>
	public ReadOnlyCollection<DeviceTypeInfo> DeviceTypes { get; }

	private DeviceManager(IEnumerable<DeviceTypeInfo> deviceTypes)
	{
		DeviceTypes = deviceTypes.ToReadOnlyCollection();
	}
	/// <summary>
	/// Creates a new <see cref="DeviceManager" /> instance and loads information about devices.
	/// </summary>
	/// <returns>
	/// The <see cref="DeviceManager" /> this method creates.
	/// </returns>
	public static DeviceManager Create()
	{
		return new
		(
			WmiContext.Root
				.GetNamespace("CIMV2")
				.GetClass("Win32_PnPentity")
				.ToArray()
				.Where(obj => obj.Properties.FirstOrDefault(p => p.Name == "ClassGuid").Value is string)
				.Select(obj => new DeviceInfo
				(
					obj.Properties
						.Where(property => property.Value != null)
						.ToDictionary(property => property.Name.Trim(), property => property.Value!)
				))
				.ToArray()
				.GroupBy(device => (string)device.Attributes["ClassGuid"])
				.Select(group =>
				{
					using RegistryKey? key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Control\Class\{group.Key}");

					string? iconPath = key?.GetStringArrayValue("IconPath")?.FirstOrDefault();
					int? iconIndex = null;

					if (iconPath?.Contains(',') == true)
					{
						iconIndex = iconPath.SubstringFromLast(',').ToInt32OrNull();
						if (iconIndex < 0)
						{
							iconIndex = -iconIndex;
						}

						iconPath = Environment.ExpandEnvironmentVariables(iconPath.SubstringUntilLast(','));
					}

					return new DeviceTypeInfo
					{
						ClassGuid = group.Key,
						ClassName = key?.GetStringValue("Class").ToNullIfEmpty(),
						IconPath = iconPath,
						IconIndex = iconIndex,
						Devices = group.OrderBy(device => device.Name).ToReadOnlyCollection()
					};
				})
				.OrderBy(deviceType => deviceType.ClassName)
				.ToArray()
		);
	}
}