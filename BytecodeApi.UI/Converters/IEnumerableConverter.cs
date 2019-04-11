using BytecodeApi.Extensions;
using System.Collections;
using System.Linq;

namespace BytecodeApi.UI.Converters
{
	public sealed class IEnumerableConverter : ConverterBase<IEnumerable, object>
	{
		public IEnumerableConverterResult ResultType { get; set; }

		public IEnumerableConverter(IEnumerableConverterResult resultType)
		{
			ResultType = resultType;
		}

		public override object Convert(IEnumerable value)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				switch (ResultType)
				{
					case IEnumerableConverterResult.Count: return value.Count();
					case IEnumerableConverterResult.JoinStrings: return value.Cast<object>().AsString();
					case IEnumerableConverterResult.AsMultilineString: return value.Cast<object>().Select(itm => itm?.ToString()).AsMultilineString();
					default: throw Throw.InvalidEnumArgument(nameof(ResultType));
				}
			}
		}
	}
}