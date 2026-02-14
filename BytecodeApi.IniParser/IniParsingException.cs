namespace BytecodeApi.IniParser;

/// <summary>
/// The exception that is thrown when parsing of an <see cref="IniFile" /> fails.
/// </summary>
public sealed class IniParsingException : Exception
{
	/// <summary>
	/// Gets the one-based line number of the line at which parsing failed.
	/// </summary>
	public int LineNumber { get; }
	/// <summary>
	/// Gets the line at which parsing failed as its original <see cref="string" /> representation.
	/// </summary>
	public string Line { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="IniParsingException" /> class.
	/// </summary>
	/// <param name="lineNumber">The one-based line number of the line at which parsing failed.</param>
	/// <param name="line">The line at which parsing failed as its original <see cref="string" /> representation.</param>
	public IniParsingException(int lineNumber, string line) : base($"INI file parsing failed at line {lineNumber}.")
	{
		Check.ArgumentNull(line);

		LineNumber = lineNumber;
		Line = line;
	}
}