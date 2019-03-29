using BytecodeApi.Extensions;
using System;
using System.Windows;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="IComparable" /> objects, such as integer values, to a <see cref="Visibility" /> value indicating whether the value is less than the given parameter.
	/// </summary>
	public sealed class IComparableLessToVisibilityConverter : ConverterBase<IComparable, IComparable, Visibility>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IComparableLessToVisibilityConverter" /> class.
		/// </summary>
		public IComparableLessToVisibilityConverter()
		{
		}

		/// <summary>
		/// Converts an <see cref="IComparable" /> value to a <see cref="Visibility" /> value indicating whether the value is less than the given parameter.
		/// </summary>
		/// <param name="value">An <see cref="IComparable" /> value to compare to <paramref name="parameter" />.</param>
		/// <param name="parameter">An <see cref="IComparable" /> value to compare to <paramref name="value" />.</param>
		/// <returns>
		/// <see cref="Visibility.Visible" />, if <paramref name="value" /> is less than <paramref name="parameter" />;
		/// otherwise, <see cref="Visibility.Collapsed" />.
		/// </returns>
		public override Visibility Convert(IComparable value, IComparable parameter)
		{
			return (value?.CompareTo(parameter) < 0).ToVisibility();
		}
	}
}