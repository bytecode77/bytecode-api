using System.Collections;
using System.Linq;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts an <see cref="IEnumerable" /> to a <see cref="bool" /> value indicating whether the specified collection has elements or is empty. If the given <see cref="bool" /> parameter is <see langword="true" />, the result is inverted.
	/// </summary>
	public sealed class IEnumerableHasElementsToBooleanConverter : ConverterBase<IEnumerable, bool?, bool>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IEnumerableHasElementsToBooleanConverter" /> class.
		/// </summary>
		public IEnumerableHasElementsToBooleanConverter()
		{
		}

		/// <summary>
		/// Converts an <see cref="IEnumerable" /> to a <see cref="bool" /> value indicating whether the specified collection has elements or is empty.
		/// </summary>
		/// <param name="value">The <see cref="IEnumerable" /> value to convert.</param>
		/// <param name="parameter"><see langword="true" /> to invert the result.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is not empty;
		/// otherwise, <see langword="false" />.
		/// If <paramref name="parameter" /> is <see langword="true" />, the result is inverted.
		/// </returns>
		public override bool Convert(IEnumerable value, bool? parameter)
		{
			return value?.Cast<object>().Any() == true ^ parameter == true;
		}
	}
}