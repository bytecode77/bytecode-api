using BytecodeApi.Text;
using System.Diagnostics;

namespace BytecodeApi.FileFormats.Ini
{
	/// <summary>
	/// Represents a section of an <see cref="IniFile" />.
	/// </summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public sealed class IniSection
	{
		private string DebuggerDisplay => CSharp.DebuggerDisplay<IniSection>("Name = {0}, Properties: {1}", new QuotedString(Name), Properties.Count);
		/// <summary>
		/// Gets or sets the name of this INI section.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Gets the collection of INI properties associated with this section.
		/// </summary>
		public IniPropertyCollection Properties { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="IniSection" /> class.
		/// </summary>
		/// <param name="name">A <see cref="string" /> value specifying the name of this INI section.</param>
		public IniSection(string name)
		{
			Name = name;
			Properties = new IniPropertyCollection();
		}
	}
}