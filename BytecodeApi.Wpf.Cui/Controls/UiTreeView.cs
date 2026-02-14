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
	public static readonly DependencyProperty SelectedItemBindingProperty = DependencyProperty.Register(nameof(SelectedItemBinding));
	/// <summary>
	/// Identifies the <see cref="SelectOnRightClick" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty SelectOnRightClickProperty = DependencyProperty.Register(nameof(SelectOnRightClick));
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

	/// <summary>
	/// Raises the <see cref="TreeView.SelectedItemChanged" /> event when the <see cref="TreeView.SelectedItem" /> property value changes.
	/// </summary>
	/// <param name="e">A <see cref="RoutedPropertyChangedEventArgs{T}" /> that contains the event data.</param>
	protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
	{
		base.OnSelectedItemChanged(e);

		SelectedItemBinding = SelectedItem;
	}
	/// <summary>
	/// Invoked when an unhandled <see cref="UIElement.MouseRightButtonDown" /> routed event reaches an element in its route that is derived from this class.
	/// </summary>
	/// <param name="e">A <see cref="MouseButtonEventArgs" /> that contains the event data.</param>
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