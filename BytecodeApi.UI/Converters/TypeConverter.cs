using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts the <see cref="Type" /> of <see cref="object" /> values. The <see cref="Convert(object, Type)" /> method returns an <see cref="object" /> based on the specified <see cref="TypeConverterMethod" /> and <see cref="BooleanConverterMethod" /> parameters.
	/// </summary>
	public sealed class TypeConverter : ConverterBase<object, Type, object>
	{
		/// <summary>
		/// Specifies the method that is used to convert the <see cref="Type" /> of the <see cref="object" /> value.
		/// </summary>
		public TypeConverterMethod Method { get; set; }
		/// <summary>
		/// Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(object, Type)" /> method returns.
		/// </summary>
		public BooleanConverterMethod Result { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeConverter" /> class with the specified conversion method.
		/// </summary>
		/// <param name="method">The method that is used to convert the <see cref="Type" /> of the <see cref="object" /> value.</param>
		public TypeConverter(TypeConverterMethod method) : this(method, BooleanConverterMethod.Default)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeConverter" /> class with the specified conversion method and result transformation.
		/// </summary>
		/// <param name="method">The method that is used to convert the <see cref="Type" /> of the <see cref="object" /> value.</param>
		/// <param name="result">Specifies how the <see cref="bool" /> result is converted before the <see cref="Convert(object, Type)" /> method returns.</param>
		public TypeConverter(TypeConverterMethod method, BooleanConverterMethod result)
		{
			Method = method;
			Result = result;
		}

		/// <summary>
		/// Converts the <see cref="Type" /> of the <see cref="object" /> value based on the specified <see cref="TypeConverterMethod" /> and <see cref="BooleanConverterMethod" /> parameters.
		/// </summary>
		/// <param name="value">The <see cref="object" /> value, of which to convert its <see cref="Type" />.</param>
		/// <param name="parameter">A parameter <see cref="Type" /> that specifies the parameter used in the <see cref="TypeConverterMethod" /> methods.</param>
		/// <returns>
		/// An <see cref="object" /> with the result of the conversion.
		/// </returns>
		public override object Convert(object value, Type parameter)
		{
			bool result;

			switch (Method)
			{
				case TypeConverterMethod.TypeEqual:
					result = CSharp.IsType(value, parameter);
					break;
				case TypeConverterMethod.IsAssignableFrom:
					result = value != null && parameter?.IsAssignableFrom(value.GetType()) == true;
					break;
				default:
					throw Throw.InvalidEnumArgument(nameof(Method), Method);
			}

			return new BooleanConverter(Result).Convert(result);
		}
	}
}