using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="IComparable" /> objects, such as integer values, to a <see cref="bool" /> value indicating whether the value is less than or equal to the given parameter.
	/// </summary>
	public sealed class IComparableLessEqualToBooleanConverter : ConverterBase<IComparable, IComparable, bool>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IComparableLessEqualToBooleanConverter" /> class.
		/// </summary>
		public IComparableLessEqualToBooleanConverter()
		{
		}

		/// <summary>
		/// Converts an <see cref="IComparable" /> value to a <see cref="bool" /> value indicating whether the value is less than or equal to the given parameter.
		/// </summary>
		/// <param name="value">An <see cref="IComparable" /> value to compare to <paramref name="parameter" />.</param>
		/// <param name="parameter">An <see cref="IComparable" /> value to compare to <paramref name="value" />.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is less than or equal to <paramref name="parameter" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Convert(IComparable value, IComparable parameter)
		{
			return value?.CompareTo(parameter) <= 0;
		}
	}
}