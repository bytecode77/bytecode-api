using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a control that contains and arranges multiple <see cref="UiToolBar" /> controls.
/// </summary>
public class UiToolBarTray : ToolBarTray
{
	/// <summary>
	/// Identifies the <see cref="BorderThickness" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register(nameof(BorderThickness), new(new Thickness()));
	/// <summary>
	/// Identifies the <see cref="BorderBrush" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register(nameof(BorderBrush), new(Brushes.Transparent));
	/// <summary>
	/// Gets or sets the thickness of the border around this <see cref="UiToolBarTray" />.
	/// </summary>
	public Thickness BorderThickness
	{
		get => this.GetValue<Thickness>(BorderThicknessProperty);
		set => SetValue(BorderThicknessProperty, value);
	}
	/// <summary>
	/// Gets or sets the brush that describes the border color of this <see cref="UiToolBarTray" />.
	/// </summary>
	public Brush BorderBrush
	{
		get => this.GetValue<Brush>(BorderBrushProperty);
		set => SetValue(BorderBrushProperty, value);
	}

	static UiToolBarTray()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiToolBarTray), new FrameworkPropertyMetadata(typeof(UiToolBarTray)));
	}

	/// <summary>
	/// Invoked whenever the effective value of any dependency property on this <see cref="FrameworkElement" /> has been updated. The specific dependency property that changed is reported in the arguments parameter.
	/// </summary>
	/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs" /> that contains the event data.</param>
	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);

		if (e.Property == BorderThicknessProperty || e.Property == BorderBrushProperty || e.Property == BackgroundProperty || e.Property == OrientationProperty)
		{
			InvalidateMeasure();
			InvalidateArrange();
			InvalidateVisual();
		}
	}
	/// <summary>
	/// Implements basic measure-pass layout system behavior for <see cref="FrameworkElement" />.
	/// </summary>
	/// <param name="constraint">The available size that the parent element can give to the child elements.</param>
	/// <returns>The desired size of this element in layout.</returns>
	protected override Size MeasureOverride(Size constraint)
	{
		Size contentConstraint = new(
			Math.Max(constraint.Width - BorderThickness.Left - BorderThickness.Right, 0),
			Math.Max(constraint.Height - BorderThickness.Top - BorderThickness.Bottom, 0));

		bool horizontal = Orientation == Orientation.Horizontal;
		double availableU = horizontal ? contentConstraint.Width : contentConstraint.Height;

		double lineU = 0;
		double lineV = 0;
		double panelU = 0;
		double panelV = 0;

		foreach (ToolBar toolbar in ToolBars)
		{
			if (toolbar.Visibility == Visibility.Collapsed)
			{
				continue;
			}

			Size childConstraint = horizontal ? new Size(double.PositiveInfinity, contentConstraint.Height) : new Size(contentConstraint.Width, double.PositiveInfinity);
			toolbar.Measure(childConstraint);

			double childU = horizontal ? toolbar.DesiredSize.Width : toolbar.DesiredSize.Height;
			double childV = horizontal ? toolbar.DesiredSize.Height : toolbar.DesiredSize.Width;

			if (lineU > 0 && lineU + childU > availableU)
			{
				panelU = Math.Max(panelU, lineU);
				panelV += lineV;
				lineU = 0;
				lineV = 0;
			}

			lineU += childU;
			lineV = Math.Max(lineV, childV);
		}

		panelU = Math.Max(panelU, lineU);
		panelV += lineV;

		Size contentSize = horizontal ? new Size(panelU, panelV) : new Size(panelV, panelU);

		return new(
			contentSize.Width + BorderThickness.Left + BorderThickness.Right,
			contentSize.Height + BorderThickness.Top + BorderThickness.Bottom);
	}
	/// <summary>
	/// Called to arrange and size its <see cref="ToolBar" /> children.
	/// </summary>
	/// <param name="arrangeSize">The size that the <see cref="ToolBarTray" /> assumes to position its children.</param>
	/// <returns>
	/// The size of the control.
	/// </returns>
	protected override Size ArrangeOverride(Size arrangeSize)
	{
		Size contentSize = new(
			Math.Max(arrangeSize.Width - BorderThickness.Left - BorderThickness.Right, 0),
			Math.Max(arrangeSize.Height - BorderThickness.Top - BorderThickness.Bottom, 0));

		bool horizontal = Orientation == Orientation.Horizontal;
		double availableU = horizontal ? contentSize.Width : contentSize.Height;

		double u = 0;
		double v = 0;
		double lineV = 0;

		foreach (ToolBar toolbar in ToolBars)
		{
			if (toolbar.Visibility == Visibility.Collapsed)
			{
				continue;
			}

			double childU = horizontal ? toolbar.DesiredSize.Width : toolbar.DesiredSize.Height;
			double childV = horizontal ? toolbar.DesiredSize.Height : toolbar.DesiredSize.Width;

			if (u > 0 && u + childU > availableU)
			{
				u = 0;
				v += lineV;
				lineV = 0;
			}

			double remainingU = Math.Max(availableU - u, 0);
			double finalU = Math.Min(childU, remainingU);
			double finalV = childV;

			if (horizontal)
			{
				toolbar.Arrange(new(
					BorderThickness.Left + u,
					BorderThickness.Top + v,
					Math.Max(finalU, 0),
					Math.Max(finalV, 0)));
			}
			else
			{
				toolbar.Arrange(new(
					BorderThickness.Left + v,
					BorderThickness.Top + u,
					Math.Max(finalV, 0),
					Math.Max(finalU, 0)));
			}

			u += childU;
			lineV = Math.Max(lineV, childV);
		}

		return arrangeSize;
	}
	/// <summary>
	/// Called when a <see cref="ToolBarTray" /> is displayed to get the Drawing Context (DC) to use to render the <see cref="ToolBarTray" />.
	/// </summary>
	/// <param name="dc">Drawing context to use to render the <see cref="ToolBarTray" />.</param>
	protected override void OnRender(DrawingContext dc)
	{
		base.OnRender(dc);

		Rect rect = new(0, 0, ActualWidth, ActualHeight);

		if (Background != null)
		{
			dc.DrawRectangle(Background, null, rect);
		}

		if (BorderBrush != null)
		{
			if (BorderThickness.Left > 0) dc.DrawRectangle(BorderBrush, null, new(0, 0, BorderThickness.Left, rect.Height));
			if (BorderThickness.Top > 0) dc.DrawRectangle(BorderBrush, null, new(0, 0, rect.Width, BorderThickness.Top));
			if (BorderThickness.Right > 0) dc.DrawRectangle(BorderBrush, null, new(rect.Width - BorderThickness.Right, 0, BorderThickness.Right, rect.Height));
			if (BorderThickness.Bottom > 0) dc.DrawRectangle(BorderBrush, null, new(0, rect.Height - BorderThickness.Bottom, rect.Width, BorderThickness.Bottom));
		}
	}
}