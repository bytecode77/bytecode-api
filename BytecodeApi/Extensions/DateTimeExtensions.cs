using System.Globalization;

namespace BytecodeApi.Extensions;

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
		return dateTime == default ? null : dateTime;
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
					dateTime = dateTime.DayOfWeek switch
					{
						DayOfWeek.Friday => dateTime.AddDays(3),
						DayOfWeek.Saturday => dateTime.AddDays(2),
						_ => dateTime.AddDays(1)
					};
				}
				else
				{
					dateTime = dateTime.DayOfWeek switch
					{
						DayOfWeek.Sunday => dateTime.AddDays(-2),
						DayOfWeek.Monday => dateTime.AddDays(-3),
						_ => dateTime.AddDays(-1)
					};
				}
			}
		}

		return dateTime;
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
		if (dateTime > value)
		{
			(dateTime, value) = (value, dateTime);
		}

		int count = 0;

		for (DateTime i = dateTime.Date; i <= value.Date; i = i.AddDays(1))
		{
			if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday)
			{
				count++;
			}
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
		return part switch
		{
			DateTimePart.Full => dateTime,
			DateTimePart.DateTimeWithSeconds => new(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Kind),
			DateTimePart.DateTime => new(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, dateTime.Kind),
			DateTimePart.Date => new(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Kind),
			DateTimePart.YearMonth => new(dateTime.Year, dateTime.Month, 1, 0, 0, 0, dateTime.Kind),
			DateTimePart.YearQuarter => new(dateTime.Year, (dateTime.Month - 1) / 3 * 3 + 1, 1, 0, 0, 0, dateTime.Kind),
			DateTimePart.Year => new(dateTime.Year, 1, 1, 0, 0, 0, dateTime.Kind),
			_ => throw Throw.InvalidEnumArgument(nameof(part), part)
		};
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
		Check.ArgumentNull(culture);

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
		while (dateTime.DayOfWeek != firstDayOfWeek)
		{
			dateTime = dateTime.AddDays(-1);
		}
		return dateTime.Date;
	}
	/// <summary>
	/// Returns a <see cref="DateOnly" /> from this <see cref="DateTime" /> value.
	/// </summary>
	/// <param name="dateTime">The <see cref="DateTime" /> value to convert.</param>
	/// <returns>
	/// The converted <see cref="DateOnly" /> value.
	/// </returns>
	public static DateOnly ToDateOnly(this DateTime dateTime)
	{
		return DateOnly.FromDateTime(dateTime);
	}
	/// <summary>
	/// Returns a <see cref="TimeOnly" /> from this <see cref="DateTime" /> value.
	/// </summary>
	/// <param name="dateTime">The <see cref="DateTime" /> value to convert.</param>
	/// <returns>
	/// The converted <see cref="TimeOnly" /> value.
	/// </returns>
	public static TimeOnly ToTimeOnly(this DateTime dateTime)
	{
		return TimeOnly.FromDateTime(dateTime);
	}
}