using BytecodeApi.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Represents a device type, containing an array of <see cref="DeviceInfo" />, retrieved by <see cref="DeviceManager" />.
	/// </summary>
	public class DeviceTypeInfo
	{
		/// <summary>
		/// Gets the class GUID of this device type.
		/// </summary>
		public string ClassGuid { get; private set; }
		/// <summary>
		/// Gets the class name of this device type.
		/// </summary>
		public string ClassName { get; private set; }
		/// <summary>
		/// Gets the display name of this device type.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets all <see cref="DeviceInfo" /> objects of this device type.
		/// </summary>
		public ReadOnlyCollection<DeviceInfo> Devices { get; private set; }

		internal DeviceTypeInfo(string classGuid, string className, string name, IEnumerable<DeviceInfo> devices)
		{
			ClassGuid = classGuid;
			ClassName = className;
			Name = name;
			Devices = devices.ToReadOnlyCollection();
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Name + ", Devices: " + Devices.Count + "]";
		}
	}
}