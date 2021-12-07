using BytecodeApi.Extensions;
using BytecodeApi.FileFormats.Ini;
using System;
using System.IO;
using System.Text;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		string iniString = @"
global_prop1 = hello
global_prop2 = world

[section1]
key1 = value1
key2 = value2

[section2]
key3 = value3

; A section that occurs multiple times
[section2]
key4 = value 4
";
		IniFile ini;

		// We quicky convert the INI string to a Stream.
		// It's easier to demonstrate than using a file on the disk. But typically, this would be a file.
		using (MemoryStream memoryStream = new MemoryStream(iniString.ToUTF8Bytes()))
		{
			// Custom parsing options (optional)
			// Where the INI file has no clear specification, IniFileParsingOptions can be used to specify details about parsing

			IniFileParsingOptions parsingOptions = new IniFileParsingOptions
			{
				// true add to errors to IniFile.ErrorLines; false to throw an exception
				IgnoreErrors = true,

				// Trim section names like [   my_section    ]
				TrimSectionNames = true,
				// Trim property and value names (Set to false, if you need whitespaces at the beginning or end of property names/values)
				TrimPropertyNames = true,
				TrimPropertyValues = true,

				// Allow global properties (without a section declaration)
				AllowGlobalProperties = true,

				// Allow section names that contain closing brackets like [my]section]
				AllowSectionNameClosingBracket = true,
				// Empty lines should typically be acceptable
				AllowEmptyLines = true,

				// Typically properties are like "key = value" (equal sign), but some may use a colon instead
				PropertyDelimiter = IniPropertyDelimiter.EqualSign,

				// Allow semicolon comments (;), but no number sign comments (#)
				AllowSemicolonComments = true,
				AllowNumberSignComments = false,

				// Merge properties of sections where the name is equal
				DuplicateSectionNameBehavior = IniDuplicateSectionNameBehavior.Merge,
				// Consider the name of two sections equal, regardless of case
				DuplicateSectionNameIgnoreCase = true,

				// Duplicate property names are an error
				DuplicatePropertyNameBehavior = IniDuplicatePropertyNameBehavior.Abort,
				// Consider the name of two properties equal, regardless of case
				DuplicatePropertyNameIgnoreCase = true
			};

			ini = IniFile.FromStream(memoryStream, Encoding.Default, parsingOptions);
		}

		// "global properties" are properties without a section. They are on top of *any* section declaration
		Console.WriteLine("Global properties without a section:");
		foreach (IniProperty globalProperty in ini.GlobalProperties.Properties)
		{
			Console.WriteLine("Name: " + globalProperty.Name + ", Value: " + globalProperty.Value);
		}
		Console.WriteLine();

		// Loop through all sections and their properties
		foreach (IniSection section in ini.Sections)
		{
			Console.WriteLine("Section: " + section.Name);
			foreach (IniProperty property in section.Properties)
			{
				Console.WriteLine("Name: " + property.Name + ", Value: " + property.Value);
			}
			Console.WriteLine();
		}

		// Typically, reading an INI file means going through specific sections and properties, not just looping over it
		if (ini.HasSection("section1", true)) // true = ignore case
		{
			IniSection section = ini.Sections["section1", true]; // true = ignore case

			// true = ignore case
			// "nothing" = the default value, if the property does not exist
			IniProperty property = section.Properties["key1", true, "nothing"];

			string theValue = property.Value;
		}

		Console.ReadKey();
	}
}