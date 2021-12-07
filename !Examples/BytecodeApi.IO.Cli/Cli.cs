using BytecodeApi.IO.Cli;
using System;
using System.Collections.ObjectModel;

public static class Program
{
	[STAThread]
	public static void Main(string[] args)
	{
		// Define what options are available:
		OptionSet optionSet = new OptionSet("-", "--")  // "-" is the prefix and "--" is the prefix for "alternative" options
			.Add("command1")                            // Add option "-command1"
			.Add(new[] { "h" }, new[] { "help" });      // "-h" or "--help" ("--h" or "-help" are invalid)

		//        Option -^   Alternative -^

		// Typical parsing of commandline:
		ParsedOptionSet parsed1 = optionSet.Parse(args);
		// or all arguments in one string, which is then split into a string[] internally
		ParsedOptionSet parsed2 = optionSet.Parse(Environment.CommandLine);

		// Examples:
		ParsedOptionSet parsed3 = optionSet.Parse("-h");                                // Option with prefix
		ParsedOptionSet parsed4 = optionSet.Parse("--help");                            // Alternative option with alternative prefix
		ParsedOptionSet parsed5 = optionSet.Parse("-command1 param1 param2 param3");    // Option with parameters

		// Parsing with validators:
		// There are two methods for validation:
		//   - Custom handlers for validation failures
		//   - Throwing of an exception

		ParsedOptionSet parsed6 = optionSet
			.Parse("-foo -command1 param1 param2")
			// Validator that throws a CliException:
			.Assert.OptionRequired("command1")
			// Custom validator:
			.Validate.MaximumValueCount("command1", 2, () =>
			{
				Console.WriteLine("-command1 may only have two parameters!");
			})
			.Validate.FileExists("command1", () =>
			{
				Console.WriteLine("All parameters of -command1 must point to existing files!");
			});
		//	etc...

		// "Arguments" are what is not associated with any option
		// In this example, "-foo", because it's not specified in our OptionSet
		ReadOnlyCollection<string> arguments = parsed6.Arguments;

		// Handle all known options:
		parsed6
			.Handle("command1", parameters =>
			{
				// Invoked, if "-command1" was specified
				Console.WriteLine("-command1 executed");
				foreach (string p in parameters) Console.WriteLine(p);
			})
			.Handle("h", parameters =>
			{
				Console.WriteLine("Help");
			});
	}
}