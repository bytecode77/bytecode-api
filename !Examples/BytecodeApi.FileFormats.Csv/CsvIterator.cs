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
			foreach (CsvRow row in CsvIterator.FromStream(memoryStream, true, ";")) // hasHeaderRow = true: The firt row is ignored
			{
				// Now we can iterate over the CSV file rather than loading it into a CsvFile object.
				// This way, CSV files of any size can be processed.
			}
		}

		using (MemoryStream memoryStream = new MemoryStream(csvString.ToUTF8Bytes()))
		{
			// Omit the "delimiter" parameter to automatically detect the delimiter
			foreach (CsvRow row in CsvIterator.FromStream(memoryStream, true))
			{
				// ...
			}
		}

		// Use the CsvFile class for CSV files that are small enough to be stored in memory completely.
	}
}