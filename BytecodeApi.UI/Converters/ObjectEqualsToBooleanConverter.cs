namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that compares an <see cref="object" /> with the <see cref="object" /> in the given parameter and returns <see langword="true" />, if the objects are equal.
	/// </summary>
	public sealed class ObjectEqualsToBooleanConverter : ConverterBase<object, object, bool>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectEqualsToBooleanConverter" /> class.
		/// </summary>
		public ObjectEqualsToBooleanConverter()
		{
		}

		/// <summary>
		/// Compares two objects and returns a <see cref="bool" /> value indicating their equality. Returns <see langword="true" />, if <paramref name="value" /> is equal to <paramref name="parameter" />; otherwise, <see langword="false" />.
		/// </summary>
		/// <param name="value">An <see cref="object" /> to compare to <paramref name="parameter" />.</param>
		/// <param name="parameter">An <see cref="object" /> to compare to <paramref name="value" />.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is equal to <paramref name="parameter" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Convert(object value, object parameter)
		{
			return Equals(value, parameter);
		}
	}
}