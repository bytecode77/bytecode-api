namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts objects to a <see cref="bool" /> value indicating wheter the <see cref="object" /> is <see langword="null" />.
	/// </summary>
	public sealed class ObjectIsNullToBooleanConverter : ConverterBase<object, bool?, bool>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectIsNullToBooleanConverter" /> class.
		/// </summary>
		public ObjectIsNullToBooleanConverter()
		{
		}

		/// <summary>
		/// Converts an <see cref="object" /> to a <see cref="bool" /> value indicating whether the given value is <see langword="null" />.
		/// </summary>
		/// <param name="value">An <see cref="object" /> to convert.</param>
		/// <param name="parameter"><see langword="true" /> to invert the result.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is not <see langword="null" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Convert(object value, bool? parameter)
		{
			return value != null ^ parameter == true;
		}
	}
}