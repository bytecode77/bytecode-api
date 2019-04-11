namespace BytecodeApi.UI.Converters
{
	public sealed class NullConverter : ConverterBase<object, object>
	{
		public BooleanConverterResult ResultType { get; set; }

		public NullConverter(BooleanConverterResult resultType)
		{
			ResultType = resultType;
		}

		public override object Convert(object value)
		{
			return new BooleanConverter(ResultType).Convert(value != null);
		}
	}
}