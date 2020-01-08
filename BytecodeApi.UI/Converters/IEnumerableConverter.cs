using BytecodeApi.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="IEnumerable" /> values. The <see cref="Convert(IEnumerable, object)" /> method returns an <see cref="object" /> based on the specified <see cref="IEnumerableConverterMethod" /> parameter.
	/// </summary>
	public sealed class IEnumerableConverter : ConverterBase<IEnumerable, object, object>
	{
		/// <summary>
		/// Specifies the method that is used to convert the <see cref="IEnumerable" /> value.
		/// </summary>
		public IEnumerableConverterMethod Method { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="IEnumerableConverter" /> class with the specified conversion method.
		/// </summary>
		/// <param name="method">The method that is used to convert the <see cref="IEnumerable" /> value.</param>
		public IEnumerableConverter(IEnumerableConverterMethod method)
		{
			Method = method;
		}

		/// <summary>
		/// Converts the <see cref="IEnumerable" /> value based the specified <see cref="IEnumerableConverterMethod" /> parameter.
		/// </summary>
		/// <param name="value">The <see cref="IEnumerable" /> value to convert.</param>
		/// <param name="parameter">A parameter <see cref="object" /> that specifies the parameter used in some of the <see cref="IEnumerableConverterMethod" /> methods.</param>
		/// <returns>
		/// An <see cref="object" /> with the result of the conversion.
		/// </returns>
		public override object Convert(IEnumerable value, object parameter)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				return Method switch
				{
					IEnumerableConverterMethod.First => value.Cast<object>().First(),
					IEnumerableConverterMethod.FirstOrDefault => value.Cast<object>().FirstOrDefault(),
					IEnumerableConverterMethod.Last => value.Cast<object>().Last(),
					IEnumerableConverterMethod.LastOrDefault => value.Cast<object>().LastOrDefault(),
					IEnumerableConverterMethod.ElementAt => value.Cast<object>().ElementAt((int)parameter),
					IEnumerableConverterMethod.ElementAtOrDefault => value.Cast<object>().ElementAtOrDefault((int)parameter),
					IEnumerableConverterMethod.Count => value.Count(),
					IEnumerableConverterMethod.Skip => value.Cast<object>().Skip((int)parameter),
					IEnumerableConverterMethod.Take => value.Cast<object>().Take((int)parameter),
					IEnumerableConverterMethod.JoinStrings => value.Cast<object>().AsString(),
					IEnumerableConverterMethod.JoinStringsComma => value.Cast<object>().Select(itm => itm?.ToString()).AsString(", "),
					IEnumerableConverterMethod.AsMultilineString => value.Cast<object>().Select(itm => itm?.ToString()).AsMultilineString(),
					IEnumerableConverterMethod.BooleansToIndeterminate =>
						value is IEnumerable<bool> booleanCollection ? booleanCollection.ToIndeterminate() :
						value is IEnumerable<bool?> nullableBooleanCollection ? nullableBooleanCollection.ToIndeterminate() :
						throw Throw.UnsupportedType(nameof(value)),
					_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
				};
			}
		}
	}
}