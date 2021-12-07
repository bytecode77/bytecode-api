using BytecodeApi.Extensions;
using BytecodeApi.FileFormats.Csv;
using System;
using System.IO;
using System.Text;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Create a CSV object, or use a CsvFile instance retrieved from the CsvFile.From*() method.
		CsvFile csv = new CsvFile();

		// Populate with data
		csv.Delimiter = ";";
		csv.Headers = new[] { "FirstName", "LastName", "Age" };

		csv.Rows.Add(new CsvRow("Addison", "Smith", "45"));
		csv.Rows.Add(new CsvRow("Cassidy", "Johnson", "12"));
		csv.Rows.Add(new CsvRow("Delaney", "Williams", "31"));
		csv.Rows.Add(new CsvRow("Fallon", "Brown", "24"));
		csv.Rows.Add(new CsvRow("Harlow", "Jones", "86"));

		// Write CSV to a file or stream.
		using (MemoryStream memoryStream = new MemoryStream())
		{
			csv.Save(memoryStream, false, Encoding.Default);

			// In this example, we want to print it to the console, so we need it as a string
			string str = memoryStream.ToArray().ToUTF8String();
			Console.WriteLine(str);
		}

		Console.ReadKey();
	}
}