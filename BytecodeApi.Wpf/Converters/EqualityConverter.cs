namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts objects or <see cref="IComparable" /> values based on equality to the specified parameter. The <see cref="Convert(object, object)" /> method returns an <see cref="object" /> based on the specified <see cref="EqualityConverterMethod" /> and <see cref="BooleanConverterMethod" /> parameters.
/// </summary>
public sealed class EqualityConverter : ConverterBase<object, object>
{
	/// <summary>
	/// Specifies the method that is used to compare the value and parameter.
	/// </summary>
	public EqualityConverterMethod Method { get; set; }
	/// <summary>
	/// Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(object, object)" /> method returns.
	/// </summary>
	public BooleanConverterMethod Result { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="EqualityConverter" /> class with the specified conversion method.
	/// </summary>
	/// <param name="method">The method that is used to compare the value and parameter.</param>
	public EqualityConverter(EqualityConverterMethod method) : this(method, BooleanConverterMethod.Default)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="EqualityConverter" /> class with the specified conversion method and result transformation.
	/// </summary>
	/// <param name="method">The method that is used to compare the value and parameter.</param>
	/// <param name="result">Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(object, object)" /> method returns.</param>
	public EqualityConverter(EqualityConverterMethod method, BooleanConverterMethod result)
	{
		Method = method;
		Result = result;
	}

	/// <summary>
	/// Converts the <see cref="object" /> or <see cref="IComparable" /> value based on equality to the specified parameter and the specified <see cref="EqualityConverterMethod" /> and <see cref="BooleanConverterMethod" /> parameters.
	/// </summary>
	/// <param name="value">The <see cref="object" /> to convert.</param>
	/// <param name="parameter">A parameter <see cref="object" /> to be compared to <paramref name="value" />.</param>
	/// <returns>
	/// An <see cref="object" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(object? value, object? parameter)
	{
		bool result;

		if (Method == EqualityConverterMethod.Equal)
		{
			result = Equals(value, parameter);
		}
		else if (Method == EqualityConverterMethod.NotEqual)
		{
			result = !Equals(value, parameter);
		}
		else if (value is IComparable comparableValue && parameter is IComparable comparableParameter)
		{
			int comparison = comparableValue.CompareTo(comparableParameter);

			result = Method switch
			{
				EqualityConverterMethod.Less => comparison < 0,
				EqualityConverterMethod.LessEqual => comparison <= 0,
				EqualityConverterMethod.Greater => comparison > 0,
				EqualityConverterMethod.GreaterEqual => comparison >= 0,
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
		else
		{
			result = false;
		}

		return new BooleanConverter(Result).Convert(result, null);
	}
}