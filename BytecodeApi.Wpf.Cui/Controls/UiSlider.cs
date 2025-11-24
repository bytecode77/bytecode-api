using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a slider control.
/// </summary>
public class UiSlider : Slider
{
    /// <summary>
    /// Identifies the <see cref="TrackHeight" /> dependency property. This field is read-only.
    /// </summary>
    public static readonly DependencyProperty TrackHeightProperty = DependencyPropertyEx.Register(nameof(TrackHeight), new(5.0));
    /// <summary>
    /// Gets or sets the height of the slider track. If this <see cref="UiSlider" /> is vertical, this property defines the width of the track.
    /// </summary>
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