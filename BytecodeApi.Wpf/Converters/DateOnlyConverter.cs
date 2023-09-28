using BytecodeApi.Extensions;
using System.Windows;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="DateOnly" />? values. The <see cref="Convert(DateOnly?, string)" /> method returns a <see cref="string" /> based on the specified <see cref="DateOnlyConverterMethod" /> parameter.
/// </summary>
public sealed class DateOnlyConverter : TwoWayConverterBase<DateOnly?, string?>
{
	/// <summary>
	/// Specifies the method that is used to convert the <see cref="DateOnly" />? value.
	/// </summary>
	public DateOnlyConverterMethod Method { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="DateOnlyConverter" /> class with the specified conversion method.
	/// </summary>
	/// <param name="method">The method that is used to convert the <see cref="DateOnly" />? value.</param>
	public DateOnlyConverter(DateOnlyConverterMethod method)
	{
		Method = method;
	}

	/// <summary>
	/// Converts the <see cref="DateOnly" />? value based on the specified <see cref="DateOnlyConverterMethod" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="DateOnly" />? value to convert.</param>
	/// <param name="parameter">A parameter <see cref="string" /> that specifies the format used in some of the <see cref="DateOnlyConverterMethod" /> methods.</param>
	/// <returns>
	/// A <see cref="string" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(DateOnly? value, string? parameter)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return Method switch
			{
				DateOnlyConverterMethod.ShortDate => value.Value.ToShortDateString(),
				DateOnlyConverterMethod.LongDate => value.Value.ToLongDateString(),
				DateOnlyConverterMethod.Year => value.Value.Year.ToString(),
				DateOnlyConverterMethod.Quarter => ((value.Value.Month - 1) / 3 + 1).ToString(),
				DateOnlyConverterMethod.Month => value.Value.Month.ToString(),
				DateOnlyConverterMethod.Day => value.Value.Day.ToString(),
				DateOnlyConverterMethod.Format => value.Value.ToStringInvariant(parameter ?? ""),
				DateOnlyConverterMethod.DateTime => value.Value.ToDateTime(),
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
	}
	/// <summary>
	/// Converts a <see cref="string" /> value back to its corresponding <see cref="DateOnly" />? value.
	/// </summary>
	/// <param name="value">The <see cref="string" /> value to convert back.</param>
	/// <param name="parameter">A parameter <see cref="string" /> that specifies the format used in some of the <see cref="DateOnlyConverterMethod" /> methods.</param>
	/// <returns>
	/// A <see cref="DateOnly" />? with the result of the conversion.
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
				DateOnlyConverterMethod.ShortDate or
				DateOnlyConverterMethod.LongDate or
				DateOnlyConverterMethod.Year or
				DateOnlyConverterMethod.Quarter or
				DateOnlyConverterMethod.Month or
				DateOnlyConverterMethod.Day => DependencyProperty.UnsetValue,
				DateOnlyConverterMethod.Format => (value as string)?.ToDateOnly(parameter ?? ""),
				DateOnlyConverterMethod.DateTime => (value as DateTime?)?.ToDateOnly(),
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
	}
}