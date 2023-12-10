using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="DateOnly" /> objects.
/// </summary>
public static class DateOnlyExtensions
{
	/// <summary>
	/// Converts the value of this <see cref="DateOnly" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
	/// </summary>
	/// <param name="dateOnly">The <see cref="DateOnly" /> value to convert.</param>
	/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="DateOnly" />.</param>
	/// <returns>
	/// The equivalent <see cref="string" /> representation of this <see cref="DateOnly" />.
	/// </returns>
	public static string ToStringInvariant(this DateOnly dateOnly, string format)
	{
		return dateOnly.ToString(format, CultureInfo.InvariantCulture);
	}
	/// <summary>
	/// Returns <see langword="null" />, if this <see cref="DateOnly" /> object is <see langword="default" />(<see cref="DateOnly" />), otherwise its original value.
	/// </summary>
	/// <param name="dateOnly">The <see cref="DateOnly" /> value to convert.</param>
	/// <returns>
	/// <see langword="null" />, if this <see cref="DateOnly" /> object is <see langword="default" />(<see cref="DateOnly" />);
	/// otherwise, its original value.
	/// </returns>
	public static DateOnly? ToNullIfDefault(this DateOnly dateOnly)
	{
		return dateOnly == default ? null : dateOnly;
	}
	/// <summary>
	/// Returns a new <see cref="DateOnly" /> that adds the specified number of business days to this <see cref="DateOnly" /> value. Business days exclude Saturday and Sunday. If <paramref name="days" /> is positive, days are added, otherwise days are subtracted.
	/// <para>Example 1: Friday + 2 business days = Tuesday</para>
	/// <para>Example 2: Monday - 2 business days = Thursday</para>
	/// </summary>
	/// <param name="dateOnly">The original <see cref="DateOnly" /> value.</param>
	/// <param name="days">A <see cref="int" /> value specifying the business days to be added to this <see cref="DateOnly" /> object.</param>
	/// <returns>
	/// A new <see cref="DateOnly" /> object whose value is the sum of this <see cref="DateOnly" /> value and the specified business days.
	/// </returns>
	public static DateOnly AddBusinessDays(this DateOnly dateOnly, int days)
	{
		if (days != 0)
		{
			int sign = Math.Sign(days);
			days = Math.Abs(days);

			for (int i = 0; i < days; i++)
			{
				if (sign > 0)
				{
					dateOnly = dateOnly.DayOfWeek switch
					{
						DayOfWeek.Friday => dateOnly.AddDays(3),
						DayOfWeek.Saturday => dateOnly.AddDays(2),
						_ => dateOnly.AddDays(1)
					};
				}
				else
				{
					dateOnly = dateOnly.DayOfWeek switch
					{
						DayOfWeek.Sunday => dateOnly.AddDays(-2),
						DayOfWeek.Monday => dateOnly.AddDays(-3),
						_ => dateOnly.AddDays(-1)
					};
				}
			}
		}

		return dateOnly;
	}
	/// <summary>
	/// Computes the total count of business days between two <see cref="DateOnly" /> instances. Business days exclude Saturday and Sunday. The returned value is inclusive.
	/// <para>Example 1: Friday through Tuesday = 3 business days</para>
	/// <para>Example 2: Saturday through Sunday = 0 business days</para>
	/// </summary>
	/// <param name="dateOnly">The <see cref="DateOnly" /> value to compare to <paramref name="value" />.</param>
	/// <param name="value">The <see cref="DateOnly" /> value to compare to this <see cref="DateOnly" />. <paramref name="value" /> can be either less or greater than this <see cref="DateOnly" /> value.</param>
	/// <returns>
	/// A <see cref="int" /> value representing the total count of business days between two <see cref="DateOnly" /> instances.
	/// </returns>
	public static int GetTotalBusinessDays(this DateOnly dateOnly, DateOnly value)
	{
		if (dateOnly > value)
		{
			(dateOnly, value) = (value, dateOnly);
		}

		int count = 0;

		for (DateOnly i = dateOnly; i <= value; i = i.AddDays(1))
		{
			if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday)
			{
				count++;
			}
		}

