using BytecodeApi.Wpf.Controls;
using BytecodeApi.Wpf.Extensions;
using BytecodeApi.Wpf.Services;
using System.Windows;
using System.Windows.Media;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a window control.
/// </summary>
public class UiWindow : ObservableWindow
{
	/// <summary>
	/// Identifies the <see cref="TitleBarBrush" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty TitleBarBrushProperty = DependencyPropertyEx.Register(nameof(TitleBarBrush), new(TitleBarBrush_Changed));
	/// <summary>
	/// Gets or sets the brush used to paint the title bar of the window.
	/// </summary>
	public Brush? TitleBarBrush
	{
		get => this.GetValue<Brush>(TitleBarBrushProperty);
		set => SetValue(TitleBarBrushProperty, value);
	}

	static UiWindow()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiWindow), new FrameworkPropertyMetadata(typeof(UiWindow)));
		BorderBrushProperty.OverrideMetadata(typeof(UiWindow), new FrameworkPropertyMetadata(BorderBrush_Changed));
	}

	/// <summary>
	/// Raises the System.Windows.Window.SourceInitialized event.
	/// </summary>
	/// <param name="e">An <see cref="EventArgs" /> that contains the event data.</param>
	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);

		UpdateBorder();
	}
	private static void BorderBrush_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		UiWindow window = (UiWindow)dependencyObject;
		window.UpdateBorder();
	}
	private static void TitleBarBrush_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		UiWindow window = (UiWindow)dependencyObject;

		if (window.TitleBarBrush is SolidColorBrush titleBarBrush)
		{
			WindowService.SetTitleBarBrush(window, titleBarBrush);
		}
	}

	private void UpdateBorder()
	{
		if (BorderBrush is SolidColorBrush borderBrush)
		{
			WindowService.SetBorderBrush(this, borderBrush);
		}
	}
}