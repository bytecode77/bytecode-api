using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BytecodeApi.FileFormats.Csv
{
	internal static class CsvHelper
	{
		public static TextFieldParser CreateTextFieldParser(Stream stream, string delimiter, Encoding encoding, bool leaveOpen)
		{
			return new TextFieldParser(stream, encoding ?? Encoding.UTF8, encoding == null, leaveOpen)
			{
				TextFieldType = FieldType.Delimited,
				TrimWhiteSpace = true,
				Delimiters = new[] { delimiter }
			};
		}
		public static IEnumerable<CsvRow> EnumerateTextFieldParser(TextFieldParser parser, bool ignoreEmptyLines)
		{
			while (!parser.EndOfData)
			{
				CsvRow csvRow = null;

				try
				{
					string[] row = parser.ReadFields();
					if (!ignoreEmptyLines || !row.All(column => column == "")) csvRow = new CsvRow(row);
				}
				catch (MalformedLineException)
				{
					csvRow = new CsvRow { ErrorLine = parser.ErrorLine };
				}

				if (csvRow != null) yield return csvRow;
			}
		}
		public static void AutoDetectDelimiter(CsvDelimiterDetector delimiterDetector, Stream stream, Encoding encoding, ref string delimiter)
		{
			if (delimiter == null)
			{
				delimiter = delimiterDetector.FromStream(stream, encoding);
				if (delimiter == null) throw Throw.FileFormat("The CSV delimiter could not be detected.");
			}
		}
	}
}