using BytecodeApi.Extensions;
using System;

namespace BytecodeApi.UI.Converters
{
	public sealed class DateTimeConverter : ConverterBase<DateTime?, string, string>
	{
		public DateTimeConverterResult ResultType { get; set; }

		public DateTimeConverter(DateTimeConverterResult resultType)
		{
			ResultType = resultType;
		}

		public override string Convert(DateTime? value, string parameter)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				switch (ResultType)
				{
					case DateTimeConverterResult.ShortDateString: return value.Value.ToShortDateString();
					case DateTimeConverterResult.ShortTimeString: return value.Value.ToShortTimeString();
					case DateTimeConverterResult.LongDateString: return value.Value.ToLongDateString();
					case DateTimeConverterResult.LongTimeString: return value.Value.ToLongTimeString();
					case DateTimeConverterResult.YearString: return value.Value.Year.ToString();
					case DateTimeConverterResult.Format: return value.Value.ToStringInvariant(parameter);
					default: throw Throw.InvalidEnumArgument(nameof(ResultType));
				}
			}
		}
	}
}