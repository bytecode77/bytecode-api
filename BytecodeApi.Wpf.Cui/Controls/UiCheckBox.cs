using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a checkbox control.
/// </summary>
public class UiCheckBox : CheckBox
{
	/// <summary>
	/// Identifies the <see cref="IndeterminateToChecked" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty IndeterminateToCheckedProperty = DependencyPropertyEx.Register(nameof(IndeterminateToChecked));
	/// <summary>
	/// Gets or sets a <see cref="bool" /> value that indicates whether a ThreeState <see cref="UiCheckBox" /> should go from null to true when clicked, instead of from null to false.
	/// </summary>
	public bool IndeterminateToChecked
	{
		get => this.GetValue<bool>(IndeterminateToCheckedProperty);
		set => SetValue(IndeterminateToCheckedProperty, value);
	}

	static UiCheckBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiCheckBox), new FrameworkPropertyMetadata(typeof(UiCheckBox)));
	}

	/// <summary>
	/// Called when a control is clicked by the mouse or the keyboard.
	/// </summary>
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