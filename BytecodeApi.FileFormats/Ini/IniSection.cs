namespace BytecodeApi.FileFormats.Ini
{
	/// <summary>
	/// Represents a section of an <see cref="IniFile" />.
	/// </summary>
	public sealed class IniSection
	{
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

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Name + ", Properties: " + Properties.Count + "]";
		}
	}
}