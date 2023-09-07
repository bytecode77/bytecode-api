using BytecodeApi.Extensions;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BytecodeApi.CommandLineParser;

/// <summary>
/// Represents a commandline option, parsed from a given commandline. An <see cref="CommandLineParser.Option" /> reference identifies the option.
/// </summary>
[DebuggerDisplay($"{nameof(ParsedOption)}: Option: {{DebuggerDisplayOption}}, Values: {{DebuggerDisplayValues}}")]
public sealed class ParsedOption
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private string DebuggerDisplayOption => Option.Arguments.First();
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private string DebuggerDisplayValues => Values.Count == 1 ? Values.First() : Values.AsString(", ");
	/// <summary>
	/// Gets a reference to the associated <see cref="CommandLineParser.Option" /> this <see cref="ParsedOption" /> is associated with.
	/// </summary>
	public Option Option { get; private init; }
	/// <summary>
	/// Gets a <see cref="string" /> collection with the parsed values.
	/// </summary>
	public ReadOnlyCollection<string> Values { get; private init; }

	internal ParsedOption(Option option, string[] values)
	{
		Check.ArgumentNull(option);
		Check.ArgumentNull(values);
		Check.ArgumentEx.ArrayValuesNotNull(values);
		Check.ArgumentEx.ArrayValuesNotStringEmpty(values);

		Option = option;
		Values = values.ToReadOnlyCollection();
	}
}