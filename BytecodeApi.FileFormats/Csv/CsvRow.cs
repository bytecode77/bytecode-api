using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BytecodeApi.FileFormats.Csv
{
	/// <summary>
	/// Represents a row of a <see cref="CsvFile" /> flat file database.
	/// </summary>
	public sealed class CsvRow : IReadOnlyCollection<CsvCell>
	{
		private readonly List<CsvCell> Cells;
		/// <summary>
		/// Gets or sets the <see cref="CsvCell" /> at the specified column index.
		/// </summary>
		/// <param name="index">The zero-based column index.</param>
		public CsvCell this[int index]
		{
			get
			{
				Check.IndexOutOfRange(index, Count);
				return Cells[index];
			}
			set
			{
				Check.IndexOutOfRange(index, Count);
				Cells[index] = value;
			}
		}
		/// <summary>
		/// Gets or sets the <see cref="CsvCell" /> at the specified column name, represented as an Excel column name (like A, B, ... AA, AB, ...).
		/// </summary>
		/// <param name="column">A <see cref="string" /> specifying the one-based column index, represented as an Excel column name (like A, B, ... AA, AB, ...).</param>
		public CsvCell this[string column]
		{
			get
			{
				Check.ArgumentNull(column, nameof(column));
				Check.ArgumentEx.StringNotEmpty(column, nameof(column));
				return this[ConvertEx.FromExcelColumnString(column)];
			}
			set
			{
				Check.ArgumentNull(column, nameof(column));
				Check.ArgumentEx.StringNotEmpty(column, nameof(column));
				this[ConvertEx.FromExcelColumnString(column)] = value;
			}
		}
		/// <summary>
		/// Gets the number of cells in this <see cref="CsvRow" />.
		/// </summary>
		public int Count => Cells.Count;
		/// <summary>
		/// Gets the one-based line number of this <see cref="CsvRow" /> object, if it was loaded from an existing flat file database source. Returns <see langword="null" />, if the row has been manually initialized.
		/// </summary>
		public long? LineNumber { get; internal set; }
		/// <summary>
		/// If the line could not be parsed, gets the <see cref="string" /> representing the original line, otherwise returns <see langword="null" />.
		/// </summary>
		public string ErrorLine { get; internal set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CsvRow" /> class.
		/// </summary>
		public CsvRow()
		{
			Cells = new List<CsvCell>();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CsvRow" /> class with a collection of cells.
		/// </summary>
		/// <param name="cells">The collection of cells to be added to this <see cref="CsvRow" />.</param>
		public CsvRow(IEnumerable<CsvCell> cells) : this()
		{
			Check.ArgumentNull(cells, nameof(cells));

			Cells.AddRange(cells);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CsvRow" /> class with a collection of cells.
		/// </summary>
		/// <param name="cells">The collection of cells to be added to this <see cref="CsvRow" />.</param>
		public CsvRow(params CsvCell[] cells) : this((IEnumerable<CsvCell>)cells)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CsvRow" /> class with a collection of cells.
		/// </summary>
		/// <param name="cells">The collection of cells to be added to this <see cref="CsvRow" />, represented as <see cref="string" /> objects.</param>
		public CsvRow(IEnumerable<string> cells) : this(cells.Select(cell => new CsvCell(cell)))
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CsvRow" /> class with a collection of cells.
		/// </summary>
		/// <param name="cells">The collection of cells to be added to this <see cref="CsvRow" />, represented as <see cref="string" /> objects.</param>
		public CsvRow(params string[] cells) : this((IEnumerable<string>)cells)
		{
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[Line: " + LineNumber + ", " + (ErrorLine == null ? "Cells: " + Count : "ErrorLine") + "]";
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<CsvCell> GetEnumerator()
		{
			return Cells.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return Cells.GetEnumerator();
		}
	}
}