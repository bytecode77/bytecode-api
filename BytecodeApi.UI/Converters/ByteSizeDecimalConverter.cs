using BytecodeApi.Text;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="long" />? values representing a byte size to an equivalent <see cref="string" /> representation. The <see cref="Convert(long?)" /> method returns a <see cref="string" /> based on the specified <see cref="Decimals" /> parameter. used for the <see cref="Wording.FormatByteSizeString(long, int)" /> method.
	/// </summary>
	public sealed class ByteSizeDecimalConverter : ConverterBase<long?, string>
	{
		/// <summary>
		/// Specifies the decimals parameter that is passed to the <see cref="Wording.FormatByteSizeString(long, int)" /> method during conversion.
		/// </summary>
		public int Decimals { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ByteSizeDecimalConverter" /> class.
		/// </summary>
		public ByteSizeDecimalConverter() : this(Wording.DefaultFormatByteSizeStringDecimals)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ByteSizeDecimalConverter" /> class with the specified <paramref name="decimals" /> property.
		/// </summary>
		/// <param name="decimals">The decimals parameter that is passed to the <see cref="Wording.FormatByteSizeString(long, int)" /> method during conversion.</param>
		public ByteSizeDecimalConverter(int decimals)
		{
			Decimals = decimals;
		}

		/// <summary>
		/// Converts the <see cref="long" />? value based on the specified <see cref="Decimals" /> parameter.
		/// </summary>
		/// <param name="value">The <see cref="long" />? value to convert.</param>
		/// <returns>
		/// A <see cref="string" /> with the result of the conversion.
		/// </returns>
		public override string Convert(long? value)
		{
			return value == null ? null : Wording.FormatByteSizeString(value.Value, Decimals);
		}
	}
}