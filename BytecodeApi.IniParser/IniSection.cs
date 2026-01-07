using System.Diagnostics;

namespace BytecodeApi.IniParser;

/// <summary>
/// Represents a section of an <see cref="IniFile" />.
/// </summary>
[DebuggerDisplay($"{nameof(IniSection)}: Name = {{Name}}, Properties: {{Properties.Count}}")]
public sealed class IniSection
{
	/// <summary>
	/// Gets or sets the name of this INI section.
	/// </summary>
	public string? Name { get; set; }
	/// <summary>
	/// Gets the collection of INI properties associated with this section.
	/// </summary>
	public IniPropertyCollection Properties { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="IniSection" /> class.
	/// </summary>
	/// <param name="name">A <see cref="string" /> value specifying the name of this INI section.</param>
	public IniSection(string? name)
	{
		Name = name;
		Properties = [];
	}

	/// <summary>
	/// Retrieves an <see cref="IniProperty" /> with the specified name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the name of the <see cref="IniProperty" />.</param>
	/// <returns>
	/// The <see cref="IniProperty" /> with the specified name, or <see langword="null" /> if no matching property was found.
	/// </returns>
	public IniProperty? Property(string name)
	{
		return Property(name, false);
	}
	/// <summary>
	/// Retrieves an <see cref="IniProperty" /> with the specified name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the name of the <see cref="IniProperty" />.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during name comparison.</param>
	/// <returns>
	/// The <see cref="IniProperty" /> with the specified name, or <see langword="null" /> if no matching property was found.
	/// </returns>
	public IniProperty? Property(string name, bool ignoreCase)
	{
		Check.ArgumentNull(name);

		return Properties.FirstOrDefault(property => property.Name.Equals(name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
	}
}