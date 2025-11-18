using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a <see cref="TabControl" /> styled as a docking panel control with a header.
/// This control only visually represents a panel and does not provide any docking functionality.
/// </summary>
public class UiDockingTabControl : TabControl
{
	static UiDockingTabControl()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiDockingTabControl), new FrameworkPropertyMetadata(typeof(UiDockingTabControl)));
	}

	protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
	{
		base.OnPreviewMouseDown(e);

		if (!IsKeyboardFocusWithin)
		{
			Focus();
		}
	}
}