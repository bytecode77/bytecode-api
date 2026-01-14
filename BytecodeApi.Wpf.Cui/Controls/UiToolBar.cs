using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a toolbar control.
/// </summary>
public class UiToolBar : ToolBar
{
	/// <summary>
	/// Identifies the <see cref="ShowThumb" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty ShowThumbProperty = DependencyProperty.Register(nameof(ShowThumb), new(true));
	/// <summary>
	/// Gets or sets a <see cref="bool" /> value indicating whether the thumb is shown on the left side of the toolbar.
	/// </summary>
	public bool ShowThumb
	{
		get => this.GetValue<bool>(ShowThumbProperty);
		set => SetValue(ShowThumbProperty, value);
	}

	static UiToolBar()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiToolBar), new FrameworkPropertyMetadata(typeof(UiToolBar)));
	}
}