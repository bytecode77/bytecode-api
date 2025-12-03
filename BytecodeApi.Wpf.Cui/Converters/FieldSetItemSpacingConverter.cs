using System.Windows;

namespace BytecodeApi.Wpf.Cui.Converters;

/// <summary>
/// Represents the converter that converts <see cref="double" /> values to <see cref="Thickness" /> values representing the bottom spacing of a field set item.
/// </summary>
public sealed class FieldSetItemSpacingConverter : ConverterBase<double>
{
	/// <summary>
	/// Converts the specified <see cref="double" /> value to a <see cref="Thickness" /> value representing the bottom spacing of a field set item.
	/// </summary>
	/// <param name="value">The spacing value.</param>
	/// <returns>
	/// A <see cref="Thickness" /> value with the bottom spacing set to the specified value.
	/// </returns>
	public override object? Convert(double value)
	{
		return new Thickness(0, 0, 0, value);
	}
}