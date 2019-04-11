using BytecodeApi.Extensions;
using System.Collections;
using System.Collections.Generic;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts an <see cref="IEnumerable{T}" /> of type <see cref="bool" /> or <see cref="bool" />? to a <see cref="bool" />? value representing an indeterminate indicator, using <see cref="IEnumerableExtensions.ToIndeterminate(IEnumerable{bool})" />.
	/// </summary>
	//CURRENT: !To be replaced by new converters
	public sealed class BooleanCollectionToIndeterminateConverter : ConverterBase<IEnumerable, bool?>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BooleanCollectionToIndeterminateConverter" /> class.
		/// </summary>
		public BooleanCollectionToIndeterminateConverter()
		{
		}

		/// <summary>
		/// Converts an <see cref="IEnumerable{T}" /> of type <see cref="bool" /> or <see cref="bool" />? to a <see cref="bool" />? value representing an indeterminate indicator, using <see cref="IEnumerableExtensions.ToIndeterminate(IEnumerable{bool})" />.
		/// </summary>
		/// <param name="value">The <see cref="IEnumerable" /> value to convert that is either of type <see cref="bool" /> or <see cref="bool" />?.</param>
		/// <returns>
		/// A <see cref="bool" />? value representing an indeterminate indicator.
		/// </returns>
		public override bool? Convert(IEnumerable value)
		{
			if (value == null) return false;
			else if (value is IEnumerable<bool> booleanCollection) return booleanCollection.ToIndeterminate();
			else if (value is IEnumerable<bool?> nullableBooleanCollection) return nullableBooleanCollection.ToIndeterminate();
			else throw Throw.UnsupportedType(nameof(value));
		}
	}
}