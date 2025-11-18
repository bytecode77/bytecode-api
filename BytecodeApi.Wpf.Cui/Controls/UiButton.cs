using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a button control.
/// </summary>
public class UiButton : Button
{
	static UiButton()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiButton), new FrameworkPropertyMetadata(typeof(UiButton)));
	}
}