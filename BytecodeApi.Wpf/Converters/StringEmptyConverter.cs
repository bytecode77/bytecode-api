using BytecodeApi.Extensions;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="string" /> values based on <see cref="string" /> emptiness. The <see cref="Convert(string)" /> method returns an <see cref="object" /> based on the specified <see cref="StringEmptyConverterMethod" /> and <see cref="BooleanConverterMethod" /> parameters.
/// </summary>
public sealed class StringEmptyConverter : ConverterBase<string>
{
	/// <summary>
	/// Specifies the method that is used to check the <see cref="string" /> value for emptiness.
	/// </summary>
	public StringEmptyConverterMethod Method { get; set; }
	/// <summary>
	/// Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(string)" /> method returns.
	/// </summary>
	public BooleanConverterMethod Result { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="StringEmptyConverter" /> class with the specified conversion method.
	/// </summary>
	/// <param name="method">The method that is used to check the <see cref="string" /> value for emptiness.</param>
	public StringEmptyConverter(StringEmptyConverterMethod method) : this(method, BooleanConverterMethod.Default)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="StringEmptyConverter" /> class with the specified conversion method and result transformation.
	/// </summary>
	/// <param name="method">The method that is used to check the <see cref="string" /> value for emptiness.</param>
	/// <param name="result">Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(string)" /> method returns.</param>
	public StringEmptyConverter(StringEmptyConverterMethod method, BooleanConverterMethod result)
	{
		Method = method;
		Result = result;
	}

	/// <summary>
	/// Converts the <see cref="string" /> value based on <see cref="string" /> emptiness and the specified <see cref="StringEmptyConverterMethod" /> and <see cref="BooleanConverterMethod" /> parameters.
	/// </summary>
	/// <param name="value">The <see cref="string" /> value to convert.</param>
	/// <returns>
	/// An <see cref="object" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(string? value)
	{
		bool result = Method switch
		{
			StringEmptyConverterMethod.NotNullOrEmpty => !value.IsNullOrEmpty(),
			StringEmptyConverterMethod.NotNullOrWhiteSpace => !value.IsNullOrWhiteSpace(),
			_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
		};

		return new BooleanConverter(Result).Convert(result);
	}
}