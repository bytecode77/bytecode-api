using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiCheckBox : CheckBox
{
	public static readonly DependencyProperty IndeterminateToCheckedProperty = DependencyPropertyEx.Register(nameof(IndeterminateToChecked));
	public bool IndeterminateToChecked
	{
		get => this.GetValue<bool>(IndeterminateToCheckedProperty);
		set => SetValue(IndeterminateToCheckedProperty, value);
	}

	static UiCheckBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiCheckBox), new FrameworkPropertyMetadata(typeof(UiCheckBox)));
	}

	protected override void OnClick()
	{
		if (IsChecked == null && IndeterminateToChecked)
		{
			// This ThreeState CheckBox should go from null -> true instead of null -> false.

			IsChecked = false; // IsChecked will be set to true in base.OnClick()
		}

		base.OnClick();
	}
}