using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a tab control.
/// </summary>
public class UiTabControl : TabControl
{
	/// <summary>
	/// Identifies the <see cref="TabPanelOffset" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty TabPanelOffsetProperty = DependencyPropertyEx.Register(nameof(TabPanelOffset));
	/// <summary>
	/// Gets or sets the offset of the tab panel from the top left corner of the control.
	/// </summary>
	public double TabPanelOffset
	{
		get => this.GetValue<double>(TabPanelOffsetProperty);
		set => SetValue(TabPanelOffsetProperty, value);
	}

	static UiTabControl()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiTabControl), new FrameworkPropertyMetadata(typeof(UiTabControl)));
	}
}