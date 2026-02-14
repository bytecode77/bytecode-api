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

	/// <summary>
	/// Invoked when an unhandled <see cref="Mouse.PreviewMouseDownEvent" /> attached routed event reaches an element in its route that is derived from this class.
	/// </summary>
	/// <param name="e">A <see cref="MouseButtonEventArgs" /> that contains the event data.</param>
	protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
	{
		base.OnPreviewMouseDown(e);

		if (!IsKeyboardFocusWithin)
		{
			Focus();
		}
	}
}