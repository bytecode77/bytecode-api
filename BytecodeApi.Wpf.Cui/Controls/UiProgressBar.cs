using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiProgressBar : ProgressBar
{
	public static readonly DependencyProperty StateProperty = DependencyPropertyEx.Register(nameof(State), new(ProgressBarState.Normal));
	public static readonly DependencyProperty TextProperty = DependencyPropertyEx.Register(nameof(Text));
	public ProgressBarState State
	{
		get => this.GetValue<ProgressBarState>(StateProperty);
		set => SetValue(StateProperty, value);
	}
	public string? Text
	{
		get => this.GetValue<string?>(TextProperty);
		set => SetValue(TextProperty, value);
	}

	static UiProgressBar()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiProgressBar), new FrameworkPropertyMetadata(typeof(UiProgressBar)));
	}
}