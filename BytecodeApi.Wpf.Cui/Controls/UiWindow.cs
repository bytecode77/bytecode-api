using BytecodeApi.Wpf.Controls;
using BytecodeApi.Wpf.Services;
using System.Windows;
using System.Windows.Media;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiWindow : ObservableWindow
{
	static UiWindow()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiWindow), new FrameworkPropertyMetadata(typeof(UiWindow)));
		BorderBrushProperty.OverrideMetadata(typeof(UiWindow), new FrameworkPropertyMetadata(BorderBrush_Changed));
	}

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

	private void UpdateBorder()
	{
		if (BorderBrush is SolidColorBrush borderBrush)
		{
			WindowService.SetBorderBrush(this, borderBrush);
		}
	}
}