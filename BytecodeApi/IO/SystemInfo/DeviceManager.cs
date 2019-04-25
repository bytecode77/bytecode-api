using BytecodeApi.Extensions;
using BytecodeApi.IO.Wmi;
using Microsoft.Win32;
using System;
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
		public static DeviceManager GetDevices()
		{
			List<DeviceInfo> devices = new List<DeviceInfo>();

			IEnumerable<WmiObject> objects = new WmiNamespace("CIMV2", false, false)
				.GetClass("Win32_PnPentity", false)
				.GetObjects()
				.Where(obj => obj.Properties["ClassGuid", null].Value != null);

			foreach (WmiObject wmiObject in objects)
			{
				devices.Add(new DeviceInfo
				(
					wmiObject.Properties
						.Where(property => property.Value != null)
						.Select(property => Tuple.Create(property.Name.Trim(), property.Value))
						.ToDictionary()
				));
			}

			return new DeviceManager
			(
				devices
					.GroupBy(device => device.Attributes["ClassGuid"] as string)
					.Select(group => new
					{
						ClassGuid = group.Key,
						RegistryKey = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Control\Class\" + group.Key),
						Devices = group.ToArray()
					})
					.Select(group =>
					{
						using (group.RegistryKey)
						{
							return new DeviceTypeInfo
							(
								group.ClassGuid,
								group.RegistryKey.GetStringValue("Class").ToNullIfEmpty(),
								group.RegistryKey.GetStringValue(null).ToNullIfEmpty(),
								group.Devices
							);
						}
					})
					.OrderBy(deviceType => deviceType.Name)
			);
		}
	}
}