using BytecodeApi.Extensions;
using System;

namespace BytecodeApi.FileFormats.Csv
{
	/// <summary>
	/// Represents a cell in a <see cref="CsvRow" /> of a flat file database.
	/// </summary>
	public struct CsvCell : IEquatable<CsvCell>
	{
		/// <summary>
		/// Gets or sets the cell content of a flat file database.
		/// </summary>
		public string Value { get; set; }
		/// <summary>
		/// Gets or sets the parsed <see cref="int" /> value of the <see cref="Value" /> property. If conversion fails, <see langword="null" /> is returned. Assigning a value sets the <see cref="Value" /> property by converting the value to a <see cref="string" />.
		/// </summary>
		public int? Int32Value
		{
			get => Value?.ToInt32OrNull();
			set => Value = value?.ToString() ?? "";
		}
		/// <summary>
		/// Gets or sets the parsed <see cref="long" /> value of the <see cref="Value" /> property. If conversion fails, <see langword="null" /> is returned. Assigning a value sets the <see cref="Value" /> property by converting the value to a <see cref="string" />.
		/// </summary>
		public long? Int64Value
		{
			get => Value?.ToInt64OrNull();
			set => Value = value?.ToString() ?? "";
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CsvCell" /> struct with the specified value.
		/// </summary>
		/// <param name="value">The <see cref="string" /> value of this cell.</param>
		public CsvCell(string value)
		{
			Value = value;
		}

		/// <summary>
		/// Returns a <see cref="string" /> whose value was parsed from the flat file database.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> whose value was parsed from the flat file database.
		/// </returns>
		public override string ToString()
		{
			return Value ?? "";
		}
		/// <summary>
		/// Determines whether the specified <see cref="object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Equals(object obj)
		{
			return obj is CsvCell csvCell && Equals(csvCell);
		}
		/// <summary>
		/// Determines whether this instance is equal to another <see cref="CsvCell" />.
		/// </summary>
		/// <param name="other">The <see cref="CsvCell" /> to compare to this instance.</param>
		/// <returns>
		/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(CsvCell other)
		{
			return Value == other.Value;
		}
		/// <summary>
		/// Returns a hash code for this <see cref="CsvCell" />.
		/// </summary>
		/// <returns>
		/// The hash code for this <see cref="CsvCell" /> instance.
		/// </returns>
		public override int GetHashCode()
		{
			return Value?.GetHashCode() ?? 0;
		}

		/// <summary>
		/// Compares two <see cref="CsvCell" /> instances for equality.
		/// </summary>
		/// <param name="a">The first <see cref="CsvCell" /> to compare.</param>
		/// <param name="b">The second <see cref="CsvCell" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="CsvCell" /> are equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator ==(CsvCell a, CsvCell b)
		{
			return Equals(a, b);
		}
		/// <summary>
		/// Compares two <see cref="CsvCell" /> instances for inequality.
		/// </summary>
		/// <param name="a">The first <see cref="CsvCell" /> to compare.</param>
		/// <param name="b">The second <see cref="CsvCell" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="CsvCell" /> are not equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator !=(CsvCell a, CsvCell b)
		{
			return !(a == b);
		}
	}
}