using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a docking panel control with a header.
/// This control only visually represents a panel and does not provide any docking functionality.
/// </summary>
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