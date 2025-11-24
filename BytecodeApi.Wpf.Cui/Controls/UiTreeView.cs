using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a tree view control.
/// </summary>
public class UiTreeView : TreeView
{
    /// <summary>
    /// Identifies the <see cref="SelectedItemBinding" /> dependency property. This field is read-only.
    /// </summary>
	public static readonly DependencyProperty SelectedItemBindingProperty = DependencyPropertyEx.Register(nameof(SelectedItemBinding));
    /// <summary>
    /// Identifies the <see cref="SelectOnRightClick" /> dependency property. This field is read-only.
    /// </summary>
	public static readonly DependencyProperty SelectOnRightClickProperty = DependencyPropertyEx.Register(nameof(SelectOnRightClick));
    /// <summary>
    /// Provides a two-way binding for the selected item.
    /// </summary>
    public object? SelectedItemBinding
    {
        get => GetValue(SelectedItemBindingProperty);
        set => SetValue(SelectedItemBindingProperty, value);
    }
    /// <summary>
    /// Gets or sets a <see cref="bool" /> value indicating whether right-clicking an item selects it.
    /// </summary>
    public bool SelectOnRightClick
    {
        get => this.GetValue<bool>(SelectOnRightClickProperty);
        set => SetValue(SelectOnRightClickProperty, value);
    }

    static UiTreeView()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(UiTreeView), new FrameworkPropertyMetadata(typeof(UiTreeView)));
    }

    protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
    {
        base.OnSelectedItemChanged(e);

        SelectedItemBinding = SelectedItem;
    }
    protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseRightButtonDown(e);

        if (SelectOnRightClick && (e.OriginalSource as DependencyObject)?.FindParent<TreeViewItem>(UITreeType.Visual) is TreeViewItem item)
        {
            item.Focus();
            e.Handled = true;
        }
    }
}