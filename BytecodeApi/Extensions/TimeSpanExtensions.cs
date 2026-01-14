using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="TimeSpan" /> objects.
/// </summary>
public static class TimeSpanExtensions
{
	extension(TimeSpan timeSpan)
	{
		/// <summary>
		/// Converts the value of this <see cref="TimeSpan" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value with the format that is used to convert this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="TimeSpan" />.
		/// </returns>
		public string ToStringInvariant(string format)
		{
			return timeSpan.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="TimeSpan" /> object is <see cref="TimeSpan.Zero" />, otherwise its original value.
		/// </summary>
		/// <returns>
		/// <see langword="null" />, if this <see cref="TimeSpan" /> object is <see cref="TimeSpan.Zero" />;
		/// otherwise, its original value.
		/// </returns>
		public TimeSpan? ToNullIfDefault()
		{
			return timeSpan == default ? null : timeSpan;
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified days.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value specifying the days to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified days.
		/// </returns>
		public TimeSpan AddDays(double value)
		{
			return timeSpan + TimeSpan.FromDays(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified hours.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value specifying the hours to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified hours.
		/// </returns>
		public TimeSpan AddHours(double value)
		{
			return timeSpan + TimeSpan.FromHours(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified minutes.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value specifying the minutes to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified minutes.
		/// </returns>
		public TimeSpan AddMinutes(double value)
		{
			return timeSpan + TimeSpan.FromMinutes(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified seconds.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value specifying the seconds to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified seconds.
		/// </returns>
		public TimeSpan AddSeconds(double value)
		{
			return timeSpan + TimeSpan.FromSeconds(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified milliseconds.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value specifying the milliseconds to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified milliseconds.
		/// </returns>
		public TimeSpan AddMilliseconds(double value)
		{
			return timeSpan + TimeSpan.FromMilliseconds(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified ticks.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value specifying the ticks to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified ticks.
		/// </returns>
		public TimeSpan AddTicks(long value)
		{
			return timeSpan + TimeSpan.FromTicks(value);
		}
	}
}