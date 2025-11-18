using BytecodeApi.Extensions;
using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiPropertyGridItem : ContentControl
{
	public static readonly DependencyProperty HeaderProperty = DependencyPropertyEx.Register(nameof(Header));
	private static readonly DependencyPropertyKey IsSelectedPropertyKey = DependencyPropertyEx.RegisterReadOnly(nameof(IsSelected), new FrameworkPropertyMetadata(false));
	public static readonly DependencyProperty IsSelectedProperty = IsSelectedPropertyKey.DependencyProperty;
	public object? Header
	{
		get => GetValue(HeaderProperty);
		set => SetValue(HeaderProperty, value);
	}
	public bool IsSelected
	{
		get => this.GetValue<bool>(IsSelectedProperty);
		private set => SetValue(IsSelectedPropertyKey, value);
	}
	private Label? HeaderLabel;
	private ContentPresenter? ContentPresenter;

	static UiPropertyGridItem()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiPropertyGridItem), new FrameworkPropertyMetadata(typeof(UiPropertyGridItem)));
	}

    /// <summary>
    /// Applies the control template to this <see cref="UiPropertyGridItem" />.
    /// </summary>
    public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();

		if (HeaderLabel != null)
		{
			HeaderLabel.MouseLeftButtonDown -= HeaderLabel_MouseLeftButtonDown;
		}

		HeaderLabel = GetTemplateChild("PART_HeaderLabel") as Label;
		ContentPresenter = GetTemplateChild("PART_ContentPresenter") as ContentPresenter;

		if (HeaderLabel != null)
		{
			HeaderLabel.MouseLeftButtonDown += HeaderLabel_MouseLeftButtonDown;
		}
	}
	protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
	{
		base.OnGotKeyboardFocus(e);

		if (this.FindParent<UiPropertyGrid>(UITreeType.Logical) is UiPropertyGrid propertyGrid)
		{
			propertyGrid
				.FindChildren<UiPropertyGridItem>(UITreeType.Logical)
				.ForEach(propertyGridItem => propertyGridItem.IsSelected = propertyGridItem == this);
		}
	}
	private void HeaderLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		if (ContentPresenter?.Content is FrameworkElement element)
		{
			if (element.Focusable)
			{
				element.Focus();
			}
			else
			{
				element.MoveFocus(new(FocusNavigationDirection.First));
			}
		}

		e.Handled = true;
	}
}