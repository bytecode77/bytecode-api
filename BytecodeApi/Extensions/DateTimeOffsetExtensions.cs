using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="DateTimeOffset" /> objects.
/// </summary>
public static class DateTimeOffsetExtensions
{
	extension(DateTimeOffset)
	{
		/// <summary>
		/// Computes the number of months between two <see cref="DateTimeOffset" /> values.
		/// </summary>
		/// <param name="a">The first <see cref="DateTimeOffset" /> value.</param>
		/// <param name="b">The second <see cref="DateTimeOffset" /> value.</param>
		/// <returns>
		/// The number of months between two <see cref="DateTimeOffset" /> values.
		/// </returns>
		public static int GetMonthsDifference(DateTimeOffset a, DateTimeOffset b)
		{
			return (b.Year - a.Year) * 12 + b.Month - a.Month;
		}
		/// <summary>
		/// Computes the number of months between two <see cref="DateTimeOffset" /> values, including fractional months.
		/// </summary>
		/// <param name="a">The first <see cref="DateTimeOffset" /> value.</param>
		/// <param name="b">The second <see cref="DateTimeOffset" /> value.</param>
		/// <returns>
		/// The number of months between two <see cref="DateTimeOffset" /> values, including fractional months.
		/// </returns>
		public static double GetTotalMonthsDifference(DateTimeOffset a, DateTimeOffset b)
		{
			return DateOnly.GetTotalMonthsDifference(a.ToDateOnly(), b.ToDateOnly());
		}
		/// <summary>
		/// Converts a <see cref="int" /> value representing a unix time stamp to a <see cref="DateTimeOffset" /> object.
		/// </summary>
		/// <param name="seconds">The seconds starting from 01.01.1970 00:00:00.</param>
		/// <returns>
		/// A new <see cref="DateTimeOffset" /> object whose value is the sum of 01.01.1970 00:00:00 and <paramref name="seconds" />.
		/// </returns>
		public static DateTimeOffset FromUnixTimeStamp(int seconds)
		{
			return new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).AddSeconds(seconds);
		}
		/// <summary>
		/// Converts a <see cref="DateTimeOffset" /> value to its equivalent unix time stamp represented as a <see cref="int" /> value. If <paramref name="dateTimeOffset" /> is out of bounds of the unix epoch, <see langword="null" /> is returned.
		/// </summary>
		/// <param name="dateTimeOffset">The <see cref="DateTimeOffset" /> object which is converted to its equivalent unix time stamp representation.</param>
		/// <returns>
		/// If <paramref name="dateTimeOffset" /> is in bounds of the unix epoch, the amount of seconds between 01.01.1970 00:00:00 and <paramref name="dateTimeOffset" />;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static int? ToUnixTimeStamp(DateTimeOffset dateTimeOffset)
		{
			double seconds = (dateTimeOffset - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds;
			return seconds is >= 0 and <= int.MaxValue ? (int)seconds : null;
		}
		/// <summary>
		/// Calculates the age from a birthday.
		/// </summary>
		/// <param name="birthday">A <see cref="DateTimeOffset" /> value representing the birthday to calculate the age from.</param>
		/// <returns>
		/// An equivalent <see cref="int" /> value representing an age, calculated from <paramref name="birthday" />.
		/// </returns>
		public static int CalculateAgeFromBirthday(DateTimeOffset birthday)
		{
			return DateOnly.CalculateAgeFromBirthday(birthday.ToDateOnly());
		}
		/// <summary>
		/// Calculates the age from a birthday at a specified point in time.
		/// </summary>
		/// <param name="birthday">A <see cref="DateTimeOffset" /> value representing the birthday to calculate the age from.</param>
		/// <param name="now">A <see cref="DateTimeOffset" /> value representing the current time stamp. This is usually <see cref="DateTimeOffset.Now" />.</param>
		/// <returns>
		/// An equivalent <see cref="int" /> value representing an age, calculated from <paramref name="birthday" /> and <paramref name="now" />.
		/// </returns>
		public static int CalculateAgeFromBirthday(DateTimeOffset birthday, DateTimeOffset now)
		{
			return DateOnly.CalculateAgeFromBirthday(birthday.ToDateOnly(), now.ToDateOnly());
		}
	}

