using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiExpander : Expander
{
	static UiExpander()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiExpander), new FrameworkPropertyMetadata(typeof(UiExpander)));
	}
}