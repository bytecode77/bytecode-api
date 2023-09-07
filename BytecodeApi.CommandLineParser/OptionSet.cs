using BytecodeApi.Extensions;
using BytecodeApi.IO;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace BytecodeApi.CommandLineParser;

/// <summary>
/// Represents a set of commandline options that can be used to parse a given commandline.
/// </summary>
[DebuggerDisplay($"{nameof(OptionSet)}: OptionPrefix = {{OptionPrefix}}, OptionAlternativePrefix = {{OptionAlternativePrefix}}, Options: {{Count}}")]
public sealed class OptionSet : ICollection<Option>
{
	private readonly List<Option> Options;
	/// <summary>
	/// Gets the prefix <see cref="string" /> for commandline options. This is typically a dash or a slash character.
	/// </summary>
	public string OptionPrefix { get; private init; }
	/// <summary>
	/// Gets the prefix <see cref="string" /> for the alternative commandline options. This is typically a double dash.
	/// </summary>
	public string OptionAlternativePrefix { get; private init; }
	/// <summary>
	/// Gets a value indicating whether the commandline parsing ignores character casing.
	/// </summary>
	public bool OptionPrefixIgnoreCase { get; private init; }
	/// <summary>
	/// Gets the number of elements contained in the <see cref="OptionSet" />.
	/// </summary>
	public int Count => Options.Count;
	/// <summary>
	/// Gets a value indicating whether the <see cref="OptionSet" /> is read-only.
	/// </summary>
	public bool IsReadOnly => false;

	/// <summary>
	/// Initializes a new instance of the <see cref="OptionSet" /> class with the specified prefixes.
	/// <para>Example: "-" and "--", or "/" and "--"</para>
	/// </summary>
	/// <param name="optionPrefix">The prefix <see cref="string" /> for commandline options. This is typically a dash or a slash character.</param>
	/// <param name="optionAlternativePrefix">The prefix <see cref="string" /> for the alternative commandline options. This is typically a double dash.</param>
	public OptionSet(string optionPrefix, string optionAlternativePrefix) : this(optionPrefix, optionAlternativePrefix, false)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="OptionSet" /> class with the specified prefixes.
	/// <para>Example: "-" and "--", or "/" and "--"</para>
	/// </summary>
	/// <param name="optionPrefix">The prefix <see cref="string" /> for commandline options. This is typically a dash or a slash character.</param>
	/// <param name="optionAlternativePrefix">The prefix <see cref="string" /> for the alternative commandline options. This is typically a double dash.</param>
	/// <param name="optionPrefixIgnoreCase"><see langword="true" /> to ignore character casing during commandline parsing.</param>
	public OptionSet(string optionPrefix, string optionAlternativePrefix, bool optionPrefixIgnoreCase)
	{
		Check.ArgumentNull(optionPrefix);
		Check.ArgumentEx.StringNotEmpty(optionPrefix);
		Check.Argument(optionPrefix.All(c => c.IsSymbol() || c.IsPunctuation()), nameof(optionPrefix), "String must contain only symbols or punctuation characters.");
		Check.ArgumentNull(optionAlternativePrefix);
		Check.ArgumentEx.StringNotEmpty(optionAlternativePrefix);
		Check.Argument(optionAlternativePrefix.All(c => c.IsSymbol() || c.IsPunctuation()), nameof(optionAlternativePrefix), "String must contain only symbols or punctuation characters.");
		Check.Argument(optionPrefix != optionAlternativePrefix, nameof(optionAlternativePrefix), "Option prefix and alternative option prefix must not be identical.");

		OptionPrefix = optionPrefix;
		OptionAlternativePrefix = optionAlternativePrefix;
		OptionPrefixIgnoreCase = optionPrefixIgnoreCase;
		Options = new();
	}

