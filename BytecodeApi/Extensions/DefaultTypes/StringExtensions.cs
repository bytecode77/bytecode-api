using System.Globalization;
using System.Text;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="string" /> objects.
/// </summary>
public static class StringExtensions
{
	private static readonly string SwapPlaceholder = Create.Guid()[..8];

	/// <summary>
	/// Indicates whether this <see cref="string" /> is <see langword="null" /> or <see cref="string.Empty" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <returns>
	/// <see langword="true" />, if this <see cref="string" /> is <see langword="null" /> or <see cref="string.Empty" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str)
	{
		return string.IsNullOrEmpty(str);
	}
	/// <summary>
	/// Indicates whether this <see cref="string" /> is <see langword="null" />, <see cref="string.Empty" />, or consists only of white-space characters.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <returns>
	/// <see langword="true" />, if this <see cref="string" /> is <see langword="null" />, <see cref="string.Empty" />, or consists only of white-space characters;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str)
	{
		return string.IsNullOrWhiteSpace(str);
	}
	/// <summary>
	/// Determines whether the beginning of this <see cref="string" /> matches any of the specified strings in <paramref name="values" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="string" /> values to match the beginning of <paramref name="str" /> against.</param>
	/// <returns>
	/// <see langword="true" />, if the beginning of this <see cref="string" /> matches any of the specified strings in <paramref name="values" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool StartsWithAny(this string str, params string[] values)
	{
		return str.StartsWithAny(values, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Determines whether the beginning of this <see cref="string" /> matches any of the specified strings in <paramref name="values" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="string" /> values to match the beginning of <paramref name="str" /> against.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// <see langword="true" />, if the beginning of this <see cref="string" /> matches any of the specified strings in <paramref name="values" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool StartsWithAny(this string str, string[] values, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(values);
		Check.ArgumentEx.ArrayValuesNotNull(values);

		foreach (string value in values)
		{
			if (str.StartsWith(value, comparison))
			{
				return true;
			}
		}

		return false;
	}
	/// <summary>
	/// Determines whether the beginning of this <see cref="string" /> matches any of the specified characters in <paramref name="values" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="char" /> values to match the beginning of <paramref name="str" /> against.</param>
	/// <returns>
	/// <see langword="true" />, if the beginning of this <see cref="string" /> matches any of the specified characters in <paramref name="values" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool StartsWithAny(this string str, params char[] values)
	{
		return str.StartsWithAny(values, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Determines whether the beginning of this <see cref="string" /> matches any of the specified characters in <paramref name="values" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="char" /> values to match the beginning of <paramref name="str" /> against.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// <see langword="true" />, if the beginning of this <see cref="string" /> matches any of the specified characters in <paramref name="values" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool StartsWithAny(this string str, char[] values, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(values);

		foreach (char value in values)
		{
			if (str.IndexOf(value, comparison) == 0)
			{
				return true;
			}
		}

		return false;
	}
	/// <summary>
	/// Determines whether the end of this <see cref="string" /> matches any of the specified strings in <paramref name="values" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="string" /> values to match the end of <paramref name="str" /> against.</param>
	/// <returns>
	/// <see langword="true" />, if the end of this <see cref="string" /> matches any of the specified strings in <paramref name="values" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool EndsWithAny(this string str, params string[] values)
	{
		return str.EndsWithAny(values, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Determines whether the end of this <see cref="string" /> matches any of the specified strings in <paramref name="values" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="string" /> values to match the end of <paramref name="str" /> against.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// <see langword="true" />, if the end of this <see cref="string" /> matches any of the specified strings in <paramref name="values" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool EndsWithAny(this string str, string[] values, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(values);
		Check.ArgumentEx.ArrayValuesNotNull(values);

		foreach (string value in values)
		{
			if (str.EndsWith(value, comparison))
			{
				return true;
			}
		}

		return false;
	}
	/// <summary>
	/// Determines whether the end of this <see cref="string" /> matches any of the specified characters in <paramref name="values" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="char" /> values to match the end of <paramref name="str" /> against.</param>
	/// <returns>
	/// <see langword="true" />, if the end of this <see cref="string" /> matches any of the specified characters in <paramref name="values" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool EndsWithAny(this string str, params char[] values)
	{
		return str.EndsWithAny(values, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Determines whether the end of this <see cref="string" /> matches any of the specified characters in <paramref name="values" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="char" /> values to match the end of <paramref name="str" /> against.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// <see langword="true" />, if the end of this <see cref="string" /> matches any of the specified characters in <paramref name="values" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool EndsWithAny(this string str, char[] values, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(values);

		foreach (char value in values)
		{
			if (str.LastIndexOf(value.ToString(), comparison) == str.Length - 1)
			{
				return true;
			}
		}

		return false;
	}
	/// <summary>
	/// Determines whether any of the specified strings in <paramref name="values" /> occurs within this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="string" /> values to search in <paramref name="str" />.</param>
	/// <returns>
	/// <see langword="true" />, if any of the specified strings in <paramref name="values" /> occurs within this <see cref="string" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool ContainsAny(this string str, params string[] values)
	{
		return str.ContainsAny(values, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Determines whether any of the specified strings in <paramref name="values" /> occurs within this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="string" /> values to search in <paramref name="str" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// <see langword="true" />, if any of the specified strings in <paramref name="values" /> occurs within this <see cref="string" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool ContainsAny(this string str, string[] values, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(values);
		Check.ArgumentEx.ArrayValuesNotNull(values);

		foreach (string value in values)
		{
			if (str.Contains(value, comparison))
			{
				return true;
			}
		}

		return false;
	}
	/// <summary>
	/// Determines whether any of the specified characters in <paramref name="values" /> occurs within this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="char" /> values to search in <paramref name="str" />.</param>
	/// <returns>
	/// <see langword="true" />, if any of the specified characters in <paramref name="values" /> occurs within this <see cref="string" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool ContainsAny(this string str, params char[] values)
	{
		return str.ContainsAny(values, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Determines whether any of the specified characters in <paramref name="values" /> occurs within this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to test.</param>
	/// <param name="values">An array of <see cref="char" /> values to search in <paramref name="str" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// <see langword="true" />, if any of the specified characters in <paramref name="values" /> occurs within this <see cref="string" />.
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool ContainsAny(this string str, char[] values, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(values);

		foreach (char value in values)
		{
			if (str.Contains(value, comparison))
			{
				return true;
			}
		}

		return false;
	}
	/// <summary>
	/// Returns <see langword="null" />, if this <see cref="string" /> is <see cref="string.Empty" />, otherwise, its original value.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to convert.</param>
	/// <returns>
	/// <see langword="null" />, if this <see cref="string" /> is <see cref="string.Empty" />;
	/// otherwise, its original value.
	/// </returns>
	public static string? ToNullIfEmpty(this string? str)
	{
		return str.IsNullOrEmpty() ? null : str;
	}
	/// <summary>
	/// Returns <see langword="null" />, if this <see cref="string" /> is <see cref="string.Empty" />, or consists only of white-space characters, otherwise, its original value.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to convert.</param>
	/// <returns>
	/// <see langword="null" />, if this <see cref="string" /> is <see cref="string.Empty" />, or consists only of white-space characters;
	/// otherwise, its original value.
	/// </returns>
	public static string? ToNullIfEmptyOrWhiteSpace(this string? str)
	{
		return str.IsNullOrWhiteSpace() ? null : str;
	}
	/// <summary>
	/// Replaces the format item in a specified <see cref="string" /> with the <see cref="string" /> representation of a corresponding <see cref="object" /> in a specified array.
	/// </summary>
	/// <param name="str">A composite format <see cref="string" />.</param>
	/// <param name="args">An <see cref="object" /> array that contains zero or more objects to format.</param>
	/// <returns>
	/// A copy of this <see cref="string" /> in which the format items have been replaced by the <see cref="string" /> representation of the corresponding objects in <paramref name="args" />.
	/// </returns>
	public static string Format(this string str, params object?[] args)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(args);

		return string.Format(str, args);
	}
	/// <summary>
	/// Replaces the format item in a specified <see cref="string" /> with the <see cref="string" /> representation of a corresponding <see cref="object" /> in a specified array using the invariant culture.
	/// </summary>
	/// <param name="str">A composite format <see cref="string" />.</param>
	/// <param name="args">An <see cref="object" /> array that contains zero or more objects to format.</param>
	/// <returns>
	/// A copy of this <see cref="string" /> in which the format items have been replaced by the <see cref="string" /> representation of the corresponding objects in <paramref name="args" />.
	/// </returns>
	public static string FormatInvariant(this string str, params object?[] args)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(args);

		return string.Format(CultureInfo.InvariantCulture, str, args);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFrom(this string str, string value)
	{
		return str.SubstringFrom(value, false);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFrom(this string str, string value, bool inclusive)
	{
		return str.SubstringFrom(value, inclusive, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFrom(this string str, string value, bool inclusive, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(value);

		int index = str.IndexOf(value, comparison);

		if (index == -1)
		{
			return str;
		}

		if (!inclusive)
		{
			index += value.Length;
		}

		return str[index..];
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />, excluding the searched <see cref="char" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />, excluding the searched <see cref="char" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFrom(this string str, char value)
	{
		return str.SubstringFrom(value, false);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFrom(this string str, char value, bool inclusive)
	{
		return str.SubstringFrom(value, inclusive, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFrom(this string str, char value, bool inclusive, StringComparison comparison)
	{
		Check.ArgumentNull(str);

		int index = str.IndexOf(value, comparison);

		if (index == -1)
		{
			return str;
		}

		if (!inclusive)
		{
			index++;
		}

		return str[index..];
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFromLast(this string str, string value)
	{
		return str.SubstringFromLast(value, false);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFromLast(this string str, string value, bool inclusive)
	{
		return str.SubstringFromLast(value, inclusive, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFromLast(this string str, string value, bool inclusive, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(value);

		int index = str.LastIndexOf(value, comparison);

		if (index == -1)
		{
			return str;
		}

		if (!inclusive)
		{
			index += value.Length;
		}

		return str[index..];
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />, excluding the searched <see cref="char" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />, excluding the searched <see cref="char" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFromLast(this string str, char value)
	{
		return str.SubstringFromLast(value, false);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFromLast(this string str, char value, bool inclusive)
	{
		return str.SubstringFromLast(value, inclusive, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> after the last occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringFromLast(this string str, char value, bool inclusive, StringComparison comparison)
	{
		Check.ArgumentNull(str);

		int index = str.LastIndexOf(value.ToString(), comparison);

		if (index == -1)
		{
			return str;
		}

		if (!inclusive)
		{
			index++;
		}

		return str[index..];
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntil(this string str, string value)
	{
		return str.SubstringUntil(value, false);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntil(this string str, string value, bool inclusive)
	{
		return str.SubstringUntil(value, inclusive, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntil(this string str, string value, bool inclusive, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(value);

		int index = str.IndexOf(value, comparison);

		if (index == -1)
		{
			return str;
		}

		if (inclusive)
		{
			index += value.Length;
		}

		return str[..index];
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />, excluding the searched <see cref="char" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />, excluding the searched <see cref="char" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntil(this string str, char value)
	{
		return str.SubstringUntil(value, false);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntil(this string str, char value, bool inclusive)
	{
		return str.SubstringUntil(value, inclusive, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntil(this string str, char value, bool inclusive, StringComparison comparison)
	{
		Check.ArgumentNull(str);

		int index = str.IndexOf(value, comparison);

		if (index == -1)
		{
			return str;
		}

		if (inclusive)
		{
			index++;
		}

		return str[..index];
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntilLast(this string str, string value)
	{
		return str.SubstringUntilLast(value, false);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntilLast(this string str, string value, bool inclusive)
	{
		return str.SubstringUntilLast(value, inclusive, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntilLast(this string str, string value, bool inclusive, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(value);

		int index = str.LastIndexOf(value, comparison);

		if (index == -1)
		{
			return str;
		}

		if (inclusive)
		{
			index += value.Length;
		}

		return str[..index];
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />, excluding the searched <see cref="char" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />, excluding the searched <see cref="char" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntilLast(this string str, char value)
	{
		return str.SubstringUntilLast(value, false);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntilLast(this string str, char value, bool inclusive)
	{
		return str.SubstringUntilLast(value, inclusive, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Returns the portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="value">The <see cref="char" /> that is searched for in <paramref name="str" />.</param>
	/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The portion of this <see cref="string" /> before the last occurrence of <paramref name="value" />.
	/// Returns the original <see cref="string" />, if <paramref name="value" /> was not found within <paramref name="str" />.
	/// </returns>
	public static string SubstringUntilLast(this string str, char value, bool inclusive, StringComparison comparison)
	{
		Check.ArgumentNull(str);

		int index = str.LastIndexOf(value.ToString(), comparison);

		if (index == -1)
		{
			return str;
		}

		if (inclusive)
		{
			index++;
		}

		return str[..index];
	}
	/// <summary>
	/// Returns a <see cref="string" /> containing a specified number of characters from the left side of this <see cref="string" />. If this <see cref="string" /> is longer than <paramref name="length" />, the <see cref="string" /> is truncated by the specified <paramref name="length" /> parameter, otherwise, the original <see cref="string" /> is returned.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be truncated.</param>
	/// <param name="length">The amount of characters to which this <see cref="string" /> is truncated.</param>
	/// <returns>
	/// If this <see cref="string" /> is longer than <paramref name="length" />, the fraction truncated by the specified <paramref name="length" /> parameter;
	/// otherwise, the original <see cref="string" />.
	/// </returns>
	public static string Left(this string str, int length)
	{
		Check.ArgumentNull(str);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(length);

		return str.Length <= length ? str : str[..length];
	}
	/// <summary>
	/// Returns a <see cref="string" /> containing a specified number of characters from the right side of this <see cref="string" />. If this <see cref="string" /> is longer than <paramref name="length" />, the <see cref="string" /> is truncated by the specified <paramref name="length" /> parameter, otherwise, the original <see cref="string" /> is returned.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be truncated.</param>
	/// <param name="length">The amount of characters to which this <see cref="string" /> is truncated.</param>
	/// <returns>
	/// If this <see cref="string" /> is longer than <paramref name="length" />, the fraction truncated by the specified <paramref name="length" /> parameter;
	/// otherwise, the original <see cref="string" />.
	/// </returns>
	public static string Right(this string str, int length)
	{
		Check.ArgumentNull(str);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(length);

		return str.Length <= length ? str : str.Substring(str.Length - length, length);
	}
	/// <summary>
	/// Returns a new <see cref="string" /> that aligns the characters in this <see cref="string" /> by padding them on both the right and the left with spaces, for a specified total length, biased to the left in case right and left padding width differes by one.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be center padded.</param>
	/// <param name="totalWidth">The number of characters in the resulting <see cref="string" />, equal to the number of original characters plus any additional padding characters.</param>
	/// <returns>
	/// A new <see cref="string" /> that is equivalent to this <see cref="string" />, but centered on the left and the right with as many spaces as needed to create a length of <paramref name="totalWidth" />. If <paramref name="totalWidth" /> is less than or equal to the length of this <see cref="string" />, the method returns the original <see cref="string" />.
	/// </returns>
	public static string PadCenter(this string str, int totalWidth)
	{
		return str.PadCenter(totalWidth, ' ');
	}
	/// <summary>
	/// Returns a new <see cref="string" /> that aligns the characters in this <see cref="string" /> by padding them on both the right and the left with a specified unicode character, for a specified total length, biased to the left in case right and left padding width differes by one.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be center padded.</param>
	/// <param name="totalWidth">The number of characters in the resulting <see cref="string" />, equal to the number of original characters plus any additional padding characters.</param>
	/// <param name="paddingChar">The padding character.</param>
	/// <returns>
	/// A new <see cref="string" /> that is equivalent to this <see cref="string" />, but centered on the left and the right with as many padding characters as needed to create a length of <paramref name="totalWidth" />. If <paramref name="totalWidth" /> is less than or equal to the length of this <see cref="string" />, the method returns the original <see cref="string" />.
	/// </returns>
	public static string PadCenter(this string str, int totalWidth, char paddingChar)
	{
		Check.ArgumentNull(str);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(totalWidth);

		return (paddingChar.Repeat(Math.Max(totalWidth - str.Length, 0) / 2) + str).PadRight(totalWidth, paddingChar);
	}
	/// <summary>
	/// Removes all leading occurrences of the specified <see cref="string" /> from this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="removedString">A <see cref="string" /> that is removed from the beginning of this <see cref="string" />.</param>
	/// <returns>
	/// The <see cref="string" /> that remains after all occurrences of <paramref name="removedString" /> are removed from the start of this <see cref="string" />.
	/// </returns>
	public static string TrimStartString(this string str, string removedString)
	{
		return str.TrimStartString(removedString, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Removes all leading occurrences of the specified <see cref="string" /> from this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="removedString">A <see cref="string" /> that is removed from the beginning of this <see cref="string" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The <see cref="string" /> that remains after all occurrences of <paramref name="removedString" /> are removed from the start of this <see cref="string" />.
	/// </returns>
	public static string TrimStartString(this string str, string removedString, StringComparison comparison)
	{
		return str.TrimStartString(removedString, comparison, false);
	}
	/// <summary>
	/// Removes one or all leading occurrences of the specified <see cref="string" /> from this <see cref="string" />. If <paramref name="removeOnlyFirst" /> is <see langword="true" />, only the first occurrence of <paramref name="removedString" /> is removed.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="removedString">A <see cref="string" /> that is removed from the beginning of this <see cref="string" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <param name="removeOnlyFirst"><see langword="true" /> to only remove the first occurrence of <paramref name="removedString" />.</param>
	/// <returns>
	/// The <see cref="string" /> that remains after one or all occurrences of <paramref name="removedString" /> are removed from the start of this <see cref="string" />.
	/// </returns>
	public static string TrimStartString(this string str, string removedString, StringComparison comparison, bool removeOnlyFirst)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(removedString);

		while (str.StartsWith(removedString, comparison))
		{
			str = str[removedString.Length..];
			if (removeOnlyFirst) break;
		}

		return str;
	}
	/// <summary>
	/// Removes all trailing occurrences of the specified <see cref="string" /> from this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="removedString">A <see cref="string" /> that is removed from the end of this <see cref="string" />.</param>
	/// <returns>
	/// The <see cref="string" /> that remains after all occurrences of <paramref name="removedString" /> are removed from the end of this <see cref="string" />.
	/// </returns>
	public static string TrimEndString(this string str, string removedString)
	{
		return str.TrimEndString(removedString, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Removes all trailing occurrences of the specified <see cref="string" /> from this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="removedString">A <see cref="string" /> that is removed from the end of this <see cref="string" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The <see cref="string" /> that remains after all occurrences of <paramref name="removedString" /> are removed from the end of this <see cref="string" />.
	/// </returns>
	public static string TrimEndString(this string str, string removedString, StringComparison comparison)
	{
		return str.TrimEndString(removedString, comparison, false);
	}
	/// <summary>
	/// Removes one or all trailing occurrences of the specified <see cref="string" /> from this <see cref="string" />. If <paramref name="removeOnlyFirst" /> is <see langword="true" />, only the first occurrence of <paramref name="removedString" /> is removed.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="removedString">A <see cref="string" /> that is removed from the end of this <see cref="string" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <param name="removeOnlyFirst"><see langword="true" /> to only remove the first occurrence of <paramref name="removedString" />.</param>
	/// <returns>
	/// The <see cref="string" /> that remains after one or all occurrences of <paramref name="removedString" /> are removed from the end of this <see cref="string" />.
	/// </returns>
	public static string TrimEndString(this string str, string removedString, StringComparison comparison, bool removeOnlyFirst)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(removedString);

		while (str.EndsWith(removedString, comparison))
		{
			str = str[..^removedString.Length];
			if (removeOnlyFirst) break;
		}

		return str;
	}
	/// <summary>
	/// Appends the specified <see cref="string" /> at the beginning of this <see cref="string" />, if it does not start with the contents of <paramref name="value" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to check.</param>
	/// <param name="value">The <see cref="string" /> to append, if this <see cref="string" /> does not start with the contents of <paramref name="value" />.</param>
	/// <returns>
	/// The original <see cref="string" />, if it starts with the contents of <paramref name="value" />;
	/// otherwise, the original <see cref="string" /> with the contents of <paramref name="value" /> appended to the beginning.
	/// </returns>
	public static string EnsureStartsWith(this string str, string value)
	{
		return str.EnsureStartsWith(value, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Appends the specified <see cref="string" /> at the beginning of this <see cref="string" />, if it does not start with the contents of <paramref name="value" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to check.</param>
	/// <param name="value">The <see cref="string" /> to append, if this <see cref="string" /> does not start with the contents of <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The original <see cref="string" />, if it starts with the contents of <paramref name="value" />;
	/// otherwise, the original <see cref="string" /> with the contents of <paramref name="value" /> appended to the beginning.
	/// </returns>
	public static string EnsureStartsWith(this string str, string value, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(value);

		return str.StartsWith(value, comparison) ? str : value + str;
	}
	/// <summary>
	/// Appends the specified <see cref="char" /> at the beginning of this <see cref="string" />, if it does not start with <paramref name="value" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to check.</param>
	/// <param name="value">The <see cref="char" /> to append, if this <see cref="string" /> does not start with <paramref name="value" />.</param>
	/// <returns>
	/// The original <see cref="string" />, if it starts with <paramref name="value" />;
	/// otherwise, the original <see cref="string" /> with <paramref name="value" /> appended to the beginning.
	/// </returns>
	public static string EnsureStartsWith(this string str, char value)
	{
		return str.EnsureStartsWith(value, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Appends the specified <see cref="char" /> at the beginning of this <see cref="string" />, if it does not start with <paramref name="value" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to check.</param>
	/// <param name="value">The <see cref="char" /> to append, if this <see cref="string" /> does not start with <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The original <see cref="string" />, if it starts with <paramref name="value" />;
	/// otherwise, the original <see cref="string" /> with <paramref name="value" /> appended to the beginning.
	/// </returns>
	public static string EnsureStartsWith(this string str, char value, StringComparison comparison)
	{
		Check.ArgumentNull(str);

		return str.IndexOf(value, comparison) == 0 ? str : value + str;
	}
	/// <summary>
	/// Appends the specified <see cref="string" /> to this <see cref="string" />, if it does not end with the contents of <paramref name="value" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to check.</param>
	/// <param name="value">The <see cref="string" /> to append, if this <see cref="string" /> does not end with the contents of <paramref name="value" />.</param>
	/// <returns>
	/// The original <see cref="string" />, if it ends with the contents of <paramref name="value" />;
	/// otherwise, the original <see cref="string" /> with the contents of <paramref name="value" /> appended to it.
	/// </returns>
	public static string EnsureEndsWith(this string str, string value)
	{
		return str.EnsureEndsWith(value, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Appends the specified <see cref="string" /> to this <see cref="string" />, if it does not end with the contents of <paramref name="value" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to check.</param>
	/// <param name="value">The <see cref="string" /> to append, if this <see cref="string" /> does not end with the contents of <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The original <see cref="string" />, if it ends with the contents of <paramref name="value" />;
	/// otherwise, the original <see cref="string" /> with the contents of <paramref name="value" /> appended to it.
	/// </returns>
	public static string EnsureEndsWith(this string str, string value, StringComparison comparison)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(value);

		return str.EndsWith(value, comparison) ? str : str + value;
	}
	/// <summary>
	/// Appends the specified <see cref="char" /> to this <see cref="string" />, if it does not end with <paramref name="value" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to check.</param>
	/// <param name="value">The <see cref="char" /> to append, if this <see cref="string" /> does not end with <paramref name="value" />.</param>
	/// <returns>
	/// The original <see cref="string" />, if it ends with <paramref name="value" />;
	/// otherwise, the original <see cref="string" /> with <paramref name="value" /> appended to it.
	/// </returns>
	public static string EnsureEndsWith(this string str, char value)
	{
		return str.EnsureEndsWith(value, StringComparison.CurrentCulture);
	}
	/// <summary>
	/// Appends the specified <see cref="char" /> to this <see cref="string" />, if it does not end with <paramref name="value" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to check.</param>
	/// <param name="value">The <see cref="char" /> to append, if this <see cref="string" /> does not end with <paramref name="value" />.</param>
	/// <param name="comparison">A <see cref="StringComparison" /> value that determines how strings are compared.</param>
	/// <returns>
	/// The original <see cref="string" />, if it ends with <paramref name="value" />;
	/// otherwise, the original <see cref="string" /> with <paramref name="value" /> appended to it.
	/// </returns>
	public static string EnsureEndsWith(this string str, char value, StringComparison comparison)
	{
		Check.ArgumentNull(str);

		return str.EndsWith(value.ToString(), comparison) ? str : str + value;
	}
	/// <summary>
	/// Returns a new <see cref="string" /> in which all occurrences of a any of the specified old values in the current instance are replaced with another specified <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="newValue">The <see cref="string" /> to replace all occurrences of elements of <paramref name="oldValues" />.</param>
	/// <param name="oldValues">An array of <see cref="string" /> objects of which all are replaced with <paramref name="newValue" />.</param>
	/// <returns>
	/// A new <see cref="string" /> in which all occurrences of a any of the specified old values in the current instance are replaced with another specified <see cref="string" />.
	/// </returns>
	public static string ReplaceMultiple(this string str, string? newValue, params string[]? oldValues)
	{
		Check.ArgumentNull(str);

		if (oldValues != null)
		{
			Check.ArgumentEx.ArrayValuesNotNull(oldValues);
			Check.ArgumentEx.ArrayValuesNotStringEmpty(oldValues);

			foreach (string oldValue in oldValues)
			{
				str = str.Replace(oldValue, newValue);
			}
		}

		return str;
	}
	/// <summary>
	/// Exchanges all occurrences of <paramref name="a" /> with <paramref name="b" /> and vice versa in this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="a">The <see cref="string" /> to be swapped with <paramref name="b" />.</param>
	/// <param name="b">The <see cref="string" /> to be swapped with <paramref name="a" />.</param>
	/// <returns>
	/// A new <see cref="string" /> where <paramref name="a" /> has been exchanged with <paramref name="b" />.
	/// </returns>
	public static string Swap(this string str, string a, string b)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(a);
		Check.ArgumentNull(b);

		if (a.Length == 1 && b.Length == 1)
		{
			return str.Swap(a[0], b[0]);
		}
		else
		{
			return str
				.Replace(a, SwapPlaceholder)
				.Replace(b, a)
				.Replace(SwapPlaceholder, b);
		}
	}
	/// <summary>
	/// Exchanges all occurrences of <paramref name="a" /> with <paramref name="b" /> and vice versa in this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="a">The <see cref="char" /> to be swapped with <paramref name="b" />.</param>
	/// <param name="b">The <see cref="char" /> to be swapped with <paramref name="a" />.</param>
	/// <returns>
	/// A new <see cref="string" /> where <paramref name="a" /> has been exchanged with <paramref name="b" />.
	/// </returns>
	public static string Swap(this string str, char a, char b)
	{
		Check.ArgumentNull(str);

		char[] newString = str.ToCharArray();
		for (int i = 0; i < str.Length; i++)
		{
			if (newString[i] == a) newString[i] = b;
			else if (newString[i] == b) newString[i] = a;
		}
		return newString.AsString();
	}
	/// <summary>
	/// Reverses the sequence of all characters in this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <returns>
	/// A new <see cref="string" /> containing the characters from the original <see cref="string" /> in reversed order.
	/// </returns>
	public static string Reverse(this string str)
	{
		Check.ArgumentNull(str);

		char[] newString = str.ToCharArray();
		Array.Reverse(newString);
		return newString.AsString();
	}
	/// <summary>
	/// Generates a sequence that contains the repeated value of this <see cref="string" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be repeated.</param>
	/// <param name="count">A <see cref="int" /> value specifying the number of times to repeat this <see cref="string" />.</param>
	/// <returns>
	/// A new <see cref="string" /> composed of the repetition of the original <see cref="string" /> value.
	/// </returns>
	public static string Repeat(this string str, int count)
	{
		Check.ArgumentNull(str);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);

		return Enumerable.Repeat(str, count).AsString();
	}
	/// <summary>
	/// Changes the casing of this <see cref="string" /> to the specified <see cref="StringCasing" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be processed.</param>
	/// <param name="casing">The <see cref="StringCasing" /> specifying the casing rule of the returned <see cref="string" />.</param>
	/// <returns>
	/// A new <see cref="string" /> with character casing changed using the specified <see cref="StringCasing" /> parameter.
	/// </returns>
	public static string ChangeCasing(this string str, StringCasing casing)
	{
		Check.ArgumentNull(str);

		if (str == "")
		{
			return str;
		}
		else
		{
			switch (casing)
			{
				case StringCasing.Lower:
					return str.ToLower();
				case StringCasing.Upper:
					return str.ToUpper();
				case StringCasing.CamelCase:
					{
						char[] newString = str.ToCharArray();
						newString[0] = newString[0].ToUpper();
						for (int i = 1; i < str.Length; i++)
						{
							newString[i] = newString[i - 1].IsWhiteSpace() || newString[i - 1].IsPunctuation() ? newString[i].ToUpper() : newString[i].ToLower();
						}
						return newString.AsString();
					}
				case StringCasing.LowerSnakeCase:
				case StringCasing.UpperSnakeCase:
				case StringCasing.LowerKebabCase:
				case StringCasing.UpperKebabCase:
					{
						StringBuilder stringBuilder = new(str.Length);

						char separator = casing == StringCasing.LowerSnakeCase || casing == StringCasing.UpperSnakeCase ? '_' : '-';
						int position = 0;
						bool skip = false;

						while (position != -1 && position < str.Length)
						{
							int index = -1;
							for (int i = position; i < str.Length; i++)
							{
								if (skip ^ (str[i].IsWhiteSpace() || str[i].IsPunctuation()))
								{
									index = i;
									break;
								}
							}

							if (!skip)
							{
								if (index == -1)
								{
									stringBuilder.Append(str[position..]);
								}
								else
								{
									stringBuilder.Append(str[position..index]).Append(separator);
								}
							}

							skip = !skip;
							position = index;
						}

						return stringBuilder
							.ToString()
							.ChangeCasing(casing == StringCasing.LowerSnakeCase || casing == StringCasing.LowerKebabCase ? StringCasing.Lower : StringCasing.Upper);
					}
				default:
					throw Throw.InvalidEnumArgument(nameof(casing), casing);
			}
		}
	}
	/// <summary>
	/// Encodes all the characters in the specified <see cref="string" /> into a sequence of bytes using the <see cref="Encoding.Default" /> encoding.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to convert.</param>
	/// <returns>
	/// A <see cref="byte" />[] containing the results of encoding the specified set of characters.
	/// </returns>
	public static byte[] ToAnsiBytes(this string str)
	{
		Check.ArgumentNull(str);

		return Encoding.Default.GetBytes(str);
	}
	/// <summary>
	/// Encodes all the characters in the specified <see cref="string" /> into a sequence of bytes using the <see cref="Encoding.UTF8" /> encoding.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to convert.</param>
	/// <returns>
	/// A <see cref="byte" />[] containing the results of encoding the specified set of characters.
	/// </returns>
	public static byte[] ToUTF8Bytes(this string str)
	{
		Check.ArgumentNull(str);

		return Encoding.UTF8.GetBytes(str);
	}
	/// <summary>
	/// Encodes all the characters in the specified <see cref="string" /> into a sequence of bytes using the <see cref="Encoding.Unicode" /> encoding.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to convert.</param>
	/// <returns>
	/// A <see cref="byte" />[] containing the results of encoding the specified set of characters.
	/// </returns>
	public static byte[] ToUnicodeBytes(this string str)
	{
		Check.ArgumentNull(str);

		return Encoding.Unicode.GetBytes(str);
	}
	/// <summary>
	/// Converts this <see cref="string" /> to its equivalent base-64 <see cref="string" /> representation using the specified encoding.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to convert.</param>
	/// <param name="encoding">The encoding be used to encode this <see cref="string" />.</param>
	/// <returns>
	/// The equivalent <see cref="string" /> representation that is encoded with base-64 using the specified encoding.
	/// </returns>
	public static string ToBase64String(this string str, Encoding encoding)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(encoding);

		return Convert.ToBase64String(encoding.GetBytes(str));
	}
	/// <summary>
	/// Converts this <see cref="string" /> from its equivalent base-64 <see cref="string" /> representation using the specified encoding.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to convert.</param>
	/// <param name="encoding">The encoding be used to encode the resulting binary to a <see cref="string" />.</param>
	/// <returns>
	/// The equivalent <see cref="string" /> representation that is decoded from a base-64 <see cref="string" /> using the specified encoding.
	/// </returns>
	public static string FromBase64String(this string str, Encoding encoding)
	{
		Check.ArgumentNull(str);
		Check.ArgumentNull(encoding);

		return encoding.GetString(Convert.FromBase64String(str));
	}
	/// <summary>
	/// Splits this <see cref="string" /> into an array of lines, which are separated by either a CR or a CRLF separator.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to split.</param>
	/// <returns>
	/// A new <see cref="string" />[] that represents the lines of the original <see cref="string" />.
	/// </returns>
	public static string[] SplitToLines(this string str)
	{
		return str.SplitToLines(false);
	}
	/// <summary>
	/// Splits this <see cref="string" /> into an array of lines, which are separated by either a CR or a CRLF separator.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to split.</param>
	/// <param name="removeEmptyEntries"><see langword="true" /> to remove empty lines.</param>
	/// <returns>
	/// A new <see cref="string" />[] that represents the lines of the original <see cref="string" />.
	/// </returns>
	public static string[] SplitToLines(this string str, bool removeEmptyEntries)
	{
		return str.SplitToLines(removeEmptyEntries, false);
	}
	/// <summary>
	/// Splits this <see cref="string" /> into an array of lines, which are separated by either a CR or a CRLF separator.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to split.</param>
	/// <param name="removeEmptyEntries"><see langword="true" /> to remove empty lines.</param>
	/// <param name="trimLines"><see langword="true" /> to trim lines after splitting.</param>
	/// <returns>
	/// A new <see cref="string" />[] that represents the lines of the original <see cref="string" />.
	/// </returns>
	public static string[] SplitToLines(this string str, bool removeEmptyEntries, bool trimLines)
	{
		Check.ArgumentNull(str);

		StringSplitOptions splitOptions = StringSplitOptions.None;
		if (removeEmptyEntries) splitOptions |= StringSplitOptions.RemoveEmptyEntries;
		if (trimLines) splitOptions |= StringSplitOptions.TrimEntries;

		return str.Split(new[] { "\r\n", "\n" }, splitOptions);
	}
	/// <summary>
	/// Splits this <see cref="string" /> into chunks of a given size. The last <see cref="string" /> may be smaller than <paramref name="chunkSize" />.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to chunk.</param>
	/// <param name="chunkSize">The size of each chunk.</param>
	/// <returns>
	/// A new <see cref="string" />[] with the chunks from the original <see cref="string" />.
	/// </returns>
	public static string[] SplitToChunks(this string str, int chunkSize)
	{
		Check.ArgumentNull(str);
		Check.ArgumentOutOfRangeEx.Greater0(chunkSize);

		if (str.Length <= chunkSize)
		{
			return new[] { str };
		}
		else
		{
			string[] chunks = new string[(str.Length + chunkSize - 1) / chunkSize];

			for (int i = 0; i < chunks.Length; i++)
			{
				chunks[i] = i < chunks.Length - 1 ? str.Substring(i * chunkSize, chunkSize) : str[(i * chunkSize)..];
			}

			return chunks;
		}
	}
}