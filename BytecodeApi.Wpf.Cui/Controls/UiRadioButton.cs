using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiRadioButton : RadioButton
{
	static UiRadioButton()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiRadioButton), new FrameworkPropertyMetadata(typeof(UiRadioButton)));
	}
}