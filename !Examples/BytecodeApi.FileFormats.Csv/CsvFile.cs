using BytecodeApi.FileFormats.Csv;
using System;

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

		// Get CSV from a string that contains CSV data
		CsvFile csv = CsvFile.FromString(csvString, true, ";");

		// or
		// CsvFile csv = CsvFile.FromFile(@"C:\path\to\data.csv", true, ";");
		// CsvFile csv = CsvFile.FromBinary(byte_array_containing_csv_file, true, ";");
		// CsvFile csv = CsvFile.FromStream(fileStream, true, ";")

		string[] headers = csv.Headers; // FirstName, LastName, Age
		CsvRow row = csv.Rows[0]; // Addison, Smith, 45

		// true, if CSV parsing failed in any row
		bool errors = csv.HasErrors;

		// true, if all rows contain the same amount of columns
		bool columnsCorrect = csv.IsColumnCountConsistent;

		// Check, if CSV has 3 columns in every row
		bool columnsAsExpected = csv.CheckColumnCount(3);

		// Get column index of "LastName", case insensitive
		// In case, columns are reordered
		int lastNameIndex = csv.GetColumnIndex("LastName", true);

		// Get column content by name rather than by index
		string lastName = csv.Rows[0][lastNameIndex].Value;

		// Omit the "delimiter" parameter to automatically detect the delimiter
		CsvFile csvAutoDetectDelimiter = CsvFile.FromString(csvString, true);
	}
}