using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Converters;

/// <summary>
/// Represents the converter that converts <see cref="TabControl" /> values to <see cref="Thickness" /> values representing the border thickness adjusted for the tab strip placement.
/// </summary>
public sealed class TabControlBorderThicknessConverter : ConverterBase<TabControl>
{
	/// <summary>
	/// Converts the specified <see cref="TabControl" /> value to a <see cref="Thickness" /> value representing the border thickness adjusted for the tab strip placement.
	/// </summary>
	/// <param name="value">The <see cref="TabControl" /> value to convert.</param>
	/// <returns>
	/// A <see cref="Thickness" /> value with the border thickness adjusted for the tab strip placement.
	/// </returns>
	public override object? Convert(TabControl? value)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return value.TabStripPlacement == Dock.Bottom
				? new Thickness(value.BorderThickness.Left, value.BorderThickness.Top, value.BorderThickness.Right, 0)
				: new Thickness(value.BorderThickness.Left, 0, value.BorderThickness.Right, value.BorderThickness.Bottom);
		}
	}
}