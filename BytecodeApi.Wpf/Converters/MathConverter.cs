namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that performs mathematical calculations on numeric value and parameter types. The <see cref="Convert(object, object)" /> method returns a numeric value based on the specified <see cref="MathConverterMethod" /> parameter and the types of the value and parameter.
/// </summary>
public sealed class MathConverter : ConverterBase<object, object>
{
	/// <summary>
	/// Specifies the method that is used to perform mathematical calculations on the value and parameter.
	/// </summary>
	public MathConverterMethod Method { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="MathConverterMethod" /> class with the specified conversion method.
	/// </summary>
	/// <param name="method">The method that is used to perform mathematical calculations on the value and parameter.</param>
	public MathConverter(MathConverterMethod method)
	{
		Method = method;
	}

	/// <summary>
	/// Performs a mathematical calculation on the value and parameter based on the specified <see cref="MathConverterMethod" /> parameter. Both value and parameter must be a numeric type (except <see cref="long" /> and <see cref="ulong" />).
	/// </summary>
	/// <param name="value">The first numeric value to use in the calculation.</param>
	/// <param name="parameter">The second numeric value to use in the calculation.</param>
	/// <returns>
	/// A numeric value with the result of the conversion.
	/// </returns>
	public override object? Convert(object? value, object? parameter)
	{
		if (value == null)
		{
			return null;
		}

		if (value is not byte and not sbyte and not char and not decimal and not double and not float and not int and not uint and not short and not ushort)
		{
			throw Throw.UnsupportedType(nameof(value));
		}

		if (Method == MathConverterMethod.Negate)
		{
			return value switch
			{
				decimal => -System.Convert.ToDecimal(value),
				double => -System.Convert.ToDouble(value),
				float => -System.Convert.ToSingle(value),
				_ => -System.Convert.ToInt32(value)
			};
		}
		else if (Method == MathConverterMethod.Abs)
		{
			return value switch
			{
				decimal => Math.Abs(System.Convert.ToDecimal(value)),
				double => Math.Abs(System.Convert.ToDouble(value)),
				float => Math.Abs(System.Convert.ToSingle(value)),
				_ => Math.Abs(System.Convert.ToInt32(value))
			};
		}

		if (Method is not MathConverterMethod.Increment and not MathConverterMethod.Decrement &&
			parameter is not byte and not sbyte and not char and not decimal and not double and not float and not int and not uint and not short and not ushort)
		{
			throw Throw.UnsupportedType(nameof(parameter));
		}

		if (value is decimal || parameter is decimal)
		{
			decimal a = System.Convert.ToDecimal(value);
			decimal b = System.Convert.ToDecimal(parameter);

			return Method switch
			{
				MathConverterMethod.Add => a + b,
				MathConverterMethod.Subtract => a - b,
				MathConverterMethod.Increment => a + 1,
				MathConverterMethod.Decrement => a - 1,
				MathConverterMethod.Multiply => a * b,
				MathConverterMethod.Divide => a / b,
				MathConverterMethod.Modulo => a % b,
				MathConverterMethod.And or MathConverterMethod.Or or MathConverterMethod.Xor => throw CreateInvalidOperationException(),
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
		else if (value is double || parameter is double)
		{
			double a = System.Convert.ToDouble(value);
			double b = System.Convert.ToDouble(parameter);

			return Method switch
			{
				MathConverterMethod.Add => a + b,
				MathConverterMethod.Subtract => a - b,
				MathConverterMethod.Increment => a + 1,
				MathConverterMethod.Decrement => a - 1,
				MathConverterMethod.Multiply => a * b,
				MathConverterMethod.Divide => a / b,
				MathConverterMethod.Modulo => a % b,
				MathConverterMethod.And or MathConverterMethod.Or or MathConverterMethod.Xor => throw CreateInvalidOperationException(),
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
		else if (value is float || parameter is float)
		{
			float a = System.Convert.ToSingle(value);
			float b = System.Convert.ToSingle(parameter);

			return Method switch
			{
				MathConverterMethod.Add => a + b,
				MathConverterMethod.Subtract => a - b,
				MathConverterMethod.Increment => a + 1,
				MathConverterMethod.Decrement => a - 1,
				MathConverterMethod.Multiply => a * b,
				MathConverterMethod.Divide => a / b,
				MathConverterMethod.Modulo => a % b,
				MathConverterMethod.And or MathConverterMethod.Or or MathConverterMethod.Xor => throw CreateInvalidOperationException(),
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
		else
		{
			int a = System.Convert.ToInt32(value);
			int b = System.Convert.ToInt32(parameter);

			return Method switch
			{
				MathConverterMethod.Add => a + b,
				MathConverterMethod.Subtract => a - b,
				MathConverterMethod.Increment => a + 1,
				MathConverterMethod.Decrement => a - 1,
				MathConverterMethod.Multiply => a * b,
				MathConverterMethod.Divide => a / b,
				MathConverterMethod.Modulo => a % b,
				MathConverterMethod.And => a & b,
				MathConverterMethod.Or => a | b,
				MathConverterMethod.Xor => a ^ b,
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}

		static Exception CreateInvalidOperationException()
		{
			return Throw.InvalidOperation("Floating point values cannot be used with this operator.");
		}
	}
}