		return count;
	}
	/// <summary>
	/// Compares the value of this <see cref="DateOnly" /> instance to a specified <see cref="DateOnly" /> value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="DateOnly" /> value. The <paramref name="part" /> parameter specifies which fraction is considered during comparison.
	/// </summary>
	/// <param name="dateOnly">The <see cref="DateOnly" /> value to be compared to <paramref name="other" />.</param>
	/// <param name="other">A <see cref="DateOnly" /> to compare with <paramref name="dateOnly" />.</param>
	/// <param name="part">The <see cref="DateOnlyPart" /> specifying, which fraction is considered during comparison.</param>
	/// <returns>
	/// A value that indicates the relative order of the objects being compared considering only the specified <see cref="DateOnlyPart" />.
	/// </returns>
	public static int CompareTo(this DateOnly dateOnly, DateOnly other, DateOnlyPart part)
	{
		return dateOnly.GetPart(part).CompareTo(other.GetPart(part));
	}
	/// <summary>
	/// Returns a new <see cref="DateOnly" /> that represents a fraction of this <see cref="DateOnly" /> value specified by the <paramref name="part" /> parameter.
	/// </summary>
	/// <param name="dateOnly">The <see cref="DateOnly" /> value to be stripped.</param>
	/// <param name="part">The <see cref="DateOnlyPart" /> specifying, which fraction of <paramref name="dateOnly" /> is returned.</param>
	/// <returns>
	/// A new <see cref="DateOnly" /> that represents a fraction of this <see cref="DateOnly" /> value specified by the <paramref name="part" /> parameter.
	/// </returns>
	public static DateOnly GetPart(this DateOnly dateOnly, DateOnlyPart part)
	{
		return part switch
		{
			DateOnlyPart.Full => dateOnly,
			DateOnlyPart.YearMonth => new(dateOnly.Year, dateOnly.Month, 1),
			DateOnlyPart.YearQuarter => new(dateOnly.Year, (dateOnly.Month - 1) / 3 * 3 + 1, 1),
			DateOnlyPart.Year => new(dateOnly.Year, 1, 1),
			_ => throw Throw.InvalidEnumArgument(nameof(part), part)
		};
	}
	/// <summary>
	/// Returns a new <see cref="DateOnly" /> representing the first day of the week according to the current culture.
	/// </summary>
	/// <param name="dateOnly">The original <see cref="DateOnly" /> value.</param>
	/// <returns>
	/// A new <see cref="DateOnly" /> object representing the first day of the week according to the current culture.
	/// </returns>
	public static DateOnly GetFirstDayOfWeek(this DateOnly dateOnly)
	{
		return dateOnly.GetFirstDayOfWeek(CultureInfo.CurrentCulture);
	}
	/// <summary>
	/// Returns a new <see cref="DateOnly" /> representing the first day of the week using specified culture-specific calendar rules.
	/// </summary>
	/// <param name="dateOnly">The original <see cref="DateOnly" /> value.</param>
	/// <param name="culture">An object that supplies culture-specific calendar rules.</param>
	/// <returns>
	/// A new <see cref="DateOnly" /> object representing the first day of the week according to <paramref name="culture" />.
	/// </returns>
	public static DateOnly GetFirstDayOfWeek(this DateOnly dateOnly, CultureInfo culture)
	{
		Check.ArgumentNull(culture);

		return dateOnly.GetFirstDayOfWeek(culture.DateTimeFormat.FirstDayOfWeek);
	}
	/// <summary>
	/// Returns a new <see cref="DateOnly" /> representing the first day of the week, according to the <paramref name="firstDayOfWeek" /> parameter.
	/// </summary>
	/// <param name="dateOnly">The original <see cref="DateOnly" /> value.</param>
	/// <param name="firstDayOfWeek">The first day of week.</param>
	/// <returns>
	/// A new <see cref="DateOnly" /> object representing the first day of the week, according to the <paramref name="firstDayOfWeek" /> parameter.
	/// </returns>
	public static DateOnly GetFirstDayOfWeek(this DateOnly dateOnly, DayOfWeek firstDayOfWeek)
	{
		while (dateOnly.DayOfWeek != firstDayOfWeek)
		{
			dateOnly = dateOnly.AddDays(-1);
		}
		return dateOnly;
	}
	/// <summary>
	/// Returns a <see cref="DateTime" /> from this <see cref="DateOnly" /> value.
	/// </summary>
	/// <param name="dateOnly">The <see cref="DateOnly" /> value to convert.</param>
	/// <returns>
	/// The converted <see cref="DateOnly" /> value.
	/// </returns>
	public static DateTime ToDateTime(this DateOnly dateOnly)
	{
		return dateOnly.ToDateTime(TimeOnly.MinValue);
	}
}