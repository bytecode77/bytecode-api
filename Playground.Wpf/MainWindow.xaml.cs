using BytecodeApi.Wpf.Controls;
using System.Windows;

namespace Playground.Wpf;

public partial class MainWindow : ObservableWindow
{
	public MainWindowViewModel ViewModel { get; set; }

	public MainWindow()
	{
		ViewModel = new(this);
		InitializeComponent();
	}
	private void MainWindow_Loaded(object sender, RoutedEventArgs e)
	{
		App.SingleInstance.RegisterWindow(this);
		App.SingleInstance.Activated += delegate
		{
			Show();

			if (WindowState == WindowState.Minimized)
			{
				WindowState = WindowState.Normal;
			}

			Activate();
		};
	}
}