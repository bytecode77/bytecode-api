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
		IniFile ini = new IniFile();

		// Set global properties (before any section declaration)
		ini.GlobalProperties.Properties.Add("global_prop1", "hello");
		ini.GlobalProperties.Properties.Add("global_prop2", "world");

		// Create a section
		IniSection section1 = new IniSection("section1");
		section1.Properties.Add("key1", "value1");
		section1.Properties.Add("key1", "value2");
		ini.Sections.Add(section1);

		// Create another section
		IniSection section2 = new IniSection("section2");
		section2.Properties.Add("key1", "value1");
		section2.Properties.Add("key1", "value2");
		ini.Sections.Add(section2);

		using (MemoryStream memoryStream = new MemoryStream())
		{
			// Custom formatting options (optional)
			IniFileFormattingOptions formattingOptions = new IniFileFormattingOptions
			{
				// property = value (equal sign)
				PropertyDelimiter = IniPropertyDelimiter.EqualSign,

				// property{SPACE}=value
				UseDelimiterSpaceBefore = true,
				// property={SPACE}value
				UseDelimiterSpaceAfter = true,

				// Line breaks between sections look nice
				UseNewLineBetweenSections = true
			};

			ini.Save(memoryStream, Encoding.Default, formattingOptions);

			// Save it to a stream so we can display in console
			string iniString = memoryStream.ToArray().ToUTF8String();
			Console.WriteLine(iniString);
		}

		Console.ReadKey();
	}
}