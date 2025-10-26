using BytecodeApi.Data;
using System.Windows;

namespace Playground.Wpf.Cui;

public sealed class DialogApplicationWindowViewModel : ObservableObject
{
	public DialogApplicationWindow View { get; set; }

	private bool _ShowIcon = true;
	private bool _ShowMenu = true;
	private bool _ShowTitle = true;
	private bool _ShowToolBar = true;
	private bool _ShowStatusBar = true;
	private ResizeMode _ResizeMode = ResizeMode.CanResize;
	public bool ShowIcon
	{
		get => _ShowIcon;
		set => Set(ref _ShowIcon, value);
	}
	public bool ShowMenu
	{
		get => _ShowMenu;
		set => Set(ref _ShowMenu, value);
	}
	public bool ShowTitle
	{
		get => _ShowTitle;
		set => Set(ref _ShowTitle, value);
	}
	public bool ShowToolBar
	{
		get => _ShowToolBar;
		set => Set(ref _ShowToolBar, value);
	}
	public bool ShowStatusBar
	{
		get => _ShowStatusBar;
		set => Set(ref _ShowStatusBar, value);
	}
	public ResizeMode ResizeMode
	{
		get => _ResizeMode;
		set
		{
			Set(ref _ResizeMode, value);
			View.ResizeMode = ResizeMode;
		}
	}

	public DialogApplicationWindowViewModel(DialogApplicationWindow view)
	{
		View = view;
	}
}