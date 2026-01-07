using BytecodeApi.Data;
using BytecodeApi.Wpf;

namespace Playground.Wpf;

public sealed class MainWindowViewModel : ObservableObject
{
	public MainWindow View { get; set; }

	public DelegateCommand<string> TestCommand => field ??= new(TestCommand_Execute);
	public bool TestProperty { get; set => Set(ref field, value); }

	public MainWindowViewModel(MainWindow view)
	{
		View = view;
	}

	private void TestCommand_Execute(string? parameter)
	{
	}
}