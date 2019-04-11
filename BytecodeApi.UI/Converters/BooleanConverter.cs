using BytecodeApi.Extensions;
using System.Windows;

namespace BytecodeApi.UI.Converters
{
	public sealed class BooleanConverter : ConverterBase<bool?, object>
	{
		public BooleanConverterResult ResultType { get; set; }

		public BooleanConverter(BooleanConverterResult resultType)
		{
			ResultType = resultType;
		}

		public override object Convert(bool? value)
		{
			switch (ResultType)
			{
				case BooleanConverterResult.Inverse: return value != true;
				case BooleanConverterResult.Visibility: return (value == true).ToVisibility();
				case BooleanConverterResult.VisibilityInverse: return (value != true).ToVisibility();
				case BooleanConverterResult.VisibilityHidden: return (value == true).ToVisibility(true);
				case BooleanConverterResult.VisibilityHiddenInverse: return (value != true).ToVisibility(true);
				case BooleanConverterResult.GridLengthZeroAuto: return value == true ? GridLength.Auto : new GridLength(0);
				case BooleanConverterResult.GridLengthZeroStar: return value == true ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
				case BooleanConverterResult.GridLengthAutoZero: return value == true ? new GridLength(0) : GridLength.Auto;
				case BooleanConverterResult.GridLengthStarZero: return value == true ? new GridLength(0) : new GridLength(1, GridUnitType.Star);
				default: throw Throw.InvalidEnumArgument(nameof(ResultType));
			}
		}
	}
}