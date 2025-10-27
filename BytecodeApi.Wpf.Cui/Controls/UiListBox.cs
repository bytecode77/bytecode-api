using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiListBox : ListBox
{
	public static readonly DependencyProperty CanSelectProperty = DependencyPropertyEx.Register(nameof(CanSelect), new(true));
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