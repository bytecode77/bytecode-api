using BytecodeApi.Extensions;
using System.Collections;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="IEnumerable" /> values. The <see cref="Convert(IEnumerable, object)" /> method returns an <see cref="object" /> based on the specified <see cref="EnumerableConverterMethod" /> parameter.
/// </summary>
public sealed class EnumerableConverter : ConverterBase<IEnumerable?, object?>
{
	/// <summary>
	/// Specifies the method that is used to convert the <see cref="IEnumerable" /> value.
	/// </summary>
	public EnumerableConverterMethod Method { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="EnumerableConverter" /> class with the specified conversion method.
	/// </summary>
	/// <param name="method">The method that is used to convert the <see cref="IEnumerable" /> value.</param>
	public EnumerableConverter(EnumerableConverterMethod method)
	{
		Method = method;
	}

	/// <summary>
	/// Converts the <see cref="IEnumerable" /> value based the specified <see cref="EnumerableConverterMethod" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="IEnumerable" /> value to convert.</param>
	/// <param name="parameter">A parameter <see cref="object" /> that specifies the parameter used in some of the <see cref="EnumerableConverterMethod" /> methods.</param>
	/// <returns>
	/// An <see cref="object" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(IEnumerable? value, object? parameter)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return Method switch
			{
				EnumerableConverterMethod.First => value.Cast<object?>().First(),
				EnumerableConverterMethod.FirstOrDefault => value.Cast<object?>().FirstOrDefault(),
				EnumerableConverterMethod.Last => value.Cast<object?>().Last(),
				EnumerableConverterMethod.LastOrDefault => value.Cast<object?>().LastOrDefault(),
				EnumerableConverterMethod.ElementAt => parameter is int ? value.Cast<object?>().ElementAt((int)parameter) : null,
				EnumerableConverterMethod.ElementAtOrDefault => parameter is int ? value.Cast<object?>().ElementAtOrDefault((int)parameter) : null,
				EnumerableConverterMethod.Count => value.Cast<object?>().Count(),
				EnumerableConverterMethod.Contains => value.Cast<object?>().Contains(parameter),
				EnumerableConverterMethod.Skip => parameter is int ? value.Cast<object?>().Skip((int)parameter) : null,
				EnumerableConverterMethod.Take => parameter is int ? value.Cast<object?>().Take((int)parameter) : null,
				EnumerableConverterMethod.JoinStrings => value.Cast<object?>().Select(itm => itm?.ToString()).AsString(parameter?.ToString()),
				EnumerableConverterMethod.AsMultilineString => value.Cast<object?>().Select(itm => itm?.ToString()).AsMultilineString(),
				EnumerableConverterMethod.BooleansToIndeterminate when value is IEnumerable<bool> booleanCollection => booleanCollection.ToIndeterminate(),
				EnumerableConverterMethod.BooleansToIndeterminate when value is IEnumerable<bool?> nullableBooleanCollection => nullableBooleanCollection.ToIndeterminate(),
				EnumerableConverterMethod.BooleansToIndeterminate => null,
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
	}
}