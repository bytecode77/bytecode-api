using BytecodeApi.Extensions;
using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="TimeSpan" /> values to their equivalent <see cref="string" /> representation using the <see cref="TimeSpanExtensions.ToStringInvariant" /> method and the specified format in the parameter.
	/// </summary>
	public sealed class TimeSpanToStringConverter : ConverterBase<TimeSpan?, string, string>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeSpanToStringConverter" /> class.
		/// </summary>
		public TimeSpanToStringConverter()
		{
		}

		/// <summary>
		/// Converts a <see cref="TimeSpan" /> value to its equivalent <see cref="string" /> representation using the specified format in the parameter.
		/// </summary>
		/// <param name="value">The <see cref="TimeSpan" /> value to convert.</param>
		/// <param name="parameter">A <see cref="string" /> specifying the format to be used for conversion.</param>
		/// <returns>
		/// An equivalent <see cref="string" /> representation of the specified value.
		/// </returns>
		public override string Convert(TimeSpan? value, string parameter)
		{
			return value?.ToStringInvariant(parameter);
		}
	}
}