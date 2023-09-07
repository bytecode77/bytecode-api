using BytecodeApi.Extensions;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="TimeSpan" />? values. The <see cref="Convert(TimeSpan?, string)" /> method returns a <see cref="string" /> based on the specified <see cref="TimeSpanConverterMethod" /> parameter.
/// </summary>
public sealed class TimeSpanConverter : ConverterBase<TimeSpan?, string?>
{
	/// <summary>
	/// Specifies the method that is used to convert the <see cref="TimeSpan" />? value.
	/// </summary>
	public TimeSpanConverterMethod Method { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeSpanConverter" /> class with the specified conversion method.
	/// </summary>
	/// <param name="method">The method that is used to convert the <see cref="TimeSpan" />? value.</param>
	public TimeSpanConverter(TimeSpanConverterMethod method)
	{
		Method = method;
	}

	/// <summary>
	/// Converts the <see cref="TimeSpan" />? value based on the specified <see cref="TimeSpanConverterMethod" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="TimeSpan" />? value to convert.</param>
	/// <param name="parameter">A parameter <see cref="string" /> that specifies the format used in some of the <see cref="TimeSpanConverterMethod" /> methods.</param>
	/// <returns>
	/// A <see cref="string" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(TimeSpan? value, string? parameter)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return Method switch
			{
				TimeSpanConverterMethod.Milliseconds => value.Value.Milliseconds.ToString(),
				TimeSpanConverterMethod.Seconds => value.Value.Seconds.ToString(),
				TimeSpanConverterMethod.Minutes => value.Value.Minutes.ToString(),
				TimeSpanConverterMethod.Hours => value.Value.Hours.ToString(),
				TimeSpanConverterMethod.Days => value.Value.Days.ToString(),
				TimeSpanConverterMethod.TotalMilliseconds => ((int)value.Value.TotalMilliseconds).ToString(),
				TimeSpanConverterMethod.TotalSeconds => ((int)value.Value.TotalSeconds).ToString(),
				TimeSpanConverterMethod.TotalMinutes => ((int)value.Value.TotalMinutes).ToString(),
				TimeSpanConverterMethod.TotalHours => ((int)value.Value.TotalHours).ToString(),
				TimeSpanConverterMethod.TotalDays => ((int)value.Value.TotalDays).ToString(),
				TimeSpanConverterMethod.Format => value.Value.ToStringInvariant(parameter ?? ""),
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
	}
}