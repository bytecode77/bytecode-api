using System.Globalization;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="char" /> objects.
	/// </summary>
	public static class CharExtensions
	{
		/// <summary>
		/// Converts the value of this <see cref="char" /> to its equivalent <see cref="string" /> representation using the invariant culture.
		/// </summary>
		/// <param name="value">The <see cref="char" /> value to convert.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="char" />.
		/// </returns>
		public static string ToStringInvariant(this char value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Converts this numeric <see langword="char" /> to a <see cref="double" /> value.
		/// </summary>
		/// <param name="value">The unicode character to convert.</param>
		/// <returns>
		/// The numeric value of <paramref name="value" />, if that character represents a number;
		/// otherwise, -1.0.
		/// </returns>
		public static double GetNumericValue(this char value)
		{
			return char.GetNumericValue(value);
		}
		/// <summary>
		/// Categorizes this <see langword="char" /> into a group identified by one of the <see cref="UnicodeCategory" /> values.
		/// </summary>
		/// <param name="value">The unicode character to categorize.</param>
		/// <returns>
		/// A <see cref="UnicodeCategory" /> value that identifies the group that contains <paramref name="value" />.
		/// </returns>
		public static UnicodeCategory GetUnicodeCategory(this char value)
		{
			return char.GetUnicodeCategory(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a control character.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is a control character;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsControl(this char value)
		{
			return char.IsControl(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a decimal.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is a decimal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsDigit(this char value)
		{
			return char.IsDigit(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is a high surrogate.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if the numeric value of the <paramref name="value" /> parameter ranges from U+D800 through U+DBFF;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsHighSurrogate(this char value)
		{
			return char.IsHighSurrogate(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a unicode letter.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is a letter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsLetter(this char value)
		{
			return char.IsLetter(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a unicode letter or a decimal digit.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is a letter or a decimal digit;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsLetterOrDigit(this char value)
		{
			return char.IsLetterOrDigit(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a lowercase letter.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is a lowercase letter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsLower(this char value)
		{
			return char.IsLower(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is a low surrogate.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if the numeric value of the <paramref name="value" /> parameter ranges from U+DC00 through U+DFFF;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsLowSurrogate(this char value)
		{
			return char.IsLowSurrogate(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a number.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is a number;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsNumber(this char value)
		{
			return char.IsNumber(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a punctuation mark.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is a punctuation mark;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsPunctuation(this char value)
		{
			return char.IsPunctuation(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a separator character.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is a separator character;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsSeparator(this char value)
		{
			return char.IsSeparator(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> has a surrogate code unit.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if the numeric value of the <paramref name="value" /> is either a high surrogate or a low surrogate;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsSurrogate(this char value)
		{
			return char.IsSurrogate(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a symbol character.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is a symbol character;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsSymbol(this char value)
		{
			return char.IsSymbol(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as an uppercase letter.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is an uppercase letter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsUpper(this char value)
		{
			return char.IsUpper(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as whitespace.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is whitespace;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsWhiteSpace(this char value)
		{
			return char.IsWhiteSpace(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a hexadecimal character (0-9, a-f, A-F).
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> is a hexadecimal character (0-9, a-f, A-F);
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsHexadecimal(this char value)
		{
			return value >= '0' && value <= '9' || value >= 'a' && value <= 'f' || value >= 'A' && value <= 'F';
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its lowercase equivalent.
		/// </summary>
		/// <param name="value">The unicode character to convert.</param>
		/// <returns>
		/// The lowercase equivalent of <paramref name="value" />, or the unchanged value of <paramref name="value" />, if <paramref name="value" /> is already lowercase or not alphabetic.
		/// </returns>
		public static char ToLower(this char value)
		{
			return char.ToLower(value);
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its lowercase equivalent using specified culture-specific formatting information.
		/// </summary>
		/// <param name="value">The unicode character to convert.</param>
		/// <param name="culture">An object that supplies culture-specific casing rules.</param>
		/// <returns>
		/// The lowercase equivalent of <paramref name="value" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="value" />, if <paramref name="value" /> is already lowercase, has no lowercase equivalent, or is not alphabetic.
		/// </returns>
		public static char ToLower(this char value, CultureInfo culture)
		{
			Check.ArgumentNull(culture, nameof(culture));

			return char.ToLower(value, culture);
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its lowercase equivalent using the casing rules of the invariant culture.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// The lowercase equivalent of <paramref name="value" />, or the unchanged value of <paramref name="value" />, if <paramref name="value" /> is already lowercase, has no lowercase equivalent, or is not alphabetic.
		/// </returns>
		public static char ToLowerInvariant(this char value)
		{
			return char.ToLowerInvariant(value);
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its uppercase equivalent.
		/// </summary>
		/// <param name="value">The unicode character to convert.</param>
		/// <returns>
		/// The uppercase equivalent of <paramref name="value" />, or the unchanged value of <paramref name="value" />, if <paramref name="value" /> is already uppercase or not alphabetic.
		/// </returns>
		public static char ToUpper(this char value)
		{
			return char.ToUpper(value);
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its uppercase equivalent using specified culture-specific formatting information.
		/// </summary>
		/// <param name="value">The unicode character to convert.</param>
		/// <param name="culture">An object that supplies culture-specific casing rules.</param>
		/// <returns>
		/// The uppercase equivalent of <paramref name="value" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="value" />, if <paramref name="value" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.
		/// </returns>
		public static char ToUpper(this char value, CultureInfo culture)
		{
			Check.ArgumentNull(culture, nameof(culture));

			return char.ToUpper(value, culture);
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its uppercase equivalent using the casing rules of the invariant culture.
		/// </summary>
		/// <param name="value">The unicode character to evaluate.</param>
		/// <returns>
		/// The uppercase equivalent of <paramref name="value" />, or the unchanged value of <paramref name="value" />, if <paramref name="value" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.
		/// </returns>
		public static char ToUpperInvariant(this char value)
		{
			return char.ToUpperInvariant(value);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="char" /> value is '\0', otherwise its original value.
		/// </summary>
		/// <param name="value">The <see cref="char" /> value to convert.</param>
		/// <returns>
		/// <see langword="null" />, if this <see cref="char" /> value is '\0';
		/// otherwise, its original value.
		/// </returns>
		public static char? ToNullIfDefault(this char value)
		{
			return value == default(char) ? null : (char?)value;
		}
		/// <summary>
		/// Creates a <see cref="string" /> using the value of the <paramref name="value" /> parameter and repeats it a specified number of times.
		/// </summary>
		/// <param name="value">The unicode character to repeat.</param>
		/// <param name="count">The number of times <paramref name="value" /> is repeated.</param>
		/// <returns>
		/// A new <see cref="string" /> object containing characters with the value of <paramref name="value" /> with a length based on <paramref name="count" />.
		/// </returns>
		public static string Repeat(this char value, int count)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));

			return new string(value, count);
		}
	}
}