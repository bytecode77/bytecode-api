using BytecodeApi.Extensions;
using System.Globalization;

namespace BytecodeApi.Wpf.Cui.Converters;

/// <summary>
/// Represents the converter that converts <see cref="DateTime" />? values to <see cref="string" /> values in the "dd.MM.yyyy" format and back.
/// </summary>
public sealed class DatePickerDateConverter : TwoWayConverterBase<DateTime?>
{
	/// <summary>
	/// Converts the <see cref="DateTime" />? value to a <see cref="string" /> in the "dd.MM.yyyy" format.
	/// </summary>
	/// <param name="value">The <see cref="DateTime" />? value to convert.</param>
	/// <returns>
	/// A <see cref="string" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(DateTime? value)
	{
		return value?.ToStringInvariant("dd.MM.yyyy");
	}
	/// <summary>
	/// Converts a <see cref="string" /> value in the "dd.MM.yyyy" format back to its corresponding <see cref="DateTime" />? value.
	/// </summary>
	/// <param name="value">The value to convert back.</param>
	/// <returns>
	/// The corresponding <see cref="DateTime" />? value, or <see langword="null" /> if the conversion failed.
	/// </returns>
	public override object? ConvertBack(object? value)
	{
		return DateTime.TryParse(value as string, CultureInfo.CurrentCulture, out DateTime dateTime) ? dateTime : null;
	}
}