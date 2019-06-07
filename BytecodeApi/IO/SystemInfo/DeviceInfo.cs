using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Represents device information, retrieved by <see cref="DeviceManager" />.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class DeviceInfo
	{
		private string DebuggerDisplay => CSharp.DebuggerDisplay<DeviceInfo>("Name = {0}, Description = {1}, Attributes: {2}", new QuotedString(Name), new QuotedString(Description), Attributes.Count);
		/// <summary>
		/// Gets the collection of attributes associated with this device.
		/// </summary>
		public ReadOnlyDictionary<string, object> Attributes { get; private set; }
		/// <summary>
		/// Gets the name associated with this device.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the description associated with this device.
		/// </summary>
		public string Description { get; private set; }

		internal DeviceInfo(IDictionary<string, object> attributes)
		{
			Attributes = attributes.ToReadOnlyDictionary();
			Name = Attributes.ValueOrDefault("Name") as string;
			Description = Attributes.ValueOrDefault("Description") as string;
		}

		/// <summary>
		/// Returns the name of this <see cref="DeviceInfo" />.
		/// </summary>
		/// <returns>
		/// The name of this <see cref="DeviceInfo" />.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}
	}
}