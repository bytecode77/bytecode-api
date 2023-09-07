using BytecodeApi.Mathematics;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="long" />? values (or values that are convertible to <see cref="long" />) representing a byte size to an equivalent <see cref="string" /> representation. The <see cref="Convert(object)" /> method returns a <see cref="string" /> based on the specified parameters using the <see cref="ByteSize.Format()" /> method.
/// </summary>
public sealed class ByteSizeConverter : ConverterBase<object?>
{
	/// <summary>
	/// Specifies the <see cref="ByteSizeUnit" /> that is used to format the result <see cref="string" />, or <see langword="null" /> to use the <see cref="ByteSize.ClosestUnit" /> property.
	/// </summary>
	public ByteSizeUnit? Unit { get; set; }
	/// <summary>
	/// Specifies the number of decimals to round the result to. The default value is 2.
	/// </summary>
	public int Decimals { get; set; }
	/// <summary>
	/// <see langword="true" /> to pad zero decimal places with a '0' character.
	/// </summary>
	public bool PadDecimals { get; set; }
	/// <summary>
	/// <see langword="true" /> to use a thousands separator.
	/// </summary>
	public bool ThousandsSeparator { get; set; }
	/// <summary>
	/// <see langword="true" /> to always round up. The <see cref="Decimals" /> property should typically be 0, if this option is used.
	/// </summary>
	public bool RoundUp { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ByteSizeConverter" /> class.
	/// </summary>
	public ByteSizeConverter() : this(null)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteSizeConverter" /> class with the specified parameters.
	/// </summary>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	public ByteSizeConverter(int decimals) : this(null, decimals)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteSizeConverter" /> class with the specified parameters.
	/// </summary>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	public ByteSizeConverter(int decimals, bool padDecimals) : this(null, decimals, padDecimals)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteSizeConverter" /> class with the specified parameters.
	/// </summary>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	/// <param name="thousandsSeparator"><see langword="true" /> to use a thousands separator.</param>
	public ByteSizeConverter(int decimals, bool padDecimals, bool thousandsSeparator) : this(null, decimals, padDecimals, thousandsSeparator)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteSizeConverter" /> class with the specified parameters.
	/// </summary>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	/// <param name="thousandsSeparator"><see langword="true" /> to use a thousands separator.</param>
	/// <param name="roundUp"><see langword="true" /> to always round up. The <paramref name="decimals" /> parameter should typically be 0, if this option is used.</param>
	public ByteSizeConverter(int decimals, bool padDecimals, bool thousandsSeparator, bool roundUp) : this(null, decimals, padDecimals, thousandsSeparator, roundUp)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteSizeConverter" /> class with the specified parameters.
	/// </summary>
	/// <param name="unit">The <see cref="ByteSizeUnit" /> that is used to format the result <see cref="string" />.</param>
	public ByteSizeConverter(ByteSizeUnit? unit) : this(unit, ByteSize.DefaultFormatDecimals)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteSizeConverter" /> class with the specified parameters.
	/// </summary>
	/// <param name="unit">The <see cref="ByteSizeUnit" /> that is used to format the result <see cref="string" />.</param>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	public ByteSizeConverter(ByteSizeUnit? unit, int decimals) : this(unit, decimals, false)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteSizeConverter" /> class with the specified parameters.
	/// </summary>
	/// <param name="unit">The <see cref="ByteSizeUnit" /> that is used to format the result <see cref="string" />.</param>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	public ByteSizeConverter(ByteSizeUnit? unit, int decimals, bool padDecimals) : this(unit, decimals, padDecimals, false)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteSizeConverter" /> class with the specified parameters.
	/// </summary>
	/// <param name="unit">The <see cref="ByteSizeUnit" /> that is used to format the result <see cref="string" />.</param>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	/// <param name="thousandsSeparator"><see langword="true" /> to use a thousands separator.</param>
	public ByteSizeConverter(ByteSizeUnit? unit, int decimals, bool padDecimals, bool thousandsSeparator) : this(unit, decimals, padDecimals, thousandsSeparator, false)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteSizeConverter" /> class with the specified parameters.
	/// </summary>
	/// <param name="unit">The <see cref="ByteSizeUnit" /> that is used to format the result <see cref="string" />.</param>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	/// <param name="thousandsSeparator"><see langword="true" /> to use a thousands separator.</param>
	/// <param name="roundUp"><see langword="true" /> to always round up. The <paramref name="decimals" /> parameter should typically be 0, if this option is used.</param>
	public ByteSizeConverter(ByteSizeUnit? unit, int decimals, bool padDecimals, bool thousandsSeparator, bool roundUp)
	{
		Unit = unit;
		Decimals = decimals;
		PadDecimals = padDecimals;
		ThousandsSeparator = thousandsSeparator;
		RoundUp = roundUp;
	}

	/// <summary>
	/// Converts the <see cref="object" /> that is convertible to an <see cref="long" />? value based on the specified parameters.
	/// </summary>
	/// <param name="value">The <see cref="object" /> that is convertible to an <see cref="long" />? value to convert.</param>
	/// <returns>
	/// A <see cref="string" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(object? value)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			ByteSize byteSize = System.Convert.ToInt64(value);

			if (Unit == null)
			{
				return byteSize.Format(Decimals, PadDecimals, ThousandsSeparator, RoundUp);
			}
			else
			{
				return byteSize.Format(Unit.Value, Decimals, PadDecimals, ThousandsSeparator, RoundUp);
			}
		}
	}
}