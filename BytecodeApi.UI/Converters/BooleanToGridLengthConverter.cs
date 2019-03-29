using System.Windows;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="bool" /> values to a <see cref="GridLength" /> value, where <see langword="true" /> is converted to <see cref="GridLength.Auto" /> and <see langword="false" /> is converted to <see langword="new" /> <see cref="GridLength" />(0). If the given <see cref="bool" /> parameter is <see langword="true" />, the result is inverted.
	/// </summary>
	public sealed class BooleanToGridLengthConverter : ConverterBase<bool?, bool?, GridLength>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BooleanToGridLengthConverter" /> class.
		/// </summary>
		public BooleanToGridLengthConverter()
		{
		}

		/// <summary>
		/// Converts a <see cref="bool" /> value to a <see cref="GridLength" /> value, where <see langword="true" /> is converted to <see cref="GridLength.Auto" /> and <see langword="false" /> is converted to <see langword="new" /> <see cref="GridLength" />(0).
		/// </summary>
		/// <param name="value">The <see cref="bool" /> value to convert.</param>
		/// <param name="parameter"><see langword="true" /> to invert the result.</param>
		/// <returns>
		/// <see cref="GridLength.Auto" />, if <paramref name="value" /> is <see langword="true" />;
		/// otherwise, <see langword="new" /> <see cref="GridLength" />(0).
		/// If <paramref name="parameter" /> is <see langword="true" />, the result is inverted.
		/// </returns>
		public override GridLength Convert(bool? value, bool? parameter)
		{
			return value == true ^ parameter == true ? GridLength.Auto : new GridLength(0);
		}
	}
}