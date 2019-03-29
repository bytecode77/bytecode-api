using BytecodeApi.Extensions;
using System;

namespace BytecodeApi
{
	/// <summary>
	/// Specifies flags for a custom <see cref="string" /> comparison used in <see cref="StringExtensions.CompareTo" />.
	/// </summary>
	[Flags]
	public enum SpecialStringComparisons
	{
		/// <summary>
		/// Specifies the default <see cref="string" /> comparison as used in <see cref="string.Compare(string, string)" />.
		/// </summary>
		Default = 0,
		/// <summary>
		/// Specifies that strings which are either <see langword="null" /> or <see cref="string.Empty" /> are treated equally.
		/// </summary>
		NullAndEmptyEqual = 1,
		/// <summary>
		/// Specifies that leading and trailing whitespaces are ignored during comparison.
		/// </summary>
		Trim = 2,
		/// <summary>
		/// Specifies that character casing is ignored during comparison.
		/// </summary>
		IgnoreCase = 4,
		/// <summary>
		/// Specifies that all whitespace characters are ignored during comparison.
		/// </summary>
		IgnoreWhiteSpaces = 8,
		/// <summary>
		/// Specifies that all punctuation characters are ignored during comparison.
		/// </summary>
		IgnorePunctuation = 16,
		/// <summary>
		/// Specifies that all symbol characters are ignored during comparison.
		/// </summary>
		IgnoreSymbols = 32,
		/// <summary>
		/// Specifies that a natural <see cref="string" /> comparison is used.
		/// </summary>
		Natural = 64
	}
}