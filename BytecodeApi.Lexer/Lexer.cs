using BytecodeApi.Extensions;
using BytecodeApi.Text;

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
		Check.ArgumentNull(pattern);

		Grammar.Add(new(true, default, pattern, null));
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
		return Match(type, pattern, null);
	}
	/// <summary>
	/// Specifies that tokens of a specific type match the given pattern.
	/// </summary>
	/// <param name="type">The type of token that matches the given pattern.</param>
	/// <param name="pattern">A <see cref="string" /> with a regular expression to match the token.</param>
	/// <param name="postProcessValue">A conversion method that converts the parsed token - e.g., to remove quotes of a quoted string literal.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public Lexer<TTokenType> Match(TTokenType type, string pattern, Func<string, string>? postProcessValue)
	{
		Check.ArgumentNull(pattern);

		Grammar.Add(new(false, type, pattern, postProcessValue));
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
					string value = match.Match.Value;

					if (match.Grammar.PostProcessValue != null)
					{
						value = match.Grammar.PostProcessValue(value);
					}

					tokens.Add(new(line, match.Grammar.Type, value));
				}

				position += match.Match.Value.Length;
				line += match.Match.Value.Count(c => c == '\n');
			}
		}

		return tokens;
	}
}