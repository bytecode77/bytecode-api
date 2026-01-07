using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a toggle switch control.
/// </summary>
public class UiToggleSwitch : CheckBox
{
	/// <summary>
	/// Identifies the <see cref="ContentOff" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty ContentOffProperty = DependencyProperty.Register(nameof(ContentOff));
	/// <summary>
	/// Gets or sets additional content that is displayed on the right side of the toggle switch, describing the "off" state.
	/// </summary>
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