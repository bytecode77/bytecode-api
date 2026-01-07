namespace BytecodeApi.Lexer;

/// <summary>
/// Specifies the types of brackets that is used in various parsing and search methods.
/// </summary>
[Flags]
public enum BracketType
{
	/// <summary>
	/// Brackets of any kind are ignored and treated as normal tokens.
	/// </summary>
	None = 0,
	/// <summary>
	/// Parentheses are included.
	/// </summary>
	Parenthesis = 1,
	/// <summary>
	/// Brackets are included.
	/// </summary>
	Bracket = 2,
	/// <summary>
	/// Curly braces are included.
	/// </summary>
	Brace = 4,
	/// <summary>
	/// Parentheses, brackets and curly braces are included.
	/// </summary>
	All = Parenthesis | Bracket | Brace
}