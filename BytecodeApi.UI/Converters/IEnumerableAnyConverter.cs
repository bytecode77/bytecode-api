using BytecodeApi.Extensions;
using System.Collections;
using System.Linq;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="IEnumerable" /> values based on <see cref="IEnumerable" /> emptiness. The <see cref="Convert(IEnumerable)" /> method returns an <see cref="object" /> based on the specified <see cref="BooleanConverterMethod" /> parameter.
	/// </summary>
	public sealed class IEnumerableAnyConverter : ConverterBase<IEnumerable, object>
	{
		/// <summary>
		/// Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(IEnumerable)" /> method returns.
		/// </summary>
		public BooleanConverterMethod Result { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="IEnumerableAnyConverter" /> class with the specified result transformation.
		/// </summary>
		/// <param name="result">Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(IEnumerable)" /> method returns.</param>
		public IEnumerableAnyConverter(BooleanConverterMethod result)
		{
			Result = result;
		}

		/// <summary>
		/// Converts the <see cref="IEnumerable" /> value based on <see cref="IEnumerable" /> emptiness and the specified <see cref="BooleanConverterMethod" /> parameter.
		/// </summary>
		/// <param name="value">The <see cref="IEnumerable" /> value to convert.</param>
		/// <returns>
		/// An <see cref="object" /> with the result of the conversion.
		/// </returns>
		public override object Convert(IEnumerable value)
		{
			return new BooleanConverter(Result).Convert(value?.Any());
		}
	}
}