using BytecodeApi.Extensions;
using System.Windows;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="TimeOnly" />? values. The <see cref="Convert(TimeOnly?, string)" /> method returns a <see cref="string" /> based on the specified <see cref="TimeOnlyConverterMethod" /> parameter.
/// </summary>
public sealed class TimeOnlyConverter : TwoWayConverterBase<TimeOnly?, string>
{
	/// <summary>
	/// Specifies the method that is used to convert the <see cref="TimeOnly" />? value.
	/// </summary>
	public TimeOnlyConverterMethod Method { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeOnlyConverter" /> class with the specified conversion method.
	/// </summary>
	/// <param name="method">The method that is used to convert the <see cref="TimeOnly" />? value.</param>
	public TimeOnlyConverter(TimeOnlyConverterMethod method)
	{
		Method = method;
	}

	/// <summary>
	/// Converts the <see cref="TimeOnly" />? value based on the specified <see cref="TimeOnlyConverterMethod" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="TimeOnly" />? value to convert.</param>
	/// <param name="parameter">A parameter <see cref="string" /> that specifies the format used in some of the <see cref="TimeOnlyConverterMethod" /> methods.</param>
	/// <returns>
	/// A <see cref="string" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(TimeOnly? value, string? parameter)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return Method switch
			{
				TimeOnlyConverterMethod.Hour => value.Value.Hour.ToString(),
				TimeOnlyConverterMethod.Minute => value.Value.Minute.ToString(),
				TimeOnlyConverterMethod.Second => value.Value.Second.ToString(),
				TimeOnlyConverterMethod.Format => value.Value.ToStringInvariant(parameter ?? ""),
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
	}
	/// <summary>
	/// Converts a value back to its corresponding <see cref="TimeOnly" />? value.
	/// </summary>
	/// <param name="value">The value to convert back.</param>
	/// <param name="parameter">A parameter <see cref="string" /> that specifies the format used in some of the <see cref="TimeOnlyConverterMethod" /> methods.</param>
	/// <returns>
	/// A <see cref="TimeOnly" />? with the result of the conversion.
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
				TimeOnlyConverterMethod.Hour or
				TimeOnlyConverterMethod.Minute or
				TimeOnlyConverterMethod.Second => DependencyProperty.UnsetValue,
				TimeOnlyConverterMethod.Format => (value as string)?.ToTimeOnly(parameter ?? ""),
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
	}
}