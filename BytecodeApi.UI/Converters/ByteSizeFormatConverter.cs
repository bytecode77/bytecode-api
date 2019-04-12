using BytecodeApi.Text;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="long" />? values representing a byte size to an equivalent <see cref="string" /> representation. The <see cref="Convert(long?)" /> method returns a <see cref="string" /> based on the specified <see cref="ByteSizeFormat" /> parameter. used for the <see cref="Wording.FormatByteSizeString(long, ByteSizeFormat)" /> method.
	/// </summary>
	public sealed class ByteSizeFormatConverter : ConverterBase<long?, string>
	{
		/// <summary>
		/// Specifies the format parameter that is passed to the <see cref="Wording.FormatByteSizeString(long, ByteSizeFormat)" /> method during conversion.
		/// </summary>
		public ByteSizeFormat Format { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ByteSizeFormatConverter" /> class with the specified <paramref name="format" /> property.
		/// </summary>
		/// <param name="format">The format parameter that is passed to the <see cref="Wording.FormatByteSizeString(long, ByteSizeFormat)" /> method during conversion.</param>
		public ByteSizeFormatConverter(ByteSizeFormat format)
		{
			Format = format;
		}

		/// <summary>
		/// Converts the <see cref="long" />? value based on the specified <see cref="Format" /> parameter.
		/// </summary>
		/// <param name="value">The <see cref="long" />? value to convert.</param>
		/// <returns>
		/// A <see cref="string" /> with the result of the conversion.
		/// </returns>
		public override string Convert(long? value)
		{
			return value == null ? null : Wording.FormatByteSizeString(value.Value, Format);
		}
	}
}