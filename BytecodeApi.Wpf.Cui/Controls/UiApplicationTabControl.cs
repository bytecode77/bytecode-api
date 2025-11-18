using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a <see cref="TabControl" /> styled for use in a <see cref="UiApplicationWindow" />, typically identifying to display the main tab control.
/// </summary>
public class UiApplicationTabControl : TabControl
{
    static UiApplicationTabControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(UiApplicationTabControl), new FrameworkPropertyMetadata(typeof(UiApplicationTabControl)));
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