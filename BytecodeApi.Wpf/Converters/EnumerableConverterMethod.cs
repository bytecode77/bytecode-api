using System.Collections;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to convert <see cref="IEnumerable" /> values.
/// </summary>
public enum EnumerableConverterMethod
{
	/// <summary>
	/// Returns the first element in the <see cref="IEnumerable" /> value.
	/// </summary>
	First,
	/// <summary>
	/// Returns the first element in the <see cref="IEnumerable" /> value, or a default value if the sequence contains no elements.
	/// </summary>
	FirstOrDefault,
	/// <summary>
	/// Returns the last element in the <see cref="IEnumerable" /> value.
	/// </summary>
	Last,
	/// <summary>
	/// Returns the last element in the <see cref="IEnumerable" /> value, or a default value if the sequence contains no elements.
	/// </summary>
	LastOrDefault,
	/// <summary>
	/// Returns the element in the <see cref="IEnumerable" /> value at the index as specified in the <see cref="int" /> parameter.
	/// </summary>
	ElementAt,
	/// <summary>
	/// Returns the element in the <see cref="IEnumerable" /> value at the index as specified in the <see cref="int" /> parameter or a default value if the index is out of range.
	/// </summary>
	ElementAtOrDefault,
	/// <summary>
	/// Returns the number of elements in the <see cref="IEnumerable" /> value.
	/// </summary>
	Count,
	/// <summary>
	/// Returns <see langword="true" />, if the specified parameter was found in the <see cref="IEnumerable" /> value;
	/// <see langword="false" />.
	/// </summary>
	Contains,
	/// <summary>
	/// Bypasses a number of elements in the <see cref="IEnumerable" /> value as specified in the <see cref="int" /> parameter and then returns the remaining elements.
	/// </summary>
	Skip,
	/// <summary>
	/// Returns a number of contiguous elements as specified in the <see cref="int" /> parameter from the start of the <see cref="IEnumerable" /> value.
	/// </summary>
	Take,
	/// <summary>
	/// Returns a <see cref="string" /> from the <see cref="IEnumerable" /> value of objects. All objects in the <see cref="IEnumerable" /> value are converted to <see cref="string" /> by calling <see cref="object.ToString" />. Then, the resulting strings are concatenated using the separator specified in the <see cref="string" /> parameter.
	/// </summary>
	JoinStrings,
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
}