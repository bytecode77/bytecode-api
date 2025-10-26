using BytecodeApi.Wpf.Cui.Controls;
using BytecodeApi.Wpf.Dialogs;
using System.Windows;

namespace Playground.Wpf.Cui;

public partial class MainWindow : UiApplicationWindow
{
	public MainWindowViewModel ViewModel { get; set; }

	public MainWindow()
	{
		ViewModel = new(this);
		InitializeComponent();
	}

	private void UiTextBox_Submit(object sender, RoutedEventArgs e)
	{
		MessageBoxes.Information(this, "UiTextBox", "Submit event fired.");
	}
}