using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiToolBar : ToolBar
{
	public static readonly DependencyProperty ShowThumbProperty = DependencyPropertyEx.Register(nameof(ShowThumb), new(true));
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