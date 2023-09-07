using BytecodeApi.Data;
using BytecodeApi.Wpf;

namespace Playground.Wpf;

public sealed class MainWindowViewModel : ObservableObject
{
	public MainWindow View { get; set; }

	private DelegateCommand<string>? _TestCommand;
	public DelegateCommand<string> TestCommand => _TestCommand ??= new(TestCommand_Execute);

	private bool _TestProperty;
	public bool TestProperty
	{
		get => _TestProperty;
		set => Set(ref _TestProperty, value);
	}

	public MainWindowViewModel(MainWindow view)
	{
		View = view;
	}

	private void TestCommand_Execute(string? parameter)
	{
	}
}