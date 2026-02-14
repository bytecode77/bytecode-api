namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to convert <see cref="string" /> values.
/// </summary>
public enum StringConverterMethod
{
	/// <summary>
	/// Returns the parameter <see cref="string" />, if the <see cref="string" /> value is <see langword="null" /> or empty, otherwise, returns the original <see cref="string" /> value.
	/// </summary>
	Coalesce,
	/// <summary>
	/// Concatenates the value and parameter and returns the combined <see cref="string" />.
	/// </summary>
	Concat,
	/// <summary>
	/// Concatenates the value and parameter and returns the combined <see cref="string" />. If value is empty, the parameter is not concatenated.
	/// </summary>
	ConcatIfNotEmpty,
	/// <summary>
	/// Concatenates the parameter before the value and returns the combined <see cref="string" />.
	/// </summary>
	ConcatBefore,
	/// <summary>
	/// Concatenates the parameter before the value and returns the combined <see cref="string" />. If value is empty, the parameter is not concatenated.
	/// </summary>
	ConcatBeforeIfNotEmpty,
	/// <summary>
	/// Splits the <see cref="string" /> value by the separator in the parameter <see cref="string" />.
	/// Returns a new <see cref="string" />[] containing the individual values.
	/// </summary>
	Split,
	/// <summary>
	/// Splits the <see cref="string" /> value by the separator in the parameter <see cref="string" /> and removes empty entries.
	/// Returns a new <see cref="string" />[] containing the individual values.
	/// </summary>
	SplitRemoveEmpty,
	/// <summary>
	/// Splits the <see cref="string" /> value by the separator in the parameter <see cref="string" />, removes empty entries, and trims the resulting values.
	/// Returns a new <see cref="string" />[] containing the individual values.
	/// </summary>
	SplitRemoveEmptyTrim,
	/// <summary>
	/// Removes all leading and trailing white-space characters from the <see cref="string" /> value.
	/// </summary>
	Trim,
	/// <summary>
	/// Removes all leading white-space characters from the <see cref="string" /> value.
	/// </summary>
	TrimStart,
	/// <summary>
	/// Removes all leading occurrences of the parameter <see cref="string" /> from the <see cref="string" /> value.
	/// </summary>
	TrimStartString,
	/// <summary>
	/// Removes all leading occurrences of the parameter <see cref="string" /> from the <see cref="string" /> value, ignoring character casing.
	/// </summary>
	TrimStartStringIgnoreCase,
	/// <summary>
	/// Removes all trailing white-space characters from the <see cref="string" /> value.
	/// </summary>
	TrimEnd,
	/// <summary>
	/// Removes all trailing occurrences of the parameter <see cref="string" /> from the <see cref="string" /> value.
	/// </summary>
	TrimEndString,
	/// <summary>
	/// Removes all trailing occurrences of the parameter <see cref="string" /> from the <see cref="string" /> value, ignoring character casing.
	/// </summary>
	TrimEndStringIgnoreCase,
	/// <summary>
	/// Appends the parameter <see cref="string" /> at the beginning of the <see cref="string" /> value, if it does not start with the contents of the parameter <see cref="string" />.
	/// </summary>
	EnsureStartsWith,
	/// <summary>
	/// Appends the parameter <see cref="string" /> at the beginning of the <see cref="string" /> value, if it does not start with the contents of the parameter <see cref="string" />, ignoring character casing.
	/// </summary>
	EnsureStartsWithIgnoreCase,
	/// <summary>
	/// Appends the parameter <see cref="string" /> to the <see cref="string" /> value, if it does not start with the contents of the parameter <see cref="string" />.
	/// </summary>
	EnsureEndsWith,
	/// <summary>
	/// Appends the parameter <see cref="string" /> to the <see cref="string" /> value, if it does not start with the contents of the parameter <see cref="string" />, ignoring character casing.
	/// </summary>
	EnsureEndsWithIgnoreCase,
	/// <summary>
	/// Returns the <see cref="string" /> value in its lowercase representation.
	/// </summary>
	ToLower,
	/// <summary>
	/// Returns the <see cref="string" /> value in its uppercase representation.
	/// </summary>
	ToUpper,
	/// <summary>
	/// Returns the <see cref="string" /> value in its camel case representation using the <see cref="StringCasing.CamelCase" /> method.
	/// </summary>
	ToCamelCase,
	/// <summary>
	/// Returns the <see cref="string" /> value in its lower snake case representation using the <see cref="StringCasing.LowerSnakeCase" /> method.
	/// </summary>
	ToLowerSnakeCase,
	/// <summary>
	/// Returns the <see cref="string" /> value in its upper snake case representation using the <see cref="StringCasing.UpperSnakeCase" /> method.
	/// </summary>
	ToUpperSnakeCase,
	/// <summary>
	/// Returns the <see cref="string" /> value in its lower kebab case representation using the <see cref="StringCasing.LowerKebabCase" /> method.
	/// </summary>
	ToLowerKebabCase,
	/// <summary>
	/// Returns the <see cref="string" /> value in its upper kebab case representation using the <see cref="StringCasing.UpperKebabCase" /> method.
	/// </summary>
	ToUpperKebabCase,
	/// <summary>
	/// Returns a substring from the <see cref="string" /> value. The substring starts at a specified character position in the parameter and continues to the end of the string.
	/// </summary>
	Substring,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value after the first occurrence of the <see cref="string" /> parameter.
	/// </summary>
	SubstringFrom,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value after the first occurrence of the <see cref="string" /> parameter, ignoring character casing.
	/// </summary>
	SubstringFromIgnoreCase,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value after the first occurrence of the <see cref="string" /> parameter. The contents of <see cref="string" /> parameter are included in the result.
	/// </summary>
	SubstringFromInclusive,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value after the first occurrence of the <see cref="string" /> parameter, ignoring character casing. The contents of <see cref="string" /> parameter are included in the result.
	/// </summary>
	SubstringFromInclusiveIgnoreCase,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value after the last occurrence of the <see cref="string" /> parameter.
	/// </summary>
	SubstringFromLast,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value after the last occurrence of the <see cref="string" /> parameter, ignoring character casing.
	/// </summary>
	SubstringFromLastIgnoreCase,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value after the last occurrence of the <see cref="string" /> parameter. The contents of <see cref="string" /> parameter are included in the result.
	/// </summary>
	SubstringFromLastInclusive,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value after the last occurrence of the <see cref="string" /> parameter, ignoring character casing. The contents of <see cref="string" /> parameter are included in the result.
	/// </summary>
	SubstringFromLastInclusiveIgnoreCase,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value before the first occurrence of the <see cref="string" /> parameter.
	/// </summary>
	SubstringUntil,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value before the first occurrence of the <see cref="string" /> parameter, ignoring character casing.
	/// </summary>
	SubstringUntilIgnoreCase,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value before the first occurrence of the <see cref="string" /> parameter. The contents of <see cref="string" /> parameter are included in the result.
	/// </summary>
	SubstringUntilInclusive,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value before the first occurrence of the <see cref="string" /> parameter, ignoring character casing. The contents of <see cref="string" /> parameter are included in the result.
	/// </summary>
	SubstringUntilInclusiveIgnoreCase,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value before the last occurrence of the <see cref="string" /> parameter.
	/// </summary>
	SubstringUntilLast,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value before the last occurrence of the <see cref="string" /> parameter, ignoring character casing.
	/// </summary>
	SubstringUntilLastIgnoreCase,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value before the last occurrence of the <see cref="string" /> parameter. The contents of <see cref="string" /> parameter are included in the result.
	/// </summary>
	SubstringUntilLastInclusive,
	/// <summary>
	/// Returns the portion of the <see cref="string" /> value before the last occurrence of the <see cref="string" /> parameter, ignoring character casing. The contents of <see cref="string" /> parameter are included in the result.
	/// </summary>
	SubstringUntilLastInclusiveIgnoreCase,
	/// <summary>
	/// Returns a <see cref="string" /> containing a specified number of characters from the left side of the <see cref="string" /> value. If the <see cref="string" /> is longer than the specified length, the <see cref="string" /> is truncated by the length parameter, otherwise, the original <see cref="string" /> is returned.
	/// </summary>
	Left,
	/// <summary>
	/// Returns a <see cref="string" /> containing a specified number of characters from the right side of the <see cref="string" /> value. If the <see cref="string" /> is longer than the specified length, the <see cref="string" /> is truncated by the length parameter, otherwise, the original <see cref="string" /> is returned.
	/// </summary>
	Right,
	/// <summary>
	/// Returns a <see cref="bool" /> value indicating whether the beginning of the <see cref="string" /> value matches the <see cref="string" /> parameter.
	/// </summary>
	StartsWith,
	/// <summary>
	/// Returns a <see cref="bool" /> value indicating whether the beginning of the <see cref="string" /> value matches the <see cref="string" /> parameter, ignoring character casing.
	/// </summary>
	StartsWithIgnoreCase,
	/// <summary>
	/// Returns a <see cref="bool" /> value indicating whether the end of the <see cref="string" /> value matches the <see cref="string" /> parameter.
	/// </summary>
	EndsWith,
	/// <summary>
	/// Returns a <see cref="bool" /> value indicating whether the end of the <see cref="string" /> value matches the <see cref="string" /> parameter, ignoring character casing.
	/// </summary>
	EndsWithIgnoreCase,
	/// <summary>
	/// Reverses the sequence of all characters in the <see cref="string" /> value.
	/// </summary>
	Reverse,
	/// <summary>
	/// Returns a <see cref="bool" /> value indicating whether the <see cref="string" /> parameter occurs within the <see cref="string" /> value.
	/// </summary>
	Contains,
	/// <summary>
	/// Returns a <see cref="bool" /> value indicating whether the <see cref="string" /> parameter occurs within the <see cref="string" /> value, ignoring character casing.
	/// </summary>
	ContainsIgnoreCase,
	/// <summary>
	/// Replaces all occurrences of linebreaks ("\n" and "\r\n") in the <see cref="string" /> value with a replacement value in the <see cref="string" /> parameter.
	/// </summary>
	ReplaceLineBreaks,
	/// <summary>
	/// Splits the <see cref="string" /> value into an array of lines, which are separated by either a CR or a CRLF separator.
	/// </summary>
	SplitToLines,
	/// <summary>
	/// Splits the <see cref="string" /> value into an array of lines, which are separated by either a CR or a CRLF separator. Empty lines are removed.
	/// </summary>
	SplitToLinesRemoveEmpty,
	/// <summary>
	/// Splits the <see cref="string" /> value into an array of lines, which are separated by either a CR or a CRLF separator. Lines are trimmed after splitting.
	/// </summary>
	SplitToLinesTrim,
	/// <summary>
	/// Splits the <see cref="string" /> value into an array of lines, which are separated by either a CR or a CRLF separator. Empty lines are removed. Lines are trimmed after splitting.
	/// </summary>
	SplitToLinesRemoveEmptyTrim,
	/// <summary>
	/// Trims the <see cref="string" /> value by the specified length in the <see cref="int" /> parameter. If the <see cref="string" /> is longer than the value of length, it will be truncated by a leading "..." to match the length parameter, including the length of the "..." appendix (3 characters).
	/// </summary>
	TrimText,
	/// <summary>
	/// Returns a <see cref="int" /> value representing the levenshtein distance between the <see cref="string" /> and the parameter <see cref="string" />.
	/// </summary>
	StringDistanceLevenshtein,
	/// <summary>
	/// Returns a <see cref="int" /> value representing the damerau-levenshtein distance between the <see cref="string" /> and the parameter <see cref="string" />.
	/// </summary>
	StringDistanceDamerauLevenshtein
}