using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a progress bar control.
/// </summary>
public class UiProgressBar : ProgressBar
{
	/// <summary>
	/// Identifies the <see cref="State" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty StateProperty = DependencyPropertyEx.Register(nameof(State), new(ProgressBarState.Normal));
	/// <summary>
	/// Identifies the <see cref="Text" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty TextProperty = DependencyPropertyEx.Register(nameof(Text));
	/// <summary>
	/// Gets or sets the state of the progress bar that determines its appearance.
	/// </summary>
	public ProgressBarState State
	{
		get => this.GetValue<ProgressBarState>(StateProperty);
		set => SetValue(StateProperty, value);
	}
	/// <summary>
	/// Gets or sets a <see cref="string" /> value that is displayed on the progress bar.
	/// </summary>
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