using System.ComponentModel;

namespace BytecodeApi.IniParser;

/// <summary>
/// Specifies the delimiter between INI property names and values.
/// </summary>
public enum IniPropertyDelimiter
{
	/// <summary>
	/// Specifies that the name and the value of INI properties are separated by an equal sign ("=")
	/// </summary>
	[Description("=")]
	EqualSign,
	/// <summary>
	/// Specifies that the name and the value of INI properties are separated by a colon (":")
	/// </summary>
	[Description(":")]
	Colon
}