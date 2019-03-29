using BytecodeApi.Extensions;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="string" /> values to a <see cref="bool" /> value indicating wheter the <see cref="string" /> is <see langword="null" />, <see cref="string.Empty" />, or consists only of white-space characters. If the given <see cref="bool" /> parameter is <see langword="true" />, the result is inverted.
	/// </summary>
	public sealed class StringIsNullOrWhiteSpaceToBooleanConverter : ConverterBase<string, bool?, bool>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StringIsNullOrWhiteSpaceToBooleanConverter" /> class.
		/// </summary>
		public StringIsNullOrWhiteSpaceToBooleanConverter()
		{
		}

		/// <summary>
		/// Converts a <see cref="string" /> value to a <see cref="bool" /> value indicating whether the <see cref="string" /> value is <see langword="null" />, <see cref="string.Empty" />, or consists only of white-space characters.
		/// </summary>
		/// <param name="value">A <see cref="string" /> value to convert.</param>
		/// <param name="parameter"><see langword="true" /> to invert the result.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is not <see langword="null" />, <see cref="string.Empty" />, or consists only of white-space characters;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Convert(string value, bool? parameter)
		{
			return !value.IsNullOrWhiteSpace() ^ parameter == true;
		}
	}
}