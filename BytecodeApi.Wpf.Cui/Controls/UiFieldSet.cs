using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiFieldSet : ItemsControl
{
	public static readonly DependencyProperty OrientationProperty = DependencyPropertyEx.Register(nameof(Orientation), new(Orientation.Horizontal));
	public static readonly DependencyProperty LabelWidthProperty = DependencyPropertyEx.Register(nameof(LabelWidth));
	public static readonly DependencyProperty SpacingProperty = DependencyPropertyEx.Register(nameof(Spacing), new(5.0));
	public Orientation Orientation
	{
		get => this.GetValue<Orientation>(OrientationProperty);
		set => SetValue(OrientationProperty, value);
	}
	public double LabelWidth
	{
		get => this.GetValue<double>(LabelWidthProperty);
		set => SetValue(LabelWidthProperty, value);
	}
	public double Spacing
	{
		get => this.GetValue<double>(SpacingProperty);
		set => SetValue(SpacingProperty, value);
	}

	static UiFieldSet()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiFieldSet), new FrameworkPropertyMetadata(typeof(UiFieldSet)));
	}
}