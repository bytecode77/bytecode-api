namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="bool" /> values to their inverted <see cref="bool" /> representation.
	/// </summary>
	public sealed class BooleanToInverseConverter : ConverterBaseExtended<bool, bool, bool, bool>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BooleanToInverseConverter" /> class.
		/// </summary>
		public BooleanToInverseConverter()
		{
		}

		/// <summary>
		/// Converts a <see cref="bool" /> value to its inverted representation.
		/// </summary>
		/// <param name="value">The <see cref="bool" /> value to convert.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is <see langword="false" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Convert(bool value)
		{
			return !value;
		}
		/// <summary>
		/// Converts an inverted <see cref="bool" /> value back to its original representation. This method returns an equal result to <see cref="Convert" />.
		/// </summary>
		/// <param name="value">The <see cref="bool" /> value to convert.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is <see langword="false" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool ConvertBack(bool value)
		{
			return !value;
		}
	}
}