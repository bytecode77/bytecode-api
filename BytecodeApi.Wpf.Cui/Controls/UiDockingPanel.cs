using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiDockingPanel : HeaderedContentControl
{
	static UiDockingPanel()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiDockingPanel), new FrameworkPropertyMetadata(typeof(UiDockingPanel)));
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