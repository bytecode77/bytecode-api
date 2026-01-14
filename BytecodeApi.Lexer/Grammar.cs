using System.Text.RegularExpressions;

namespace BytecodeApi.Lexer;

internal sealed class Grammar<TTokenType> where TTokenType : struct, IConvertible
{
	private readonly Regex Regex;
	public bool Ignore { get; }
	public TTokenType Type { get; }
	public Func<Match, string>? GetValue { get; }

	public Grammar(bool ignore, TTokenType type, string pattern, RegexOptions regexOptions, Func<Match, string>? getValue)
	{
		Regex = new(@"\G(" + pattern + ")", RegexOptions.Compiled | regexOptions);
		Ignore = ignore;
		Type = type;
		GetValue = getValue;
	}

	public Match Match(string code, int position)
	{
		return Regex.Match(code, position);
	}
}