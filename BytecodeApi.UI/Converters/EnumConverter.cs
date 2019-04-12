using BytecodeApi.Extensions;
using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="Enum" /> values. The <see cref="Convert(Enum)" /> method returns an <see cref="object" /> based on the specified <see cref="EnumConverterMethod" /> parameter.
	/// </summary>
	public sealed class EnumConverter : ConverterBase<Enum, object>
	{
		/// <summary>
		/// Specifies the method that is used to convert the <see cref="Enum" /> value.
		/// </summary>
		public EnumConverterMethod Method { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EnumConverter" /> class with the specified conversion method.
		/// </summary>
		/// <param name="method">The method that is used to convert the <see cref="Enum" /> value.</param>
		public EnumConverter(EnumConverterMethod method)
		{
			Method = method;
		}

		/// <summary>
		/// Converts the <see cref="Enum" /> value based on the specified <see cref="EnumConverterMethod" /> parameter.
		/// </summary>
		/// <param name="value">The <see cref="Enum" /> value to convert.</param>
		/// <returns>
		/// An <see cref="object" /> with the result of the conversion.
		/// </returns>
		public override object Convert(Enum value)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				switch (Method)
				{
					case EnumConverterMethod.String: return value.ToString();
					case EnumConverterMethod.Description: return value.GetDescription(); //CURRENT: DescriptionOrString, do not automatically "coelesce"
					case EnumConverterMethod.Value: return System.Convert.ToInt32(value);
					default: throw Throw.InvalidEnumArgument(nameof(Method));
				}
			}
		}
	}
}