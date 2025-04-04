namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts objects based on whether the value is <see langword="null" /> and returns the parameter instead.
/// </summary>
public sealed class CoalesceConverter : ConverterBase<object, object>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CoalesceConverter" /> class.
	/// </summary>
	public CoalesceConverter()
	{
	}

	/// <summary>
	/// Converts the <see cref="object" /> value based on whether the value is <see langword="null" /> and returns the parameter instead.
	/// </summary>
	/// <param name="value">The <see cref="object" /> value to convert.</param>
	/// <param name="parameter">The <see cref="object" /> that is returned, if <paramref name="value" /> is <see langword="null" />.</param>
	/// <returns>
	/// An <see cref="object" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(object? value, object? parameter)
	{
		return value ?? parameter;
	}
}