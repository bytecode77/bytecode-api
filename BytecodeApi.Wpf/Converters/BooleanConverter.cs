using BytecodeApi.Wpf.Extensions;
using System.Windows;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="bool" />? values. The <see cref="Convert(bool?)" /> method returns an <see cref="object" /> based on the specified <see cref="BooleanConverterMethod" /> parameter.
/// </summary>
public sealed class BooleanConverter : TwoWayConverterBase<bool?>
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
	public override object? Convert(bool? value)
	{
		return Method switch
		{
			BooleanConverterMethod.Default => value,
			BooleanConverterMethod.Inverse => value != true,
			BooleanConverterMethod.Visibility => (value == true).ToVisibility(),
			BooleanConverterMethod.VisibilityInverse => (value != true).ToVisibility(),
			BooleanConverterMethod.VisibilityHidden => (value == true).ToVisibility(true),
			BooleanConverterMethod.VisibilityHiddenInverse => (value != true).ToVisibility(true),
			BooleanConverterMethod.GridLengthZeroAuto => value == true ? GridLength.Auto : new(0),
			BooleanConverterMethod.GridLengthZeroAutoInverse => value == true ? new(0) : GridLength.Auto,
			BooleanConverterMethod.GridLengthZeroStar => value == true ? new GridLength(1, GridUnitType.Star) : new(0),
			BooleanConverterMethod.GridLengthZeroStarInverse => value == true ? new GridLength(0) : new(1, GridUnitType.Star),
			_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
		};
	}
	/// <summary>
	/// Converts a value back to its corresponding <see cref="bool" />? value.
	/// </summary>
	/// <param name="value">The value to convert back.</param>
	/// <returns>
	/// A <see cref="bool" />? with the result of the conversion.
	/// </returns>
	public override object? ConvertBack(object? value)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return Method switch
			{
				BooleanConverterMethod.Default => value is bool and true,
				BooleanConverterMethod.Inverse => value is not bool or not true,
				BooleanConverterMethod.Visibility => (value as Visibility?)?.ToBoolean() == true,
				BooleanConverterMethod.VisibilityInverse => (value as Visibility?)?.ToBoolean() != true,
				BooleanConverterMethod.VisibilityHidden => (value as Visibility?)?.ToBoolean() == true,
				BooleanConverterMethod.VisibilityHiddenInverse => (value as Visibility?)?.ToBoolean() != true,
				BooleanConverterMethod.GridLengthZeroAuto or
				BooleanConverterMethod.GridLengthZeroAutoInverse or
				BooleanConverterMethod.GridLengthZeroStar or
				BooleanConverterMethod.GridLengthZeroStarInverse => DependencyProperty.UnsetValue,
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
	}
}