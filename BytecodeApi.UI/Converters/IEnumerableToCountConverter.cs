using BytecodeApi.Extensions;
using System.Collections;
using System.Linq;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts an <see cref="IEnumerable" /> to a <see cref="int" /> value representing the element count of the value.
	/// </summary>
	public sealed class IEnumerableToCountConverter : ConverterBase<IEnumerable, int>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IEnumerableToCountConverter" /> class.
		/// </summary>
		public IEnumerableToCountConverter()
		{
		}

		/// <summary>
		/// Converts an <see cref="IEnumerable" /> to a <see cref="int" /> value representing the element count of the value.
		/// </summary>
		/// <param name="value">The <see cref="IEnumerable" /> value to convert.</param>
		/// <returns>
		/// The number of elements of <paramref name="value" />,
		/// or 0, if <paramref name="value" /> is <see langword="null" />.
		/// </returns>
		public override int Convert(IEnumerable value)
		{
			return value?.Count() ?? 0;
		}
	}
}