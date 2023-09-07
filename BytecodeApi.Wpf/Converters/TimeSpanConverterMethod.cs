namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to convert <see cref="TimeSpan" />? values.
/// </summary>
public enum TimeSpanConverterMethod
{
	/// <summary>
	/// Returns the milliseconds component of the <see cref="TimeSpan" />? value as a <see cref="string" /> without decimal digits.
	/// </summary>
	Milliseconds,
	/// <summary>
	/// Returns the seconds component of the <see cref="TimeSpan" />? value as a <see cref="string" /> without decimal digits.
	/// </summary>
	Seconds,
	/// <summary>
	/// Returns the minutes component of the <see cref="TimeSpan" />? value as a <see cref="string" /> without decimal digits.
	/// </summary>
	Minutes,
	/// <summary>
	/// Returns the hours component of the <see cref="TimeSpan" />? value as a <see cref="string" /> without decimal digits.
	/// </summary>
	Hours,
	/// <summary>
	/// Returns the days component of the <see cref="TimeSpan" />? value as a <see cref="string" /> without decimal digits.
	/// </summary>
	Days,
	/// <summary>
	/// Returns the <see cref="TimeSpan" />? value expressed in milliseconds as a <see cref="string" /> without decimal digits.
	/// </summary>
	TotalMilliseconds,
	/// <summary>
	/// Returns the <see cref="TimeSpan" />? value expressed in seconds as a <see cref="string" /> without decimal digits.
	/// </summary>
	TotalSeconds,
	/// <summary>
	/// Returns the <see cref="TimeSpan" />? value expressed in minutes as a <see cref="string" /> without decimal digits.
	/// </summary>
	TotalMinutes,
	/// <summary>
	/// Returns the <see cref="TimeSpan" />? value expressed in hours as a <see cref="string" /> without decimal digits.
	/// </summary>
	TotalHours,
	/// <summary>
	/// Returns the <see cref="TimeSpan" />? value expressed in days as a <see cref="string" /> without decimal digits.
	/// </summary>
	TotalDays,
	/// <summary>
	/// Returns the equivalent <see cref="string" /> representation of the <see cref="TimeSpan" />? value using a specified format parameter and the invariant culture.
	/// </summary>
	Format
}