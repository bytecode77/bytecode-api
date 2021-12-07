using BytecodeApi.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BytecodeApi.IO.Cli
{
	/// <summary>
	/// Represents a set of commandline options, parsed from a given commandline.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class ParsedOptionSet
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<ParsedOptionSet>("Arguments: {0}, Options: {1}", Arguments.Count, Options.Count);
		/// <summary>
		/// Gets a <see cref="string" /> collection with arguments that are not associated to any <see cref="Option" />. These are the first arguments before any option parameter, e.g. anything before "-a".
		/// </summary>
		public ReadOnlyCollection<string> Arguments { get; private set; }
		/// <summary>
		/// Gets a collection of <see cref="ParsedOption" /> objects that were parsed from the given commandline.
		/// </summary>
		public ReadOnlyCollection<ParsedOption> Options { get; private set; }
		/// <summary>
		/// Gets the <see cref="ParsedOption" />, identified by an <see cref="Option" /> that matches the <paramref name="argument" /> parameter and throws an exception, if it was not found.
		/// </summary>
		/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />. If <paramref name="argument" /> matches any of the strings in the <see cref="Option.Arguments" /> property, the <see cref="ParsedOption" /> is returned.</param>
		public ParsedOption this[string argument]
		{
			get
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
				Check.KeyNotFoundException(HasOption(argument), "An option that matches the specified argument was not found.");
				return GetOptions(argument).First();
			}
		}
		/// <summary>
		/// Gets a value indicating whether multiple <see cref="ParsedOption" /> objects share the same <see cref="Option" /> object. This can be used to sanitize commandline arguments.
		/// </summary>
		public bool HasDuplicateOptions => Options.Select(option => option.Option).Distinct().Count() != Options.Count;
		/// <summary>
		/// Validates conditions of this <see cref="ParsedOptionSet" /> and invokes a custom handler, if the condition was not met.
		/// </summary>
		public ValidateHelper Validate { get; private set; }
		/// <summary>
		/// Asserts conditions of this <see cref="ParsedOptionSet" /> and throws a <see cref="CliException" />, if the condition was not met.
		/// </summary>
		public AssertHelper Assert { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ParsedOptionSet" /> class.
		/// </summary>
		/// <param name="arguments">A <see cref="string" /> collection with arguments that are not associated to any <see cref="Option" />. These are the first arguments before any option parameter, e.g. anything before "-a".</param>
		/// <param name="options">A collection of <see cref="ParsedOption" /> objects that were parsed from the given commandline.</param>
		public ParsedOptionSet(IEnumerable<string> arguments, IEnumerable<ParsedOption> options)
		{
			Arguments = arguments.ToReadOnlyCollection();
			Options = options.ToReadOnlyCollection();
			Validate = new ValidateHelper(this);
			Assert = new AssertHelper(this);
		}

		/// <summary>
		/// Determines whether a <see cref="ParsedOption" /> with the specified <see cref="Option" /> argument exists in this collection.
		/// </summary>
		/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
		/// <returns>
		/// <see langword="true" />, if the <see cref="ParsedOption" /> with the specified <see cref="Option" /> argument exists;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool HasOption(string argument)
		{
			return GetOptions(argument).Any();
		}
		/// <summary>
		/// Returns an enumerable collection of all <see cref="ParsedOption" />, identified by an <see cref="Option" /> that matches the <paramref name="argument" /> parameter.
		/// </summary>
		/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />. If <paramref name="argument" /> matches any of the strings in the <see cref="Option.Arguments" /> property, the <see cref="ParsedOption" /> is returned.</param>
		/// <returns>
		/// An enumerable collection of all <see cref="ParsedOption" />, identified by an <see cref="Option" /> that matches the <paramref name="argument" /> parameter.
		/// </returns>
		public IEnumerable<ParsedOption> GetOptions(string argument)
		{
			return Options.Where(o => o.Option.Arguments.Contains(argument));
		}
		/// <summary>
		/// Handles all <see cref="ParsedOption" /> objects with the specified argument using a custom handler.
		/// </summary>
		/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
		/// <param name="handler">A custom handler that is invoked with a <see cref="string" />[] containing the values of the <see cref="ParsedOption" />.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public ParsedOptionSet Handle(string argument, Action<string[]> handler)
		{
			Check.ArgumentNull(argument, nameof(argument));
			Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
			Check.ArgumentNull(handler, nameof(handler));

			foreach (ParsedOption option in GetOptions(argument))
			{
				handler(option.Values.ToArray());
			}

			return this;
		}

		/// <summary>
		/// Helper class that validates <see cref="Cli.ParsedOptionSet" /> conditions. If a condition is not met, a custom handler is invoked.
		/// </summary>
		public sealed class ValidateHelper
		{
			private readonly ParsedOptionSet ParsedOptionSet;

			internal ValidateHelper(ParsedOptionSet parsedOptionSet)
			{
				ParsedOptionSet = parsedOptionSet;
			}

			/// <summary>
			/// Validates that the specified argument is present.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="failed">A custom handler that is invoked, if the condition is not met.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet OptionRequired(string argument, Action failed)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
				Check.ArgumentNull(failed, nameof(failed));

				if (!ParsedOptionSet.HasOption(argument)) failed();

				return ParsedOptionSet;
			}
			/// <summary>
			/// Validates that the argument has only one occurrence, or none.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="failed">A custom handler that is invoked, if the condition is not met.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet OptionNotDuplicate(string argument, Action failed)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
				Check.ArgumentNull(failed, nameof(failed));

				if (ParsedOptionSet.GetOptions(argument).Count() > 1) failed();

				return ParsedOptionSet;
			}
			/// <summary>
			/// Validates that the argument has a specific amount of values. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="count">A <see cref="int" /> specifying the amount of values for the <see cref="ParsedOption" />.</param>
			/// <param name="failed">A custom handler that is invoked, if the condition is not met.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet ValueCount(string argument, int count, Action failed)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
				Check.ArgumentNull(failed, nameof(failed));

				foreach (ParsedOption option in ParsedOptionSet.GetOptions(argument))
				{
					if (option.Values.Count != count) failed();
				}

				return ParsedOptionSet;
			}
			/// <summary>
			/// Validates that the argument has a minimum amount of values. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="count">A <see cref="int" /> specifying the minimum amount of values for the <see cref="ParsedOption" />.</param>
			/// <param name="failed">A custom handler that is invoked, if the condition is not met.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet MinimumValueCount(string argument, int count, Action failed)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
				Check.ArgumentNull(failed, nameof(failed));

				foreach (ParsedOption option in ParsedOptionSet.GetOptions(argument))
				{
					if (option.Values.Count < count) failed();
				}

				return ParsedOptionSet;
			}
			/// <summary>
			/// Validates that the argument does not exceed a maximum amount of values. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="count">A <see cref="int" /> specifying the maximum amount of values for the <see cref="ParsedOption" />.</param>
			/// <param name="failed">A custom handler that is invoked, if the condition is not met.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet MaximumValueCount(string argument, int count, Action failed)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
				Check.ArgumentNull(failed, nameof(failed));

				foreach (ParsedOption option in ParsedOptionSet.GetOptions(argument))
				{
					if (option.Values.Count > count) failed();
				}

				return ParsedOptionSet;
			}
			/// <summary>
			/// Performs a custom validation on the values. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="validate">The <see cref="Predicate{T}" /> that determines whether the validation succeeded.</param>
			/// <param name="failed">A custom handler that is invoked, if the condition is not met.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet Custom(string argument, Predicate<string[]> validate, Action failed)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
				Check.ArgumentNull(validate, nameof(validate));
				Check.ArgumentNull(failed, nameof(failed));

				foreach (ParsedOption option in ParsedOptionSet.GetOptions(argument))
				{
					if (!validate(option.Values.ToArray())) failed();
				}

				return ParsedOptionSet;
			}
			/// <summary>
			/// Validates that all values represent a valid <see cref="int" /> value. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="failed">A custom handler that is invoked, if the condition is not met.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet Int32(string argument, Action failed)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
				Check.ArgumentNull(failed, nameof(failed));

				foreach (ParsedOption option in ParsedOptionSet.GetOptions(argument))
				{
					if (!option.Values.All(value => value.ToInt32OrNull() != null)) failed();
				}

				return ParsedOptionSet;
			}
			/// <summary>
			/// Validates that all values represent existing files. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="failed">A custom handler that is invoked, if the condition is not met.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet FileExists(string argument, Action failed)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
				Check.ArgumentNull(failed, nameof(failed));

				foreach (ParsedOption option in ParsedOptionSet.GetOptions(argument))
				{
					if (!option.Values.All(value => File.Exists(value))) failed();
				}

				return ParsedOptionSet;
			}
			/// <summary>
			/// Validates that all values represent filenames with one of several specific extensions. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="extensions">A set of allowed extensions for each filename.</param>
			/// <param name="failed">A custom handler that is invoked, if the condition is not met.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet FileExtension(string argument, string[] extensions, Action failed)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
				Check.ArgumentNull(failed, nameof(failed));

				foreach (ParsedOption option in ParsedOptionSet.GetOptions(argument))
				{
					if (!option.Values.All(value => extensions.Any(extension => extension.TrimStart('.').Equals(Path.GetExtension(value).TrimStart('.'), StringComparison.OrdinalIgnoreCase)))) failed();
				}

				return ParsedOptionSet;
			}
			/// <summary>
			/// Validates that all values represent existing directories. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="failed">A custom handler that is invoked, if the condition is not met.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet DirectoryExists(string argument, Action failed)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));
				Check.ArgumentNull(failed, nameof(failed));

				foreach (ParsedOption option in ParsedOptionSet.GetOptions(argument))
				{
					if (!option.Values.All(value => Directory.Exists(value))) failed();
				}

				return ParsedOptionSet;
			}
		}
		/// <summary>
		/// Helper class that asserts <see cref="Cli.ParsedOptionSet" /> conditions. If a condition is not met, an exception is thrown.
		/// </summary>
		public sealed class AssertHelper
		{
			private readonly ParsedOptionSet ParsedOptionSet;

			internal AssertHelper(ParsedOptionSet parsedOptionSet)
			{
				ParsedOptionSet = parsedOptionSet;
			}

			/// <summary>
			/// Validates that the specified argument is present; otherwise, a <see cref="CliException" /> is thrown.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet OptionRequired(string argument)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));

				return ParsedOptionSet.Validate.OptionRequired(argument, () =>
				{
					throw new CliException(CliValidationSource.OptionRequired, "Option '" + argument + "' is required.");
				});
			}
			/// <summary>
			/// Validates that the argument has only one occurrence, or none; otherwise, a <see cref="CliException" /> is thrown.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet OptionNotDuplicate(string argument)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));

				return ParsedOptionSet.Validate.OptionNotDuplicate(argument, () =>
				{
					throw new CliException(CliValidationSource.OptionNotDuplicate, "Option '" + argument + "' must not have multiple occurrences.");
				});
			}
			/// <summary>
			/// Validates that the argument has a specific amount of values; otherwise, a <see cref="CliException" /> is thrown. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="count">A <see cref="int" /> specifying the amount of values for the <see cref="ParsedOption" />.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet ValueCount(string argument, int count)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));

				return ParsedOptionSet.Validate.ValueCount(argument, count, () =>
				{
					throw new CliException(CliValidationSource.ValueCount, "Option '" + argument + "' must have at least " + count + " values.");
				});
			}
			/// <summary>
			/// Validates that the argument has a minimum amount of values; otherwise, a <see cref="CliException" /> is thrown. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="count">A <see cref="int" /> specifying the minimum amount of values for the <see cref="ParsedOption" />.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet MinimumValueCount(string argument, int count)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));

				return ParsedOptionSet.Validate.MinimumValueCount(argument, count, () =>
				{
					throw new CliException(CliValidationSource.MinimumValueCount, "Option '" + argument + "' must have at least " + count + " values.");
				});
			}
			/// <summary>
			/// Validates that the argument does not exceed a maximum amount of values; otherwise, a <see cref="CliException" /> is thrown. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="count">A <see cref="int" /> specifying the maximum amount of values for the <see cref="ParsedOption" />.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet MaximumValueCount(string argument, int count)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));

				return ParsedOptionSet.Validate.MaximumValueCount(argument, count, () =>
				{
					throw new CliException(CliValidationSource.MaximumValueCount, "Option '" + argument + "' must not have more than " + count + " values.");
				});
			}
			/// <summary>
			/// Performs a custom validation on the values and throws a <see cref="CliException" />, if the validation failed. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="validate">The <see cref="Predicate{T}" /> that determines whether the validation succeeded.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet Custom(string argument, Predicate<string[]> validate)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));

				return ParsedOptionSet.Validate.Custom(argument, validate, () =>
				{
					throw new CliException(CliValidationSource.Custom, "Custom validation for option '" + argument + "' failed.");
				});
			}
			/// <summary>
			/// Validates that all values represent a valid <see cref="int" /> value; otherwise, a <see cref="CliException" /> is thrown. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet Int32(string argument)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));

				return ParsedOptionSet.Validate.Int32(argument, () =>
				{
					throw new CliException(CliValidationSource.Int32, "The value of option '" + argument + "' is not a valid Int32 value.");
				});
			}
			/// <summary>
			/// Validates that all values represent existing files; otherwise, a <see cref="CliException" /> is thrown. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet FileExists(string argument)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));

				return ParsedOptionSet.Validate.FileExists(argument, () =>
				{
					throw new CliException(CliValidationSource.FileExists, "File as specified in option '" + argument + "' was not found.");
				});
			}
			/// <summary>
			/// Validates that all values represent filenames with one of several specific extensions; otherwise, a <see cref="CliException" /> is thrown. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <param name="extensions">A set of allowed extensions for each filename.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet FileExtension(string argument, params string[] extensions)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));

				return ParsedOptionSet.Validate.FileExtension(argument, extensions, () =>
				{
					throw new CliException(CliValidationSource.FileExtension, "File extension as specified in option '" + argument + "' is not allowed.");
				});
			}
			/// <summary>
			/// Validates that all values represent existing directories; otherwise, a <see cref="CliException" /> is thrown. If the <see cref="Option" /> was not found, this validation succeeds.
			/// </summary>
			/// <param name="argument">A <see cref="string" /> that identifies an <see cref="Option" />.</param>
			/// <returns>
			/// A reference to the instance of <see cref="Cli.ParsedOptionSet" /> after the operation has completed.
			/// </returns>
			public ParsedOptionSet DirectoryExists(string argument)
			{
				Check.ArgumentNull(argument, nameof(argument));
				Check.ArgumentEx.StringNotEmpty(argument, nameof(argument));

				return ParsedOptionSet.Validate.DirectoryExists(argument, () =>
				{
					throw new CliException(CliValidationSource.DirectoryExists, "Directory as specified in option '" + argument + "' was not found.");
				});
			}
		}
	}
}