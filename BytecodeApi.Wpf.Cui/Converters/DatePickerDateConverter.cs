using BytecodeApi.Extensions;
using System.Globalization;

namespace BytecodeApi.Wpf.Cui.Converters;

public sealed class DatePickerDateConverter : TwoWayConverterBase<DateTime?>
{
	public override object? Convert(DateTime? value)
	{
		return value?.ToStringInvariant("dd.MM.yyyy");
	}
	public override object? ConvertBack(object? value)
	{
		return DateTime.TryParse(value as string, CultureInfo.CurrentCulture, out DateTime dateTime) ? dateTime : null;
	}
}