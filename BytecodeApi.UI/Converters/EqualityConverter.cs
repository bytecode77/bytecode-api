using System;

namespace BytecodeApi.UI.Converters
{
	public sealed class EqualityConverter : ConverterBase<object, object, object>
	{
		public EqualityConverterMethod Method { get; set; }
		public BooleanConverterResult ResultType { get; set; }

		public EqualityConverter(EqualityConverterMethod method, BooleanConverterResult resultType)
		{
			Method = method;
			ResultType = resultType;
		}

		public override object Convert(object value, object parameter)
		{
			bool result;

			if (Method == EqualityConverterMethod.Equal)
			{
				result = Equals(value, parameter);
			}
			else if (Method == EqualityConverterMethod.NotEqual)
			{
				result = !Equals(value, parameter);
			}
			else
			{
				if (value is IComparable comparableValue && parameter is IComparable comparableParameter)
				{
					int comparison = comparableValue.CompareTo(comparableParameter);

					switch (Method)
					{
						case EqualityConverterMethod.Less: result = comparison < 0; break;
						case EqualityConverterMethod.LessEqual: result = comparison <= 0; break;
						case EqualityConverterMethod.Greater: result = comparison > 0; break;
						case EqualityConverterMethod.GreaterEqual: result = comparison >= 0; break;
						default: throw Throw.InvalidEnumArgument(nameof(ResultType));
					}
				}
				else
				{
					result = false;
				}
			}

			return new BooleanConverter(ResultType).Convert(result);
		}
	}
}