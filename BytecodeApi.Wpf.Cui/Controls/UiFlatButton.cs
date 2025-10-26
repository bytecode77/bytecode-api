using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiFlatButton : Button
{
	public static readonly DependencyProperty BorderBrushMouseOverProperty = DependencyPropertyEx.Register(nameof(BorderBrushMouseOver), new(Brushes.Transparent));
	public static readonly DependencyProperty BorderBrushMouseDownProperty = DependencyPropertyEx.Register(nameof(BorderBrushMouseDown), new(Brushes.Transparent));
	public static readonly DependencyProperty BackgroundMouseOverProperty = DependencyPropertyEx.Register(nameof(BackgroundMouseOver), new(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20000000"))));
	public static readonly DependencyProperty BackgroundMouseDownProperty = DependencyPropertyEx.Register(nameof(BackgroundMouseDown), new(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#30000000"))));
	new public static readonly DependencyProperty OpacityProperty = DependencyPropertyEx.Register(nameof(Opacity), new(1.0));
	public static readonly DependencyProperty OpacityMouseOverProperty = DependencyPropertyEx.Register(nameof(OpacityMouseOver), new(1.0));
	public static readonly DependencyProperty OpacityMouseDownProperty = DependencyPropertyEx.Register(nameof(OpacityMouseDown), new(1.0));
	public static readonly DependencyProperty InvertImageProperty = DependencyPropertyEx.Register(nameof(InvertImage));
	public static readonly DependencyProperty InvertImageMouseOverProperty = DependencyPropertyEx.Register(nameof(InvertImageMouseOver));
	public static readonly DependencyProperty InvertImageMouseDownProperty = DependencyPropertyEx.Register(nameof(InvertImageMouseDown));
	public static readonly DependencyProperty CornerRadiusProperty = DependencyPropertyEx.Register(nameof(CornerRadius));
	public Brush? BorderBrushMouseOver
	{
		get => this.GetValue<Brush>(BorderBrushMouseOverProperty);
		set => SetValue(BorderBrushMouseOverProperty, value);
	}
	public Brush? BorderBrushMouseDown
	{
		get => this.GetValue<Brush>(BorderBrushMouseDownProperty);
		set => SetValue(BorderBrushMouseDownProperty, value);
	}
	public Brush? BackgroundMouseOver
	{
		get => this.GetValue<Brush>(BackgroundMouseOverProperty);
		set => SetValue(BackgroundMouseOverProperty, value);
	}
	public Brush? BackgroundMouseDown
	{
		get => this.GetValue<Brush>(BackgroundMouseDownProperty);
		set => SetValue(BackgroundMouseDownProperty, value);
	}
	new public double Opacity
	{
		get => this.GetValue<double>(OpacityProperty);
		set => SetValue(OpacityProperty, value);
	}
	public double OpacityMouseOver
	{
		get => this.GetValue<double>(OpacityMouseOverProperty);
		set => SetValue(OpacityMouseOverProperty, value);
	}
	public double OpacityMouseDown
	{
		get => this.GetValue<double>(OpacityMouseDownProperty);
		set => SetValue(OpacityMouseDownProperty, value);
	}
	public bool InvertImage
	{
		get => this.GetValue<bool>(InvertImageProperty);
		set => SetValue(InvertImageProperty, value);
	}
	public bool InvertImageMouseOver
	{
		get => this.GetValue<bool>(InvertImageMouseOverProperty);
		set => SetValue(InvertImageMouseOverProperty, value);
	}
	public bool InvertImageMouseDown
	{
		get => this.GetValue<bool>(InvertImageMouseDownProperty);
		set => SetValue(InvertImageMouseDownProperty, value);
	}
	public CornerRadius CornerRadius
	{
		get => this.GetValue<CornerRadius>(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, value);
	}

	static UiFlatButton()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiFlatButton), new FrameworkPropertyMetadata(typeof(UiFlatButton)));
	}

	protected override void OnClick()
	{
		if (this.FindParent<ContextMenu>(UITreeType.Logical) is ContextMenu contextMenu)
		{
			contextMenu.IsOpen = false;
		}

		base.OnClick();
	}
}