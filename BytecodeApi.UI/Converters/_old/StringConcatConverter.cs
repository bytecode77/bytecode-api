namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that concatenates two <see cref="string" /> values. Both values can be provided using any data type. <see langword="null" /> values are treated as <see cref="string.Empty" />.
	/// </summary>
	public sealed class StringConcatConverter : ConverterBase<object, object, string>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StringConcatConverter" /> class.
		/// </summary>
		public StringConcatConverter()
		{
		}

		/// <summary>
		/// Concatenates the <see cref="string" /> representations of <paramref name="value" /> and <paramref name="parameter" /> to a new <see cref="string" />. <see langword="null" /> values are treated as <see cref="string.Empty" />.
		/// </summary>
		/// <param name="value">The first <see cref="object" /> that is converted to a <see cref="string" />.</param>
		/// <param name="parameter">The second <see cref="object" /> that is converted to a <see cref="string" /> and appended to <paramref name="value" />.</param>
		/// <returns>
		/// A new <see cref="string" />, where <paramref name="value" /> and <paramref name="parameter" /> are converted to a <see cref="string" /> and concatenated.
		/// </returns>
		public override string Convert(object value, object parameter)
		{
			return (value?.ToString() ?? "") + parameter;
		}
	}
}