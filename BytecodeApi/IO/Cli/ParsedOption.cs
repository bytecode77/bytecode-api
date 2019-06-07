using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace BytecodeApi.IO.Cli
{
	/// <summary>
	/// Represents a commandline option, parsed from a given commandline. An <see cref="Cli.Option" /> reference identifies the option.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class ParsedOption
	{
		private string DebuggerDisplay => CSharp.DebuggerDisplay<ParsedOption>("Option = {0}, Values = {1}", new QuotedString(Option.Arguments.First()), Values.Count == 1 ? (object)new QuotedString(Values.First()) : Values.ToArray());
		/// <summary>
		/// Gets a reference to the associated <see cref="Cli.Option" /> this <see cref="ParsedOption" /> is associated with.
		/// </summary>
		public Option Option { get; private set; }
		/// <summary>
		/// Gets a <see cref="string" /> collection with the parsed values.
		/// </summary>
		public ReadOnlyCollection<string> Values { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ParsedOption" /> class with a reference to the associated <see cref="Cli.Option" /> and a <see cref="string" />[] with the parsed values.
		/// </summary>
		/// <param name="option">A reference to the associated <see cref="Cli.Option" /> this <see cref="ParsedOption" /> is associated with.</param>
		/// <param name="values">A <see cref="string" />[] with the parsed values.</param>
		public ParsedOption(Option option, string[] values)
		{
			Check.ArgumentNull(option, nameof(option));
			Check.ArgumentNull(values, nameof(values));
			Check.ArgumentEx.ArrayValuesNotNull(values, nameof(values));
			Check.ArgumentEx.ArrayValuesNotStringEmpty(values, nameof(values));

			Option = option;
			Values = values.ToReadOnlyCollection();
		}
	}
}