	extension(DateTimeOffset dateTimeOffset)
	{
		/// <summary>
		/// Converts the value of this <see cref="DateTimeOffset" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="DateTimeOffset" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="DateTimeOffset" />.
		/// </returns>
		public string ToStringInvariant(string format)
		{
			return dateTimeOffset.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="DateTimeOffset" /> object is <see langword="default" />(<see cref="DateTimeOffset" />), otherwise its original value.
		/// </summary>
		/// <returns>
		/// <see langword="null" />, if this <see cref="DateTimeOffset" /> object is <see langword="default" />(<see cref="DateTimeOffset" />);
		/// otherwise, its original value.
		/// </returns>
		public DateTimeOffset? ToNullIfDefault()
		{
			return dateTimeOffset == default ? null : dateTimeOffset;
		}
		/// <summary>
		/// Returns a new <see cref="DateTimeOffset" /> that adds the specified number of business days to this <see cref="DateTimeOffset" /> value. Business days exclude Saturday and Sunday. If <paramref name="days" /> is positive, days are added, otherwise days are subtracted.
		/// <para>Example 1: Friday + 2 business days = Tuesday</para>
		/// <para>Example 2: Monday - 2 business days = Thursday</para>
		/// </summary>
		/// <param name="days">A <see cref="int" /> value specifying the business days to be added to this <see cref="DateTimeOffset" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTimeOffset" /> object whose value is the sum of this <see cref="DateTimeOffset" /> value and the specified business days.
		/// </returns>
		public DateTimeOffset AddBusinessDays(int days)
		{
			if (days != 0)
			{
				int sign = Math.Sign(days);
				days = Math.Abs(days);

				for (int i = 0; i < days; i++)
				{
					if (sign > 0)
					{
						dateTimeOffset = dateTimeOffset.DayOfWeek switch
						{
							DayOfWeek.Friday => dateTimeOffset.AddDays(3),
							DayOfWeek.Saturday => dateTimeOffset.AddDays(2),
							_ => dateTimeOffset.AddDays(1)
						};
					}
					else
					{
						dateTimeOffset = dateTimeOffset.DayOfWeek switch
						{
							DayOfWeek.Sunday => dateTimeOffset.AddDays(-2),
							DayOfWeek.Monday => dateTimeOffset.AddDays(-3),
							_ => dateTimeOffset.AddDays(-1)
						};
					}
				}
			}

			return dateTimeOffset;
		}
		/// <summary>
		/// Computes the total count of business days between two <see cref="DateTimeOffset" /> instances. Business days exclude Saturday and Sunday. The time fraction is ignored and the returned value is inclusive.
		/// <para>Example 1: Friday through Tuesday = 3 business days</para>
		/// <para>Example 2: Saturday through Sunday = 0 business days</para>
		/// </summary>
		/// <param name="value">The <see cref="DateTimeOffset" /> value to compare to this <see cref="DateTimeOffset" />. <paramref name="value" /> can be either less or greater than this <see cref="DateTimeOffset" /> value.</param>
		/// <returns>
		/// A <see cref="int" /> value representing the total count of business days between two <see cref="DateTimeOffset" /> instances.
		/// </returns>
		public int GetTotalBusinessDays(DateTimeOffset value)
		{
			return dateTimeOffset.ToDateOnly().GetTotalBusinessDays(value.ToDateOnly());
		}
		/// <summary>
		/// Gets the number of days in the month of the specified <see cref="DateTimeOffset" />.
		/// </summary>
		/// <returns>
		/// The number of days in the month of the specified <see cref="DateTimeOffset" />.
		/// </returns>
		public int GetDaysInMonth()
		{
			return DateTime.DaysInMonth(dateTimeOffset.Year, dateTimeOffset.Month);
		}
		/// <summary>
		/// Returns a new <see cref="DateTimeOffset" /> that adds the specified number of months, including fractions of a month.
		/// </summary>
		/// <param name="months">A number of months. This number can be negative or positive. If the number is fractional, the fraction is multiplied by the number of days in the month after the whole months were added.</param>
		/// <returns>
		/// A new <see cref="DateTimeOffset" /> whose value is the sum of the original <see cref="DateTimeOffset" /> value and <paramref name="months" />.
		/// </returns>
		public DateTimeOffset AddMonths(double months)
		{
			dateTimeOffset = dateTimeOffset.AddMonths((int)months);

			months %= 1;
			if (months != 0)
			{
				dateTimeOffset = dateTimeOffset.AddDays((int)Math.Round(months * dateTimeOffset.GetDaysInMonth()));
			}

			return dateTimeOffset;
		}
		/// <summary>
		/// Determines whether the specified <see cref="DateTimeOffset" /> is equal to this instance. The <paramref name="part" /> parameter specifies which fraction is considered during comparison.
		/// </summary>
		/// <param name="other">A <see cref="DateTimeOffset" /> to compare with this <see cref="DateTimeOffset" />.</param>
		/// <param name="part">The <see cref="DateTimePart" /> specifying, which fraction is considered during comparison.</param>
		/// <returns>
		/// <see langword="true" />, if the specified <see cref="DateTimeOffset" /> is equal to this instance;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(DateTimeOffset other, DateTimePart part)
		{
			return dateTimeOffset.GetPart(part).DateTime == other.GetPart(part).DateTime;
		}
		/// <summary>
		/// Compares the value of this <see cref="DateTimeOffset" /> instance to a specified <see cref="DateTimeOffset" /> value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="DateTimeOffset" /> value. The <paramref name="part" /> parameter specifies which fraction is considered during comparison.
		/// </summary>
		/// <param name="other">A <see cref="DateTimeOffset" /> to compare with this <see cref="DateTimeOffset" />.</param>
		/// <param name="part">The <see cref="DateTimePart" /> specifying, which fraction is considered during comparison.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared considering only the specified <see cref="DateTimePart" />.
		/// </returns>
		public int CompareTo(DateTimeOffset other, DateTimePart part)
		{
			return dateTimeOffset.GetPart(part).DateTime.CompareTo(other.GetPart(part).DateTime);
		}
		/// <summary>
		/// Returns a new <see cref="DateTimeOffset" /> that represents a fraction of this <see cref="DateTimeOffset" /> value specified by the <paramref name="part" /> parameter.
		/// </summary>
		/// <param name="part">The <see cref="DateTimePart" /> specifying, which fraction of this <see cref="DateTimeOffset" /> is returned.</param>
		/// <returns>
		/// A new <see cref="DateTimeOffset" /> that represents a fraction of this <see cref="DateTimeOffset" /> value specified by the <paramref name="part" /> parameter.
		/// </returns>
		public DateTimeOffset GetPart(DateTimePart part)
		{
			return part switch
			{
				DateTimePart.Full => dateTimeOffset,
				DateTimePart.DateTimeWithSeconds => new(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second, dateTimeOffset.Offset),
				DateTimePart.DateTime => new(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, 0, dateTimeOffset.Offset),
				DateTimePart.Date => new(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, 0, 0, 0, dateTimeOffset.Offset),
				DateTimePart.YearMonth => new(dateTimeOffset.Year, dateTimeOffset.Month, 1, 0, 0, 0, dateTimeOffset.Offset),
				DateTimePart.YearQuarter => new(dateTimeOffset.Year, (dateTimeOffset.Month - 1) / 3 * 3 + 1, 1, 0, 0, 0, dateTimeOffset.Offset),
				DateTimePart.Year => new(dateTimeOffset.Year, 1, 1, 0, 0, 0, dateTimeOffset.Offset),
				_ => throw Throw.InvalidEnumArgument(nameof(part), part)
			};
		}
		/// <summary>
		/// Returns a new <see cref="DateTimeOffset" /> representing the first day of the week according to the current culture.
		/// </summary>
		/// <returns>
		/// A new <see cref="DateTimeOffset" /> object representing the first day of the week according to the current culture.
		/// </returns>
		public DateTimeOffset GetFirstDayOfWeek()
		{
			return dateTimeOffset.GetFirstDayOfWeek(CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Returns a new <see cref="DateTimeOffset" /> representing the first day of the week using specified culture-specific calendar rules.
		/// </summary>
		/// <param name="culture">An object that supplies culture-specific calendar rules.</param>
		/// <returns>
		/// A new <see cref="DateTimeOffset" /> object representing the first day of the week according to <paramref name="culture" />.
		/// </returns>
		public DateTimeOffset GetFirstDayOfWeek(CultureInfo culture)
		{
			Check.ArgumentNull(culture);

			return dateTimeOffset.GetFirstDayOfWeek(culture.DateTimeFormat.FirstDayOfWeek);
		}
		/// <summary>
		/// Returns a new <see cref="DateTimeOffset" /> representing the first day of the week, according to the <paramref name="firstDayOfWeek" /> parameter.
		/// </summary>
		/// <param name="firstDayOfWeek">The first day of week.</param>
		/// <returns>
		/// A new <see cref="DateTimeOffset" /> object representing the first day of the week, according to the <paramref name="firstDayOfWeek" /> parameter.
		/// </returns>
		public DateTimeOffset GetFirstDayOfWeek(DayOfWeek firstDayOfWeek)
		{
			while (dateTimeOffset.DayOfWeek != firstDayOfWeek)
			{
				dateTimeOffset = dateTimeOffset.AddDays(-1);
			}

			return dateTimeOffset.GetPart(DateTimePart.Date);
		}
		/// <summary>
		/// Returns a <see cref="DateOnly" /> from this <see cref="DateTimeOffset" /> value.
		/// </summary>
		/// <returns>
		/// The converted <see cref="DateOnly" /> value.
		/// </returns>
		public DateOnly ToDateOnly()
		{
			return DateOnly.FromDateTime(dateTimeOffset.DateTime);
		}
		/// <summary>
		/// Returns a <see cref="TimeOnly" /> from this <see cref="DateTimeOffset" /> value.
		/// </summary>
		/// <returns>
		/// The converted <see cref="TimeOnly" /> value.
		/// </returns>
		public TimeOnly ToTimeOnly()
		{
			return TimeOnly.FromDateTime(dateTimeOffset.DateTime);
		}
	}
}