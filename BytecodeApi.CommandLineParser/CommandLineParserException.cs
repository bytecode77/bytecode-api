namespace BytecodeApi.CommandLineParser;

/// <summary>
/// The exception that is thrown when commandline parsing failed or was asserted using the <see cref="ParsedOptionSet.Assert" /> object.
/// </summary>
public sealed class CommandLineParserException : Exception
{
	/// <summary>
	/// Gets a <see cref="CommandLineParserError" /> value that indicates what validation took place using the <see cref="ParsedOptionSet.Assert" /> object. For general exceptions, <see cref="CommandLineParserError.None" /> is returned.
	/// </summary>
	public CommandLineParserError Error { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="CommandLineParserException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public CommandLineParserException(string? message) : this(CommandLineParserError.None, message)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="CommandLineParserException" /> class.
	/// </summary>
	/// <param name="error">If validated using the <see cref="ParsedOptionSet.Assert" /> object, indicates what validation took place.</param>
	/// <param name="message">The message that describes the error.</param>
	public CommandLineParserException(CommandLineParserError error, string? message) : base(message)
	{
		Error = error;
	}
}