namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="IComparable" /> values based on whether they lie in a specific range. The <see cref="Convert(IComparable)" /> method returns an <see cref="object" /> based on the specified min/max parameters and the <see cref="BooleanConverterMethod" /> parameter.
/// </summary>
public sealed class RangeConverter : ConverterBase<IComparable>
{
	/// <summary>
	/// Specifies the minimum value of the range.
	/// </summary>
	public IComparable Min { get; set; }
	/// <summary>
	/// Specifies the maximum value of the range.
	/// </summary>
	public IComparable Max { get; set; }
	/// <summary>
	/// Specifies whether the minimum value is inclusive or exclusive.
	/// </summary>
	public bool IsMinExclusive { get; set; }
	/// <summary>
	/// Specifies whether the maximum value is inclusive or exclusive.
	/// </summary>
	public bool IsMaxExclusive { get; set; }

	/// <summary>
	/// Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(IComparable)" /> method returns.
	/// </summary>
	public BooleanConverterMethod Result { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="RangeConverter" /> class with the specified range. By default, <paramref name="min" /> and <paramref name="max" /> are inclusive.
	/// </summary>
	/// <param name="min">The minimum value of the range.</param>
	/// <param name="max">The maximum value of the range.</param>
	public RangeConverter(IComparable min, IComparable max) : this(min, max, BooleanConverterMethod.Default)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="RangeConverter" /> class with the specified range and result transformation. By default, <paramref name="min" /> and <paramref name="max" /> are inclusive.
	/// </summary>
	/// <param name="min">The minimum value of the range.</param>
	/// <param name="max">The maximum value of the range.</param>
	/// <param name="result">Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(IComparable)" /> method returns.</param>
	public RangeConverter(IComparable min, IComparable max, BooleanConverterMethod result)
	{
		Min = min;
		Max = max;
		Result = result;
	}

	/// <summary>
	/// Converts the <see cref="IComparable" /> value based on whether they lie in the range of the specified min/max parameters and the <see cref="BooleanConverterMethod" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="IComparable" /> value to convert.</param>
	/// <returns>
	/// An <see cref="object" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(IComparable? value)
	{
		bool result =
			value != null &&
			(IsMinExclusive ? value.CompareTo(Min) > 0 : value.CompareTo(Min) >= 0) &&
			(IsMaxExclusive ? value.CompareTo(Max) < 0 : value.CompareTo(Max) <= 0);

		return new BooleanConverter(Result).Convert(result);
	}
}