using BytecodeApi.Extensions;
using BytecodeApi.Text;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts or manipulates strings. The <see cref="Convert(object?, object)" /> method returns an <see cref="object" /> based on the specified <see cref="StringConverterMethod" /> parameter.
/// </summary>
public sealed class StringConverter : ConverterBase<object, object>
{
	/// <summary>
	/// Specifies the method that is used to convert the <see cref="string" /> value.
	/// </summary>
	public StringConverterMethod Method { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="StringConverter" /> class with the specified conversion method.
	/// </summary>
	/// <param name="method">The method that is used to convert the <see cref="string" /> value.</param>
	public StringConverter(StringConverterMethod method)
	{
		Method = method;
	}

	/// <summary>
	/// Converts or manipulates the <see cref="string" /> value based on the specified <see cref="StringConverterMethod" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="string" /> value to convert.</param>
	/// <param name="parameter">A parameter <see cref="object" /> that specifies the parameter used in some of the <see cref="StringConverterMethod" /> methods.</param>
	/// <returns>
	/// An <see cref="object" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(object? value, object? parameter)
	{
		string? str = value?.ToString();

		return Method switch
		{
			StringConverterMethod.Coalesce => str.IsNullOrEmpty() ? parameter : str,
			StringConverterMethod.Concat => str + parameter,
			StringConverterMethod.ConcatIfNotEmpty => str.IsNullOrEmpty() ? str : str + parameter,
			StringConverterMethod.ConcatBefore => parameter + str,
			StringConverterMethod.ConcatBeforeIfNotEmpty => str.IsNullOrEmpty() ? str : parameter + str,
			StringConverterMethod.Split => str?.Split(parameter?.ToString()),
			StringConverterMethod.SplitRemoveEmpty => str?.Split(parameter?.ToString(), StringSplitOptions.RemoveEmptyEntries),
			StringConverterMethod.SplitRemoveEmptyTrim => str?.Split(parameter?.ToString(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
			StringConverterMethod.Trim => str?.Trim(),
			StringConverterMethod.TrimStart => str?.TrimStart(),
			StringConverterMethod.TrimStartString => str?.TrimStartString(parameter?.ToString() ?? ""),
			StringConverterMethod.TrimStartStringIgnoreCase => str?.TrimStartString(parameter?.ToString() ?? "", StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.TrimEnd => str?.TrimEnd(),
			StringConverterMethod.TrimEndString => str?.TrimEndString(parameter?.ToString() ?? ""),
			StringConverterMethod.TrimEndStringIgnoreCase => str?.TrimEndString(parameter?.ToString() ?? "", StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.EnsureStartsWith => str?.EnsureStartsWith(parameter?.ToString() ?? ""),
			StringConverterMethod.EnsureStartsWithIgnoreCase => str?.EnsureStartsWith(parameter?.ToString() ?? "", StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.EnsureEndsWith => str?.EnsureEndsWith(parameter?.ToString() ?? ""),
			StringConverterMethod.EnsureEndsWithIgnoreCase => str?.EnsureEndsWith(parameter?.ToString() ?? "", StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.ToLower => str?.ToLower(),
			StringConverterMethod.ToUpper => str?.ToUpper(),
			StringConverterMethod.ToCamelCase => str?.ChangeCasing(StringCasing.CamelCase),
			StringConverterMethod.ToLowerSnakeCase => str?.ChangeCasing(StringCasing.LowerSnakeCase),
			StringConverterMethod.ToUpperSnakeCase => str?.ChangeCasing(StringCasing.UpperSnakeCase),
			StringConverterMethod.ToLowerKebabCase => str?.ChangeCasing(StringCasing.LowerKebabCase),
			StringConverterMethod.ToUpperKebabCase => str?.ChangeCasing(StringCasing.UpperKebabCase),
			StringConverterMethod.Substring => parameter is int ? str?[(int)parameter..] : null,
			StringConverterMethod.SubstringFrom => str?.SubstringFrom(parameter?.ToString() ?? ""),
			StringConverterMethod.SubstringFromIgnoreCase => str?.SubstringFrom(parameter?.ToString() ?? "", false, StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.SubstringFromInclusive => str?.SubstringFrom(parameter?.ToString() ?? "", true),
			StringConverterMethod.SubstringFromInclusiveIgnoreCase => str?.SubstringFrom(parameter?.ToString() ?? "", true, StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.SubstringFromLast => str?.SubstringFromLast(parameter?.ToString() ?? ""),
			StringConverterMethod.SubstringFromLastIgnoreCase => str?.SubstringFromLast(parameter?.ToString() ?? "", false, StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.SubstringFromLastInclusive => str?.SubstringFromLast(parameter?.ToString() ?? "", true),
			StringConverterMethod.SubstringFromLastInclusiveIgnoreCase => str?.SubstringFromLast(parameter?.ToString() ?? "", true, StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.SubstringUntil => str?.SubstringUntil(parameter?.ToString() ?? ""),
			StringConverterMethod.SubstringUntilIgnoreCase => str?.SubstringUntil(parameter?.ToString() ?? "", false, StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.SubstringUntilInclusive => str?.SubstringUntil(parameter?.ToString() ?? "", true),
			StringConverterMethod.SubstringUntilInclusiveIgnoreCase => str?.SubstringUntil(parameter?.ToString() ?? "", true, StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.SubstringUntilLast => str?.SubstringUntilLast(parameter?.ToString() ?? ""),
			StringConverterMethod.SubstringUntilLastIgnoreCase => str?.SubstringUntilLast(parameter?.ToString() ?? "", false, StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.SubstringUntilLastInclusive => str?.SubstringUntilLast(parameter?.ToString() ?? "", true),
			StringConverterMethod.SubstringUntilLastInclusiveIgnoreCase => str?.SubstringUntilLast(parameter?.ToString() ?? "", true, StringComparison.OrdinalIgnoreCase),
			StringConverterMethod.Left => parameter is int ? str?.Left((int)parameter) : null,
			StringConverterMethod.Right => parameter is int ? str?.Right((int)parameter) : null,
			StringConverterMethod.StartsWith => str?.StartsWith(parameter?.ToString() ?? "") == true,
			StringConverterMethod.StartsWithIgnoreCase => str?.StartsWith(parameter?.ToString() ?? "", StringComparison.OrdinalIgnoreCase) == true,
			StringConverterMethod.EndsWith => str?.EndsWith(parameter?.ToString() ?? "") == true,
			StringConverterMethod.EndsWithIgnoreCase => str?.EndsWith(parameter?.ToString() ?? "", StringComparison.OrdinalIgnoreCase) == true,
			StringConverterMethod.Reverse => str?.Reverse(),
			StringConverterMethod.Contains => str?.Contains(parameter?.ToString() ?? "") == true,
			StringConverterMethod.ContainsIgnoreCase => str?.Contains(parameter?.ToString() ?? "", StringComparison.OrdinalIgnoreCase) == true,
			StringConverterMethod.ReplaceLineBreaks => str?.ReplaceLineEndings(parameter?.ToString() ?? ""),
			StringConverterMethod.SplitToLines => str?.SplitToLines(),
			StringConverterMethod.SplitToLinesRemoveEmpty => str?.SplitToLines(true),
			StringConverterMethod.SplitToLinesTrim => str?.SplitToLines(false, true),
			StringConverterMethod.SplitToLinesRemoveEmptyTrim => str?.SplitToLines(true, true),
			StringConverterMethod.TrimText => str != null && parameter is int ? Wording.TrimText(str, (int)parameter) : null,
			StringConverterMethod.StringDistanceLevenshtein => StringDistance.Levenshtein(str ?? "", parameter?.ToString() ?? ""),
			StringConverterMethod.StringDistanceDamerauLevenshtein => StringDistance.DamerauLevenshtein(str ?? "", parameter?.ToString() ?? ""),
			_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
		};
	}
}