namespace BytecodeApi;

/// <summary>
/// Specifies the subset of a <see cref="System.DateOnly" /> representation.
/// </summary>
public enum DateOnlyPart
{
	/// <summary>
	/// The full <see cref="System.DateOnly" /> is used.
	/// </summary>
	Full,
	/// <summary>
	/// The year and month of the <see cref="System.DateOnly" /> object is used. <see cref="System.DateOnly.Day" /> is set to 1.
	/// </summary>
	YearMonth,
	/// <summary>
	/// The year and month of the <see cref="System.DateOnly" /> object is used. <see cref="System.DateOnly.Month" /> is rounded down to the current quarter and <see cref="System.DateOnly.Day" /> is set to 1.
	/// </summary>
	YearQuarter,
	/// <summary>
	/// The year is used. <see cref="System.DateOnly.Month" /> and <see cref="System.DateOnly.Day" /> are both set to 1.
	/// </summary>
	Year
}