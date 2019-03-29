using BytecodeApi.Extensions;
using System.Windows;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="bool" /> values to <see cref="Visibility" /> enumeration values. If the given <see cref="bool" /> parameter is <see langword="true" />, the result is inverted.
	/// </summary>
	public sealed class BooleanToVisibilityConverter : ConverterBase<bool?, bool?, Visibility>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BooleanToVisibilityConverter" /> class.
		/// </summary>
		public BooleanToVisibilityConverter()
		{
		}

		/// <summary>
		/// Converts a <see cref="bool" /> value to a <see cref="Visibility" /> value.
		/// </summary>
		/// <param name="value">The <see cref="bool" /> value to convert.</param>
		/// <param name="parameter"><see langword="true" /> to invert the result.</param>
		/// <returns>
		/// <see cref="Visibility.Visible" />, if <paramref name="value" /> is <see langword="true" />;
		/// otherwise, <see cref="Visibility.Collapsed" />.
		/// If <paramref name="parameter" /> is <see langword="true" />, the result is inverted.
		/// </returns>
		public override Visibility Convert(bool? value, bool? parameter)
		{
			return (value == true ^ parameter == true).ToVisibility();
		}
	}
}