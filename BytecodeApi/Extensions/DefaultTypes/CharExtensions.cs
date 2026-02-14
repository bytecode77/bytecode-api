using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="char" /> objects.
/// </summary>
public static class CharExtensions
{
	extension(char value)
	{
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a decimal.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is a decimal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsDigit()
		{
			return char.IsDigit(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a number.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is a number;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsNumber()
		{
			return char.IsNumber(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a unicode letter.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is a letter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsLetter()
		{
			return char.IsLetter(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as whitespace.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is whitespace;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsWhiteSpace()
		{
			return char.IsWhiteSpace(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as an uppercase letter.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is an uppercase letter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsUpper()
		{
			return char.IsUpper(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a lowercase letter.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is a lowercase letter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsLower()
		{
			return char.IsLower(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a hexadecimal character (0-9, a-f, A-F).
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is a hexadecimal character (0-9, a-f, A-F);
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsHexadecimal()
		{
			return char.IsAsciiHexDigit(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a punctuation mark.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is a punctuation mark;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsPunctuation()
		{
			return char.IsPunctuation(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a separator character.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is a separator character;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsSeparator()
		{
			return char.IsSeparator(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a symbol character.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is a symbol character;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsSymbol()
		{
			return char.IsSymbol(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a unicode letter or a decimal digit.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is a letter or a decimal digit;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsLetterOrDigit()
		{
			return char.IsLetterOrDigit(value);
		}
		/// <summary>
		/// Indicates whether this <see langword="char" /> is categorized as a control character.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="char" /> is a control character;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsControl()
		{
			return char.IsControl(value);
		}
		/// <summary>
		/// Categorizes this <see langword="char" /> into a group identified by one of the <see cref="UnicodeCategory" /> values.
		/// </summary>
		/// <returns>
		/// A <see cref="UnicodeCategory" /> value that identifies the group that contains this <see cref="char" />.
		/// </returns>
		public UnicodeCategory GetUnicodeCategory()
		{
			return char.GetUnicodeCategory(value);
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its uppercase equivalent.
		/// </summary>
		/// <returns>
		/// The uppercase equivalent of this <see cref="char" />, or the unchanged value of this <see cref="char" />, if this <see cref="char" /> is already uppercase or not alphabetic.
		/// </returns>
		public char ToUpper()
		{
			return char.ToUpper(value);
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its uppercase equivalent using specified culture-specific formatting information.
		/// </summary>
		/// <param name="culture">An object that supplies culture-specific casing rules.</param>
		/// <returns>
		/// The uppercase equivalent of this <see cref="char" />, modified according to <paramref name="culture" />, or the unchanged value of this <see cref="char" />, if this <see cref="char" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.
		/// </returns>
		public char ToUpper(CultureInfo culture)
		{
			Check.ArgumentNull(culture);

			return char.ToUpper(value, culture);
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its uppercase equivalent using the casing rules of the invariant culture.
		/// </summary>
		/// <returns>
		/// The uppercase equivalent of this <see cref="char" />, or the unchanged value of this <see cref="char" />, if this <see cref="char" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.
		/// </returns>
		public char ToUpperInvariant()
		{
			return char.ToUpperInvariant(value);
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its lowercase equivalent.
		/// </summary>
		/// <returns>
		/// The lowercase equivalent of this <see cref="char" />, or the unchanged value of this <see cref="char" />, if this <see cref="char" /> is already lowercase or not alphabetic.
		/// </returns>
		public char ToLower()
		{
			return char.ToLower(value);
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its lowercase equivalent using specified culture-specific formatting information.
		/// </summary>
		/// <param name="culture">An object that supplies culture-specific casing rules.</param>
		/// <returns>
		/// The lowercase equivalent of this <see cref="char" />, modified according to <paramref name="culture" />, or the unchanged value of this <see cref="char" />, if this <see cref="char" /> is already lowercase, has no lowercase equivalent, or is not alphabetic.
		/// </returns>
		public char ToLower(CultureInfo culture)
		{
			Check.ArgumentNull(culture);

			return char.ToLower(value, culture);
		}
		/// <summary>
		/// Converts the value of this <see langword="char" /> to its lowercase equivalent using the casing rules of the invariant culture.
		/// </summary>
		/// <returns>
		/// The lowercase equivalent of this <see cref="char" />, or the unchanged value of this <see cref="char" />, if this <see cref="char" /> is already lowercase, has no lowercase equivalent, or is not alphabetic.
		/// </returns>
		public char ToLowerInvariant()
		{
			return char.ToLowerInvariant(value);
		}
		/// <summary>
		/// Converts the value of this <see cref="char" /> to its equivalent <see cref="string" /> representation using the invariant culture.
		/// </summary>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="char" />.
		/// </returns>
		public string ToStringInvariant()
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="char" /> value is '\0', otherwise its original value.
		/// </summary>
		/// <returns>
		/// <see langword="null" />, if this <see cref="char" /> value is '\0';
		/// otherwise, its original value.
		/// </returns>
		public char? ToNullIfDefault()
		{
			return value == default(char) ? null : value;
		}
		/// <summary>
		/// Creates a <see cref="string" /> using the value of the this <see cref="char" /> parameter and repeats it a specified number of times.
		/// </summary>
		/// <param name="count">The number of times this <see cref="char" /> is repeated.</param>
		/// <returns>
		/// A new <see cref="string" /> object containing characters with the value of this <see cref="char" /> with a length based on <paramref name="count" />.
		/// </returns>
		public string Repeat(int count)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count);

			return new(value, count);
		}
	}
}