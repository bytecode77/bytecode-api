using BytecodeApi.Extensions;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;
using System.Text;

namespace BytecodeApi.CsvParser;

/// <summary>
/// Represents a database read from a CSV files with lines separated by a delimiter.
/// </summary>
public class CsvFile
{
	/// <summary>
	/// Gets or sets the delimiter detector to be used when reading a CSV file and the delimiter parameter is <see langword="null" />.
	/// <para>This property is global for the <see cref="CsvFile" /> class.</para>
	/// <para>Default values are:</para>
	/// <para><see cref="CsvDelimiterDetector.DelimitersToTest" /> = ",", ";", "\t", "|"</para>
	/// <para><see cref="CsvDelimiterDetector.MinRowsToTest" /> = 2</para>
	/// <para><see cref="CsvDelimiterDetector.MaxRowsToTest" /> = 10</para>
	/// </summary>
	public static CsvDelimiterDetector DelimiterDetector { get; set; }

	/// <summary>
	/// Gets or sets a <see cref="string" />[] that contains the headers of this CSV file or <see langword="null" />, if the hasHeaderRow parameter was set to <see langword="false" />.
	/// </summary>
	public string[]? Headers { get; set; }
	/// <summary>
	/// Gets or sets the delimiter for this CSV file. The initial value is set to the specified or detected delimiter when loading the CSV file, or <see langword="null" />, if the constructor was used to create an empty <see cref="CsvFile" />. This property is used by the <see cref="Save(Stream, bool, Encoding, bool)" /> method.
	/// </summary>
	public string? Delimiter { get; set; }
	/// <summary>
	/// Gets a collection of <see cref="CsvRow" /> objects that represents the content of this CSV file.
	/// </summary>
	public CsvRowCollection Rows { get; private init; }
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether this CSV file contains rows that could not be parsed. This property does not change once the file is loaded.
	/// </summary>
	public bool HasErrors { get; private set; }
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether all rows in this CSV file have the same amount of columns, excluding the <see cref="Headers" /> property and error lines. If zero rows or only error rows were loaded, this property returns <see langword="true" />. This property does not change once the file is loaded.
	/// </summary>
	public bool IsColumnCountConsistent { get; private set; }

