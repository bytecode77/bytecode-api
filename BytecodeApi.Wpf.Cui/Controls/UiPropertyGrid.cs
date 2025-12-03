using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a property grid control containing <see cref="UiPropertyGridGroup" /> children.
/// </summary>
public class UiPropertyGrid : ItemsControl
{
	/// <summary>
	/// Identifies the <see cref="LabelWidth" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty LabelWidthProperty = DependencyPropertyEx.Register(nameof(LabelWidth));
	/// <summary>
	/// Gets or sets the width of the labels in the property grid.
	/// </summary>
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