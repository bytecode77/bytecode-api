using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents an item within a <see cref="UiFieldSet" /> control.
/// </summary>
public class UiFieldSetItem : ContentControl
{
	/// <summary>
	/// Identifies the <see cref="Header" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty HeaderProperty = DependencyPropertyEx.Register(nameof(Header));
	/// <summary>
	/// Gets or sets a header object for the field set item.
	/// </summary>
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