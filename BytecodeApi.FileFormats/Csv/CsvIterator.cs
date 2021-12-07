using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BytecodeApi.FileFormats.Csv
{
	/// <summary>
	/// Provides support for read and write iteration over arbitrarily large CSV files.
	/// </summary>
	public static class CsvIterator
	{
		/// <summary>
		/// Gets or sets the delimiter detector to be used when reading a CSV file and the delimiter parameter is <see langword="null" />.
		/// <para>This property is global for the <see cref="CsvIterator" /> class.</para>
		/// <para>Default values are:</para>
		/// <para><see cref="CsvDelimiterDetector.DelimitersToTest" /> = ",", ";", "\t", "|"</para>
		/// <para><see cref="CsvDelimiterDetector.MinRowsToTest" /> = 2</para>
		/// <para><see cref="CsvDelimiterDetector.MaxRowsToTest" /> = 10</para>
		/// </summary>
		public static CsvDelimiterDetector DelimiterDetector { get; set; }

		static CsvIterator()
		{
			DelimiterDetector = CsvDelimiterDetector.CreateDefault();
		}

		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified file. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> FromFile(string path)
		{
			return FromFile(path, false);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified file. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and ignore it.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> FromFile(string path, bool hasHeaderRow)
		{
			return FromFile(path, hasHeaderRow, null);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified file. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and ignore it.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> FromFile(string path, bool hasHeaderRow, string delimiter)
		{
			return FromFile(path, hasHeaderRow, delimiter, false);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified file. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and ignore it.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> FromFile(string path, bool hasHeaderRow, string delimiter, bool ignoreEmptyLines)
		{
			return FromFile(path, hasHeaderRow, delimiter, ignoreEmptyLines, null);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified file. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and ignore it.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> FromFile(string path, bool hasHeaderRow, string delimiter, bool ignoreEmptyLines, Encoding encoding)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.FileNotFound(path);
			Check.ArgumentEx.StringNotEmpty(delimiter, nameof(delimiter));

			using (FileStream file = File.OpenRead(path))
			{
				foreach (CsvRow row in FromStream(file, hasHeaderRow, delimiter, ignoreEmptyLines, encoding))
				{
					yield return row;
				}
			}
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="Stream" />. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> FromStream(Stream stream)
		{
			return FromStream(stream, false);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="Stream" />. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and ignore it.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> FromStream(Stream stream, bool hasHeaderRow)
		{
			return FromStream(stream, hasHeaderRow, null);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="Stream" />. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and ignore it.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> FromStream(Stream stream, bool hasHeaderRow, string delimiter)
		{
			return FromStream(stream, hasHeaderRow, delimiter, false);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="Stream" />. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and ignore it.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> FromStream(Stream stream, bool hasHeaderRow, string delimiter, bool ignoreEmptyLines)
		{
			return FromStream(stream, hasHeaderRow, delimiter, ignoreEmptyLines, null);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="Stream" />. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and ignore it.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> FromStream(Stream stream, bool hasHeaderRow, string delimiter, bool ignoreEmptyLines, Encoding encoding)
		{
			return FromStream(stream, hasHeaderRow, delimiter, ignoreEmptyLines, encoding, false);
		}
		/// <summary>
		/// Returns an enumerable collection of <see cref="CsvRow" /> objects from the specified <see cref="Stream" />. This method streams the file and does not load it into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from.</param>
		/// <param name="hasHeaderRow"><see langword="true" /> to treat the first row as a header row and ignore it.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to be used during CSV parsing. If <see langword="null" /> is provided, the delimiter is automatically detected. If automatic detection fails, an exception is thrown.</param>
		/// <param name="ignoreEmptyLines"><see langword="true" /> to ignore empty lines and lines where all columns are empty.</param>
		/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
		/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with the <see cref="CsvRow" /> objects from the specified file.
		/// </returns>
		public static IEnumerable<CsvRow> FromStream(Stream stream, bool hasHeaderRow, string delimiter, bool ignoreEmptyLines, Encoding encoding, bool leaveOpen)
		{
			Check.ArgumentNull(stream, nameof(stream));
			Check.ArgumentEx.StringNotEmpty(delimiter, nameof(delimiter));

			CsvHelper.AutoDetectDelimiter(DelimiterDetector, stream, encoding, ref delimiter);

			using (TextFieldParser parser = CsvHelper.CreateTextFieldParser(stream, delimiter, encoding, leaveOpen))
			{
				long lineNumber = 1;

				foreach (CsvRow row in CsvHelper.EnumerateTextFieldParser(parser, ignoreEmptyLines))
				{
					if (hasHeaderRow)
					{
						hasHeaderRow = false;
					}
					else
					{
						row.LineNumber = lineNumber;
						yield return row;
					}

					lineNumber++;
				}
			}
		}

		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a file. This method streams an <see cref="IEnumerable{T}" /> into the file and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the file.</param>
		public static void ToFile(string path, IEnumerable<CsvRow> rows, string delimiter)
		{
			ToFile(path, rows, delimiter, false);
		}
		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a file. This method streams an <see cref="IEnumerable{T}" /> into the file and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the file.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		public static void ToFile(string path, IEnumerable<CsvRow> rows, string delimiter, bool alwaysQuote)
		{
			ToFile(path, rows, delimiter, alwaysQuote, null);
		}
		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a file. This method streams an <see cref="IEnumerable{T}" /> into the file and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the file.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the file.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		/// <param name="encoding">The encoding to use to write to the file.</param>
		public static void ToFile(string path, IEnumerable<CsvRow> rows, string delimiter, bool alwaysQuote, Encoding encoding)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.ArgumentNull(rows, nameof(rows));
			Check.ArgumentNull(delimiter, nameof(delimiter));
			Check.ArgumentEx.StringNotEmpty(delimiter, nameof(delimiter));

			using (FileStream stream = File.Create(path))
			{
				ToStream(stream, rows, delimiter, alwaysQuote, encoding, false);
			}
		}
		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a <see cref="Stream" />. This method streams an <see cref="IEnumerable{T}" /> into the <see cref="Stream" /> and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the <see cref="Stream" />.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the <see cref="Stream" />.</param>
		public static void ToStream(Stream stream, IEnumerable<CsvRow> rows, string delimiter)
		{
			ToStream(stream, rows, delimiter, false);
		}
		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a <see cref="Stream" />. This method streams an <see cref="IEnumerable{T}" /> into the <see cref="Stream" /> and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the <see cref="Stream" />.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the <see cref="Stream" />.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		public static void ToStream(Stream stream, IEnumerable<CsvRow> rows, string delimiter, bool alwaysQuote)
		{
			ToStream(stream, rows, delimiter, alwaysQuote, null);
		}
		/// <summary>
		/// Writes an enumerable collection of <see cref="CsvRow" /> to a <see cref="Stream" />. This method streams an <see cref="IEnumerable{T}" /> into the <see cref="Stream" /> and does not require to load all rows into memory and is typically used for large files that are processed in a <see langword="foreach" /> loop.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which the rows are written to.</param>
		/// <param name="rows">An <see cref="IEnumerable{T}" /> with the rows to write to the <see cref="Stream" />.</param>
		/// <param name="delimiter">A <see cref="string" /> specifying the delimiter to use to write to the <see cref="Stream" />.</param>
		/// <param name="alwaysQuote"><see langword="true" /> to wrap all cells with quotes; <see langword="false" /> to only use quotes when needed.</param>
		/// <param name="encoding">The encoding to use to write to the <see cref="Stream" />.</param>
		public static void ToStream(Stream stream, IEnumerable<CsvRow> rows, string delimiter, bool alwaysQuote, Encoding encoding)
		{
			ToStream(stream, rows, delimiter, alwaysQuote, encoding, false);
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
		public static void ToStream(Stream stream, IEnumerable<CsvRow> rows, string delimiter, bool alwaysQuote, Encoding encoding, bool leaveOpen)
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

							if (alwaysQuote || cell?.Contains(delimiter) == true || cell?.Contains("\n") == true)
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
	}
}