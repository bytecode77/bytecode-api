using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiButton : Button
{
	static UiButton()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiButton), new FrameworkPropertyMetadata(typeof(UiButton)));
	}
}