using System;
using System.Globalization;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="TimeSpan" /> objects.
	/// </summary>
	public static class TimeSpanExtensions
	{
		/// <summary>
		/// Converts the value of this <see cref="TimeSpan" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="timeSpan">The <see cref="TimeSpan" /> value to convert.</param>
		/// <param name="format">A <see cref="string" /> value with the format that is used to convert this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="TimeSpan" />.
		/// </returns>
		public static string ToStringInvariant(this TimeSpan timeSpan, string format)
		{
			return timeSpan.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="TimeSpan" /> object is <see cref="TimeSpan.Zero" />, otherwise its original value.
		/// </summary>
		/// <param name="timeSpan">The <see cref="TimeSpan" /> value to convert.</param>
		/// <returns>
		/// <see langword="null" />, if this <see cref="TimeSpan" /> object is <see cref="TimeSpan.Zero" />;
		/// otherwise, its original value.
		/// </returns>
		public static TimeSpan? ToNullIfDefault(this TimeSpan timeSpan)
		{
			return timeSpan == default ? (TimeSpan?)null : timeSpan;
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified days.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the days to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified days.
		/// </returns>
		public static TimeSpan AddDays(this TimeSpan timeSpan, double value)
		{
			return timeSpan + TimeSpan.FromDays(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified hours.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the hours to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified hours.
		/// </returns>
		public static TimeSpan AddHours(this TimeSpan timeSpan, double value)
		{
			return timeSpan + TimeSpan.FromHours(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified minutes.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the minutes to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified minutes.
		/// </returns>
		public static TimeSpan AddMinutes(this TimeSpan timeSpan, double value)
		{
			return timeSpan + TimeSpan.FromMinutes(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified seconds.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the seconds to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified seconds.
		/// </returns>
		public static TimeSpan AddSeconds(this TimeSpan timeSpan, double value)
		{
			return timeSpan + TimeSpan.FromSeconds(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified milliseconds.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the milliseconds to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified milliseconds.
		/// </returns>
		public static TimeSpan AddMilliseconds(this TimeSpan timeSpan, double value)
		{
			return timeSpan + TimeSpan.FromMilliseconds(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified ticks.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the ticks to be added to this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the sum of this <see cref="TimeSpan" /> and the specified ticks.
		/// </returns>
		public static TimeSpan AddTicks(this TimeSpan timeSpan, long value)
		{
			return timeSpan + TimeSpan.FromTicks(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified days.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the days to be subtracted from this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified days.
		/// </returns>
		public static TimeSpan SubtractDays(this TimeSpan timeSpan, double value)
		{
			return timeSpan - TimeSpan.FromDays(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified hours.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the hours to be subtracted from this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified hours.
		/// </returns>
		public static TimeSpan SubtractHours(this TimeSpan timeSpan, double value)
		{
			return timeSpan - TimeSpan.FromHours(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified minutes.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the minutes to be subtracted from this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified minutes.
		/// </returns>
		public static TimeSpan SubtractMinutes(this TimeSpan timeSpan, double value)
		{
			return timeSpan - TimeSpan.FromMinutes(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified seconds.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the seconds to be subtracted from this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified seconds.
		/// </returns>
		public static TimeSpan SubtractSeconds(this TimeSpan timeSpan, double value)
		{
			return timeSpan - TimeSpan.FromSeconds(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified milliseconds.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the milliseconds to be subtracted from this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified milliseconds.
		/// </returns>
		public static TimeSpan SubtractMilliseconds(this TimeSpan timeSpan, double value)
		{
			return timeSpan - TimeSpan.FromMilliseconds(value);
		}
		/// <summary>
		/// Returns a new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified ticks.
		/// </summary>
		/// <param name="timeSpan">The original <see cref="TimeSpan" /> value.</param>
		/// <param name="value">A <see cref="double" /> value specifying the ticks to be subtracted from this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> object whose value is the difference of this <see cref="TimeSpan" /> and the specified ticks.
		/// </returns>
		public static TimeSpan SubtractTicks(this TimeSpan timeSpan, long value)
		{
			return timeSpan - TimeSpan.FromTicks(value);
		}
	}
}