using System.Windows;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts objects to a <see cref="GridLength" /> value, where <see langword="null" /> is converted to <see langword="new" /> <see cref="GridLength" />(0) and <see langword="false" /> is converted to <see cref="GridLength.Auto" />. If the given <see cref="bool" /> parameter is <see langword="true" />, the result is inverted.
	/// </summary>
	public sealed class ObjectIsNullToGridLengthConverter : ConverterBase<object, bool?, GridLength>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectIsNullToGridLengthConverter" /> class.
		/// </summary>
		public ObjectIsNullToGridLengthConverter()
		{
		}

		/// <summary>
		/// Converts an <see cref="object" /> to a <see cref="GridLength" /> value indicating whether the given value is <see langword="null" />. Returns <see langword="new" /> <see cref="GridLength" />(0), if <paramref name="value" /> is <see langword="null" />; otherwise, <see cref="GridLength.Auto" />.
		/// </summary>
		/// <param name="value">An <see cref="object" /> to convert.</param>
		/// <param name="parameter"><see langword="true" /> to invert the result.</param>
		/// <returns>
		/// <see cref="GridLength.Auto" />, if <paramref name="value" /> is <see langword="null" />;
		/// otherwise, <see langword="new" /> <see cref="GridLength" />(0).
		/// If <paramref name="parameter" /> is <see langword="true" />, the result is inverted.
		/// </returns>
		public override GridLength Convert(object value, bool? parameter)
		{
			return value == null ^ parameter == true ? new GridLength(0) : GridLength.Auto;
		}
	}
}