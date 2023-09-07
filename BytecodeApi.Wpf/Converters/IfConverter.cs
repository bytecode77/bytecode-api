namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="bool" />? values. The <see cref="Convert(bool?)" /> method returns <see cref="IfTrue" /> or <see cref="IfFalse" /> based on whether the <see cref="bool" />? value is <see langword="true" />.
/// </summary>
public sealed class IfConverter : ConverterBase<bool?>
{
	/// <summary>
	/// The <see cref="object" /> that is returned, if the <see cref="bool" />? value is <see langword="true" />.
	/// </summary>
	public object IfTrue { get; set; }
	/// <summary>
	/// The <see cref="object" /> that is returned, if the <see cref="bool" />? value is <see langword="false" /> or <see langword="null" />.
	/// </summary>
	public object IfFalse { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="IfConverter" /> class with the specified return objects.
	/// </summary>
	/// <param name="ifTrue">The <see cref="object" /> that is returned, if the <see cref="bool" />? value is <see langword="true" />.</param>
	/// <param name="ifFalse">The <see cref="object" /> that is returned, if the <see cref="bool" />? value is <see langword="false" /> or <see langword="null" />.</param>
	public IfConverter(object ifTrue, object ifFalse)
	{
		IfTrue = ifTrue;
		IfFalse = ifFalse;
	}

	/// <summary>
	/// Converts the <see cref="bool" />? value and returns <see cref="IfTrue" /> or <see cref="IfFalse" /> based on whether the <see cref="bool" />? value is <see langword="true" />.
	/// </summary>
	/// <param name="value">The <see cref="bool" />? value to convert.</param>
	/// <returns>
	/// An <see cref="object" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(bool? value)
	{
		return value == true ? IfTrue : IfFalse;
	}
}