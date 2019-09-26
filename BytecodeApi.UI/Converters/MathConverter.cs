using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that performs mathematical calculations on numeric value and parameter types. The <see cref="Convert(object, object)" /> method returns a numeric value based on the specified <see cref="MathConverterMethod" /> parameter and the types of the value and parameter.
	/// </summary>
	public sealed class MathConverter : ConverterBase<object, object, object>
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
		public override object Convert(object value, object parameter)
		{
			if (!(value is byte || value is sbyte || value is char || value is decimal || value is double || value is float || value is int || value is uint || value is short || value is ushort))
			{
				throw Throw.UnsupportedType(nameof(value));
			}
			else if (!(parameter is byte || parameter is sbyte || parameter is char || parameter is decimal || parameter is double || parameter is float || parameter is int || parameter is uint || parameter is short || parameter is ushort))
			{
				throw Throw.UnsupportedType(nameof(parameter));
			}
			else if (value is decimal || parameter is decimal)
			{
				decimal a = System.Convert.ToDecimal(value);
				decimal b = System.Convert.ToDecimal(parameter);

				return Method switch
				{
					MathConverterMethod.Add => a + b,
					MathConverterMethod.Subtract => a - b,
					MathConverterMethod.Multiply => a * b,
					MathConverterMethod.Divide => a / b,
					MathConverterMethod.Modulo => a % b,
					MathConverterMethod method when method == MathConverterMethod.And || method == MathConverterMethod.Or || method == MathConverterMethod.Xor => throw CreateInvalidOperationException(),
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
					MathConverterMethod.Multiply => a * b,
					MathConverterMethod.Divide => a / b,
					MathConverterMethod.Modulo => a % b,
					MathConverterMethod.And => throw CreateInvalidOperationException(),
					MathConverterMethod.Or => throw CreateInvalidOperationException(),
					MathConverterMethod.Xor => throw CreateInvalidOperationException(),
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
					MathConverterMethod.Multiply => a * b,
					MathConverterMethod.Divide => a / b,
					MathConverterMethod.Modulo => a % b,
					MathConverterMethod.And => throw CreateInvalidOperationException(),
					MathConverterMethod.Or => throw CreateInvalidOperationException(),
					MathConverterMethod.Xor => throw CreateInvalidOperationException(),
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
					MathConverterMethod.Multiply => a * b,
					MathConverterMethod.Divide => a / b,
					MathConverterMethod.Modulo => a % b,
					MathConverterMethod.And => a & b,
					MathConverterMethod.Or => a | b,
					MathConverterMethod.Xor => a ^ b,
					_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
				};
			}

			Exception CreateInvalidOperationException()
			{
				return Throw.InvalidOperation("Floating point values cannot be used with this operator.");
			}
		}
	}
}