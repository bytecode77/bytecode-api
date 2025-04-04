namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts objects based on whether the value is <see langword="null" />. The <see cref="Convert(object)" /> method returns an <see cref="object" /> based on the specified <see cref="BooleanConverterMethod" /> parameter.
/// </summary>
public sealed class NullConverter : ConverterBase<object>
{
	/// <summary>
	/// Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(object)" /> method returns.
	/// </summary>
	public BooleanConverterMethod Result { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="NullConverter" /> class.
	/// </summary>
	public NullConverter() : this(BooleanConverterMethod.Default)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="NullConverter" /> class with the specified result transformation.
	/// </summary>
	/// <param name="result">Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(object)" /> method returns.</param>
	public NullConverter(BooleanConverterMethod result)
	{
		Result = result;
	}

	/// <summary>
	/// Converts the <see cref="object" /> value based on whether the value is <see langword="null" /> and the specified <see cref="BooleanConverterMethod" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="object" /> value to convert.</param>
	/// <returns>
	/// An <see cref="object" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(object? value)
	{
		return new BooleanConverter(Result).Convert(value != null);
	}
}