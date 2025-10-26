using BytecodeApi.Extensions;
using System.Globalization;

namespace BytecodeApi.Wpf.Cui.Converters;

public sealed class DatePickerWeekDayConverter : ConverterBase<int?>
{
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