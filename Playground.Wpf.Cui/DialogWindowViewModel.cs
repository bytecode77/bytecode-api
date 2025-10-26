using BytecodeApi.Data;

namespace Playground.Wpf.Cui;

public sealed class DialogWindowViewModel : ObservableObject
{
	public DialogWindow View { get; set; }

	public DialogWindowViewModel(DialogWindow view)
	{
		View = view;
	}
}