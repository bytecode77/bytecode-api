using BytecodeApi.UI.Data;

namespace Playground.Wpf
{
	public class MainWindowViewModel : ObservableObject
	{
		public MainWindow View { get; set; }

		private string _Title;
		public string Title
		{
			get => _Title;
			set => Set(ref _Title, value);
		}

		public MainWindowViewModel(MainWindow view)
		{
			View = view;

			Title = "WPF Playground";
		}
	}
}