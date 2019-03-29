using BytecodeApi.Extensions;
using System.Collections;
using System.Linq;
using System.Windows;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts an <see cref="IEnumerable" /> to a <see cref="Visibility" /> value indicating whether the specified collection has elements or is empty. If the given <see cref="bool" /> parameter is <see langword="true" />, the result is inverted.
	/// </summary>
	public sealed class IEnumerableHasElementsToVisibilityConverter : ConverterBase<IEnumerable, bool?, Visibility>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IEnumerableHasElementsToVisibilityConverter" /> class.
		/// </summary>
		public IEnumerableHasElementsToVisibilityConverter()
		{
		}

		/// <summary>
		/// Converts an <see cref="IEnumerable" /> to a <see cref="Visibility" /> value indicating whether the specified collection has elements or is empty.
		/// </summary>
		/// <param name="value">The <see cref="IEnumerable" /> value to convert.</param>
		/// <param name="parameter"><see langword="true" /> to invert the result.</param>
		/// <returns>
		/// <see cref="Visibility.Visible" />, if <paramref name="value" /> is not empty;
		/// otherwise, <see cref="Visibility.Collapsed" />.
		/// If <paramref name="parameter" /> is <see langword="true" />, the result is inverted.
		/// </returns>
		public override Visibility Convert(IEnumerable value, bool? parameter)
		{
			return (value?.Cast<object>().Any() == true ^ parameter == true).ToVisibility();
		}
	}
}