using BytecodeApi.Extensions;
using BytecodeApi.FileFormats.Csv;
using System;
using System.IO;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// CSV file as string
		string csvString = @"
FirstName;LastName;Age
Addison;Smith;45
Cassidy;Johnson;12
Delaney;Williams;31
Fallon;Brown;24
Harlow;Jones;86
";

		// We quicky convert the CSV string to a Stream.
		// It's easier to demonstrate than using a file on the disk. But typically, this would be a file.
		using (MemoryStream memoryStream = new MemoryStream(csvString.ToUTF8Bytes()))
		{
			string delimiter = CsvDelimiterDetector.CreateDefault().FromStream(memoryStream);

			// ";" was detected
		}

		// CsvDelimiterDetector.CreateDefault() has default settings.
		// To customize, create a custom detector like this:
		CsvDelimiterDetector customDelimiter = new CsvDelimiterDetector
		(
			// Test these delimiters:
			new[] { ";", ",", "|", "\t" },
			// The tested file must have at least 10 lines, otherwise the delimiter is considered indeterminable.
			10,
			// A maximum of 100 lines is tested before the detector exits. The file may contain less, but no less than 10.
			100
		);

		// Typically, the delimiter detector is implemented in CsvFile and CsvIterator per default.
		// Unless the delimiter is clearly defined, it is recommended to to pass null to the "delimiter" parameter.
		CsvFile csv = CsvFile.FromString(csvString); // no "delimiter" parameter was passed

		// When no delimiter is explicitly passed to the CsvFile.From*() method, the CsvFile.DelimiterDetector is used.
		// This static field is initialized to CsvDelimiterDetector.CreateDefault() and can be changed globally.

		// When omitting the "delimiter" parameter from the CsvIterator.From*() method, the CsvIterator.DelimiterDetector is used.
		// CsvFile.DelimiterDetector and CsvIterator.DelimiterDetector can be defined separately.
		using (MemoryStream memoryStream = new MemoryStream(csvString.ToUTF8Bytes()))
		{
			foreach (CsvRow row in CsvIterator.FromStream(memoryStream))
			{
				// ...
			}
		}
	}
}