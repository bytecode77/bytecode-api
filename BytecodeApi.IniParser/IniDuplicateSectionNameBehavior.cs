namespace BytecodeApi.IniParser;

/// <summary>
/// Specifies how duplicate section names of INI files are treated.
/// </summary>
public enum IniDuplicateSectionNameBehavior
{
	/// <summary>
	/// Specifies that an <see cref="IniParsingException" /> is thrown, unless <see cref="IniFileParsingOptions.IgnoreErrors" /> is set to <see langword="true" />.
	/// </summary>
	Abort,
	/// <summary>
	/// Specifies that the section and its content are ignored and parsing is skipped to the next section.
	/// </summary>
	Ignore,
	/// <summary>
	/// Specifies that properties of the section are added to the existing <see cref="IniSection" /> object, instead of creating a new <see cref="IniSection" />.
	/// </summary>
	Merge,
	/// <summary>
	/// Specifies that a new <see cref="IniSection" /> object is created each time.
	/// </summary>
	Duplicate
}