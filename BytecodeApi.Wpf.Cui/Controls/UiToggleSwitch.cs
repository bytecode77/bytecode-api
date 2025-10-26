using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiToggleSwitch : CheckBox
{
	public static readonly DependencyProperty ContentOffProperty = DependencyPropertyEx.Register(nameof(ContentOff));
	public object? ContentOff
	{
		get => GetValue(ContentOffProperty);
		set => SetValue(ContentOffProperty, value);
	}

	static UiToggleSwitch()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiToggleSwitch), new FrameworkPropertyMetadata(typeof(UiToggleSwitch)));
	}
}