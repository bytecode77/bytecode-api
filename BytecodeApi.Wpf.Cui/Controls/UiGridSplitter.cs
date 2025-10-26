using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiGridSplitter : GridSplitter
{
	static UiGridSplitter()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiGridSplitter), new FrameworkPropertyMetadata(typeof(UiGridSplitter)));
	}
}