namespace Playground.Wpf
{
	/// <summary>
	/// Playground project for development and case testing of class libraries.
	/// </summary>
	public partial class MainWindow
	{
		public MainWindowViewModel ViewModel { get; set; }

		public MainWindow()
		{
			ViewModel = new MainWindowViewModel(this);
			InitializeComponent();
		}
	}
}