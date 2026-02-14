using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a control that enables the user to resize rows or columns in a <see cref="Grid" />.
/// </summary>
public class UiGridSplitter : GridSplitter
{
	static UiGridSplitter()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiGridSplitter), new FrameworkPropertyMetadata(typeof(UiGridSplitter)));
	}
}