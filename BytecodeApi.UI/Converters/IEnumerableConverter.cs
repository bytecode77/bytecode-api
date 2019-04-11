using BytecodeApi.Extensions;
using System.Collections;

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
					default: throw Throw.InvalidEnumArgument(nameof(ResultType));
				}
			}
		}
	}
}