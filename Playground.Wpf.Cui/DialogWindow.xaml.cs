using BytecodeApi.Wpf.Cui.Controls;
using System.Windows;

namespace Playground.Wpf.Cui;

public partial class DialogWindow : UiWindow
{
	public DialogWindowViewModel ViewModel { get; set; }

	public DialogWindow(Window? owner)
	{
		ViewModel = new(this);
		InitializeComponent();
		Owner = owner;
	}
}