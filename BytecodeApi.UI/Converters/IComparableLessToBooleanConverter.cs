using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="IComparable" /> objects, such as integer values, to a <see cref="bool" /> value indicating whether the value is less than the given parameter.
	/// </summary>
	public sealed class IComparableLessToBooleanConverter : ConverterBase<IComparable, IComparable, bool>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IComparableLessToBooleanConverter" /> class.
		/// </summary>
		public IComparableLessToBooleanConverter()
		{
		}

		/// <summary>
		/// Converts an <see cref="IComparable" /> value to a <see cref="bool" /> value indicating whether the value is less than the given parameter.
		/// </summary>
		/// <param name="value">An <see cref="IComparable" /> value to compare to <paramref name="parameter" />.</param>
		/// <param name="parameter">An <see cref="IComparable" /> value to compare to <paramref name="value" />.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is less than <paramref name="parameter" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Convert(IComparable value, IComparable parameter)
		{
			return value?.CompareTo(parameter) < 0;
		}
	}
}