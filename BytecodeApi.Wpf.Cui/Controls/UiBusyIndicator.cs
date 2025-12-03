using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a circular busy indicator control.
/// </summary>
public class UiBusyIndicator : Control
{
	private readonly Stopwatch Stopwatch;
	private readonly DispatcherTimer Timer;
	private PathFigure? Figure;
	private ArcSegment? Arc;

	static UiBusyIndicator()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiBusyIndicator), new FrameworkPropertyMetadata(typeof(UiBusyIndicator)));
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="UiBusyIndicator" /> class.
	/// </summary>
	public UiBusyIndicator()
	{
		Stopwatch = Stopwatch.StartNew();

		Timer = new(DispatcherPriority.Render)
		{
			Interval = TimeSpan.FromMilliseconds(16)
		};
		Timer.Tick += Timer_Tick;

		Loaded += UiBusyIndicator_Loaded;
		Unloaded += UiBusyIndicator_Unloaded;
	}

	/// <summary>
	/// Applies the control template to this <see cref="UiBusyIndicator" />.
	/// </summary>
	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();

		Figure = GetTemplateChild("PART_Figure") as PathFigure;
		Arc = GetTemplateChild("PART_Arc") as ArcSegment;
	}
	private void UiBusyIndicator_Loaded(object sender, RoutedEventArgs e)
	{
		Timer.Start();
	}
	private void UiBusyIndicator_Unloaded(object sender, RoutedEventArgs e)
	{
		Timer.Stop();
	}
	private void Timer_Tick(object? sender, EventArgs e)
	{
		const double speed = .5;

		if (Figure != null && Arc != null)
		{
			// Percent: 1..100
			double percent = (long)(Stopwatch.ElapsedMilliseconds * speed) % 1000 / 10.0;

			// Keyframe: 1..2
			// Percent: 1..100 for each keyframe
			(int keyframe, percent) = percent switch
			{
				< 50 => (1, percent * 2),
				_ => (2, (percent - 50) * 2)
			};

			double angle1, angle2;
			if (keyframe == 1)
			{
				angle1 = 90 + percent * .45;
				angle2 = 90 + percent * 2.7;
			}
			else
			{
				angle1 = 135 + percent * 3.1;
				angle2 = 360 + percent * .9;
			}

			Figure.StartPoint = new(
				Math.Cos((angle1 - 90) * Math.PI / 180) * 50,
				Math.Sin((angle1 - 90) * Math.PI / 180) * 50);

			Arc.Point = new(
				Math.Cos((angle2 - 90) * Math.PI / 180) * 50,
				Math.Sin((angle2 - 90) * Math.PI / 180) * 50);

			Arc.IsLargeArc = (angle2 - angle1) % 360 > 180;
		}
	}
}