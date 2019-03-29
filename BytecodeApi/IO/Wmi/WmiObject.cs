using BytecodeApi.Extensions;
using System.Collections.ObjectModel;
using System.Linq;

namespace BytecodeApi.IO.Wmi
{
	/// <summary>
	/// Represents a WMI object instance.
	/// </summary>
	public class WmiObject
	{
		/// <summary>
		/// Gets the <see cref="WmiClass" /> that this <see cref="WmiObject" /> was created from.
		/// </summary>
		public WmiClass Class { get; private set; }
		/// <summary>
		/// Gets the collection of properties of this <see cref="WmiObject" /> instance.
		/// </summary>
		public WmiPropertyCollection Properties { get; private set; }
		/// <summary>
		/// Gets a <see cref="string" /> collection of property names of this <see cref="WmiObject" /> instance.
		/// </summary>
		public ReadOnlyCollection<string> PropertyNames { get; private set; }

		internal WmiObject(WmiClass wmiClass, WmiProperty[] properties)
		{
			Class = wmiClass;
			Properties = new WmiPropertyCollection(properties);
			PropertyNames = properties.Select(property => property.Name).ToReadOnlyCollection();
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Class.Name + ", Properties: " + Properties.Count + "]";
		}
	}
}