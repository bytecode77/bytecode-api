using BytecodeApi.Text;
using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="TimeSpan" /> values to their equivalent <see cref="string" /> representation in human readable form using the <see cref="Wording.FormatTimeSpanString(TimeSpan)" /> method. The parameter specifies the maxElement parameter in the <see cref="Wording.FormatTimeSpanString(TimeSpan, int)" /> method.
	/// </summary>
	public sealed class TimeSpanToHumanReadableStringConverter : ConverterBase<TimeSpan?, int?, string>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeSpanToHumanReadableStringConverter" /> class.
		/// </summary>
		public TimeSpanToHumanReadableStringConverter()
		{
		}

		/// <summary>
		/// Converts a <see cref="TimeSpan" /> value to its equivalent <see cref="string" /> representation in human readable form using the <see cref="Wording.FormatTimeSpanString(TimeSpan)" /> method. The parameter specifies the maxElement parameter in the <see cref="Wording.FormatTimeSpanString(TimeSpan, int)" /> method.
		/// </summary>
		/// <param name="value">The <see cref="TimeSpan" /> value to convert.</param>
		/// <param name="parameter">A <see cref="int" /> specifying the maxElement parameter in the <see cref="Wording.FormatTimeSpanString(TimeSpan, int)" /> method.</param>
		/// <returns>
		/// An equivalent <see cref="string" /> representation of the specified value.
		/// </returns>
		public override string Convert(TimeSpan? value, int? parameter)
		{
			if (value == null) return null;
			else return parameter == null ? Wording.FormatTimeSpanString(value.Value) : Wording.FormatTimeSpanString(value.Value, parameter.Value);
		}
	}
}