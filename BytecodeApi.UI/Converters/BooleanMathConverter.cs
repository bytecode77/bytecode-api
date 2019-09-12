namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that performs boolean calculations on the value and parameter. The <see cref="Convert(bool?, bool?)" /> method returns a <see cref="bool" /> value based on the specified <see cref="BooleanMathConverterMethod" /> parameter.
	/// </summary>
	public sealed class BooleanMathConverter : ConverterBase<bool?, bool?, bool>
	{
		/// <summary>
		/// Specifies the method that is used to perform boolean calculations on the value and parameter.
		/// </summary>
		public BooleanMathConverterMethod Method { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BooleanMathConverterMethod" /> class with the specified conversion method.
		/// </summary>
		/// <param name="method">The method that is used to perform boolean calculations on the value and parameter.</param>
		public BooleanMathConverter(BooleanMathConverterMethod method)
		{
			Method = method;
		}

		/// <summary>
		/// Performs a boolean calculation on the value and parameter based on the specified <see cref="BooleanMathConverterMethod" /> parameter.
		/// </summary>
		/// <param name="value">The first <see cref="bool" />? value to use in the calculation.</param>
		/// <param name="parameter">The second <see cref="bool" />? value to use in the calculation.</param>
		/// <returns>
		/// A <see cref="bool" /> value with the result of the conversion.
		/// </returns>
		public override bool Convert(bool? value, bool? parameter)
		{
			switch (Method)
			{
				case BooleanMathConverterMethod.And: return (value ?? false) && (parameter ?? false);
				case BooleanMathConverterMethod.Or: return (value ?? false) || (parameter ?? false);
				case BooleanMathConverterMethod.Xor: return (value ?? false) ^ (parameter ?? false);
				default: throw Throw.InvalidEnumArgument(nameof(Method), Method);
			}
		}
	}
}