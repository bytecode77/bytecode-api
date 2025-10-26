using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiTreeView : TreeView
{
	public static readonly DependencyProperty SelectedItemBindingProperty = DependencyPropertyEx.Register(nameof(SelectedItemBinding));
	public static readonly DependencyProperty SelectOnRightClickProperty = DependencyPropertyEx.Register(nameof(SelectOnRightClick));
	public object? SelectedItemBinding
	{
		get => GetValue(SelectedItemBindingProperty);
		set => SetValue(SelectedItemBindingProperty, value);
	}
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