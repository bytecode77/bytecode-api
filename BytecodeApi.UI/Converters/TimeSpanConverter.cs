using BytecodeApi.Extensions;
using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="TimeSpan" />? values. The <see cref="Convert(TimeSpan?, string)" /> method returns a <see cref="string" /> based on the specified <see cref="TimeSpanConverterMethod" /> parameter.
	/// </summary>
	public sealed class TimeSpanConverter : ConverterBase<TimeSpan?, string, string>
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
		public override string Convert(TimeSpan? value, string parameter)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				switch (Method)
				{
					case TimeSpanConverterMethod.Milliseconds: return value.Value.Milliseconds.ToString();
					case TimeSpanConverterMethod.Seconds: return value.Value.Seconds.ToString();
					case TimeSpanConverterMethod.Minutes: return value.Value.Minutes.ToString();
					case TimeSpanConverterMethod.Hours: return value.Value.Hours.ToString();
					case TimeSpanConverterMethod.Days: return value.Value.Days.ToString();
					case TimeSpanConverterMethod.TotalMilliseconds: return ((int)value.Value.TotalMilliseconds).ToString();
					case TimeSpanConverterMethod.TotalSeconds: return ((int)value.Value.TotalSeconds).ToString();
					case TimeSpanConverterMethod.TotalMinutes: return ((int)value.Value.TotalMinutes).ToString();
					case TimeSpanConverterMethod.TotalHours: return ((int)value.Value.TotalHours).ToString();
					case TimeSpanConverterMethod.TotalDays: return ((int)value.Value.TotalDays).ToString();
					case TimeSpanConverterMethod.Format: return value.Value.ToStringInvariant(parameter);
					default: throw Throw.InvalidEnumArgument(nameof(Method), Method);
				};
			}
		}
	}
}