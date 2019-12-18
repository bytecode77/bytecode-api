using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace BytecodeApi.IO.Wmi
{
	/// <summary>
	/// Represents a WMI object instance.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public class WmiObject
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<WmiObject>("Class = {0}, Properties: {1}", new QuotedString(Class.Name), Properties.Count);
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
	}
}