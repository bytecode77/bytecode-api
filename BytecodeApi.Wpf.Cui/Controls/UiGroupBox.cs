using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiGroupBox : GroupBox
{
	static UiGroupBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiGroupBox), new FrameworkPropertyMetadata(typeof(UiGroupBox)));
	}
}