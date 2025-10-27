using BytecodeApi.Wpf.Controls;
using System.Windows;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiWindow : ObservableWindow
{
	static UiWindow()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiWindow), new FrameworkPropertyMetadata(typeof(UiWindow)));
	}
}