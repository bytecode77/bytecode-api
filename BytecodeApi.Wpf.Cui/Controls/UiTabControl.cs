using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiTabControl : TabControl
{
	public static readonly DependencyProperty TabPanelOffsetProperty = DependencyPropertyEx.Register(nameof(TabPanelOffset));
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