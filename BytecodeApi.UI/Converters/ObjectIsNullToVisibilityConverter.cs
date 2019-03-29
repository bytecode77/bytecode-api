using BytecodeApi.Extensions;
using System.Windows;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts objects to a <see cref="Visibility" /> value indicating wheter the <see cref="object" /> is <see langword="null" />.
	/// </summary>
	public sealed class ObjectIsNullToVisibilityConverter : ConverterBase<object, bool?, Visibility>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectIsNullToVisibilityConverter" /> class.
		/// </summary>
		public ObjectIsNullToVisibilityConverter()
		{
		}

		/// <summary>
		/// Converts an <see cref="object" /> to a <see cref="Visibility" /> value indicating whether the given value is <see langword="null" />.
		/// </summary>
		/// <param name="value">An <see cref="object" /> to convert.</param>
		/// <param name="parameter"><see langword="true" /> to invert the result.</param>
		/// <returns>
		/// <see cref="Visibility.Visible" />, if <paramref name="value" /> is not <see langword="null" />;
		/// otherwise, <see cref="Visibility.Collapsed" />.
		/// </returns>
		public override Visibility Convert(object value, bool? parameter)
		{
			return (value != null ^ parameter == true).ToVisibility();
		}
	}
}