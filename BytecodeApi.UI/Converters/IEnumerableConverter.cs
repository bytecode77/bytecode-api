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
				switch (Method)
				{
					case IEnumerableConverterMethod.First: return value.Cast<object>().First();
					case IEnumerableConverterMethod.FirstOrDefault: return value.Cast<object>().FirstOrDefault();
					case IEnumerableConverterMethod.Last: return value.Cast<object>().Last();
					case IEnumerableConverterMethod.LastOrDefault: return value.Cast<object>().LastOrDefault();
					case IEnumerableConverterMethod.ElementAt: return value.Cast<object>().ElementAt((int)parameter);
					case IEnumerableConverterMethod.ElementAtOrDefault: return value.Cast<object>().ElementAtOrDefault((int)parameter);
					case IEnumerableConverterMethod.Count: return value.Count();
					case IEnumerableConverterMethod.Skip: return value.Cast<object>().Skip((int)parameter);
					case IEnumerableConverterMethod.Take: return value.Cast<object>().Take((int)parameter);
					case IEnumerableConverterMethod.JoinStrings: return value.Cast<object>().AsString();
					case IEnumerableConverterMethod.JoinStringsComma: return value.Cast<object>().Select(itm => itm?.ToString()).AsString(", ");
					case IEnumerableConverterMethod.AsMultilineString: return value.Cast<object>().Select(itm => itm?.ToString()).AsMultilineString();
					case IEnumerableConverterMethod.BooleansToIndeterminate:
						if (value is IEnumerable<bool> booleanCollection) return booleanCollection.ToIndeterminate();
						else if (value is IEnumerable<bool?> nullableBooleanCollection) return nullableBooleanCollection.ToIndeterminate();
						else throw Throw.UnsupportedType(nameof(value));
					default: throw Throw.InvalidEnumArgument(nameof(Method));
				}
			}
		}
	}
}