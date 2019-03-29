using BytecodeApi.Extensions;
using System;
using System.ComponentModel;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="Enum" /> values to their associated description specified in the <see cref="DescriptionAttribute" /> attribute.
	/// </summary>
	public sealed class EnumDescriptionToStringConverter : ConverterBase<Enum, string>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EnumDescriptionToStringConverter" /> class.
		/// </summary>
		public EnumDescriptionToStringConverter()
		{
		}

		/// <summary>
		/// Converts an <see cref="Enum" /> value to its associated description specified in the <see cref="DescriptionAttribute" /> attribute.
		/// </summary>
		/// <param name="value">The <see cref="Enum" /> value to convert.</param>
		/// <returns>
		/// The <see cref="string" /> value in <see cref="DescriptionAttribute.Description" />.
		/// </returns>
		public override string Convert(Enum value)
		{
			return value?.GetDescription();
		}
	}
}