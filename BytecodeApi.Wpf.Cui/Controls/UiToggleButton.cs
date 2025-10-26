using System.Windows;
using System.Windows.Controls.Primitives;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiToggleButton : ToggleButton
{
	static UiToggleButton()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiToggleButton), new FrameworkPropertyMetadata(typeof(UiToggleButton)));
	}
}