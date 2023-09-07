using System.Text.RegularExpressions;

namespace BytecodeApi.Lexer;

internal sealed class Grammar<TTokenType> where TTokenType : struct, IConvertible
{
	private readonly Regex Regex;
	public bool Ignore { get; private init; }
	public TTokenType Type { get; private init; }
	public Func<string, string>? PostProcessValue { get; private init; }

	public Grammar(bool ignore, TTokenType type, string pattern, Func<string, string>? postProcessValue)
	{
		Regex = new(@"\G(" + pattern + ")", RegexOptions.Compiled);
		Ignore = ignore;
		Type = type;
		PostProcessValue = postProcessValue;
	}

	public Match Match(string code, int position)
	{
		return Regex.Match(code, position);
	}
}