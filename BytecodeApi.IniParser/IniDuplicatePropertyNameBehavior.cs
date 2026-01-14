namespace BytecodeApi.IniParser;

/// <summary>
/// Specifies how duplicate property names of INI files are treated.
/// </summary>
public enum IniDuplicatePropertyNameBehavior
{
	/// <summary>
	/// Specifies that an <see cref="IniParsingException" /> is thrown, unless <see cref="IniFileParsingOptions.IgnoreErrors" /> is set to <see langword="true" />.
	/// </summary>
	Abort,
	/// <summary>
	/// Specifies that the property is ignored.
	/// </summary>
	Ignore,
	/// <summary>
	/// Specifies that the property overwrites the value of the existing property within the scope of its <see cref="IniSection" />.
	/// </summary>
	Overwrite,
	/// <summary>
	/// Specifies that a new <see cref="IniProperty" /> object is created each time.
	/// </summary>
	Duplicate
}