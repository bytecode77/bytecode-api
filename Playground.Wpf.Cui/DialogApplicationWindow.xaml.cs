using BytecodeApi.Wpf.Cui.Controls;
using System.Windows;

namespace Playground.Wpf.Cui;

public partial class DialogApplicationWindow : UiApplicationWindow
{
	public DialogApplicationWindowViewModel ViewModel { get; set; }

	public DialogApplicationWindow(Window? owner)
	{
		ViewModel = new(this);
		InitializeComponent();
		Owner = owner;
	}
}