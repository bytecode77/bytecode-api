using Microsoft.VisualBasic.FileIO;
using System.Reflection;
using System.Text;

namespace BytecodeApi.CsvParser;

internal static class CsvHelper
{
	public static TextFieldParser CreateTextFieldParser(Stream stream, string delimiter, Encoding? encoding, bool leaveOpen, out FieldInfo lineNumberField)
	{
		TextFieldParser parser = new(stream, encoding ?? Encoding.UTF8, encoding == null, leaveOpen)
		{
			TextFieldType = FieldType.Delimited,
			TrimWhiteSpace = true,
			Delimiters = new[] { delimiter }
		};

		// The only way to retrieve the file line number:
		// * Using a counter does not count empty lines
		// * TextFieldParser.LineNumber points to the next line and returns -1 for the last line
		lineNumberField = typeof(TextFieldParser).GetField("m_LineNumber", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw Throw.InvalidOperation("Field 'm_LineNumber' not found.");

		return parser;
	}
	public static IEnumerable<CsvRow> EnumerateTextFieldParser(TextFieldParser parser, bool ignoreEmptyLines)
	{
		while (!parser.EndOfData)
		{
			CsvRow? csvRow = null;

			try
			{
				string[] row = parser.ReadFields() ?? new string[0];

				if (!ignoreEmptyLines || !row.All(column => column == ""))
				{
					csvRow = new(row);
				}
			}
			catch (MalformedLineException)
			{
				csvRow = new() { ErrorLine = parser.ErrorLine };
			}

			if (csvRow != null)
			{
				yield return csvRow;
			}
		}
	}
	public static void AutoDetectDelimiter(CsvDelimiterDetector delimiterDetector, Stream stream, Encoding? encoding, [NotNull] ref string? delimiter)
	{
		delimiter ??= delimiterDetector.FromStream(stream, encoding) ?? throw Throw.Format("The CSV delimiter could not be detected.");
	}
}