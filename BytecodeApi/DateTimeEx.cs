using BytecodeApi.Extensions;

namespace BytecodeApi;

/// <summary>
/// Provides constants and <see langword="static" /> methods that extend the <see cref="DateTime" /> class.
/// </summary>
public static class DateTimeEx
{
	/// <summary>
	/// Represents the average days per year as a <see cref="double" /> value. This field is constant.
	/// </summary>
	public const double AverageDaysInYear = 365.2422;
	/// <summary>
	/// Represents the average days per month as a <see cref="double" /> value. This field is constant.
	/// </summary>
	public const double AverageDaysInMonth = AverageDaysInYear / 12;

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
	/// Computes the number of months between two <see cref="DateOnly" /> values.
	/// </summary>
	/// <param name="a">The first <see cref="DateOnly" /> value.</param>
	/// <param name="b">The second <see cref="DateOnly" /> value.</param>
	/// <returns>
	/// The number of months between two <see cref="DateOnly" /> values.
	/// </returns>
	public static int GetMonthsDifference(DateOnly a, DateOnly b)
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
		return GetTotalMonthsDifference(a.ToDateOnly(), b.ToDateOnly());
	}
	/// <summary>
	/// Computes the number of months between two <see cref="DateOnly" /> values, including fractional months.
	/// </summary>
	/// <param name="a">The first <see cref="DateOnly" /> value.</param>
	/// <param name="b">The second <see cref="DateOnly" /> value.</param>
	/// <returns>
	/// The number of months between two <see cref="DateOnly" /> values, including fractional months.
	/// </returns>
	public static double GetTotalMonthsDifference(DateOnly a, DateOnly b)
	{
		bool negative = false;
		if (a > b)
		{
			negative = true;
			(a, b) = (b, a);
		}

		double difference = 0;

		if (Math.Min(a.Day, b.GetDaysInMonth()) - 1 == b.Day)
		{
			// Full month with 1 day offset (e.g. 16.03. - 15.06.)
			// This includes "end of month days" (e.g. 30.01. - 27.02. is two full months)
			difference = GetMonthsDifference(a, b);
		}
		else
		{
			if (a.Day > 1)
			{
				// The starting month is partial.
				difference += 1 - (double)(a.Day - 1) / a.GetDaysInMonth();
				a = a.GetPart(DateOnlyPart.YearMonth).AddMonths(1);
			}

			if (b.Day < b.GetDaysInMonth())
			{
				// The end month is partial.
				difference += (double)b.Day / b.GetDaysInMonth();
				b = b.GetPart(DateOnlyPart.YearMonth).AddDays(-1);
			}

			// Add whole months.
			difference += GetMonthsDifference(a, b) + 1;
		}

		return negative ? -difference : difference;
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
		return seconds > 0 && seconds <= int.MaxValue ? (int)seconds : null;
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
	/// <summary>
	/// Calculates the age from a birthday.
	/// </summary>
	/// <param name="birthday">A <see cref="DateOnly" /> value representing the birhtday to calculate the age from.</param>
	/// <returns>
	/// An equivalent <see cref="int" /> value representing an age, calculated from <paramref name="birthday" />.
	/// </returns>
	public static int CalculateAgeFromBirthday(DateOnly birthday)
	{
		return CalculateAgeFromBirthday(birthday, DateTime.Today.ToDateOnly());
	}
	/// <summary>
	/// Calculates the age from a birthday at a specified point in time.
	/// </summary>
	/// <param name="birthday">A <see cref="DateOnly" /> value representing the birhtday to calculate the age from.</param>
	/// <param name="now">A <see cref="DateOnly" /> value representing the current date. This is usually <see cref="DateTime.Now" />.</param>
	/// <returns>
	/// An equivalent <see cref="int" /> value representing an age, calculated from <paramref name="birthday" /> and <paramref name="now" />.
	/// </returns>
	public static int CalculateAgeFromBirthday(DateOnly birthday, DateOnly now)
	{
		int age = now.Year - birthday.Year;
		if (now < birthday.AddYears(age))
		{
			age--;
		}

		return age;
	}
}