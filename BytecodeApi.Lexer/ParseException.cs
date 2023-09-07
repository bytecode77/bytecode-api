namespace BytecodeApi.Lexer;

/// <summary>
/// The exception that is thrown when lexing fails.
/// </summary>
public sealed class ParseException : Exception
{
	/// <summary>
	/// Gets the one-based line number of the line at which lexing failed, or -1, if there is no line number in this context.
	/// </summary>
	public int LineNumber { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ParseException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public ParseException(string message) : base(message)
	{
		LineNumber = -1;
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ParseException" /> class.
	/// </summary>
	/// <param name="lineNumber">The one-based line number of the line at which lexing failed, or -1, if there is no line number in this context.</param>
	/// <param name="message">The message that describes the error.</param>
	public ParseException(int lineNumber, string message) : this(message)
	{
		LineNumber = lineNumber;
	}
}