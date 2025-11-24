using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a control that displays a framed container with a header.
/// </summary>
public class UiGroupBox : GroupBox
{
	static UiGroupBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiGroupBox), new FrameworkPropertyMetadata(typeof(UiGroupBox)));
	}
}