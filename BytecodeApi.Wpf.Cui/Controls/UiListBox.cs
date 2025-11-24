using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a list box control.
/// </summary>
public class UiListBox : ListBox
{
    /// <summary>
    /// Identifies the <see cref="CanSelect" /> dependency property. This field is read-only.
    /// </summary>
    public static readonly DependencyProperty CanSelectProperty = DependencyPropertyEx.Register(nameof(CanSelect), new(true));
    /// <summary>
    /// Gets or sets a <see cref="bool" /> value indicating whether the user can select items.
    /// </summary>
    public bool CanSelect
    {
        get => this.GetValue<bool>(CanSelectProperty);
        set => SetValue(CanSelectProperty, value);
    }

    static UiListBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(UiListBox), new FrameworkPropertyMetadata(typeof(UiListBox)));
    }

    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
        if (!CanSelect && (SelectedIndex != -1 || e.AddedItems.Count > 0))
        {
            SelectedIndex = -1;
            return;
        }

        base.OnSelectionChanged(e);
    }
    protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
    {
        if (!CanSelect && UIContext.Find<ListBoxItem>(e.OriginalSource) != null)
        {
            e.Handled = true;
            Focus();
            return;
        }
        base.OnPreviewMouseDown(e);
    }
    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        if (!CanSelect && (e.Key == Key.Space || e.Key == Key.Enter))
        {
            e.Handled = true;
            return;
        }

        base.OnPreviewKeyDown(e);
    }
}