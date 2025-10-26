using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiFieldSetItem : ContentControl
{
	public static readonly DependencyProperty HeaderProperty = DependencyPropertyEx.Register(nameof(Header));
	public object? Header
	{
		get => GetValue(HeaderProperty);
		set => SetValue(HeaderProperty, value);
	}

	static UiFieldSetItem()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiFieldSetItem), new FrameworkPropertyMetadata(typeof(UiFieldSetItem)));
	}
}