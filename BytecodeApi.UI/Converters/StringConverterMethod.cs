namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Specifies the method that is used to convert <see cref="string" /> values.
	/// </summary>
	public enum StringConverterMethod
	{
		Concat,
		Trim,
		TrimStart,
		TrimStartString,
		TrimStartStringIgnoreCase,
		TrimEnd,
		TrimEndString,
		TrimEndStringIgnoreCase,
		ToUpper,
		ToLower,
		ToCamelCase,
		Substring,
		Left,
		Right,
		StartsWith,
		StartsWithIgnoreCase,
		EndsWith,
		EndsWithIgnoreCase,
		Contains,
		ContainsIgnoreCase,
		ReplaceLineBreaks
	}
}