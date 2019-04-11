using BytecodeApi.Text;
using System;

namespace BytecodeApi.UI.Converters
{
	public sealed class TimeSpanHumanReadableConverter : ConverterBase<TimeSpan?, string>
	{
		public int MaxElements { get; set; }
		public string Separator { get; set; }

		public TimeSpanHumanReadableConverter() : this(Wording.DefaultFormatTimeSpanStringMaxElements)
		{
		}
		public TimeSpanHumanReadableConverter(int maxElements) : this(maxElements, Wording.DefaultFormatTimeSpanStringSeparator)
		{
		}
		public TimeSpanHumanReadableConverter(int maxElements, string separator)
		{
			MaxElements = maxElements;
			Separator = separator;
		}

		public override string Convert(TimeSpan? value)
		{
			return value == null ? null : Wording.FormatTimeSpanString(value.Value, MaxElements, Separator);
		}
	}
}