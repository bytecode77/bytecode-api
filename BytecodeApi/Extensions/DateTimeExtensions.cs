using System;
using System.Globalization;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="DateTime" /> objects.
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Converts the value of this <see cref="DateTime" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime" /> value to convert.</param>
		/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="DateTime" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="DateTime" />.
		/// </returns>
		public static string ToStringInvariant(this DateTime dateTime, string format)
		{
			return dateTime.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="DateTime" /> object is <see langword="default" />(<see cref="DateTime" />), otherwise its original value.
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime" /> value to convert.</param>
		/// <returns>
		/// <see langword="null" />, if this <see cref="DateTime" /> object is <see langword="default" />(<see cref="DateTime" />);
		/// otherwise, its original value.
		/// </returns>
		public static DateTime? ToNullIfDefault(this DateTime dateTime)
		{
			return dateTime == default ? (DateTime?)null : dateTime;
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that subtracts the specified number of years from this <see cref="DateTime" /> value.
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="value">A <see cref="int" /> value specifying the years to be subtracted from this <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the difference of this <see cref="DateTime" /> value and the specified years.
		/// </returns>
		public static DateTime SubtractYears(this DateTime dateTime, int value)
		{
			return dateTime.AddYears(-value);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that subtracts the specified number of months from this <see cref="DateTime" /> value.
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="value">A <see cref="int" /> value specifying the months to be subtracted from this <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the difference of this <see cref="DateTime" /> value and the specified months.
		/// </returns>
		public static DateTime SubtractMonths(this DateTime dateTime, int value)
		{
			return dateTime.AddMonths(-value);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that subtracts the specified number of days from this <see cref="DateTime" /> value.
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="value">A <see cref="int" /> value specifying the days to be subtracted from this <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the difference of this <see cref="DateTime" /> value and the specified days.
		/// </returns>
		public static DateTime SubtractDays(this DateTime dateTime, double value)
		{
			return dateTime.AddDays(-value);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that subtracts the specified number of hours from this <see cref="DateTime" /> value.
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="value">A <see cref="int" /> value specifying the hours to be subtracted from this <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the difference of this <see cref="DateTime" /> value and the specified hours.
		/// </returns>
		public static DateTime SubtractHours(this DateTime dateTime, double value)
		{
			return dateTime.AddHours(-value);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that subtracts the specified number of minutes from this <see cref="DateTime" /> value.
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="value">A <see cref="int" /> value specifying the minutes to be subtracted from this <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the difference of this <see cref="DateTime" /> value and the specified minutes.
		/// </returns>
		public static DateTime SubtractMinutes(this DateTime dateTime, double value)
		{
			return dateTime.AddMinutes(-value);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that subtracts the specified number of seconds from this <see cref="DateTime" /> value.
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="value">A <see cref="int" /> value specifying the seconds to be subtracted from this <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the difference of this <see cref="DateTime" /> value and the specified seconds.
		/// </returns>
		public static DateTime SubtractSeconds(this DateTime dateTime, double value)
		{
			return dateTime.AddSeconds(-value);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that subtracts the specified number of milliseconds from this <see cref="DateTime" /> value.
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="value">A <see cref="int" /> value specifying the milliseconds to be subtracted from this <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the difference of this <see cref="DateTime" /> value and the specified milliseconds.
		/// </returns>
		public static DateTime SubtractMilliseconds(this DateTime dateTime, double value)
		{
			return dateTime.AddMilliseconds(-value);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that subtracts the specified number of ticks from this <see cref="DateTime" /> value.
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="value">A <see cref="int" /> value specifying the ticks to be subtracted from this <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the difference of this <see cref="DateTime" /> value and the specified ticks.
		/// </returns>
		public static DateTime SubtractTicks(this DateTime dateTime, long value)
		{
			return dateTime.AddTicks(-value);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that adds the specified number of business days to this <see cref="DateTime" /> value. Business days exclude Saturday and Sunday. The calculation is iterative. If <paramref name="days" /> is positive, days are added, otherwise days are subtracted.
		/// <para>Example 1: Friday + 2 business days = Tuesday</para>
		/// <para>Example 2: Monday - 2 business days = Thursday</para>
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="days">A <see cref="int" /> value specifying the business days to be added to this <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the sum of this <see cref="DateTime" /> value and the specified business days.
		/// </returns>
		public static DateTime AddBusinessDays(this DateTime dateTime, int days)
		{
			if (days != 0)
			{
				int sign = Math.Sign(days);
				days = Math.Abs(days);

				for (int i = 0; i < days; i++)
				{
					if (sign > 0)
					{
						switch (dateTime.DayOfWeek)
						{
							case DayOfWeek.Friday:
								dateTime = dateTime.AddDays(3);
								break;
							case DayOfWeek.Saturday:
								dateTime = dateTime.AddDays(2);
								break;
							default:
								dateTime = dateTime.AddDays(1);
								break;
						}
					}
					else
					{
						switch (dateTime.DayOfWeek)
						{
							case DayOfWeek.Sunday:
								dateTime = dateTime.SubtractDays(2);
								break;
							case DayOfWeek.Monday:
								dateTime = dateTime.SubtractDays(3);
								break;
							default:
								dateTime = dateTime.SubtractDays(1);
								break;
						}
					}
				}
			}

			return dateTime;
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that subtracts the specified number of business days from this <see cref="DateTime" /> value. Business days exclude Saturday and Sunday. The calculation is iterative. If <paramref name="days" /> is positive, days are subtracted, otherwise days are added.
		/// <para>Example 1: Friday + 2 business days = Tuesday</para>
		/// <para>Example 2: Monday - 2 business days = Thursday</para>
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="days">A <see cref="int" /> value specifying the business days to be subtracted from this <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the difference of this <see cref="DateTime" /> value and the specified business days.
		/// </returns>
		public static DateTime SubtractBusinessDays(this DateTime dateTime, int days)
		{
			return dateTime.AddBusinessDays(-days);
		}
		/// <summary>
		/// Computes the total count of business days between two <see cref="DateTime" /> instances. Business days exclude Saturday and Sunday. The time fraction is ignored and the returned value is inclusive.
		/// <para>Example 1: Friday through Tuesday = 3 business days</para>
		/// <para>Example 2: Saturday through Sunday = 0 business days</para>
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime" /> value to compare to <paramref name="value" />.</param>
		/// <param name="value">The <see cref="DateTime" /> value to compare to this <see cref="DateTime" />. <paramref name="value" /> can be either less or greater than this <see cref="DateTime" /> value.</param>
		/// <returns>
		/// A <see cref="int" /> value representing the total count of business days between two <see cref="DateTime" /> instances.
		/// </returns>
		public static int GetTotalBusinessDays(this DateTime dateTime, DateTime value)
		{
			if (dateTime > value) CSharp.Swap(ref dateTime, ref value);
			int count = 0;

			for (DateTime i = dateTime.Date; i <= value.Date; i = i.AddDays(1))
			{
				if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday) count++;
			}

			return count;
		}
		/// <summary>
		/// Compares the value of this <see cref="DateTime" /> instance to a specified <see cref="DateTime" /> value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="DateTime" /> value. The <paramref name="part" /> parameter specifies which fraction is considered during comparison.
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime" /> value to be compared to <paramref name="other" />.</param>
		/// <param name="other">A <see cref="DateTime" /> to compare with <paramref name="dateTime" />.</param>
		/// <param name="part">The <see cref="DateTimePart" /> specifying, which fraction is considered during comparison.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared considering only the specified <see cref="DateTimePart" />.
		/// </returns>
		public static int CompareTo(this DateTime dateTime, DateTime other, DateTimePart part)
		{
			return dateTime.GetPart(part).CompareTo(other.GetPart(part));
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that represents a fraction of this <see cref="DateTime" /> value specified by the <paramref name="part" /> parameter.
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime" /> value to be stripped.</param>
		/// <param name="part">The <see cref="DateTimePart" /> specifying, which fraction of <paramref name="dateTime" /> is returned.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> that represents a fraction of this <see cref="DateTime" /> value specified by the <paramref name="part" /> parameter.
		/// </returns>
		public static DateTime GetPart(this DateTime dateTime, DateTimePart part)
		{
			switch (part)
			{
				case DateTimePart.Full:
					return dateTime;
				case DateTimePart.DateTimeWithSeconds:
					return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Kind);
				case DateTimePart.DateTime:
					return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, dateTime.Kind);
				case DateTimePart.Date:
					return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Kind);
				case DateTimePart.YearMonth:
					return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, dateTime.Kind);
				case DateTimePart.Year:
					return new DateTime(dateTime.Year, 1, 1, 0, 0, 0, dateTime.Kind);
				default:
					throw Throw.InvalidEnumArgument(nameof(part), part);
			}
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> representing the first day of the week according to the current culture.
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object representing the first day of the week according to the current culture.
		/// </returns>
		public static DateTime GetFirstDayOfWeek(this DateTime dateTime)
		{
			return GetFirstDayOfWeek(dateTime, CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> representing the first day of the week using specified culture-specific calendar rules.
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="culture">An object that supplies culture-specific calendar rules.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object representing the first day of the week according to <paramref name="culture" />.
		/// </returns>
		public static DateTime GetFirstDayOfWeek(this DateTime dateTime, CultureInfo culture)
		{
			Check.ArgumentNull(culture, nameof(culture));

			return dateTime.GetFirstDayOfWeek(culture.DateTimeFormat.FirstDayOfWeek);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> representing the first day of the week, according to the <paramref name="firstDayOfWeek" /> parameter.
		/// </summary>
		/// <param name="dateTime">The original <see cref="DateTime" /> value.</param>
		/// <param name="firstDayOfWeek">The first day of week.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object representing the first day of the week, according to the <paramref name="firstDayOfWeek" /> parameter.
		/// </returns>
		public static DateTime GetFirstDayOfWeek(this DateTime dateTime, DayOfWeek firstDayOfWeek)
		{
			while (dateTime.DayOfWeek != firstDayOfWeek) dateTime = dateTime.SubtractDays(1);
			return dateTime.Date;
		}
	}
}