	/// <summary>
	/// Creates a new <see cref="Option" /> and adds it to this <see cref="OptionSet" />.
	/// </summary>
	/// <param name="arguments">A collection of strings that defines what arguments apply to the new <see cref="Option" />.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public OptionSet Add(params string[] arguments)
	{
		return Add(new Option(arguments));
	}
	/// <summary>
	/// Creates a new <see cref="Option" /> and adds it to this <see cref="OptionSet" />.
	/// </summary>
	/// <param name="arguments">A collection of strings that defines what arguments apply to the new <see cref="Option" />.</param>
	/// <param name="alternatives">A collection of strings that defines what arguments apply to the new <see cref="Option" /> alternatively.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public OptionSet Add(string[] arguments, string[]? alternatives)
	{
		return Add(new Option(arguments, alternatives));
	}
	/// <summary>
	/// Adds the specified <see cref="Option" /> to this <see cref="OptionSet" />.
	/// </summary>
	/// <param name="option">The <see cref="Option" /> to be added.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public OptionSet Add(Option option)
	{
		Check.ArgumentNull(option);
		Check.Argument(Options.SelectMany(o => o.Arguments).Intersect(option.Arguments).None(), nameof(option), "An option with this argument was already added.");
		Check.Argument(Options.SelectMany(o => o.Alternatives).Intersect(option.Alternatives).None(), nameof(option), "An option with this argument alternative was already added.");

		Options.Add(option);
		return this;
	}
	/// <summary>
	/// Parses a commandline arguments and returns a new <see cref="ParsedOptionSet" /> with the result.
	/// </summary>
	/// <param name="args">An array of <see cref="string" /> objects with the commandline arguments, excluding the executable filename.</param>
	/// <returns>
	/// A new <see cref="ParsedOptionSet" /> with the parsed commandline.
	/// </returns>
	public ParsedOptionSet Parse(string[] args)
	{
		Check.ArgumentNull(args);
		Check.ArgumentEx.ArrayValuesNotNull(args);

		List<string> parsedArguments = new();
		List<ParsedOption> parsedOptions = new();
		Option? currentOption = null;
		List<string> currentOptionValues = new();

		for (int i = 0; i < args.Length; i++)
		{
			StringComparison comparison = OptionPrefixIgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			Option? option = Options.FirstOrDefault(o =>
				o.Arguments.Any(a => args[i].Equals(OptionPrefix + a, comparison)) ||
				o.Alternatives.Any(a => args[i].Equals(OptionAlternativePrefix + a, comparison)));

			if (option == null)
			{
				if (currentOption == null)
				{
					parsedArguments.Add(args[i]);
				}
				else
				{
					currentOptionValues.Add(args[i]);
				}
			}
			else
			{
				AddToCurrentOption();
				currentOption = option;
			}
		}

		AddToCurrentOption();
		return new(parsedArguments, parsedOptions);

		void AddToCurrentOption()
		{
			if (currentOption != null)
			{
				parsedOptions.Add(new(currentOption, currentOptionValues.ToArray()));
				currentOptionValues.Clear();
			}
		}
	}
	/// <summary>
	/// Parses a commandline <see cref="string" /> and returns a new <see cref="ParsedOptionSet" /> with the result.
	/// </summary>
	/// <param name="commandLine">The commandline <see cref="string" />, where each argument is separated with spaces, excluding the executable filename. Arguments containing spaces should be quoted.</param>
	/// <returns>
	/// A new <see cref="ParsedOptionSet" /> with the parsed commandline.
	/// </returns>
	[SupportedOSPlatform("windows")]
	public ParsedOptionSet Parse(string commandLine)
	{
		Check.ArgumentNull(commandLine);

		return Parse(CommandLine.GetArguments(commandLine));
	}

	void ICollection<Option>.Add(Option item)
	{
		Add(item);
	}
	/// <summary>
	/// Removes the first occurrence of a specific <see cref="Option" /> from the <see cref="OptionSet" />.
	/// </summary>
	/// <param name="item">The <see cref="Option" /> to remove from the <see cref="OptionSet" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="item" /> is successfully removed;
	/// otherwise, <see langword="false" />.
	/// This method also returns <see langword="false" />, if <paramref name="item" /> was not found in the <see cref="OptionSet" />.</returns>
	public bool Remove(Option item)
	{
		return Options.Remove(item);
	}
	/// <summary>
	/// Removes all elements from the <see cref="OptionSet" />.
	/// </summary>
	public void Clear()
	{
		Options.Clear();
	}
	/// <summary>
	/// Determines whether an element is in the <see cref="OptionSet" />.
	/// </summary>
	/// <param name="item">The <see cref="Option" /> to locate in the <see cref="OptionSet" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="item" /> is found in the <see cref="OptionSet" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Contains(Option item)
	{
		return Options.Contains(item);
	}
	void ICollection<Option>.CopyTo(Option[] array, int arrayIndex)
	{
		Check.ArgumentNull(array);
		Check.IndexOutOfRange(arrayIndex, array.Length - Count + 1);

		Options.CopyTo(array, arrayIndex);
	}
	/// <summary>
	/// Returns an enumerator that iterates through the <see cref="OptionSet" />.
	/// </summary>
	/// <returns>
	/// An enumerator that can be used to iterate through the <see cref="OptionSet" />.
	/// </returns>
	public IEnumerator<Option> GetEnumerator()
	{
		return Options.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return Options.GetEnumerator();
	}
}