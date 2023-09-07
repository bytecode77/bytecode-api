namespace BytecodeApi.Lexer;

/// <summary>
/// Represents a token that was parsed using a lexer.
/// </summary>
/// <typeparam name="TTokenType">The <see langword="enum" /> type of the token.</typeparam>
public sealed class Token<TTokenType> where TTokenType : struct, IConvertible
{
	/// <summary>
	/// Gets the one-based line number of this token.
	/// </summary>
	public int LineNumber { get; private init; }
	/// <summary>
	/// Gets the type of this token.
	/// </summary>
	public TTokenType Type { get; private init; }
	/// <summary>
	/// Gets a <see cref="string" /> with the value of this token.
	/// </summary>
	public string Value { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Token{TTokenType}" /> class.
	/// </summary>
	/// <param name="lineNumber">The one-based line number of the token.</param>
	/// <param name="type">The type of the token.</param>
	/// <param name="value">A <see cref="string" /> with the value of the token.</param>
	public Token(int lineNumber, TTokenType type, string value)
	{
		LineNumber = lineNumber;
		Type = type;
		Value = value;
	}

	/// <summary>
	/// Checks, if this token is of a given type.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <returns>
	/// <see langword="true" />, if this token is of the given type;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Is(TTokenType type)
	{
		return Type.Equals(type);
	}
	/// <summary>
	/// Checks, if this token is of any of the given types.
	/// </summary>
	/// <param name="types">A collection of types to check this token against.</param>
	/// <returns>
	/// <see langword="true" />, if this token is of any of the given types;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Is(params TTokenType[] types)
	{
		return types.Any(t => Type.Equals(t));
	}
	/// <summary>
	/// Checks, if this token is of a given type and matches a given value.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <returns>
	/// <see langword="true" />, if this token is of the given type and matches the given value;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Is(TTokenType type, string value)
	{
		return Type.Equals(type) && Value == value;
	}

	/// <summary>
	/// Returns the value of this <see cref="Token{TTokenType}" />.
	/// </summary>
	/// <returns>
	/// The value of this <see cref="Token{TTokenType}" />.
	/// </returns>
	public override string ToString()
	{
		return Value;
	}
}