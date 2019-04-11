using BytecodeApi.Extensions;
using System;

namespace BytecodeApi.UI.Converters
{
	public sealed class EnumConverter : ConverterBase<Enum, object>
	{
		public EnumConverterResult ResultType { get; set; }

		public EnumConverter(EnumConverterResult resultType)
		{
			ResultType = resultType;
		}

		public override object Convert(Enum value)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				switch (ResultType)
				{
					case EnumConverterResult.String: return value.ToString();
					case EnumConverterResult.Description: return value.GetDescription(); //CURRENT: DescriptionOrString
					case EnumConverterResult.Value: return System.Convert.ToInt32(value);
					default: throw Throw.InvalidEnumArgument(nameof(ResultType));
				}
			}
		}
	}
}