using BytecodeApi.Extensions;
using System.Collections;
using System.Linq;

namespace BytecodeApi.UI.Converters
{
	public sealed class IEnumerableAnyConverter : ConverterBase<IEnumerable, object>
	{
		public BooleanConverterResult ResultType { get; set; }

		public IEnumerableAnyConverter(BooleanConverterResult resultType)
		{
			ResultType = resultType;
		}

		public override object Convert(IEnumerable value)
		{
			return new BooleanConverter(ResultType).Convert(value?.Any());
		}
	}
}