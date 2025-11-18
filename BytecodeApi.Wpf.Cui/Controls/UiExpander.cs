using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a collapsible container control that can be used to show or hide content.
/// </summary>
public class UiExpander : Expander
{
    static UiExpander()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(UiExpander), new FrameworkPropertyMetadata(typeof(UiExpander)));
    }
}