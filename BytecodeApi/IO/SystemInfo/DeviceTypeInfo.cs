using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Represents a device type, containing an array of <see cref="DeviceInfo" />, retrieved by <see cref="DeviceManager" />.
	/// </summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public sealed class DeviceTypeInfo
	{
		private string DebuggerDisplay => CSharp.DebuggerDisplay<DeviceTypeInfo>("Name = {0}, ClassName = {1}, Devices: {2}", new QuotedString(Name), new QuotedString(ClassName), Devices.Count);
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
		/// Returns the name of this <see cref="DeviceTypeInfo" />.
		/// </summary>
		/// <returns>
		/// The name of this <see cref="DeviceTypeInfo" />.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}
	}
}