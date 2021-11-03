using BytecodeApi.Extensions;
using BytecodeApi.IO.Wmi;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Provides information for devices as shown in the Windows Device Manager.
	/// </summary>
	public sealed class DeviceManager
	{
		/// <summary>
		/// Gets all retrieved <see cref="DeviceInfo" /> objects, categorized by device types.
		/// </summary>
		public ReadOnlyCollection<DeviceTypeInfo> DeviceTypes { get; private set; }

		private DeviceManager(IEnumerable<DeviceTypeInfo> deviceTypes)
		{
			DeviceTypes = deviceTypes.ToReadOnlyCollection();
		}
		/// <summary>
		/// Creates a new <see cref="DeviceManager" /> and retrieves all device info objects.
		/// </summary>
		/// <returns>
		/// The <see cref="DeviceManager" /> this method creates.
		/// </returns>
		public static DeviceManager Create()
		{
			return new DeviceManager
			(
				new WmiNamespace("CIMV2", false, false)
					.GetClass("Win32_PnPentity", false)
					.GetObjects()
					.Where(obj => obj.Properties["ClassGuid", null].Value is string)
					.Select(obj => new DeviceInfo
					(
						obj.Properties
							.Where(property => property.Value != null)
							.ToDictionary(property => property.Name.Trim(), property => property.Value)
					))
					.ToArray()
					.GroupBy(device => (string)device.Attributes["ClassGuid"])
					.Select(group =>
					{
						using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Control\Class\" + group.Key))
						{
							return new DeviceTypeInfo
							(
								group.Key,
								key.GetStringValue("Class").ToNullIfEmpty(),
								key.GetStringValue(null).ToNullIfEmpty(),
								group.OrderBy(device => device.Name).ToArray()
							);
						}
					})
					.OrderBy(deviceType => deviceType.Name)
					.ToArray()
			);
		}
	}
}