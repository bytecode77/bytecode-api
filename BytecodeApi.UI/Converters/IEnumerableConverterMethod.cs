using System.Collections;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Specifies the method that is used to convert <see cref="IEnumerable" /> values.
	/// </summary>
	public enum IEnumerableConverterMethod
	{
		/// <summary>
		/// Returns the number of elements in the <see cref="IEnumerable" /> value.
		/// </summary>
		Count,
		/// <summary>
		/// Returns a <see cref="string" /> from the <see cref="IEnumerable" /> value of objects. All objects in the <see cref="IEnumerable" /> value are converted to <see cref="string" /> by calling <see cref="object.ToString" />. Then, the resulting strings are concatenated.
		/// </summary>
		JoinStrings,
		/// <summary>
		/// Returns a <see cref="string" /> from the <see cref="IEnumerable" /> value of objects. All objects in the <see cref="IEnumerable" /> value are converted to <see cref="string" /> by calling <see cref="object.ToString" />. Then, the resulting strings are concatenated using ", " as separator.
		/// </summary>
		JoinStringsComma,
		/// <summary>
		/// Returns a multiline <see cref="string" /> from the <see cref="IEnumerable" /> collection of objects, by concatenating all values with a CRLF. All objects in the <see cref="IEnumerable" /> value are converted to <see cref="string" /> by calling <see cref="object.ToString" />.
		/// </summary>
		AsMultilineString,
		/// <summary>
		/// Returns <see langword="true" />, if all values from the <see cref="IEnumerable" /> value are equal to <see langword="true" />;
		/// <see langword="false" />, if all values are equal to <see langword="false" /> or the <see cref="IEnumerable" /> has no elements;
		/// otherwise, <see langword="null" />.
		/// The value must be a collection of <see cref="bool" /> or <see cref="bool" />? values.
		/// </summary>
		BooleansToIndeterminate
		//CURRENT: Additional methods
	}
}