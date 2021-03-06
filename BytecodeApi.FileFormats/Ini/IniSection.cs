﻿using BytecodeApi.Text;
using System.Diagnostics;

namespace BytecodeApi.FileFormats.Ini
{
	/// <summary>
	/// Represents a section of an <see cref="IniFile" />.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class IniSection
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
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

		/// <summary>
		/// Determines whether a property with the specified name exists in this section.
		/// </summary>
		/// <param name="name">The name of the property to check.</param>
		/// <returns>
		/// <see langword="true" />, if the property with the specified name exists;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool HasProperty(string name)
		{
			return HasProperty(name, false);
		}
		/// <summary>
		/// Determines whether a property with the specified name exists in this section.
		/// </summary>
		/// <param name="name">The name of the property to check.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <returns>
		/// <see langword="true" />, if the property with the specified name exists;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool HasProperty(string name, bool ignoreCase)
		{
			return Properties.HasProperty(name, ignoreCase);
		}

	}
}