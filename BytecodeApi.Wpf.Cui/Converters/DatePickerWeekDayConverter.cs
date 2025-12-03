using BytecodeApi.Extensions;
using System.Globalization;

namespace BytecodeApi.Wpf.Cui.Converters;

/// <summary>
/// Represents the converter that converts <see cref="int" />? values representing the day of the week (0 = Monday, 6 = Sunday) to their corresponding two-letter abbreviated day names.
/// </summary>
public sealed class DatePickerWeekDayConverter : ConverterBase<int?>
{
	/// <summary>
	/// Converts the specified <see cref="int" /> value to a two-letter abbreviated day name.
	/// </summary>
	/// <param name="value">The zero-based index of the day of the week (0 = Monday, 6 = Sunday).</param>
	/// <returns>
	/// A <see cref="string" /> with the two-letter abbreviation of the day name.
	/// </returns>
	public override object? Convert(int? value)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			string[] dayNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames
				.Skip(1)
				.Append(CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[0])
				.ToArray();

			return dayNames[value.Value].Left(2);
		}
	}
}