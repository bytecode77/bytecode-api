using System.Collections;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for string manipulations on structures that implement <see cref="IEnumerable" />.
/// </summary>
public static class EnumerableStringExtensions
{
	/// <summary>
	/// Creates a <see cref="string" /> from this collection of characters.
	/// </summary>
	/// <param name="source">The collection of characters to create the <see cref="string" /> from.</param>
	/// <returns>
	/// A new <see cref="string" />, created from this collection of characters.
	/// </returns>
	public static string AsString(this IEnumerable<char> source)
	{
		Check.ArgumentNull(source);

		return source is char[] chars ? new(chars) : new(source.ToArray());
	}
	/// <summary>
	/// Concatenates all elements of this <see cref="IEnumerable{T}" />, thereby converting each element to a <see cref="string" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements to concatenate.</typeparam>
	/// <param name="source">A sequence of values to concatenate.</param>
	/// <returns>
	/// A <see cref="string" /> with all elements of <paramref name="source" />.
	/// </returns>
	public static string AsString<TSource>(this IEnumerable<TSource> source)
	{
		return source.AsString(null);
	}
	/// <summary>
	/// Concatenates all elements of this <see cref="IEnumerable{T}" />, thereby converting each element to a <see cref="string" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements to concatenate.</typeparam>
	/// <param name="source">A sequence of values to concatenate.</param>
	/// <param name="separator">A <see cref="string" /> value specifying the separator between each <see cref="string" /> value.</param>
	/// <returns>
	/// A <see cref="string" /> with all elements of <paramref name="source" />.
	/// </returns>
	public static string AsString<TSource>(this IEnumerable<TSource> source, string? separator)
	{
		Check.ArgumentNull(source);

		return string.Join(separator, source);
	}
	/// <summary>
	/// Creates a multiline <see cref="string" /> from this collection of strings, by concatenating all values with a CRLF.
	/// </summary>
	/// <param name="source">The <see cref="string" />[] to concatenate.</param>
	/// <returns>
	/// A new <see cref="string" /> that represents this <see cref="string" /> collection as multiline text, where each element is concatenated with a CRLF.
	/// </returns>
	public static string AsMultilineString(this IEnumerable<string?> source)
	{
		return source.AsMultilineString(false);
	}
	/// <summary>
	/// Creates a multiline <see cref="string" /> from this collection of strings, by concatenating all values with a CRLF.
	/// </summary>
	/// <param name="source">The <see cref="string" />[] to concatenate.</param>
	/// <param name="trailingNewLine"><see langword="true" /> to add an additional CRLF at the end.</param>
	/// <returns>
	/// A new <see cref="string" /> that represents this <see cref="string" /> collection as multiline text, where each element is concatenated with a CRLF.
	/// </returns>
	public static string AsMultilineString(this IEnumerable<string?> source, bool trailingNewLine)
	{
		Check.ArgumentNull(source);

		return source.AsString("\r\n") + (trailingNewLine ? "\r\n" : null);
	}
}