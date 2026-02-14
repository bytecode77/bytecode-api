using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="DateTime" /> objects.
/// </summary>
public static class DateTimeExtensions
{
	extension(DateTime)
	{
		/// <summary>
		/// Determines whether a combination of year, month and day represents a valid date.
		/// </summary>
		/// <param name="year">The year of the date.</param>
		/// <param name="month">The month of the date.</param>
		/// <param name="day">The day of the date.</param>
		/// <returns>
		/// <see langword="true" />, if the combination of year, month and day represents a valid date;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsValidDate(int year, int month, int day)
		{
			return
				year is >= 1 and <= 9999 &&
				month is >= 1 and <= 12 &&
				day is >= 1 and <= 31 &&
				day <= DateTime.DaysInMonth(year, month);
		}
		/// <summary>
		/// Computes the number of months between two <see cref="DateTime" /> values.
		/// </summary>
		/// <param name="a">The first <see cref="DateTime" /> value.</param>
		/// <param name="b">The second <see cref="DateTime" /> value.</param>
		/// <returns>
		/// The number of months between two <see cref="DateTime" /> values.
		/// </returns>
		public static int GetMonthsDifference(DateTime a, DateTime b)
		{
			return (b.Year - a.Year) * 12 + b.Month - a.Month;
		}
		/// <summary>
		/// Computes the number of months between two <see cref="DateTime" /> values, including fractional months.
		/// </summary>
		/// <param name="a">The first <see cref="DateTime" /> value.</param>
		/// <param name="b">The second <see cref="DateTime" /> value.</param>
		/// <returns>
		/// The number of months between two <see cref="DateTime" /> values, including fractional months.
		/// </returns>
		public static double GetTotalMonthsDifference(DateTime a, DateTime b)
		{
			return DateOnly.GetTotalMonthsDifference(a.ToDateOnly(), b.ToDateOnly());
		}
		/// <summary>
		/// Converts a <see cref="int" /> value representing a unix time stamp to a <see cref="DateTime" /> object, using the <see cref="DateTimeKind.Unspecified" /> kind.
		/// </summary>
		/// <param name="seconds">The seconds starting from 01.01.1970 00:00:00.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the sum of 01.01.1970 00:00:00 and <paramref name="seconds" />.
		/// </returns>
		public static DateTime FromUnixTimeStamp(int seconds)
		{
			return FromUnixTimeStamp(seconds, DateTimeKind.Unspecified);
		}
		/// <summary>
		/// Converts a <see cref="int" /> value representing a unix time stamp to a <see cref="DateTime" /> object, using the specified <see cref="DateTimeKind" />.
		/// </summary>
		/// <param name="seconds">The seconds starting from 01.01.1970 00:00:00.</param>
		/// <param name="kind">The <see cref="DateTimeKind" /> to be used for creation of the <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the sum of 01.01.1970 00:00:00 and <paramref name="seconds" />.
		/// </returns>
		public static DateTime FromUnixTimeStamp(int seconds, DateTimeKind kind)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, kind).AddSeconds(seconds);
		}
		/// <summary>
		/// Converts a <see cref="DateTime" /> value to its equivalent unix time stamp represented as a <see cref="int" /> value, using the specified <see cref="DateTimeKind" />. If <paramref name="dateTime" /> is out of bounds of the unix epoch, <see langword="null" /> is returned.
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime" /> object which is converted to its equivalent unix time stamp representation.</param>
		/// <returns>
		/// If <paramref name="dateTime" /> is in bounds of the unix epoch, the amount of seconds between 01.01.1970 00:00:00 and <paramref name="dateTime" />;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static int? ToUnixTimeStamp(DateTime dateTime)
		{
			return ToUnixTimeStamp(dateTime, DateTimeKind.Unspecified);
		}
		/// <summary>
		/// Converts a <see cref="DateTime" /> value to its equivalent unix time stamp represented as a <see cref="int" /> value, using the <see cref="DateTimeKind.Unspecified" /> kind. If <paramref name="dateTime" /> is out of bounds of the unix epoch, <see langword="null" /> is returned.
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime" /> object which is converted to its equivalent unix time stamp representation.</param>
		/// <param name="kind">The <see cref="DateTimeKind" /> to be used for conversion of the <see cref="DateTime" /> object.</param>
		/// <returns>
		/// If <paramref name="dateTime" /> is in bounds of the unix epoch, the amount of seconds between 01.01.1970 00:00:00 and <paramref name="dateTime" />;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static int? ToUnixTimeStamp(DateTime dateTime, DateTimeKind kind)
		{
			double seconds = (dateTime - new DateTime(1970, 1, 1, 0, 0, 0, kind)).TotalSeconds;
			return seconds is > 0 and <= int.MaxValue ? (int)seconds : null;
		}
		/// <summary>
		/// Calculates the age from a birthday.
		/// </summary>
		/// <param name="birthday">A <see cref="DateTime" /> value representing the birhtday to calculate the age from.</param>
		/// <returns>
		/// An equivalent <see cref="int" /> value representing an age, calculated from <paramref name="birthday" />.
		/// </returns>
		public static int CalculateAgeFromBirthday(DateTime birthday)
		{
			return CalculateAgeFromBirthday(birthday, DateTime.Now);
		}
		/// <summary>
		/// Calculates the age from a birthday at a specified point in time.
		/// </summary>
		/// <param name="birthday">A <see cref="DateTime" /> value representing the birhtday to calculate the age from.</param>
		/// <param name="now">A <see cref="DateTime" /> value representing the current time stamp. This is usually <see cref="DateTime.Now" />.</param>
		/// <returns>
		/// An equivalent <see cref="int" /> value representing an age, calculated from <paramref name="birthday" /> and <paramref name="now" />.
		/// </returns>
		public static int CalculateAgeFromBirthday(DateTime birthday, DateTime now)
		{
			int age = now.Year - birthday.Year;
			if (now < birthday.AddYears(age))
			{
				age--;
			}

			return age;
		}
	}

	extension(DateTime dateTime)
	{
		/// <summary>
		/// Converts the value of this <see cref="DateTime" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="DateTime" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="DateTime" />.
		/// </returns>
		public string ToStringInvariant(string format)
		{
			return dateTime.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="DateTime" /> object is <see langword="default" />(<see cref="DateTime" />), otherwise its original value.
		/// </summary>
		/// <returns>
		/// <see langword="null" />, if this <see cref="DateTime" /> object is <see langword="default" />(<see cref="DateTime" />);
		/// otherwise, its original value.
		/// </returns>
		public DateTime? ToNullIfDefault()
		{
			return dateTime == default ? null : dateTime;
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that adds the specified number of business days to this <see cref="DateTime" /> value. Business days exclude Saturday and Sunday. If <paramref name="days" /> is positive, days are added, otherwise days are subtracted.
		/// <para>Example 1: Friday + 2 business days = Tuesday</para>
		/// <para>Example 2: Monday - 2 business days = Thursday</para>
		/// </summary>
		/// <param name="days">A <see cref="int" /> value specifying the business days to be added to this <see cref="DateTime" /> object.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object whose value is the sum of this <see cref="DateTime" /> value and the specified business days.
		/// </returns>
		public DateTime AddBusinessDays(int days)
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
		/// <param name="value">The <see cref="DateTime" /> value to compare to this <see cref="DateTime" />. <paramref name="value" /> can be either less or greater than this <see cref="DateTime" /> value.</param>
		/// <returns>
		/// A <see cref="int" /> value representing the total count of business days between two <see cref="DateTime" /> instances.
		/// </returns>
		public int GetTotalBusinessDays(DateTime value)
		{
			if (dateTime > value)
			{
				(dateTime, value) = (value, dateTime);
			}

			int count = 0;

			for (DateTime i = dateTime.Date; i <= value.Date; i = i.AddDays(1))
			{
				if (i.DayOfWeek is not DayOfWeek.Saturday and not DayOfWeek.Sunday)
				{
					count++;
				}
			}

			return count;
		}
		/// <summary>
		/// Gets the number of days in the month of the specified <see cref="DateTime" />.
		/// </summary>
		/// <returns>
		/// The number of days in the month of the specified <see cref="DateTime" />.
		/// </returns>
		public int GetDaysInMonth()
		{
			return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that adds the specified number of months, including fractions of a month.
		/// </summary>
		/// <param name="months">A number of months. This number can be negative or positive. If the number is fractional, the fraction is multiplied by the number of days in the month after the whole months were added.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> whose value is the sum of the original <see cref="DateTime" /> value and <paramref name="months" />.
		/// </returns>
		public DateTime AddMonths(double months)
		{
			dateTime = dateTime.AddMonths((int)months);

			months %= 1;
			if (months != 0)
			{
				dateTime = dateTime.AddDays((int)Math.Round(months * dateTime.GetDaysInMonth()));
			}

			return dateTime;
		}
		/// <summary>
		/// Determines whether the specified <see cref="DateTime" /> is equal to this instance. The <paramref name="part" /> parameter specifies which fraction is considered during comparison.
		/// </summary>
		/// <param name="other">A <see cref="DateTime" /> to compare with this <see cref="DateTime" />.</param>
		/// <param name="part">The <see cref="DateTimePart" /> specifying, which fraction is considered during comparison.</param>
		/// <returns>
		/// <see langword="true" />, if the specified <see cref="DateTime" /> is equal to this instance;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(DateTime other, DateTimePart part)
		{
			return dateTime.GetPart(part) == other.GetPart(part);
		}
		/// <summary>
		/// Compares the value of this <see cref="DateTime" /> instance to a specified <see cref="DateTime" /> value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="DateTime" /> value. The <paramref name="part" /> parameter specifies which fraction is considered during comparison.
		/// </summary>
		/// <param name="other">A <see cref="DateTime" /> to compare with this <see cref="DateTime" />.</param>
		/// <param name="part">The <see cref="DateTimePart" /> specifying, which fraction is considered during comparison.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared considering only the specified <see cref="DateTimePart" />.
		/// </returns>
		public int CompareTo(DateTime other, DateTimePart part)
		{
			return dateTime.GetPart(part).CompareTo(other.GetPart(part));
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> that represents a fraction of this <see cref="DateTime" /> value specified by the <paramref name="part" /> parameter.
		/// </summary>
		/// <param name="part">The <see cref="DateTimePart" /> specifying, which fraction of this <see cref="DateTime" /> is returned.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> that represents a fraction of this <see cref="DateTime" /> value specified by the <paramref name="part" /> parameter.
		/// </returns>
		public DateTime GetPart(DateTimePart part)
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
		/// <returns>
		/// A new <see cref="DateTime" /> object representing the first day of the week according to the current culture.
		/// </returns>
		public DateTime GetFirstDayOfWeek()
		{
			return dateTime.GetFirstDayOfWeek(CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> representing the first day of the week using specified culture-specific calendar rules.
		/// </summary>
		/// <param name="culture">An object that supplies culture-specific calendar rules.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object representing the first day of the week according to <paramref name="culture" />.
		/// </returns>
		public DateTime GetFirstDayOfWeek(CultureInfo culture)
		{
			Check.ArgumentNull(culture);

			return dateTime.GetFirstDayOfWeek(culture.DateTimeFormat.FirstDayOfWeek);
		}
		/// <summary>
		/// Returns a new <see cref="DateTime" /> representing the first day of the week, according to the <paramref name="firstDayOfWeek" /> parameter.
		/// </summary>
		/// <param name="firstDayOfWeek">The first day of week.</param>
		/// <returns>
		/// A new <see cref="DateTime" /> object representing the first day of the week, according to the <paramref name="firstDayOfWeek" /> parameter.
		/// </returns>
		public DateTime GetFirstDayOfWeek(DayOfWeek firstDayOfWeek)
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
		/// <returns>
		/// The converted <see cref="DateOnly" /> value.
		/// </returns>
		public DateOnly ToDateOnly()
		{
			return DateOnly.FromDateTime(dateTime);
		}
		/// <summary>
		/// Returns a <see cref="TimeOnly" /> from this <see cref="DateTime" /> value.
		/// </summary>
		/// <returns>
		/// The converted <see cref="TimeOnly" /> value.
		/// </returns>
		public TimeOnly ToTimeOnly()
		{
			return TimeOnly.FromDateTime(dateTime);
		}
	}
}