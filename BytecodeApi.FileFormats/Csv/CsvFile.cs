using BytecodeApi.Extensions;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BytecodeApi.FileFormats.Csv
{
	//FEATURE: Auto-detect delimiter in Enumerate[...] methods
	/// <summary>
	/// Represents a flat file database, typically a CSV file with lines separated by a separator.
	/// </summary>
	public class CsvFile
	{
		private const int DefaultMinimumRowsToTest = 2;
		private const int DefaultMaximumRowsToTest = 10;
		private static readonly string[] DefaultDelimitersToTest = new[] { ",", ";", "\t", "|" };
		/// <summary>
		/// Gets or sets a <see cref="string" />[] that contains the headers of this flat file database or <see langword="null" />, if the hasHeaderRow parameter was set to <see langword="false" />.
		/// </summary>
		public string[] Headers { get; set; }
		/// <summary>
		/// Gets or sets the delimiter for this <see cref="CsvFile" />. The initial value is set to the specified or detected delimiter when loading a flat file database, or <see langword="null" />, if the constructor was used to create an empty <see cref="CsvFile" />. This property is used by the <see cref="Save(Stream, bool, bool, Encoding, bool)" /> method.
		/// </summary>
		public string Delimiter { get; set; }
		/// <summary>
		/// Gets a collection of <see cref="CsvRow" /> objects that represents the content of this flat file database.
		/// </summary>
		public CsvRowCollection Rows { get; private set; }
		/// <summary>
		/// Gets a <see cref="bool" /> value indicating whether this <see cref="CsvFile" /> contains rows that could not be parsed. This property does not change once the file is loaded.
		/// </summary>
		public bool HasErrors { get; private set; }
		/// <summary>
		/// Gets a <see cref="bool" /> value indicating whether all rows in this <see cref="CsvFile" /> have the same amount of columns, excluding the <see cref="Headers" /> property and error lines. If zero rows or only error rows were loaded, this property returns <see langword="true" />. This property does not change once the file is loaded.
		/// </summary>
		public bool IsColumnCountConsistent { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CsvFile" /> class.
		/// </summary>
		public CsvFile()
		{
			Rows = new CsvRowCollection();
			IsColumnCountConsistent = true;
		}
		/// <summary>
		/// Creates a <see cref="CsvFile" /> object from the specified file.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a flat file database, such as a CSV file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <returns>
		/// The <see cref="CsvFile" /> this method creates.
		/// </returns>
		public static CsvFile FromFile(string path, string delimiter, bool hasHeaderRow)
		{
			return FromFile(path, delimiter, hasHeaderRow, false);
		}
		/// <summary>
		/// Creates a <see cref="CsvFile" /> object from the specified file.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a flat file database, such as a CSV file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <returns>
		/// The <see cref="CsvFile" /> this method creates.
		/// </returns>
		public static CsvFile FromFile(string path, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines)
		{
			return FromFile(path, delimiter, hasHeaderRow, ignoreEmptyLines, null);
		}
		/// <summary>
		/// Creates a <see cref="CsvFile" /> object from the specified file.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a flat file database, such as a CSV file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <returns>
		/// The <see cref="CsvFile" /> this method creates.
		/// </returns>
		public static CsvFile FromFile(string path, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines, Encoding encoding)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.FileNotFound(path);
			Check.ArgumentNull(delimiter, nameof(delimiter));
			Check.ArgumentEx.StringNotEmpty(delimiter, nameof(delimiter));

			using (FileStream file = File.OpenRead(path))
			{
				return FromStream(file, delimiter, hasHeaderRow, ignoreEmptyLines, encoding);
			}
		}
		/// <summary>
		/// Creates a <see cref="CsvFile" /> object from the specified <see cref="byte" />[] that represents a flat file database.
		/// </summary>
		/// <param name="file">The <see cref="byte" />[] that represents a flat file database to read from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <returns>
		/// The <see cref="CsvFile" /> this method creates.
		/// </returns>
		public static CsvFile FromBinary(byte[] file, string delimiter, bool hasHeaderRow)
		{
			return FromBinary(file, delimiter, hasHeaderRow, false);
		}
		/// <summary>
		/// Creates a <see cref="CsvFile" /> object from the specified <see cref="byte" />[] that represents a flat file database.
		/// </summary>
		/// <param name="file">The <see cref="byte" />[] that represents a flat file database to read from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <returns>
		/// The <see cref="CsvFile" /> this method creates.
		/// </returns>
		public static CsvFile FromBinary(byte[] file, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines)
		{
			return FromBinary(file, delimiter, hasHeaderRow, ignoreEmptyLines, null);
		}
		/// <summary>
		/// Creates a <see cref="CsvFile" /> object from the specified <see cref="byte" />[] that represents a flat file database.
		/// </summary>
		/// <param name="file">The <see cref="byte" />[] that represents a flat file database to read from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <returns>
		/// The <see cref="CsvFile" /> this method creates.
		/// </returns>
		public static CsvFile FromBinary(byte[] file, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines, Encoding encoding)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.ArgumentNull(delimiter, nameof(delimiter));
			Check.ArgumentEx.StringNotEmpty(delimiter, nameof(delimiter));

			using (MemoryStream memoryStream = new MemoryStream(file))
			{
				return FromStream(memoryStream, delimiter, hasHeaderRow, ignoreEmptyLines, encoding);
			}
		}
		/// <summary>
		/// Creates a <see cref="CsvFile" /> object from the specified <see cref="Stream" />.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the flat file database from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <returns>
		/// The <see cref="CsvFile" /> this method creates.
		/// </returns>
		public static CsvFile FromStream(Stream stream, string delimiter, bool hasHeaderRow)
		{
			return FromStream(stream, delimiter, hasHeaderRow, false);
		}
		/// <summary>
		/// Creates a <see cref="CsvFile" /> object from the specified <see cref="Stream" />.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the flat file database from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <returns>
		/// The <see cref="CsvFile" /> this method creates.
		/// </returns>
		public static CsvFile FromStream(Stream stream, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines)
		{
			return FromStream(stream, delimiter, hasHeaderRow, ignoreEmptyLines, null);
		}
		/// <summary>
		/// Creates a <see cref="CsvFile" /> object from the specified <see cref="Stream" />.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the flat file database from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <returns>
		/// The <see cref="CsvFile" /> this method creates.
		/// </returns>
		public static CsvFile FromStream(Stream stream, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines, Encoding encoding)
		{
			return FromStream(stream, delimiter, hasHeaderRow, ignoreEmptyLines, encoding, false);
		}
		/// <summary>
		/// Creates a <see cref="CsvFile" /> object from the specified <see cref="Stream" />.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the flat file database from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
		/// <returns>
		/// The <see cref="CsvFile" /> this method creates.
		/// </returns>
		public static CsvFile FromStream(Stream stream, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines, Encoding encoding, bool leaveOpen)
		{
			Check.ArgumentNull(stream, nameof(stream));
			Check.ArgumentNull(delimiter, nameof(delimiter));
			Check.ArgumentEx.StringNotEmpty(delimiter, nameof(delimiter));

			if (delimiter == null)
			{
				delimiter = DetectDelimiter(stream, encoding);
				if (delimiter == null) throw Throw.FileFormat("The delimiter could not be detected.");
			}

			CsvFile csv = new CsvFile
			{
				Delimiter = delimiter
			};

			using (TextFieldParser parser = CreateTextFieldParser(stream, delimiter, encoding, leaveOpen))
			{
				long lineNumber = 1;
				if (hasHeaderRow)
				{
					try
					{
						csv.Headers = parser.ReadFields();
					}
					catch (MalformedLineException) { }

					lineNumber++;
				}

				int columnCount = -1;

				foreach (CsvRow row in EnumerateTextFieldParser(parser, ignoreEmptyLines))
				{
					row.LineNumber = lineNumber++;
					csv.HasErrors |= row.ErrorLine != null;

					if (row.ErrorLine == null && csv.IsColumnCountConsistent)
					{
						if (columnCount == -1) columnCount = row.Count;
						else if (columnCount != row.Count) csv.IsColumnCountConsistent = false;
					}

					csv.Rows.Add(row);
				}
			}

			return csv;
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified file. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a flat file database, such as a CSV file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> EnumerateFile(string path, string delimiter, bool hasHeaderRow)
		{
			return EnumerateFile(path, delimiter, hasHeaderRow, false);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified file. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a flat file database, such as a CSV file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> EnumerateFile(string path, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines)
		{
			return EnumerateFile(path, delimiter, hasHeaderRow, ignoreEmptyLines, null);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified file. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a flat file database, such as a CSV file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> EnumerateFile(string path, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines, Encoding encoding)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.FileNotFound(path);
			Check.ArgumentNull(delimiter, nameof(delimiter));
			Check.ArgumentEx.StringNotEmpty(delimiter, nameof(delimiter));

			using (FileStream file = File.OpenRead(path))
			{
				foreach (CsvRow row in EnumerateStream(file, delimiter, hasHeaderRow, ignoreEmptyLines, encoding))
				{
					yield return row;
				}
			}
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="byte" />[] that represents a flat file database. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="file">The <see cref="byte" />[] that represents a flat file database to read from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> EnumerateBinary(byte[] file, string delimiter, bool hasHeaderRow)
		{
			return EnumerateBinary(file, delimiter, hasHeaderRow, false);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="byte" />[] that represents a flat file database. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="file">The <see cref="byte" />[] that represents a flat file database to read from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> EnumerateBinary(byte[] file, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines)
		{
			return EnumerateBinary(file, delimiter, hasHeaderRow, ignoreEmptyLines, null);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="byte" />[] that represents a flat file database. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="file">The <see cref="byte" />[] that represents a flat file database to read from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> EnumerateBinary(byte[] file, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines, Encoding encoding)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.ArgumentNull(delimiter, nameof(delimiter));
			Check.ArgumentEx.StringNotEmpty(delimiter, nameof(delimiter));

			using (MemoryStream memoryStream = new MemoryStream(file))
			{
				foreach (CsvRow row in EnumerateStream(memoryStream, delimiter, hasHeaderRow, ignoreEmptyLines, encoding))
				{
					yield return row;
				}
			}
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="Stream" />. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the flat file database from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> EnumerateStream(Stream stream, string delimiter, bool hasHeaderRow)
		{
			return EnumerateStream(stream, delimiter, hasHeaderRow, false);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="Stream" />. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the flat file database from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> EnumerateStream(Stream stream, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines)
		{
			return EnumerateStream(stream, delimiter, hasHeaderRow, ignoreEmptyLines, null);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="Stream" />. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the flat file database from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> EnumerateStream(Stream stream, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines, Encoding encoding)
		{
			return EnumerateStream(stream, delimiter, hasHeaderRow, ignoreEmptyLines, encoding, false);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="Stream" />. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the flat file database from.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during flat file parsing.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> EnumerateStream(Stream stream, string delimiter, bool hasHeaderRow, bool ignoreEmptyLines, Encoding encoding, bool leaveOpen)
		{
			Check.ArgumentNull(stream, nameof(stream));
			Check.ArgumentNull(delimiter, nameof(delimiter));
			Check.ArgumentEx.StringNotEmpty(delimiter, nameof(delimiter));

			using (TextFieldParser parser = CreateTextFieldParser(stream, delimiter, encoding, leaveOpen))
			{
				long lineNumber = 1;
				if (hasHeaderRow)
				{
					parser.ReadLine();
					lineNumber++;
				}

				foreach (CsvRow row in EnumerateTextFieldParser(parser, ignoreEmptyLines))
				{
					row.LineNumber = lineNumber++;
					yield return row;
				}
			}
		}

		/// <summary>
		/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
		/// For detection, a specified number of rows is tested for various delimiters. By default, a minimum of 2 rows is required and up to 10 rows are tested. The tested delimiters are ",", ";", "\t" and "|". If all rows have the same column count and the column count is greater than 1, the tested delimiter is considered correct.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a flat file database, such as a CSV file.</param>
		/// <returns>
		/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
		/// </returns>
		public static string DetectDelimiter(string path)
		{
			return DetectDelimiter(path, null);
		}
		/// <summary>
		/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
		/// For detection, a specified number of rows is tested for various delimiters. By default, a minimum of 2 rows is required and up to 10 rows are tested. The tested delimiters are ",", ";", "\t" and "|". If all rows have the same column count and the column count is greater than 1, the tested delimiter is considered correct.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a flat file database, such as a CSV file.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <returns>
		/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
		/// </returns>
		public static string DetectDelimiter(string path, Encoding encoding)
		{
			return DetectDelimiter(path, encoding, DefaultMinimumRowsToTest, DefaultMaximumRowsToTest, DefaultDelimitersToTest);
		}
		/// <summary>
		/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
		/// For detection, a specified number of rows is tested for various delimiters. By default, a minimum of 2 rows is required and up to 10 rows are tested. The tested delimiters are ",", ";", "\t" and "|". If all rows have the same column count and the column count is greater than 1, the tested delimiter is considered correct.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a flat file database, such as a CSV file.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <param name="minimumRowsToTest">A <see cref="int" /> value specifying the minimum number of rows that the CSV file must have. The default value is 2.</param>
		/// <param name="maximumRowsToTest">A <see cref="int" /> value specifying the maximum number of rows that will be tested. If the file has less rows, the complete file is tested. The default value is 10.</param>
		/// <param name="delimitersToTest">A <see cref="string" />[] specifying the delimiters to test for in the specified order. By default, ",", ";", "\t" and "|" are tested.</param>
		/// <returns>
		/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
		/// </returns>
		public static string DetectDelimiter(string path, Encoding encoding, int minimumRowsToTest, int maximumRowsToTest, params string[] delimitersToTest)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.FileNotFound(path);
			Check.ArgumentOutOfRangeEx.Greater0(minimumRowsToTest, nameof(minimumRowsToTest));
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(maximumRowsToTest, minimumRowsToTest, nameof(maximumRowsToTest), nameof(minimumRowsToTest));
			Check.ArgumentNull(delimitersToTest, nameof(delimitersToTest));
			Check.ArgumentEx.ArrayElementsRequired(delimitersToTest, nameof(delimitersToTest));
			Check.ArgumentEx.ArrayValuesNotNull(delimitersToTest, nameof(delimitersToTest));

			using (FileStream file = File.OpenRead(path))
			{
				return DetectDelimiter(file, encoding, minimumRowsToTest, maximumRowsToTest, delimitersToTest);
			}
		}
		/// <summary>
		/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
		/// For detection, a specified number of rows is tested for various delimiters. By default, a minimum of 2 rows is required and up to 10 rows are tested. The tested delimiters are ",", ";", "\t" and "|". If all rows have the same column count and the column count is greater than 1, the tested delimiter is considered correct.
		/// </summary>
		/// <param name="file">The <see cref="byte" />[] that represents a flat file database to read from.</param>
		/// <returns>
		/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
		/// </returns>
		public static string DetectDelimiter(byte[] file)
		{
			return DetectDelimiter(file, null);
		}
		/// <summary>
		/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
		/// For detection, a specified number of rows is tested for various delimiters. By default, a minimum of 2 rows is required and up to 10 rows are tested. The tested delimiters are ",", ";", "\t" and "|". If all rows have the same column count and the column count is greater than 1, the tested delimiter is considered correct.
		/// </summary>
		/// <param name="file">The <see cref="byte" />[] that represents a flat file database to read from.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <returns>
		/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
		/// </returns>
		public static string DetectDelimiter(byte[] file, Encoding encoding)
		{
			return DetectDelimiter(file, encoding, DefaultMinimumRowsToTest, DefaultMaximumRowsToTest, DefaultDelimitersToTest);
		}
		/// <summary>
		/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
		/// For detection, a specified number of rows is tested for various delimiters. By default, a minimum of 2 rows is required and up to 10 rows are tested. The tested delimiters are ",", ";", "\t" and "|". If all rows have the same column count and the column count is greater than 1, the tested delimiter is considered correct.
		/// </summary>
		/// <param name="file">The <see cref="byte" />[] that represents a flat file database to read from.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <param name="minimumRowsToTest">A <see cref="int" /> value specifying the minimum number of rows that the CSV file must have. The default value is 2.</param>
		/// <param name="maximumRowsToTest">A <see cref="int" /> value specifying the maximum number of rows that will be tested. If the file has less rows, the complete file is tested. The default value is 10.</param>
		/// <param name="delimitersToTest">A <see cref="string" />[] specifying the delimiters to test for in the specified order. By default, ",", ";", "\t" and "|" are tested.</param>
		/// <returns>
		/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
		/// </returns>
		public static string DetectDelimiter(byte[] file, Encoding encoding, int minimumRowsToTest, int maximumRowsToTest, params string[] delimitersToTest)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.ArgumentOutOfRangeEx.Greater0(minimumRowsToTest, nameof(minimumRowsToTest));
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(maximumRowsToTest, minimumRowsToTest, nameof(maximumRowsToTest), nameof(minimumRowsToTest));
			Check.ArgumentNull(delimitersToTest, nameof(delimitersToTest));
			Check.ArgumentEx.ArrayElementsRequired(delimitersToTest, nameof(delimitersToTest));
			Check.ArgumentEx.ArrayValuesNotNull(delimitersToTest, nameof(delimitersToTest));

			using (MemoryStream memoryStream = new MemoryStream(file))
			{
				return DetectDelimiter(memoryStream, encoding, minimumRowsToTest, maximumRowsToTest, delimitersToTest);
			}
		}
		/// <summary>
		/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
		/// For detection, a specified number of rows is tested for various delimiters. By default, a minimum of 2 rows is required and up to 10 rows are tested. The tested delimiters are ",", ";", "\t" and "|". If all rows have the same column count and the column count is greater than 1, the tested delimiter is considered correct.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the flat file database from. The <see cref="Stream" /> is read multiple times and the position is reset before this method returns.</param>
		/// <returns>
		/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
		/// </returns>
		public static string DetectDelimiter(Stream stream)
		{
			return DetectDelimiter(stream, null);
		}
		/// <summary>
		/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
		/// For detection, a specified number of rows is tested for various delimiters. By default, a minimum of 2 rows is required and up to 10 rows are tested. The tested delimiters are ",", ";", "\t" and "|". If all rows have the same column count and the column count is greater than 1, the tested delimiter is considered correct.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the flat file database from. The <see cref="Stream" /> is read multiple times and the position is reset before this method returns.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <returns>
		/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
		/// </returns>
		public static string DetectDelimiter(Stream stream, Encoding encoding)
		{
			return DetectDelimiter(stream, encoding, DefaultMinimumRowsToTest, DefaultMaximumRowsToTest, DefaultDelimitersToTest);
		}
		/// <summary>
		/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
		/// For detection, a specified number of rows is tested for various delimiters. By default, a minimum of 2 rows is required and up to 10 rows are tested. The tested delimiters are ",", ";", "\t" and "|". If all rows have the same column count and the column count is greater than 1, the tested delimiter is considered correct.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the flat file database from. The <see cref="Stream" /> is read multiple times and the position is reset before this method returns.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <param name="minimumRowsToTest">A <see cref="int" /> value specifying the minimum number of rows that the CSV file must have. The default value is 2.</param>
		/// <param name="maximumRowsToTest">A <see cref="int" /> value specifying the maximum number of rows that will be tested. If the file has less rows, the complete file is tested. The default value is 10.</param>
		/// <param name="delimitersToTest">A <see cref="string" />[] specifying the delimiters to test for in the specified order. By default, ",", ";", "\t" and "|" are tested.</param>
		/// <returns>
		/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
		/// </returns>
		public static string DetectDelimiter(Stream stream, Encoding encoding, int minimumRowsToTest, int maximumRowsToTest, params string[] delimitersToTest)
		{
			Check.ArgumentNull(stream, nameof(stream));
			Check.ArgumentOutOfRangeEx.Greater0(minimumRowsToTest, nameof(minimumRowsToTest));
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(maximumRowsToTest, minimumRowsToTest, nameof(maximumRowsToTest), nameof(minimumRowsToTest));
			Check.ArgumentNull(delimitersToTest, nameof(delimitersToTest));
			Check.ArgumentEx.ArrayElementsRequired(delimitersToTest, nameof(delimitersToTest));
			Check.ArgumentEx.ArrayValuesNotNull(delimitersToTest, nameof(delimitersToTest));

			long position = stream.Position;

			try
			{
				foreach (string delimiterTest in delimitersToTest)
				{
					stream.Seek(0);
					int rowCount = 0;
					int cellCount = -1;

					foreach (CsvRow row in EnumerateStream(stream, delimiterTest, false, true, encoding, true).Take(maximumRowsToTest))
					{
						if (cellCount == -1) cellCount = row.Count;
						else if (cellCount != row.Count) break;
						rowCount++;
					}

					if (rowCount >= minimumRowsToTest && cellCount > 1)
					{
						return delimiterTest;
					}
				}
			}
			finally
			{
				stream.Seek(position);
			}

			return null;
		}

		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a file. This method streams an <see cref="IEnumerable{T}" /> into the file and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the file.</param>
		public static void SaveRows(string path, IEnumerable<CsvRow> rows, string delimiter)
		{
			SaveRows(path, rows, delimiter, false);
		}
		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a file. This method streams an <see cref="IEnumerable{T}" /> into the file and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the file.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		public static void SaveRows(string path, IEnumerable<CsvRow> rows, string delimiter, bool alwaysQuote)
		{
			SaveRows(path, rows, delimiter, alwaysQuote, null);
		}
		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a file. This method streams an <see cref="IEnumerable{T}" /> into the file and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the file.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		/// <param name="encoding">The encoding to use to write to the file.</param>
		public static void SaveRows(string path, IEnumerable<CsvRow> rows, string delimiter, bool alwaysQuote, Encoding encoding)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.ArgumentNull(rows, nameof(rows));
			Check.ArgumentNull(delimiter, nameof(delimiter));
			Check.ArgumentEx.StringNotEmpty(delimiter, nameof(delimiter));

			using (FileStream stream = File.Create(path))
			{
				SaveRows(stream, rows, delimiter, alwaysQuote, encoding, false);
			}
		}
		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a <see cref="Stream" />. This method streams an <see cref="IEnumerable{T}" /> into the <see cref="Stream" /> and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the <see cref="Stream" />.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the <see cref="Stream" />.</param>
		public static void SaveRows(Stream stream, IEnumerable<CsvRow> rows, string delimiter)
		{
			SaveRows(stream, rows, delimiter, false);
		}
		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a <see cref="Stream" />. This method streams an <see cref="IEnumerable{T}" /> into the <see cref="Stream" /> and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the <see cref="Stream" />.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the <see cref="Stream" />.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		public static void SaveRows(Stream stream, IEnumerable<CsvRow> rows, string delimiter, bool alwaysQuote)
		{
			SaveRows(stream, rows, delimiter, alwaysQuote, null);
		}
		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a <see cref="Stream" />. This method streams an <see cref="IEnumerable{T}" /> into the <see cref="Stream" /> and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the <see cref="Stream" />.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the <see cref="Stream" />.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		/// <param name="encoding">The encoding to use to write to the <see cref="Stream" />.</param>
		public static void SaveRows(Stream stream, IEnumerable<CsvRow> rows, string delimiter, bool alwaysQuote, Encoding encoding)
		{
			SaveRows(stream, rows, delimiter, alwaysQuote, encoding, false);
		}
		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a <see cref="Stream" />. This method streams an <see cref="IEnumerable{T}" /> into the <see cref="Stream" /> and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the <see cref="Stream" />.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the <see cref="Stream" />.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		/// <param name="encoding">The encoding to use to write to the <see cref="Stream" />.</param>
		/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
		public static void SaveRows(Stream stream, IEnumerable<CsvRow> rows, string delimiter, bool alwaysQuote, Encoding encoding, bool leaveOpen)
		{
			Check.ArgumentNull(stream, nameof(stream));
			Check.ArgumentNull(rows, nameof(rows));
			Check.ArgumentNull(delimiter, nameof(delimiter));
			Check.ArgumentEx.StringNotEmpty(delimiter, nameof(delimiter));

			using (StreamWriter streamWriter = new StreamWriter(stream, encoding ?? Encoding.UTF8, 4096, leaveOpen))
			{
				foreach (CsvRow row in rows)
				{
					if (row.ErrorLine != null)
					{
						streamWriter.WriteLine(row.ErrorLine);
					}
					else
					{
						for (int i = 0; i < row.Count; i++)
						{
							string cell = row[i].Value;

							if (alwaysQuote || cell?.Contains(delimiter) == true || cell?.Contains('\n') == true)
							{
								streamWriter.Write('"');
								if (cell != null) streamWriter.Write(cell.Replace("\"", "\"\""));
								streamWriter.Write('"');
							}
							else
							{
								if (cell != null) streamWriter.Write(cell);
							}

							if (i < row.Count - 1) streamWriter.Write(delimiter);
						}
						streamWriter.WriteLine();
					}
				}
			}
		}

		/// <summary>
		/// Checks whether the column count of all rows is equal to <paramref name="columnCount" />, excluding the <see cref="Headers" /> property and error rows.
		/// </summary>
		/// <param name="columnCount">A <see cref="int" /> value specifying the expected column count for all rows.</param>
		/// <returns>
		/// <see langword="true" />, if the column count of all rows is equal to <paramref name="columnCount" />, or it no rows were imported;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool CheckColumnCount(int columnCount)
		{
			if (IsColumnCountConsistent) return Rows.Count == 0 || Rows.First().Count == columnCount;
			else return Rows.All(row => row.Count == columnCount);
		}
		/// <summary>
		/// Writes the contents of this flat file database to a file. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which this flat file database is written to.</param>
		public void Save(string path)
		{
			Save(path, false);
		}
		/// <summary>
		/// Writes the contents of this flat file database to a file. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which this flat file database is written to.</param>
		/// <param name="excludeErrorRows"><see langword="true" /> to include parsed error lines; otherwise, <see langword="false" />.</param>
		public void Save(string path, bool excludeErrorRows)
		{
			Save(path, excludeErrorRows, false);
		}
		/// <summary>
		/// Writes the contents of this flat file database to a file. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which this flat file database is written to.</param>
		/// <param name="excludeErrorRows"><see langword="true" /> to include parsed error lines; otherwise, <see langword="false" />.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		public void Save(string path, bool excludeErrorRows, bool alwaysQuote)
		{
			Save(path, excludeErrorRows, alwaysQuote, null);
		}
		/// <summary>
		/// Writes the contents of this flat file database to a file. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which this flat file database is written to.</param>
		/// <param name="excludeErrorRows"><see langword="true" /> to include parsed error lines; otherwise, <see langword="false" />.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		/// <param name="encoding">The encoding to use to write to the file.</param>
		public void Save(string path, bool excludeErrorRows, bool alwaysQuote, Encoding encoding)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.ArgumentNull(Delimiter, nameof(Delimiter));
			Check.ArgumentEx.StringNotEmpty(Delimiter, nameof(Delimiter));

			using (FileStream stream = File.Create(path))
			{
				Save(stream, excludeErrorRows, alwaysQuote, encoding, false);
			}
		}
		/// <summary>
		/// Writes the contents of this flat file database to a <see cref="Stream" />. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which this flat file database is written to.</param>
		public void Save(Stream stream)
		{
			Save(stream, false);
		}
		/// <summary>
		/// Writes the contents of this flat file database to a <see cref="Stream" />. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which this flat file database is written to.</param>
		/// <param name="excludeErrorRows"><see langword="true" /> to include parsed error lines; otherwise, <see langword="false" />.</param>
		public void Save(Stream stream, bool excludeErrorRows)
		{
			Save(stream, excludeErrorRows, false);
		}
		/// <summary>
		/// Writes the contents of this flat file database to a <see cref="Stream" />. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which this flat file database is written to.</param>
		/// <param name="excludeErrorRows"><see langword="true" /> to include parsed error lines; otherwise, <see langword="false" />.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		public void Save(Stream stream, bool excludeErrorRows, bool alwaysQuote)
		{
			Save(stream, excludeErrorRows, alwaysQuote, null);
		}
		/// <summary>
		/// Writes the contents of this flat file database to a <see cref="Stream" />. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which this flat file database is written to.</param>
		/// <param name="excludeErrorRows"><see langword="true" /> to include parsed error lines; otherwise, <see langword="false" />.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		/// <param name="encoding">The encoding to use to write to the file.</param>
		public void Save(Stream stream, bool excludeErrorRows, bool alwaysQuote, Encoding encoding)
		{
			Save(stream, excludeErrorRows, alwaysQuote, encoding, false);
		}
		/// <summary>
		/// Writes the contents of this flat file database to a <see cref="Stream" />. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which this flat file database is written to.</param>
		/// <param name="excludeErrorRows"><see langword="true" /> to include parsed error lines; otherwise, <see langword="false" />.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		/// <param name="encoding">The encoding to use to write to the file.</param>
		/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
		public void Save(Stream stream, bool excludeErrorRows, bool alwaysQuote, Encoding encoding, bool leaveOpen)
		{
			Check.ArgumentNull(stream, nameof(stream));
			Check.ArgumentNull(Delimiter, nameof(Delimiter));
			Check.ArgumentEx.StringNotEmpty(Delimiter, nameof(Delimiter));

			if (Headers != null) SaveRows(stream, Singleton.Array(new CsvRow(Headers)), Delimiter, alwaysQuote, encoding, true);
			SaveRows(stream, Rows, Delimiter, alwaysQuote, encoding, leaveOpen);
		}

		private static TextFieldParser CreateTextFieldParser(Stream stream, string delimiter, Encoding encoding, bool leaveOpen)
		{
			return new TextFieldParser(stream, encoding ?? Encoding.UTF8, encoding == null, leaveOpen)
			{
				TextFieldType = FieldType.Delimited,
				TrimWhiteSpace = true,
				Delimiters = Singleton.Array(delimiter)
			};
		}
		private static IEnumerable<CsvRow> EnumerateTextFieldParser(TextFieldParser parser, bool ignoreEmptyLines)
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
	}
}