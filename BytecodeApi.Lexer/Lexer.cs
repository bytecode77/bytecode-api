using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System.Text.RegularExpressions;

namespace BytecodeApi.Lexer;

/// <summary>
/// Class that parses text for specific tokens. This is typically used to tokenize source code.
/// </summary>
public static class Lexer
{
	/// <summary>
	/// Creates a new <see cref="Lexer" />.
	/// </summary>
	/// <typeparam name="TTokenType">The <see langword="enum" /> type of the token.</typeparam>
	/// <returns>
	/// The newly created <see cref="Lexer{TTokenType}" />.
	/// </returns>
	public static Lexer<TTokenType> Create<TTokenType>() where TTokenType : struct, IConvertible
	{
		return new();
	}
}

/// <summary>
/// Class that parses text for specific tokens. This is typically used to tokenize source code.
/// </summary>
/// <typeparam name="TTokenType">The <see langword="enum" /> type of the token.</typeparam>
public sealed class Lexer<TTokenType> where TTokenType : struct, IConvertible
{
	private List<Grammar<TTokenType>> Grammar { get; init; }

	internal Lexer()
	{
		Grammar = new();
	}

	/// <summary>
	/// Specifies that tokens that match the given pattern are ignored.
	/// </summary>
	/// <param name="pattern">A <see cref="string" /> with a regular expression to match the token.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public Lexer<TTokenType> Ignore(string pattern)
	{
		return Ignore(pattern, RegexOptions.None);
	}
	/// <summary>
	/// Specifies that tokens that match the given pattern are ignored.
	/// </summary>
	/// <param name="pattern">A <see cref="string" /> with a regular expression to match the token.</param>
	/// <param name="regexOptions">The <see cref="RegexOptions" /> to be used to match the pattern.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public Lexer<TTokenType> Ignore(string pattern, RegexOptions regexOptions)
	{
		Check.ArgumentNull(pattern);

		Grammar.Add(new(true, default, pattern, regexOptions, null));
		return this;
	}
	/// <summary>
	/// Specifies that tokens of a specific type match the given pattern.
	/// </summary>
	/// <param name="type">The type of token that matches the given pattern.</param>
	/// <param name="pattern">A <see cref="string" /> with a regular expression to match the token.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public Lexer<TTokenType> Match(TTokenType type, string pattern)
	{
		return Match(type, pattern, RegexOptions.None);
	}
	/// <summary>
	/// Specifies that tokens of a specific type match the given pattern.
	/// </summary>
	/// <param name="type">The type of token that matches the given pattern.</param>
	/// <param name="pattern">A <see cref="string" /> with a regular expression to match the token.</param>
	/// <param name="regexOptions">The <see cref="RegexOptions" /> to be used to match the pattern.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public Lexer<TTokenType> Match(TTokenType type, string pattern, RegexOptions regexOptions)
	{
		return Match(type, pattern, regexOptions, null);
	}
	/// <summary>
	/// Specifies that tokens of a specific type match the given pattern.
	/// </summary>
	/// <param name="type">The type of token that matches the given pattern.</param>
	/// <param name="pattern">A <see cref="string" /> with a regular expression to match the token.</param>
	/// <param name="getValue">A custom conversion method that converts the parsed <see cref="System.Text.RegularExpressions.Match" /> - e.g., to remove quotes of a quoted string literal.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public Lexer<TTokenType> Match(TTokenType type, string pattern, Func<Match, string>? getValue)
	{
		return Match(type, pattern, RegexOptions.None, getValue);
	}
	/// <summary>
	/// Specifies that tokens of a specific type match the given pattern.
	/// </summary>
	/// <param name="type">The type of token that matches the given pattern.</param>
	/// <param name="pattern">A <see cref="string" /> with a regular expression to match the token.</param>
	/// <param name="regexOptions">The <see cref="RegexOptions" /> to be used to match the pattern.</param>
	/// <param name="getValue">A custom conversion method that converts the parsed <see cref="System.Text.RegularExpressions.Match" /> - e.g., to remove quotes of a quoted string literal.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public Lexer<TTokenType> Match(TTokenType type, string pattern, RegexOptions regexOptions, Func<Match, string>? getValue)
	{
		Check.ArgumentNull(pattern);

		Grammar.Add(new(false, type, pattern, regexOptions, getValue));
		return this;
	}

	/// <summary>
	/// Parses <paramref name="code" /> and returns a collection of tokens that represent <paramref name="code" />.
	/// </summary>
	/// <param name="code">A <see cref="string" /> with the code to tokenize.</param>
	/// <returns>
	/// A collection of tokens that represent <paramref name="code" />.
	/// </returns>
	public TokenCollection<TTokenType> Parse(string code)
	{
		Check.ArgumentNull(code);

		TokenCollection<TTokenType> tokens = new();

		for (int position = 0, line = 1; position < code.Length;)
		{
			var match = Grammar
				.Select(grammar => new
				{
					Grammar = grammar,
					Match = grammar.Match(code, position)
				})
				.FirstOrDefault(itm => itm.Match.Success);

			if (match == null)
			{
				throw new ParseException(line, $"Parse error at line {line} near {Wording.TrimText(code[position..].SubstringUntil('\n').Trim(), 20)}");
			}
			else
			{
				if (!match.Grammar.Ignore)
				{
					string value = match.Grammar.GetValue != null
						? match.Grammar.GetValue(match.Match)
						: match.Match.Value;

					tokens.Add(new(line, match.Grammar.Type, value));
				}

				position += match.Match.Value.Length;
				line += match.Match.Value.Count(c => c == '\n');
			}
		}

		return tokens;
	}
}