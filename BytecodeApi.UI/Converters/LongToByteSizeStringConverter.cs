using BytecodeApi.Text;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="long" /> values to a <see cref="string" /> using the <see cref="Wording.FormatByteSizeString(long, ByteSizeFormat)" /> method using the specified <see cref="ByteSizeFormat" /> in the given parameter.
	/// </summary>
	public sealed class LongToByteSizeStringConverter : ConverterBase<long?, ByteSizeFormat?, string>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LongToByteSizeStringConverter" /> class.
		/// </summary>
		public LongToByteSizeStringConverter()
		{
		}

		/// <summary>
		/// Converts a <see cref="long" /> value to a <see cref="string" /> using the <see cref="Wording.FormatByteSizeString(long, ByteSizeFormat)" /> method using the specified <see cref="ByteSizeFormat" /> in the given parameter.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to convert.</param>
		/// <param name="parameter">The <see cref="ByteSizeFormat" /> specifying the format to use for conversion. The default value is <see cref="ByteSizeFormat.ByteSizeTwoDigits" />.</param>
		/// <returns>
		/// An equivalent <see cref="string" /> representation of the specified value.
		/// </returns>
		public override string Convert(long? value, ByteSizeFormat? parameter)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				Check.ArgumentOutOfRangeEx.GreaterEqual0(value.Value, nameof(value));

				return Wording.FormatByteSizeString(value.Value, parameter ?? ByteSizeFormat.ByteSizeTwoDigits);
			}
		}
	}
}