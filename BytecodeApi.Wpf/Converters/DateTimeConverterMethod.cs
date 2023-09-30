namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to convert <see cref="DateTime" />? values.
/// </summary>
public enum DateTimeConverterMethod
{
	/// <summary>
	/// Returns the equivalent short date <see cref="string" /> representation of the <see cref="DateTime" />? value.
	/// </summary>
	ShortDate,
	/// <summary>
	/// Returns the equivalent short time <see cref="string" /> representation of the <see cref="DateTime" />? value.
	/// </summary>
	ShortTime,
	/// <summary>
	/// Returns the equivalent long date <see cref="string" /> representation of the <see cref="DateTime" />? value.
	/// </summary>
	LongDate,
	/// <summary>
	/// Returns the equivalent long time <see cref="string" /> representation of the <see cref="DateTime" />? value.
	/// </summary>
	LongTime,
	/// <summary>
	/// Returns the year component of the <see cref="DateTime" />? value as a <see cref="string" />.
	/// </summary>
	Year,
	/// <summary>
	/// Returns the quarter (a number between 1 and 4) of the <see cref="DateTime" />? value as a <see cref="string" />.
	/// </summary>
	Quarter,
	/// <summary>
	/// Returns the month component of the <see cref="DateTime" />? value as a <see cref="string" />.
	/// </summary>
	Month,
	/// <summary>
	/// Returns the day component of the <see cref="DateTime" />? value as a <see cref="string" />.
	/// </summary>
	Day,
	/// <summary>
	/// Returns the hour component of the <see cref="DateTime" />? value as a <see cref="string" />.
	/// </summary>
	Hour,
	/// <summary>
	/// Returns the minute component of the <see cref="DateTime" />? value as a <see cref="string" />.
	/// </summary>
	Minute,
	/// <summary>
	/// Returns the second component of the <see cref="DateTime" />? value as a <see cref="string" />.
	/// </summary>
	Second,
	/// <summary>
	/// Returns the equivalent <see cref="string" /> representation of the <see cref="DateTime" />? value using a specified format parameter and the invariant culture.
	/// </summary>
	Format,
	/// <summary>
	/// Returns the equivalent <see cref="System.DateOnly" /> value, converted from the <see cref="DateTime" />? value.
	/// </summary>
	DateOnly,
	/// <summary>
	/// Returns the equivalent <see cref="System.TimeOnly" /> value, converted from the <see cref="DateTime" />? value.
	/// </summary>
	TimeOnly
}