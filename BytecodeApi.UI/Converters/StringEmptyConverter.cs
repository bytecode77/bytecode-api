using BytecodeApi.Extensions;

namespace BytecodeApi.UI.Converters
{
	public sealed class StringEmptyConverter : ConverterBase<string, object>
	{
		public StringEmptyConverterMethod Method { get; set; }
		public BooleanConverterResult ResultType { get; set; }

		public StringEmptyConverter(StringEmptyConverterMethod method, BooleanConverterResult resultType)
		{
			Method = method;
			ResultType = resultType;
		}

		public override object Convert(string value)
		{
			bool result;

			switch (Method)
			{
				case StringEmptyConverterMethod.NotNullOrEmpty: result = !value.IsNullOrEmpty(); break;
				case StringEmptyConverterMethod.NotNullOrWhiteSpace: result = !value.IsNullOrWhiteSpace(); break;
				default: throw Throw.InvalidEnumArgument(nameof(ResultType));
			}

			return new BooleanConverter(ResultType).Convert(result);
		}
	}
}