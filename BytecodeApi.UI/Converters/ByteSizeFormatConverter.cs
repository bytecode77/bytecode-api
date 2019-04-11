using BytecodeApi.Text;

namespace BytecodeApi.UI.Converters
{
	public sealed class ByteSizeFormatConverter : ConverterBase<long?, string>
	{
		public ByteSizeFormat Format { get; set; }

		public ByteSizeFormatConverter(ByteSizeFormat format)
		{
			Format = format;
		}

		public override string Convert(long? value)
		{
			return value == null ? null : Wording.FormatByteSizeString(value.Value, Format);
		}
	}
}