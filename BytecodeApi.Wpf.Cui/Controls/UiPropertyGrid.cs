using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiPropertyGrid : ItemsControl
{
	public static readonly DependencyProperty LabelWidthProperty = DependencyPropertyEx.Register(nameof(LabelWidth));
	public double LabelWidth
	{
		get => this.GetValue<double>(LabelWidthProperty);
		set => SetValue(LabelWidthProperty, value);
	}

	static UiPropertyGrid()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiPropertyGrid), new FrameworkPropertyMetadata(typeof(UiPropertyGrid)));
	}
}