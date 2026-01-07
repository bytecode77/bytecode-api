namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to convert <see cref="DateOnly" />? values.
/// </summary>
public enum DateOnlyConverterMethod
{
	/// <summary>
	/// Returns the equivalent short date <see cref="string" /> representation of the <see cref="DateOnly" />? value.
	/// </summary>
	ShortDate,
	/// <summary>
	/// Returns the equivalent long date <see cref="string" /> representation of the <see cref="DateOnly" />? value.
	/// </summary>
	LongDate,
	/// <summary>
	/// Returns the year component of the <see cref="DateOnly" />? value as a <see cref="string" />.
	/// </summary>
	Year,
	/// <summary>
	/// Returns the quarter (a number between 1 and 4) of the <see cref="DateOnly" />? value as a <see cref="string" />.
	/// </summary>
	Quarter,
	/// <summary>
	/// Returns the month component of the <see cref="DateOnly" />? value as a <see cref="string" />.
	/// </summary>
	Month,
	/// <summary>
	/// Returns the day component of the <see cref="DateOnly" />? value as a <see cref="string" />.
	/// </summary>
	Day,
	/// <summary>
	/// Returns the equivalent <see cref="string" /> representation of the <see cref="DateOnly" />? value using a specified format parameter and the invariant culture.
	/// </summary>
	Format,
	/// <summary>
	/// Returns the equivalent <see cref="System.DateTime" /> value, converted from the <see cref="DateOnly" />? value.
	/// </summary>
	DateTime
}