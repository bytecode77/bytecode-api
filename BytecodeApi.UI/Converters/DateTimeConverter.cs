using BytecodeApi.Extensions;
using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="DateTime" />? values. The <see cref="Convert(DateTime?, string)" /> method returns a <see cref="string" /> based on the specified <see cref="DateTimeConverterMethod" /> parameter.
	/// </summary>
	public sealed class DateTimeConverter : ConverterBase<DateTime?, string, string>
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
		public override string Convert(DateTime? value, string parameter)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				switch (Method)
				{
					case DateTimeConverterMethod.ShortDate: return value.Value.ToShortDateString();
					case DateTimeConverterMethod.ShortTime: return value.Value.ToShortTimeString();
					case DateTimeConverterMethod.LongDate: return value.Value.ToLongDateString();
					case DateTimeConverterMethod.LongTime: return value.Value.ToLongTimeString();
					case DateTimeConverterMethod.Year: return value.Value.Year.ToString();
					case DateTimeConverterMethod.Month: return value.Value.Month.ToString();
					case DateTimeConverterMethod.Day: return value.Value.Day.ToString();
					case DateTimeConverterMethod.Hour: return value.Value.Hour.ToString();
					case DateTimeConverterMethod.Minute: return value.Value.Minute.ToString();
					case DateTimeConverterMethod.Second: return value.Value.Second.ToString();
					case DateTimeConverterMethod.Format: return value.Value.ToStringInvariant(parameter);
					default: throw Throw.InvalidEnumArgument(nameof(Method), Method);
				}
			}
		}
	}
}