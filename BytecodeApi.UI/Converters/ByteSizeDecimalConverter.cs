using BytecodeApi.Text;

namespace BytecodeApi.UI.Converters
{
	public sealed class ByteSizeDecimalConverter : ConverterBase<long?, string>
	{
		public int DecimalPlaces { get; }

		public ByteSizeDecimalConverter() : this(Wording.DefaultFormatByteSizeStringDecimalPlaces)
		{
		}
		public ByteSizeDecimalConverter(int decimalPlaces)
		{
			DecimalPlaces = decimalPlaces;
		}

		public override string Convert(long? value)
		{
			return value == null ? null : Wording.FormatByteSizeString(value.Value, DecimalPlaces);
		}
	}
}