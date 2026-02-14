using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a combobox control.
/// </summary>
public class UiComboBox : ComboBox
{
	static UiComboBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiComboBox), new FrameworkPropertyMetadata(typeof(UiComboBox)));
	}
}