using BytecodeApi.Extensions;
using System.Windows;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that compares an <see cref="object" /> with the <see cref="object" /> in the given parameter and returns a <see cref="Visibility" /> value indicating wheter the objects are not equal.
	/// </summary>
	public sealed class ObjectEqualsNotToVisibilityConverter : ConverterBase<object, object, Visibility>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectEqualsNotToVisibilityConverter" /> class.
		/// </summary>
		public ObjectEqualsNotToVisibilityConverter()
		{
		}

		/// <summary>
		/// Compares two objects and returns a <see cref="Visibility" /> value indicating their equality. Returns <see cref="Visibility.Visible" />, if <paramref name="value" /> is not equal to <paramref name="parameter" />; otherwise, <see cref="Visibility.Collapsed" />.
		/// </summary>
		/// <param name="value">An <see cref="object" /> to compare to <paramref name="parameter" />.</param>
		/// <param name="parameter">An <see cref="object" /> to compare to <paramref name="value" />.</param>
		/// <returns>
		/// <see cref="Visibility.Visible" />, if <paramref name="value" /> is not equal to <paramref name="parameter" />;
		/// otherwise, <see cref="Visibility.Collapsed" />.
		/// </returns>
		public override Visibility Convert(object value, object parameter)
		{
			return (!Equals(value, parameter)).ToVisibility();
		}
	}
}