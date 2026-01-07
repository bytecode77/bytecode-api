using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="DateOnly" /> objects.
/// </summary>
public static class DateOnlyExtensions
{
	extension(DateOnly)
	{
		/// <summary>
		/// Gets the current date.
		/// </summary>
		public static DateOnly Today => DateTime.Today.ToDateOnly();

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
		/// Calculates the age from a birthday.
		/// </summary>
		/// <param name="birthday">A <see cref="DateOnly" /> value representing the birhtday to calculate the age from.</param>
		/// <returns>
		/// An equivalent <see cref="int" /> value representing an age, calculated from <paramref name="birthday" />.
		/// </returns>
		public static int CalculateAgeFromBirthday(DateOnly birthday)
		{
			return CalculateAgeFromBirthday(birthday, DateOnly.Today);
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

	extension(DateOnly dateOnly)
	{
		/// <summary>
		/// Converts the value of this <see cref="DateOnly" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="DateOnly" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="DateOnly" />.
		/// </returns>
		public string ToStringInvariant(string format)
		{
			return dateOnly.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="DateOnly" /> object is <see langword="default" />(<see cref="DateOnly" />), otherwise its original value.
		/// </summary>
		/// <returns>
		/// <see langword="null" />, if this <see cref="DateOnly" /> object is <see langword="default" />(<see cref="DateOnly" />);
		/// otherwise, its original value.
		/// </returns>
		public DateOnly? ToNullIfDefault()
		{
			return dateOnly == default ? null : dateOnly;
		}
		/// <summary>
		/// Returns a new <see cref="DateOnly" /> that adds the specified number of business days to this <see cref="DateOnly" /> value. Business days exclude Saturday and Sunday. If <paramref name="days" /> is positive, days are added, otherwise days are subtracted.
		/// <para>Example 1: Friday + 2 business days = Tuesday</para>
		/// <para>Example 2: Monday - 2 business days = Thursday</para>
		/// </summary>
		/// <param name="days">A <see cref="int" /> value specifying the business days to be added to this <see cref="DateOnly" /> object.</param>
		/// <returns>
		/// A new <see cref="DateOnly" /> object whose value is the sum of this <see cref="DateOnly" /> value and the specified business days.
		/// </returns>
		public DateOnly AddBusinessDays(int days)
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
		/// <param name="value">The <see cref="DateOnly" /> value to compare to this <see cref="DateOnly" />. <paramref name="value" /> can be either less or greater than this <see cref="DateOnly" /> value.</param>
		/// <returns>
		/// A <see cref="int" /> value representing the total count of business days between two <see cref="DateOnly" /> instances.
		/// </returns>
		public int GetTotalBusinessDays(DateOnly value)
		{
			if (dateOnly > value)
			{
				(dateOnly, value) = (value, dateOnly);
			}

			int count = 0;

			for (DateOnly i = dateOnly; i <= value; i = i.AddDays(1))
			{
				if (i.DayOfWeek is not DayOfWeek.Saturday and not DayOfWeek.Sunday)
				{
					count++;
				}
			}

			return count;
		}
		/// <summary>
		/// Gets the number of days in the month of the specified <see cref="DateOnly" />.
		/// </summary>
		/// <returns>
		/// The number of days in the month of the specified <see cref="DateOnly" />.
		/// </returns>
		public int GetDaysInMonth()
		{
			return DateTime.DaysInMonth(dateOnly.Year, dateOnly.Month);
		}
		/// <summary>
		/// Returns a new <see cref="DateOnly" /> that adds the specified number of months, including fractions of a month.
		/// </summary>
		/// <param name="months">A number of months. This number can be negative or positive. If the number is fractional, the fraction is multiplied by the number of days in the month after the whole months were added.</param>
		/// <returns>
		/// A new <see cref="DateOnly" /> whose value is the sum of the original <see cref="DateOnly" /> value and <paramref name="months" />.
		/// </returns>
		public DateOnly AddMonths(double months)
		{
			dateOnly = dateOnly.AddMonths((int)months);

			months %= 1;
			if (months != 0)
			{
				dateOnly = dateOnly.AddDays((int)Math.Round(months * dateOnly.GetDaysInMonth()));
			}

			return dateOnly;
		}
		/// <summary>
		/// Compares the value of this <see cref="DateOnly" /> instance to a specified <see cref="DateOnly" /> value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="DateOnly" /> value. The <paramref name="part" /> parameter specifies which fraction is considered during comparison.
		/// </summary>
		/// <param name="other">A <see cref="DateOnly" /> to compare with this <see cref="DateOnly" />.</param>
		/// <param name="part">The <see cref="DateOnlyPart" /> specifying, which fraction is considered during comparison.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared considering only the specified <see cref="DateOnlyPart" />.
		/// </returns>
		public int CompareTo(DateOnly other, DateOnlyPart part)
		{
			return dateOnly.GetPart(part).CompareTo(other.GetPart(part));
		}
		/// <summary>
		/// Returns a new <see cref="DateOnly" /> that represents a fraction of this <see cref="DateOnly" /> value specified by the <paramref name="part" /> parameter.
		/// </summary>
		/// <param name="part">The <see cref="DateOnlyPart" /> specifying, which fraction of this <see cref="DateOnly" /> is returned.</param>
		/// <returns>
		/// A new <see cref="DateOnly" /> that represents a fraction of this <see cref="DateOnly" /> value specified by the <paramref name="part" /> parameter.
		/// </returns>
		public DateOnly GetPart(DateOnlyPart part)
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
		/// <returns>
		/// A new <see cref="DateOnly" /> object representing the first day of the week according to the current culture.
		/// </returns>
		public DateOnly GetFirstDayOfWeek()
		{
			return dateOnly.GetFirstDayOfWeek(CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Returns a new <see cref="DateOnly" /> representing the first day of the week using specified culture-specific calendar rules.
		/// </summary>
		/// <param name="culture">An object that supplies culture-specific calendar rules.</param>
		/// <returns>
		/// A new <see cref="DateOnly" /> object representing the first day of the week according to <paramref name="culture" />.
		/// </returns>
		public DateOnly GetFirstDayOfWeek(CultureInfo culture)
		{
			Check.ArgumentNull(culture);

			return dateOnly.GetFirstDayOfWeek(culture.DateTimeFormat.FirstDayOfWeek);
		}
		/// <summary>
		/// Returns a new <see cref="DateOnly" /> representing the first day of the week, according to the <paramref name="firstDayOfWeek" /> parameter.
		/// </summary>
		/// <param name="firstDayOfWeek">The first day of week.</param>
		/// <returns>
		/// A new <see cref="DateOnly" /> object representing the first day of the week, according to the <paramref name="firstDayOfWeek" /> parameter.
		/// </returns>
		public DateOnly GetFirstDayOfWeek(DayOfWeek firstDayOfWeek)
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
		/// <returns>
		/// The converted <see cref="DateOnly" /> value.
		/// </returns>
		public DateTime ToDateTime()
		{
			return dateOnly.ToDateTime(default);
		}
	}
}