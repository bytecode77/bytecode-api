using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Converters;

public sealed class TabControlBorderThicknessConverter : ConverterBase<TabControl>
{
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