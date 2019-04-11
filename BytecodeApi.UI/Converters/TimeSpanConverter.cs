using BytecodeApi.Extensions;
using System;

namespace BytecodeApi.UI.Converters
{
	public sealed class TimeSpanConverter : ConverterBase<TimeSpan?, string, string>
	{
		public TimeSpanConverterResult ResultType { get; set; }

		public TimeSpanConverter(TimeSpanConverterResult resultType)
		{
			ResultType = resultType;
		}

		public override string Convert(TimeSpan? value, string parameter)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				switch (ResultType)
				{
					case TimeSpanConverterResult.SecondsString: return ((int)value.Value.TotalSeconds).ToString();
					case TimeSpanConverterResult.MillisecondsString: return ((int)value.Value.TotalMilliseconds).ToString();
					case TimeSpanConverterResult.Format: return value.Value.ToStringInvariant(parameter);
					default: throw Throw.InvalidEnumArgument(nameof(ResultType));
				}
			}
		}
	}
}