	static CsvFile()
	{
		DelimiterDetector = CsvDelimiterDetector.CreateDefault();
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="CsvFile" /> class.
	/// </summary>
	public CsvFile()
	{
		Rows = new();
		IsColumnCountConsistent = true;
	}

	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromFile(string path)
	{
		return FromFile(path, false);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromFile(string path, bool hasHeaderRow)
	{
		return FromFile(path, hasHeaderRow, null);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromFile(string path, bool hasHeaderRow, string? delimiter)
	{
		return FromFile(path, hasHeaderRow, delimiter, false);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromFile(string path, bool hasHeaderRow, string? delimiter, bool ignoreEmptyLines)
	{
		return FromFile(path, hasHeaderRow, delimiter, ignoreEmptyLines, null);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
	/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromFile(string path, bool hasHeaderRow, string? delimiter, bool ignoreEmptyLines, Encoding? encoding)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);
		Check.ArgumentEx.StringNotEmpty(delimiter);

		using FileStream file = File.OpenRead(path);
		return FromStream(file, hasHeaderRow, delimiter, ignoreEmptyLines, encoding);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="byte" />[] that represents a CSV file.
	/// </summary>
	/// <param name="file">The <see cref="byte" />[] that represents a CSV file to read from.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromBinary(byte[] file)
	{
		return FromBinary(file, false);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="byte" />[] that represents a CSV file.
	/// </summary>
	/// <param name="file">The <see cref="byte" />[] that represents a CSV file to read from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromBinary(byte[] file, bool hasHeaderRow)
	{
		return FromBinary(file, hasHeaderRow, null);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="byte" />[] that represents a CSV file.
	/// </summary>
	/// <param name="file">The <see cref="byte" />[] that represents a CSV file to read from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromBinary(byte[] file, bool hasHeaderRow, string? delimiter)
	{
		return FromBinary(file, hasHeaderRow, delimiter, false);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="byte" />[] that represents a CSV file.
	/// </summary>
	/// <param name="file">The <see cref="byte" />[] that represents a CSV file to read from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromBinary(byte[] file, bool hasHeaderRow, string? delimiter, bool ignoreEmptyLines)
	{
		return FromBinary(file, hasHeaderRow, delimiter, ignoreEmptyLines, null);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="byte" />[] that represents a CSV file.
	/// </summary>
	/// <param name="file">The <see cref="byte" />[] that represents a CSV file to read from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
	/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromBinary(byte[] file, bool hasHeaderRow, string? delimiter, bool ignoreEmptyLines, Encoding? encoding)
	{
		Check.ArgumentNull(file);
		Check.ArgumentEx.StringNotEmpty(delimiter);

		using MemoryStream memoryStream = new(file);
		return FromStream(memoryStream, hasHeaderRow, delimiter, ignoreEmptyLines, encoding);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="string" /> that represents the contents of a CSV file.
	/// </summary>
	/// <param name="csv">The <see cref="string" /> that represents the contents of a CSV file to read from.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromString(string csv)
	{
		return FromString(csv, false);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="string" /> that represents the contents of a CSV file.
	/// </summary>
	/// <param name="csv">The <see cref="string" /> that represents the contents of a CSV file to read from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromString(string csv, bool hasHeaderRow)
	{
		return FromString(csv, hasHeaderRow, null);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="string" /> that represents the contents of a CSV file.
	/// </summary>
	/// <param name="csv">The <see cref="string" /> that represents the contents of a CSV file to read from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromString(string csv, bool hasHeaderRow, string? delimiter)
	{
		return FromString(csv, hasHeaderRow, delimiter, false);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="string" /> that represents the contents of a CSV file.
	/// </summary>
	/// <param name="csv">The <see cref="string" /> that represents the contents of a CSV file to read from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromString(string csv, bool hasHeaderRow, string? delimiter, bool ignoreEmptyLines)
	{
		return FromBinary(csv.ToUTF8Bytes(), hasHeaderRow, delimiter, ignoreEmptyLines, Encoding.UTF8);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromStream(Stream stream)
	{
		return FromStream(stream, false);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromStream(Stream stream, bool hasHeaderRow)
	{
		return FromStream(stream, hasHeaderRow, null);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromStream(Stream stream, bool hasHeaderRow, string? delimiter)
	{
		return FromStream(stream, hasHeaderRow, delimiter, false);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromStream(Stream stream, bool hasHeaderRow, string? delimiter, bool ignoreEmptyLines)
	{
		return FromStream(stream, hasHeaderRow, delimiter, ignoreEmptyLines, null);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
	/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromStream(Stream stream, bool hasHeaderRow, string? delimiter, bool ignoreEmptyLines, Encoding? encoding)
	{
		return FromStream(stream, hasHeaderRow, delimiter, ignoreEmptyLines, encoding, false);
	}
	/// <summary>
	/// Creates a <see cref="CsvFile" /> object from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
	/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and load its contents into the <see cref="Headers" /> property.</param>
	/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
	/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
	/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
	/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
	/// <returns>
	/// The <see cref="CsvFile" /> this method creates.
	/// </returns>
	public static CsvFile FromStream(Stream stream, bool hasHeaderRow, string? delimiter, bool ignoreEmptyLines, Encoding? encoding, bool leaveOpen)
	{
		Check.ArgumentNull(stream);
		Check.ArgumentEx.StringNotEmpty(delimiter);

		CsvHelper.AutoDetectDelimiter(DelimiterDetector, stream, encoding, ref delimiter);

		using TextFieldParser parser = CsvHelper.CreateTextFieldParser(stream, delimiter, encoding, leaveOpen, out FieldInfo lineNumberField);
		CsvFile csv = new() { Delimiter = delimiter };

		if (hasHeaderRow)
		{
			try
			{
				csv.Headers = parser.ReadFields();
			}
			catch (MalformedLineException)
			{
				csv.HasErrors = true;
			}
		}

		int columnCount = -1;

		foreach (CsvRow row in CsvHelper.EnumerateTextFieldParser(parser, ignoreEmptyLines))
		{
			row.LineNumber = lineNumberField.GetValue(parser) is long lineNumber ? lineNumber - 1 : throw Throw.InvalidOperation("Error retrieving line number.");

			if (row.ErrorLine != null)
			{
				csv.HasErrors = true;
			}

			if (row.ErrorLine == null && csv.IsColumnCountConsistent)
			{
				if (columnCount == -1)
				{
					columnCount = row.Count;
				}
				else if (columnCount != row.Count)
				{
					csv.IsColumnCountConsistent = false;
				}
			}

			csv.Rows.Add(row);
		}

		return csv;
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
		if (IsColumnCountConsistent)
		{
			return Rows.Count == 0 || Rows.First().Count == columnCount;
		}
		else
		{
			return Rows.All(row => row.Count == columnCount);
		}
	}
	/// <summary>
	/// Tries to find the column index of a specified case sensitive column header name. Returns -1, if the column header was not found.
	/// </summary>
	/// <param name="header">A <see cref="string" /> specifying the case sensitive name of the column header to search for.</param>
	/// <returns>
	/// The zero-based index of the column header, or -1, if the column header was not found.
	/// </returns>
	public int GetColumnIndex(string header)
	{
		return GetColumnIndex(header, false);
	}
	/// <summary>
	/// Tries to find the column index of a specified case header name. Returns -1, if the column header was not found.
	/// </summary>
	/// <param name="header">A <see cref="string" /> specifying the case sensitive name of the column header to search for.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during column name comparison.</param>
	/// <returns>
	/// The zero-based index of the column header, or -1, if the column header was not found.
	/// </returns>
	public int GetColumnIndex(string header, bool ignoreCase)
	{
		return Headers?.IndexOf(h => h.Equals(header, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)) ?? -1;
	}

	/// <summary>
	/// Writes the contents of this CSV to a file. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path to a file to which this CSV file is written to.</param>
	public void Save(string path)
	{
		Save(path, false);
	}
	/// <summary>
	/// Writes the contents of this CSV to a file. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path to a file to which this CSV file is written to.</param>
	/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
	public void Save(string path, bool alwaysQuote)
	{
		Save(path, alwaysQuote, null);
	}
	/// <summary>
	/// Writes the contents of this CSV to a file. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path to a file to which this CSV file is written to.</param>
	/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
	/// <param name="encoding">The encoding to use to write to the file.</param>
	public void Save(string path, bool alwaysQuote, Encoding? encoding)
	{
		Check.ArgumentNull(path);
		Check.ArgumentNull(Delimiter);
		Check.ArgumentEx.StringNotEmpty(Delimiter);

		using FileStream stream = File.Create(path);
		Save(stream, alwaysQuote, encoding, false);
	}
	/// <summary>
	/// Writes the contents of this CSV to a <see cref="Stream" />. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> to which this CSV is written to.</param>
	public void Save(Stream stream)
	{
		Save(stream, false);
	}
	/// <summary>
	/// Writes the contents of this CSV to a <see cref="Stream" />. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> to which this CSV is written to.</param>
	/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
	public void Save(Stream stream, bool alwaysQuote)
	{
		Save(stream, alwaysQuote, null);
	}
	/// <summary>
	/// Writes the contents of this CSV to a <see cref="Stream" />. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> to which this CSV is written to.</param>
	/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
	/// <param name="encoding">The encoding to use to write to the file.</param>
	public void Save(Stream stream, bool alwaysQuote, Encoding? encoding)
	{
		Save(stream, alwaysQuote, encoding, false);
	}
	/// <summary>
	/// Writes the contents of this CSV to a <see cref="Stream" />. If <see cref="Headers" /> is not <see langword="null" />, the header row is included. The <see cref="Delimiter" /> property specifies the delimiter to use when writing.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> to which this CSV is written to.</param>
	/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
	/// <param name="encoding">The encoding to use to write to the file.</param>
	/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
	public void Save(Stream stream, bool alwaysQuote, Encoding? encoding, bool leaveOpen)
	{
		Check.ArgumentNull(stream);
		Check.ArgumentNull(Delimiter);
		Check.ArgumentEx.StringNotEmpty(Delimiter);

		if (Headers?.Any() == true)
		{
			CsvIterator.ToStream(stream, new[] { new CsvRow(Headers) }, Delimiter, alwaysQuote, encoding, true);
		}

		CsvIterator.ToStream(stream, Rows, Delimiter, alwaysQuote, encoding, leaveOpen);
	}
}