using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts or manipulates strings. The <see cref="Convert(string, object)" /> method returns an <see cref="object" /> based on the specified <see cref="StringConverterMethod" /> parameter.
	/// </summary>
	public sealed class StringConverter : ConverterBase<string, object, object>
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
		public override object Convert(string value, object parameter)
		{
			return Method switch
			{
				StringConverterMethod.Concat => value + parameter,
				StringConverterMethod.Trim => value?.Trim(),
				StringConverterMethod.TrimStart => value?.TrimStart(),
				StringConverterMethod.TrimStartString => value?.TrimStartString(parameter?.ToString()),
				StringConverterMethod.TrimStartStringIgnoreCase => value?.TrimStartString(parameter?.ToString(), true),
				StringConverterMethod.TrimEnd => value?.TrimEnd(),
				StringConverterMethod.TrimEndString => value?.TrimEndString(parameter?.ToString()),
				StringConverterMethod.TrimEndStringIgnoreCase => value?.TrimEndString(parameter?.ToString(), true),
				StringConverterMethod.ToLower => value?.ToLower(),
				StringConverterMethod.ToUpper => value?.ToUpper(),
				StringConverterMethod.ToCamelCase => value?.ChangeCasing(StringCasing.CamelCase),
				StringConverterMethod.ToLowerSnakeCase => value?.ChangeCasing(StringCasing.LowerSnakeCase),
				StringConverterMethod.ToUpperSnakeCase => value?.ChangeCasing(StringCasing.UpperSnakeCase),
				StringConverterMethod.ToLowerKebabCase => value?.ChangeCasing(StringCasing.LowerKebabCase),
				StringConverterMethod.ToUpperKebabCase => value?.ChangeCasing(StringCasing.UpperKebabCase),
				StringConverterMethod.Substring => value?.Substring((int)parameter),
				StringConverterMethod.Left => value?.Left((int)parameter),
				StringConverterMethod.Right => value?.Right((int)parameter),
				StringConverterMethod.StartsWith => value?.StartsWith(parameter?.ToString()) == true,
				StringConverterMethod.StartsWithIgnoreCase => value?.StartsWith(parameter?.ToString(), StringComparison.OrdinalIgnoreCase) == true,
				StringConverterMethod.EndsWith => value?.EndsWith(parameter?.ToString()) == true,
				StringConverterMethod.EndsWithIgnoreCase => value?.EndsWith(parameter?.ToString(), StringComparison.OrdinalIgnoreCase) == true,
				StringConverterMethod.Contains => value?.Contains(parameter?.ToString()) == true,
				StringConverterMethod.ContainsIgnoreCase => value?.Contains(parameter?.ToString(), SpecialStringComparisons.IgnoreCase) == true,
				StringConverterMethod.ReplaceLineBreaks => value?.ReplaceLineBreaks(parameter?.ToString()),
				StringConverterMethod.TrimText => value == null ? null : Wording.TrimText(value, (int)parameter),
				StringConverterMethod.StringDistanceLevenshtein => value != null && parameter is string ? StringDistance.Levenshtein(value, (string)parameter) : (int?)null,
				StringConverterMethod.StringDistanceDamerauLevenshtein => value != null && parameter is string ? StringDistance.DamerauLevenshtein(value, (string)parameter) : (int?)null,
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
	}
}