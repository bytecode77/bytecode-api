using BytecodeApi.Extensions;
using System.Globalization;
using System.Windows;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="DateTime" />? values. The <see cref="Convert(DateTime?, string)" /> method returns a <see cref="string" /> based on the specified <see cref="DateTimeConverterMethod" /> parameter.
/// </summary>
public sealed class DateTimeConverter : TwoWayConverterBase<DateTime?, string?>
{
	/// <summary>
	/// Specifies the method that is used to convert the <see cref="DateTime" />? value.
	/// </summary>
	public DateTimeConverterMethod Method { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="DateTimeConverter" /> class with the specified conversion method.
	/// </summary>
	/// <param name="method">The method that is used to convert the <see cref="DateTime" />? value.</param>
	public DateTimeConverter(DateTimeConverterMethod method)
	{
		Method = method;
	}

	/// <summary>
	/// Converts the <see cref="DateTime" />? value based on the specified <see cref="DateTimeConverterMethod" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="DateTime" />? value to convert.</param>
	/// <param name="parameter">A parameter <see cref="string" /> that specifies the format used in some of the <see cref="DateTimeConverterMethod" /> methods.</param>
	/// <returns>
	/// A <see cref="string" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(DateTime? value, string? parameter)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return Method switch
			{
				DateTimeConverterMethod.ShortDate => value.Value.ToShortDateString(),
				DateTimeConverterMethod.ShortTime => value.Value.ToShortTimeString(),
				DateTimeConverterMethod.LongDate => value.Value.ToLongDateString(),
				DateTimeConverterMethod.LongTime => value.Value.ToLongTimeString(),
				DateTimeConverterMethod.Year => value.Value.Year.ToString(),
				DateTimeConverterMethod.Quarter => ((value.Value.Month - 1) / 3 + 1).ToString(),
				DateTimeConverterMethod.Month => value.Value.Month.ToString(),
				DateTimeConverterMethod.Day => value.Value.Day.ToString(),
				DateTimeConverterMethod.Hour => value.Value.Hour.ToString(),
				DateTimeConverterMethod.Minute => value.Value.Minute.ToString(),
				DateTimeConverterMethod.Second => value.Value.Second.ToString(),
				DateTimeConverterMethod.Format => value.Value.ToStringInvariant(parameter ?? ""),
				DateTimeConverterMethod.DateOnly => value.Value.ToDateOnly(),
				DateTimeConverterMethod.TimeOnly => value.Value.ToTimeOnly(),
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
	}
	/// <summary>
	/// Converts a <see cref="string" /> value back to its corresponding <see cref="DateTime" />? value.
	/// </summary>
	/// <param name="value">The <see cref="string" /> value to convert back.</param>
	/// <param name="parameter">A parameter <see cref="string" /> that specifies the format used in some of the <see cref="DateTimeConverterMethod" /> methods.</param>
	/// <returns>
	/// A <see cref="DateTime" />? with the result of the conversion.
	/// </returns>
	public override object? ConvertBack(object? value, string? parameter)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return Method switch
			{
				DateTimeConverterMethod.ShortDate or
				DateTimeConverterMethod.ShortTime or
				DateTimeConverterMethod.LongDate or
				DateTimeConverterMethod.LongTime => DateTime.TryParse(value as string, CultureInfo.CurrentCulture, out DateTime dateTime) ? dateTime : null,
				DateTimeConverterMethod.Year or
				DateTimeConverterMethod.Quarter or
				DateTimeConverterMethod.Month or
				DateTimeConverterMethod.Day or
				DateTimeConverterMethod.Hour or
				DateTimeConverterMethod.Minute or
				DateTimeConverterMethod.Second => DependencyProperty.UnsetValue,
				DateTimeConverterMethod.Format => (value as string)?.ToDateTime(parameter ?? ""),
				DateTimeConverterMethod.DateOnly => (value as DateOnly?)?.ToDateTime(),
				DateTimeConverterMethod.TimeOnly => DependencyProperty.UnsetValue,
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
	}
}