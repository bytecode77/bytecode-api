using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiSlider : Slider
{
	public static readonly DependencyProperty TrackHeightProperty = DependencyPropertyEx.Register(nameof(TrackHeight), new(5.0));
	public double TrackHeight
	{
		get => this.GetValue<double>(TrackHeightProperty);
		set => SetValue(TrackHeightProperty, value);
	}

	static UiSlider()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiSlider), new FrameworkPropertyMetadata(typeof(UiSlider)));
	}
}