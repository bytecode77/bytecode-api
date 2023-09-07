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
}