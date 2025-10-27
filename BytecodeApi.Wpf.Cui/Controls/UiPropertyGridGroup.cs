using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiPropertyGridGroup : ItemsControl
{
	public static readonly DependencyProperty HeaderProperty = DependencyPropertyEx.Register(nameof(Header));
	public static readonly DependencyProperty IsExpandedProperty = DependencyPropertyEx.Register(nameof(IsExpanded), new(true));
	public object? Header
	{
		get => GetValue(HeaderProperty);
		set => SetValue(HeaderProperty, value);
	}
	public bool IsExpanded
	{
		get => this.GetValue<bool>(IsExpandedProperty);
		set => SetValue(IsExpandedProperty, value);
	}
	private Button? HeaderButton;

	static UiPropertyGridGroup()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiPropertyGridGroup), new FrameworkPropertyMetadata(typeof(UiPropertyGridGroup)));
	}

	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();

		if (HeaderButton != null)
		{
			HeaderButton.MouseDoubleClick -= HeaderButton_MouseDoubleClick;
		}

		HeaderButton = GetTemplateChild("PART_HeaderButton") as Button;

		if (HeaderButton != null)
		{
			HeaderButton.MouseDoubleClick += HeaderButton_MouseDoubleClick;
		}
	}

	private void HeaderButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		IsExpanded = !IsExpanded;
	}
}