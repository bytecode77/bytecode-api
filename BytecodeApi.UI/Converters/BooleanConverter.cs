using BytecodeApi.Extensions;
using System.Windows;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts <see cref="bool" />? values. The <see cref="Convert(bool?)" /> method returns an <see cref="object" /> based on the specified <see cref="BooleanConverterMethod" /> parameter.
	/// </summary>
	public sealed class BooleanConverter : ConverterBase<bool?, object>
	{
		/// <summary>
		/// Specifies the method that is used to convert the <see cref="bool" />? value.
		/// </summary>
		public BooleanConverterMethod Method { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BooleanConverter" /> class with the specified conversion method.
		/// </summary>
		/// <param name="method">The method that is used to convert the <see cref="bool" />? value.</param>
		public BooleanConverter(BooleanConverterMethod method)
		{
			Method = method;
		}

		/// <summary>
		/// Converts the <see cref="bool" />? value based on the specified <see cref="BooleanConverterMethod" /> parameter.
		/// </summary>
		/// <param name="value">The <see cref="bool" />? value to convert.</param>
		/// <returns>
		/// An <see cref="object" /> with the result of the conversion.
		/// </returns>
		public override object Convert(bool? value)
		{
			switch (Method)
			{
				case BooleanConverterMethod.Default: return value;
				case BooleanConverterMethod.Inverse: return value != true;
				case BooleanConverterMethod.Visibility: return (value == true).ToVisibility();
				case BooleanConverterMethod.VisibilityInverse: return (value != true).ToVisibility();
				case BooleanConverterMethod.VisibilityHidden: return (value == true).ToVisibility(true);
				case BooleanConverterMethod.VisibilityHiddenInverse: return (value != true).ToVisibility(true);
				case BooleanConverterMethod.GridLengthZeroAuto: return value == true ? GridLength.Auto : new GridLength(0);
				case BooleanConverterMethod.GridLengthZeroAutoInverse: return value == true ? new GridLength(0) : GridLength.Auto;
				case BooleanConverterMethod.GridLengthZeroStar: return value == true ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
				case BooleanConverterMethod.GridLengthZeroStarInverse: return value == true ? new GridLength(0) : new GridLength(1, GridUnitType.Star);
				default: throw Throw.InvalidEnumArgument(nameof(Method));
			}
		}
	}
}