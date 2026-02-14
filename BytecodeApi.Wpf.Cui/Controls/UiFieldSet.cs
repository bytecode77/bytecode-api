using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a vertical or horizontal field set containing <see cref="UiFieldSetItem" /> children.
/// </summary>
public class UiFieldSet : ItemsControl
{
	/// <summary>
	/// Identifies the <see cref="Orientation" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), new(Orientation.Horizontal));
	/// <summary>
	/// Identifies the <see cref="LabelWidth" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty LabelWidthProperty = DependencyProperty.Register(nameof(LabelWidth));
	/// <summary>
	/// Identifies the <see cref="Spacing" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(nameof(Spacing), new(5.0));
	/// <summary>
	/// Gets or sets the orientation of the field set.
	/// </summary>
	public Orientation Orientation
	{
		get => this.GetValue<Orientation>(OrientationProperty);
		set => SetValue(OrientationProperty, value);
	}
	/// <summary>
	/// Gets or sets the width of the labels in a horizontal field set.
	/// </summary>
	public double LabelWidth
	{
		get => this.GetValue<double>(LabelWidthProperty);
		set => SetValue(LabelWidthProperty, value);
	}
	/// <summary>
	/// Gets or sets the spacing between items in the field set.
	/// </summary>
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