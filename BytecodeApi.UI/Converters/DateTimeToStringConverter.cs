using BytecodeApi.Extensions;
using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="DateTime" /> values to their equivalent <see cref="string" /> representation using the <see cref="DateTimeExtensions.ToStringInvariant" /> method and the specified format in the parameter.
	/// </summary>
	public sealed class DateTimeToStringConverter : ConverterBase<DateTime?, string, string>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeToStringConverter" /> class.
		/// </summary>
		public DateTimeToStringConverter()
		{
		}

		/// <summary>
		/// Converts a <see cref="DateTime" /> value to its equivalent <see cref="string" /> representation using the specified format in the parameter.
		/// </summary>
		/// <param name="value">The <see cref="DateTime" /> value to convert.</param>
		/// <param name="parameter">A <see cref="string" /> specifying the format to be used for conversion.</param>
		/// <returns>
		/// An equivalent <see cref="string" /> representation of the specified value.
		/// </returns>
		public override string Convert(DateTime? value, string parameter)
		{
			return value?.ToStringInvariant(parameter);
		}
	}
}