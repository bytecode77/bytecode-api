using System.Text;

namespace BytecodeApi.CsvParser;

/// <summary>
/// Class to automatically detect the delimiter of CSV files.
/// </summary>
public sealed class CsvDelimiterDetector
{
	/// <summary>
	/// A <see cref="string" />[] specifying the delimiters to test for.
	/// </summary>
	public string[] DelimitersToTest { get; set; }
	/// <summary>
	/// The minimum amount of rows that the CSV file must have in order to determine a delimiter.
	/// </summary>
	public int MinRowsToTest { get; set; }
	/// <summary>
	/// The maximum amount of rows that should be tested. The CSV file can contain less rows, but no less than <see cref="MinRowsToTest" />.
	/// </summary>
	public int MaxRowsToTest { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="CsvDelimiterDetector" /> class.
	/// </summary>
	/// <param name="delimitersToTest">A <see cref="string" />[] specifying the delimiters to test for.</param>
	/// <param name="minRowsToTest">The minimum amount of rows that the CSV file must have in order to determine a delimiter. If the file has less rows, the delimiter is considered indeterminable.</param>
	/// <param name="maxRowsToTest">The maximum amount of rows that should be tested. The CSV file can contain less rows, but no less than <paramref name="minRowsToTest" />.</param>
	public CsvDelimiterDetector(string[] delimitersToTest, int minRowsToTest, int maxRowsToTest)
	{
		DelimitersToTest = delimitersToTest;
		MinRowsToTest = minRowsToTest;
		MaxRowsToTest = maxRowsToTest;
	}
	/// <summary>
	/// Creates a default <see cref="CsvDelimiterDetector" /> with following values:
	/// <para><see cref="DelimitersToTest" /> = ",", ";", "\t", "|"</para>
	/// <para><see cref="MinRowsToTest" /> = 2</para>
	/// <para><see cref="MaxRowsToTest" /> = 10</para>
	/// </summary>
	/// <returns>
	/// A new <see cref="CsvDelimiterDetector" /> with default values.
	/// </returns>
	public static CsvDelimiterDetector CreateDefault()
	{
		return new(new[] { ",", ";", "\t", "|" }, 2, 10);
	}

	/// <summary>
	/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
	/// <returns>
	/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
	/// </returns>
	public string? FromFile(string path)
	{
		return FromFile(path, null);
	}
	/// <summary>
	/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a CSV file.</param>
	/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
	/// <returns>
	/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
	/// </returns>
	public string? FromFile(string path, Encoding? encoding)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);
		Check.ArgumentNull(DelimitersToTest);
		Check.ArgumentEx.ArrayElementsRequired(DelimitersToTest);
		Check.ArgumentEx.ArrayValuesNotNull(DelimitersToTest);
		Check.ArgumentEx.ArrayValuesNotStringEmpty(DelimitersToTest);
		Check.ArgumentOutOfRangeEx.Greater0(MinRowsToTest);
		Check.ArgumentOutOfRangeEx.GreaterEqualValue(MaxRowsToTest, MinRowsToTest);

		using FileStream file = File.OpenRead(path);
		return FromStream(file, encoding);
	}
	/// <summary>
	/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
	/// </summary>
	/// <param name="file">The <see cref="byte" />[] that represents a CSV file to read from.</param>
	/// <returns>
	/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
	/// </returns>
	public string? FromBinary(byte[] file)
	{
		return FromBinary(file, null);
	}
	/// <summary>
	/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
	/// </summary>
	/// <param name="file">The <see cref="byte" />[] that represents a CSV file to read from.</param>
	/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
	/// <returns>
	/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
	/// </returns>
	public string? FromBinary(byte[] file, Encoding? encoding)
	{
		Check.ArgumentNull(file);
		Check.ArgumentNull(DelimitersToTest);
		Check.ArgumentEx.ArrayElementsRequired(DelimitersToTest);
		Check.ArgumentEx.ArrayValuesNotNull(DelimitersToTest);
		Check.ArgumentEx.ArrayValuesNotStringEmpty(DelimitersToTest);
		Check.ArgumentOutOfRangeEx.Greater0(MinRowsToTest);
		Check.ArgumentOutOfRangeEx.GreaterEqualValue(MaxRowsToTest, MinRowsToTest);

		using MemoryStream memoryStream = new(file);
		return FromStream(memoryStream, encoding);
	}
	/// <summary>
	/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from. The <see cref="Stream" /> is read multiple times and the position is reset to the original position before this method returns.</param>
	/// <returns>
	/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
	/// </returns>
	public string? FromStream(Stream stream)
	{
		return FromStream(stream, null);
	}
	/// <summary>
	/// Tries to detect the delimiter of a CSV file and returns a <see cref="string" /> indicating the delimiter, or <see langword="null" />, if it could not be detected.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to read the CSV file from. The <see cref="Stream" /> is read multiple times and the position is reset to the original position before this method returns.</param>
	/// <param name="encoding">The encoding to use if encoding is not determined from the file. Specify <see langword="null" /> to detect encoding automatically or provide a value to explicitly parse with a specific <see cref="Encoding" />.</param>
	/// <returns>
	/// The detected delimiter, or <see langword="null" />, if no definite delimiter was detected.
	/// </returns>
	public string? FromStream(Stream stream, Encoding? encoding)
	{
		Check.ArgumentNull(stream);
		Check.ArgumentNull(DelimitersToTest);
		Check.ArgumentEx.ArrayElementsRequired(DelimitersToTest);
		Check.ArgumentEx.ArrayValuesNotNull(DelimitersToTest);
		Check.ArgumentEx.ArrayValuesNotStringEmpty(DelimitersToTest);
		Check.ArgumentOutOfRangeEx.Greater0(MinRowsToTest);
		Check.ArgumentOutOfRangeEx.GreaterEqualValue(MaxRowsToTest, MinRowsToTest);

		long position = stream.Position;

		try
		{
			List<string> detectedDelimiters = new();

			foreach (string delimiter in DelimitersToTest)
			{
				stream.Seek(0, SeekOrigin.Begin);
				int rowCount = 0;
				int cellCount = -1;

				foreach (CsvRow row in CsvIterator.FromStream(stream, false, delimiter, true, encoding, true).Take(MaxRowsToTest))
				{
					if (cellCount == -1)
					{
						cellCount = row.Count;
					}
					else if (cellCount != row.Count)
					{
						cellCount = -1;
						break;
					}

					rowCount++;
				}

				if (rowCount >= MinRowsToTest && cellCount > 1)
				{
					detectedDelimiters.Add(delimiter);
				}
			}

			if (detectedDelimiters.Count == 1)
			{
				return detectedDelimiters.First();
			}
		}
		finally
		{
			stream.Seek(position, SeekOrigin.Begin);
		}

		return null;
	}
}