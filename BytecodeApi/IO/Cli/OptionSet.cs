using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace BytecodeApi.IO.Cli
{
	/// <summary>
	/// Represents a set of commandline options that can be used to parse a given commandline.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class OptionSet : ICollection<Option>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<OptionSet>("OptionPrefix = {0}, OptionAlternativePrefix = {1}, Options: {2}", new QuotedString(OptionPrefix), new QuotedString(OptionAlternativePrefix), Count);
		private readonly List<Option> Options;
		/// <summary>
		/// Gets the prefix <see cref="string" /> for commandline options. This is typically a dash or a slash character.
		/// </summary>
		public string OptionPrefix { get; private set; }
		/// <summary>
		/// Gets the prefix <see cref="string" /> for the alternative commandline options. This is typically a double dash.
		/// </summary>
		public string OptionAlternativePrefix { get; private set; }
		/// <summary>
		/// Gets a value indicating whether the commandline parsing ignores character casing.
		/// </summary>
		public bool OptionPrefixIgnoreCase { get; private set; }
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
			Check.ArgumentNull(optionPrefix, nameof(optionPrefix));
			Check.ArgumentEx.StringNotEmpty(optionPrefix, nameof(optionPrefix));
			Check.Argument(optionPrefix.All(c => c.IsSymbol() || c.IsPunctuation()), nameof(optionPrefix), "String must contain only symbols or punctuation characters.");
			Check.ArgumentNull(optionAlternativePrefix, nameof(optionAlternativePrefix));
			Check.ArgumentEx.StringNotEmpty(optionAlternativePrefix, nameof(optionAlternativePrefix));
			Check.Argument(optionAlternativePrefix.All(c => c.IsSymbol() || c.IsPunctuation()), nameof(optionAlternativePrefix), "String must contain only symbols or punctuation characters.");
			Check.Argument(optionPrefix != optionAlternativePrefix, nameof(optionAlternativePrefix), "Option prefix and alternative option prefix must not be identical.");

			OptionPrefix = optionPrefix;
			OptionAlternativePrefix = optionAlternativePrefix;
			OptionPrefixIgnoreCase = optionPrefixIgnoreCase;
			Options = new List<Option>();
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
		public OptionSet Add(string[] arguments, string[] alternatives)
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
			Check.ArgumentNull(option, nameof(option));
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
			Check.ArgumentNull(args, nameof(args));
			Check.ArgumentEx.ArrayValuesNotNull(args, nameof(args));

			List<string> parsedArguments = new List<string>();
			List<ParsedOption> parsedOptions = new List<ParsedOption>();
			Option currentOption = null;
			List<string> currentOptionValues = new List<string>();

			for (int i = 0; i < args.Length; i++)
			{
				SpecialStringComparisons comparison = OptionPrefixIgnoreCase ? SpecialStringComparisons.IgnoreCase : SpecialStringComparisons.None;
				Option option = Options.FirstOrDefault(o =>
					o.Arguments.Any(a => args[i].Equals(OptionPrefix + a, comparison)) ||
					o.Alternatives.Any(a => args[i].Equals(OptionAlternativePrefix + a, comparison))
				);

				if (option == null)
				{
					if (currentOption == null) parsedArguments.Add(args[i]);
					else currentOptionValues.Add(args[i]);
				}
				else
				{
					AddToCurrentOption();
					currentOption = option;
				}
			}

			AddToCurrentOption();
			return new ParsedOptionSet(parsedArguments, parsedOptions);

			void AddToCurrentOption()
			{
				if (currentOption != null)
				{
					parsedOptions.Add(new ParsedOption(currentOption, currentOptionValues.ToArray()));
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
		public ParsedOptionSet Parse(string commandLine)
		{
			Check.ArgumentNull(commandLine, nameof(commandLine));

			return Parse(ParseCommandLine(commandLine));
		}
		/// <summary>
		/// Parses commandline arguments from a <see cref="string" /> and returns the equivalent <see cref="string" />[] with split commandline arguments.
		/// </summary>
		/// <param name="commandLine">A <see cref="string" /> specifying a commandline.</param>
		/// <returns>
		/// The equivalent <see cref="string" />[] with split commandline arguments from the given commandline.
		/// </returns>
		public static string[] ParseCommandLine(string commandLine)
		{
			Check.ArgumentNull(commandLine, nameof(commandLine));

			IntPtr argumentsPtr = Native.CommandLineToArgvW(commandLine, out int count);
			if (argumentsPtr == IntPtr.Zero) throw Throw.Win32();

			try
			{
				return Create.Array(count, i => Marshal.PtrToStringUni(Marshal.ReadIntPtr(argumentsPtr, i * IntPtr.Size)));
			}
			finally
			{
				Native.LocalFree(argumentsPtr);
			}
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
			Check.ArgumentNull(array, nameof(array));
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
}