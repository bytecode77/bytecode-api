using BytecodeApi.Data;
using System.Windows;

namespace Playground.Wpf.Cui;

public sealed class DialogApplicationWindowViewModel : ObservableObject
{
	public DialogApplicationWindow View { get; set; }

	public bool ShowIcon { get; set => Set(ref field, value); }
	public bool ShowMenu { get; set => Set(ref field, value); }
	public bool ShowTitle { get; set => Set(ref field, value); }
	public bool ShowToolBar { get; set => Set(ref field, value); }
	public bool ShowStatusBar { get; set => Set(ref field, value); }
	public ResizeMode ResizeMode
	{
		get;
		set
		{
			Set(ref field, value);
			View.ResizeMode = ResizeMode;
		}
	}

	public DialogApplicationWindowViewModel(DialogApplicationWindow view)
	{
		View = view;

		ShowIcon = true;
		ShowMenu = true;
		ShowTitle = true;
		ShowToolBar = true;
		ShowStatusBar = true;
		ResizeMode = ResizeMode.CanResize;
	}
}