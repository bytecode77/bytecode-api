using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="string" /> objects.
	/// </summary>
	public static class StringExtensions
	{
		private static readonly string SwapPlaceholder = Create.HexadecimalString(8);

		/// <summary>
		/// Indicates whether this <see cref="string" /> is <see langword="null" /> or <see cref="string.Empty" />.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to test.</param>
		/// <returns>
		/// <see langword="true" />, if this <see cref="string" /> is <see langword="null" /> or <see cref="string.Empty" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsNullOrEmpty(this string str)
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
		public static bool IsNullOrWhiteSpace(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="string" /> is <see cref="string.Empty" />, otherwise, its original value.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to convert.</param>
		/// <returns>
		/// <see langword="null" />, if this <see cref="string" /> is <see cref="string.Empty" />;
		/// otherwise, its original value.
		/// </returns>
		public static string ToNullIfEmpty(this string str)
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
		public static string ToNullIfEmptyOrWhiteSpace(this string str)
		{
			return str.IsNullOrWhiteSpace() ? null : str;
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
			Check.ArgumentNull(str, nameof(str));

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
			Check.ArgumentNull(str, nameof(str));

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
			Check.ArgumentNull(str, nameof(str));

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
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentNull(encoding, nameof(encoding));

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
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentNull(encoding, nameof(encoding));

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
			Check.ArgumentNull(str, nameof(str));

			return str.Split(new[] { "\r\n", "\n" }, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
		}
		/// <summary>
		/// Compares this <see cref="string" /> with a specified <see cref="string" /> object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="string" />. One or multiple <see cref="SpecialStringComparisons" /> flags specify what comparison properties apply.
		/// </summary>
		/// <param name="strA">A <see cref="string" /> to compare to <paramref name="strB" />.</param>
		/// <param name="strB">A <see cref="string" /> to compare to <paramref name="strA" />.</param>
		/// <param name="comparison">The <see cref="SpecialStringComparisons" /> flags specifying what comparison properties apply.</param>
		/// <returns>
		/// A signed integer that indicates the relative values of <paramref name="strA" /> and <paramref name="strB" />.
		/// </returns>
		public static int CompareTo(this string strA, string strB, SpecialStringComparisons comparison)
		{
			PrepareSpecialStringComparison(comparison, ref strA, true);
			PrepareSpecialStringComparison(comparison, ref strB, true);

			if (comparison.HasFlag(SpecialStringComparisons.Natural)) return Math.Sign(Native.StrCmpLogicalW(strA ?? "", strB ?? ""));
			else return string.Compare(strA, strB, CultureInfo.InvariantCulture, CompareOptions.None);
		}
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether a specified substring occurs within this <see cref="string" />. One or multiple <see cref="SpecialStringComparisons" /> flags specify what comparison properties apply.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to check.</param>
		/// <param name="value">The <see cref="string" /> to compare.</param>
		/// <param name="comparison">The <see cref="SpecialStringComparisons" /> flags specifying what comparison properties apply.</param>
		/// <returns>
		/// <see langword="true" />, if this <see cref="string" /> contains the specified <see cref="string" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool Contains(this string str, string value, SpecialStringComparisons comparison)
		{
			PrepareSpecialStringComparison(comparison, ref str, false);
			PrepareSpecialStringComparison(comparison, ref value, false);

			return str.Contains(value);
		}
		/// <summary>
		/// Determines whether the beginning of this <see cref="string" /> instance matches the specified <see cref="string" />. One or multiple <see cref="SpecialStringComparisons" /> flags specify what comparison properties apply.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to check.</param>
		/// <param name="value">The <see cref="string" /> to compare.</param>
		/// <param name="comparison">The <see cref="SpecialStringComparisons" /> flags specifying what comparison properties apply.</param>
		/// <returns>
		/// <see langword="true" />, if value matches the beginning of this <see cref="string" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool StartsWith(this string str, string value, SpecialStringComparisons comparison)
		{
			PrepareSpecialStringComparison(comparison, ref str, false);
			PrepareSpecialStringComparison(comparison, ref value, false);

			return str.StartsWith(value);
		}
		/// <summary>
		/// Determines whether the end of this <see cref="string" /> instance matches the specified <see cref="string" />. One or multiple <see cref="SpecialStringComparisons" /> flags specify what comparison properties apply.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to check.</param>
		/// <param name="value">The <see cref="string" /> to compare.</param>
		/// <param name="comparison">The <see cref="SpecialStringComparisons" /> flags specifying what comparison properties apply.</param>
		/// <returns>
		/// <see langword="true" />, if value matches the end of this <see cref="string" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool EndsWith(this string str, string value, SpecialStringComparisons comparison)
		{
			PrepareSpecialStringComparison(comparison, ref str, false);
			PrepareSpecialStringComparison(comparison, ref value, false);

			return str.EndsWith(value);
		}
		/// <summary>
		/// Replaces the format item in a specified <see cref="string" /> with the <see cref="string" /> representation of a corresponding <see cref="object" /> in a specified array using the invariant culture.
		/// </summary>
		/// <param name="str">A composite format <see cref="string" />.</param>
		/// <param name="args">An <see cref="object" /> array that contains zero or more objects to format.</param>
		/// <returns>
		/// A copy of this <see cref="string" /> in which the format items have been replaced by the <see cref="string" /> representation of the corresponding objects in <paramref name="args" />.
		/// </returns>
		public static string FormatInvariant(this string str, params object[] args)
		{
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentNull(args, nameof(args));

			return string.Format(CultureInfo.InvariantCulture, str, args);
		}
		/// <summary>
		/// Returns the portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
		/// <returns>
		/// The portion of this <see cref="string" /> after the first occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
		/// </returns>
		public static string SubstringFrom(this string str, string value)
		{
			return str.SubstringFrom(value, false);
		}
		/// <summary>
		/// Returns the portion of this <see cref="string" /> after the first or last occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />. If <paramref name="findLast" /> is <see langword="true" />, the last occurrence of <paramref name="value" /> is searched.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
		/// <param name="findLast"><see langword="true" /> to find the last occurrence of <paramref name="value" />; otherwise, <see langword="false" /> to find the first occurrence.</param>
		/// <returns>
		/// The portion of this <see cref="string" /> after the first or last occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
		/// </returns>
		public static string SubstringFrom(this string str, string value, bool findLast)
		{
			return str.SubstringFrom(value, findLast, false);
		}
		/// <summary>
		/// Returns the portion of this <see cref="string" /> after the first or last occurrence of <paramref name="value" />. If <paramref name="findLast" /> is <see langword="true" />, the last occurrence of <paramref name="value" /> is searched. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
		/// <param name="findLast"><see langword="true" /> to find the last occurrence of <paramref name="value" />; otherwise, <see langword="false" /> to find the first occurrence.</param>
		/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; otherwise, <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
		/// <returns>
		/// The portion of this <see cref="string" /> after the first or last occurrence of <paramref name="value" />.
		/// </returns>
		public static string SubstringFrom(this string str, string value, bool findLast, bool inclusive)
		{
			return str.SubstringFrom(value, findLast, inclusive, false);
		}
		/// <summary>
		/// Returns the portion of this <see cref="string" /> after the first or last occurrence of <paramref name="value" />. If <paramref name="findLast" /> is <see langword="true" />, the last occurrence of <paramref name="value" /> is searched. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
		/// <param name="findLast"><see langword="true" /> to find the last occurrence of <paramref name="value" />; otherwise, <see langword="false" /> to find the first occurrence.</param>
		/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; otherwise, <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <returns>
		/// The portion of this <see cref="string" /> after the first or last occurrence of <paramref name="value" />.
		/// </returns>
		public static string SubstringFrom(this string str, string value, bool findLast, bool inclusive, bool ignoreCase)
		{
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentNull(value, nameof(value));

			StringComparison comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			int index = findLast ? str.LastIndexOf(value, comparison) : str.IndexOf(value, comparison);

			return index == -1 ? str : str.Substring(index + (inclusive ? 0 : value.Length));
		}
		/// <summary>
		/// Returns the portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
		/// <returns>
		/// The portion of this <see cref="string" /> before the first occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
		/// </returns>
		public static string SubstringUntil(this string str, string value)
		{
			return str.SubstringUntil(value, false);
		}
		/// <summary>
		/// Returns the portion of this <see cref="string" /> before the first or last occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />. If <paramref name="findLast" /> is <see langword="true" />, the last occurrence of <paramref name="value" /> is searched.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
		/// <param name="findLast"><see langword="true" /> to find the last occurrence of <paramref name="value" />; otherwise, <see langword="false" /> to find the first occurrence.</param>
		/// <returns>
		/// The portion of this <see cref="string" /> before the first or last occurrence of <paramref name="value" />, excluding the contents of the searched <see cref="string" />.
		/// </returns>
		public static string SubstringUntil(this string str, string value, bool findLast)
		{
			return str.SubstringUntil(value, findLast, false);
		}
		/// <summary>
		/// Returns the portion of this <see cref="string" /> before the first or last occurrence of <paramref name="value" />. If <paramref name="findLast" /> is <see langword="true" />, the last occurrence of <paramref name="value" /> is searched. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
		/// <param name="findLast"><see langword="true" /> to find the last occurrence of <paramref name="value" />; otherwise, <see langword="false" /> to find the first occurrence.</param>
		/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; otherwise, <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
		/// <returns>
		/// The portion of this <see cref="string" /> before the first or last occurrence of <paramref name="value" />.
		/// </returns>
		public static string SubstringUntil(this string str, string value, bool findLast, bool inclusive)
		{
			return str.SubstringUntil(value, findLast, inclusive, false);
		}
		/// <summary>
		/// Returns the portion of this <see cref="string" /> before the first or last occurrence of <paramref name="value" />. If <paramref name="findLast" /> is <see langword="true" />, the last occurrence of <paramref name="value" /> is searched. If <paramref name="inclusive" /> is <see langword="true" />, the contents of <paramref name="value" /> are included in the result.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="value">The <see cref="string" /> that is searched for in <paramref name="str" />.</param>
		/// <param name="findLast"><see langword="true" /> to find the last occurrence of <paramref name="value" />; otherwise, <see langword="false" /> to find the first occurrence.</param>
		/// <param name="inclusive"><see langword="true" /> to include the contents of <paramref name="value" />; otherwise, <see langword="false" /> to exclude the contents of <paramref name="value" />.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <returns>
		/// The portion of this <see cref="string" /> before the first or last occurrence of <paramref name="value" />.
		/// </returns>
		public static string SubstringUntil(this string str, string value, bool findLast, bool inclusive, bool ignoreCase)
		{
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentNull(value, nameof(value));

			StringComparison comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			int index = findLast ? str.LastIndexOf(value, comparison) : str.IndexOf(value, comparison);

			return index == -1 ? str : str.Left(index + (inclusive ? value.Length : 0));
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
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(length, nameof(length));

			return str.Length <= length ? str : str.Substring(0, length);
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
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(length, nameof(length));

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
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(totalWidth, nameof(totalWidth));

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
			return str.TrimStartString(removedString, false);
		}
		/// <summary>
		/// Removes all leading occurrences of the specified <see cref="string" /> from this <see cref="string" />.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="removedString">A <see cref="string" /> that is removed from the beginning of this <see cref="string" />.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <returns>
		/// The <see cref="string" /> that remains after all occurrences of <paramref name="removedString" /> are removed from the start of this <see cref="string" />.
		/// </returns>
		public static string TrimStartString(this string str, string removedString, bool ignoreCase)
		{
			return str.TrimStartString(removedString, ignoreCase, false);
		}
		/// <summary>
		/// Removes one or all leading occurrences of the specified <see cref="string" /> from this <see cref="string" />. If <paramref name="removeOnlyFirst" /> is <see langword="true" />, only the first occurrence of <paramref name="removedString" /> is removed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="removedString">A <see cref="string" /> that is removed from the beginning of this <see cref="string" />.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <param name="removeOnlyFirst"><see langword="true" /> to only remove the first occurrence of <paramref name="removedString" />.</param>
		/// <returns>
		/// The <see cref="string" /> that remains after one or all occurrences of <paramref name="removedString" /> are removed from the start of this <see cref="string" />.
		/// </returns>
		public static string TrimStartString(this string str, string removedString, bool ignoreCase, bool removeOnlyFirst)
		{
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentNull(removedString, nameof(removedString));

			while (str.StartsWith(removedString, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
			{
				str = str.Substring(removedString.Length);
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
			return str.TrimEndString(removedString, false);
		}
		/// <summary>
		/// Removes all trailing occurrences of the specified <see cref="string" /> from this <see cref="string" />.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="removedString">A <see cref="string" /> that is removed from the end of this <see cref="string" />.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <returns>
		/// The <see cref="string" /> that remains after all occurrences of <paramref name="removedString" /> are removed from the end of this <see cref="string" />.
		/// </returns>
		public static string TrimEndString(this string str, string removedString, bool ignoreCase)
		{
			return str.TrimEndString(removedString, ignoreCase, false);
		}
		/// <summary>
		/// Removes one or all trailing occurrences of the specified <see cref="string" /> from this <see cref="string" />. If <paramref name="removeOnlyFirst" /> is <see langword="true" />, only the first occurrence of <paramref name="removedString" /> is removed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="removedString">A <see cref="string" /> that is removed from the end of this <see cref="string" />.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <param name="removeOnlyFirst"><see langword="true" /> to only remove the first occurrence of <paramref name="removedString" />.</param>
		/// <returns>
		/// The <see cref="string" /> that remains after one or all occurrences of <paramref name="removedString" /> are removed from the end of this <see cref="string" />.
		/// </returns>
		public static string TrimEndString(this string str, string removedString, bool ignoreCase, bool removeOnlyFirst)
		{
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentNull(removedString, nameof(removedString));

			while (str.EndsWith(removedString, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
			{
				str = str.Left(str.Length - removedString.Length);
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
			return str.EnsureStartsWith(value, false);
		}
		/// <summary>
		/// Appends the specified <see cref="string" /> at the beginning of this <see cref="string" />, if it does not start with the contents of <paramref name="value" />.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to check.</param>
		/// <param name="value">The <see cref="string" /> to append, if this <see cref="string" /> does not start with the contents of <paramref name="value" />.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <returns>
		/// The original <see cref="string" />, if it starts with the contents of <paramref name="value" />;
		/// otherwise, the original <see cref="string" /> with the contents of <paramref name="value" /> appended to the beginning.
		/// </returns>
		public static string EnsureStartsWith(this string str, string value, bool ignoreCase)
		{
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentNull(value, nameof(value));

			return str.StartsWith(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) ? str : value + str;
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
			return str.EnsureEndsWith(value, false);
		}
		/// <summary>
		/// Appends the specified <see cref="string" /> to this <see cref="string" />, if it does not end with the contents of <paramref name="value" />.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to check.</param>
		/// <param name="value">The <see cref="string" /> to append, if this <see cref="string" /> does not end with the contents of <paramref name="value" />.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <returns>
		/// The original <see cref="string" />, if it ends with the contents of <paramref name="value" />;
		/// otherwise, the original <see cref="string" /> with the contents of <paramref name="value" /> appended to it.
		/// </returns>
		public static string EnsureEndsWith(this string str, string value, bool ignoreCase)
		{
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentNull(value, nameof(value));

			return str.EndsWith(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) ? str : str + value;
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
		public static string ReplaceMultiple(this string str, string newValue, params string[] oldValues)
		{
			Check.ArgumentNull(str, nameof(str));

			if (oldValues != null)
			{
				Check.ArgumentEx.ArrayValuesNotNull(oldValues, nameof(oldValues));

				foreach (string oldValue in oldValues) str = str.Replace(oldValue, newValue);
			}

			return str;
		}
		/// <summary>
		/// Returns a new <see cref="string" /> in which <paramref name="oldValue" /> has been replaced with <paramref name="newValue" />, only if this <see cref="string" /> is equal to <paramref name="oldValue" />.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="oldValue">The <see cref="string" /> which has to match this <see cref="string" /> exactly.</param>
		/// <param name="newValue">The <see cref="string" /> which is returned, if this <see cref="string" /> is equal to <paramref name="oldValue" />.</param>
		/// <returns>
		/// <paramref name="newValue" />, if this <see cref="string" /> is equal to <paramref name="oldValue" />;
		/// otherwise, the original <see cref="string" /> value.
		/// </returns>
		public static string Exchange(this string str, string oldValue, string newValue)
		{
			Check.ArgumentNull(str, nameof(str));

			return str == oldValue ? newValue : str;
		}
		/// <summary>
		/// Replaces all occurrences of linebreaks ("\n" and "\r\n") in this <see cref="string" /> with a specified replacement value.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be processed.</param>
		/// <param name="newValue">The <see cref="string" /> which will replace "\n" and "\r\n" occurrences in this <see cref="string" />.</param>
		/// <returns>
		/// The original value of this <see cref="string" /> where "\r" and "\r\n" have been replaced with <paramref name="newValue" />.
		/// </returns>
		public static string ReplaceLineBreaks(this string str, string newValue)
		{
			Check.ArgumentNull(str, nameof(str));

			return str.Replace("\r\n", newValue).Replace("\n", newValue);
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
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentNull(a, nameof(a));
			Check.ArgumentNull(b, nameof(b));

			if (a.Length == 1 && b.Length == 1) return str.Swap(a[0], b[0]);
			else return str.Replace(a, SwapPlaceholder).Replace(b, a).Replace(SwapPlaceholder, b);
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
			Check.ArgumentNull(str, nameof(str));

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
			Check.ArgumentNull(str, nameof(str));

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
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));

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
			Check.ArgumentNull(str, nameof(str));

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
						char[] newString = str.ToCharArray();
						newString[0] = newString[0].ToUpper();
						for (int i = 1; i < str.Length; i++)
						{
							newString[i] = newString[i - 1].IsWhiteSpace() || newString[i - 1].IsPunctuation() ? newString[i].ToUpper() : newString[i].ToLower();
						}
						return newString.AsString();
					default:
						throw Throw.InvalidEnumArgument(nameof(casing));
				}
			}
		}

		private static void PrepareSpecialStringComparison(SpecialStringComparisons comparison, ref string str, bool allowNatural)
		{
			if (!allowNatural && comparison.HasFlag(SpecialStringComparisons.Natural))
			{
				throw Throw.InvalidOperation("Natural string comparison can only be used on whole strings.");
			}
			else if (str != null)
			{
				if (comparison.HasFlag(SpecialStringComparisons.IgnoreWhiteSpaces)) str = str.Where(c => !c.IsWhiteSpace()).AsString();
				else if (comparison.HasFlag(SpecialStringComparisons.Trim)) str = str.Trim();

				if (comparison.HasFlag(SpecialStringComparisons.IgnorePunctuation)) str = str.Where(c => !c.IsPunctuation()).AsString();
				if (comparison.HasFlag(SpecialStringComparisons.IgnoreSymbols)) str = str.Where(c => !c.IsSymbol()).AsString();
				if (comparison.HasFlag(SpecialStringComparisons.IgnoreCase)) str = str.ToLower();
			}
			else if (comparison.HasFlag(SpecialStringComparisons.NullAndEmptyEqual))
			{
				str = "";
			}
		}
	}
}