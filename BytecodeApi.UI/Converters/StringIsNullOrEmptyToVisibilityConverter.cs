using BytecodeApi.Extensions;
using System.Windows;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="string" /> values to a <see cref="Visibility" /> value indicating wheter the <see cref="string" /> is <see langword="null" /> or <see cref="string.Empty" />. If the given <see cref="bool" /> parameter is <see langword="true" />, the result is inverted.
	/// </summary>
	public sealed class StringIsNullOrEmptyToVisibilityConverter : ConverterBase<string, bool?, Visibility>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StringIsNullOrEmptyToVisibilityConverter" /> class.
		/// </summary>
		public StringIsNullOrEmptyToVisibilityConverter()
		{
		}

		/// <summary>
		/// Converts a <see cref="string" /> value to a <see cref="Visibility" /> value indicating whether the <see cref="string" /> value is <see langword="null" /> or <see cref="string.Empty" />.
		/// </summary>
		/// <param name="value">A <see cref="string" /> value to convert.</param>
		/// <param name="parameter"><see langword="true" /> to invert the result.</param>
		/// <returns>
		/// <see cref="Visibility.Visible" />, if <paramref name="value" /> is not <see langword="null" /> or <see cref="string.Empty" />;
		/// otherwise, <see cref="Visibility.Collapsed" />.
		/// </returns>
		public override Visibility Convert(string value, bool? parameter)
		{
			return (!value.IsNullOrEmpty() ^ parameter == true).ToVisibility();
		}
	}
}