using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

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