using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a button whose background, border and related properties can be customized for normal, mouse over and mouse down states.
/// </summary>
public class UiFlatButton : Button
{
	/// <summary>
	/// Identifies the <see cref="BorderBrushMouseOver" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty BorderBrushMouseOverProperty = DependencyProperty.Register(nameof(BorderBrushMouseOver), new(Brushes.Transparent));
	/// <summary>
	/// Identifies the <see cref="BorderBrushMouseDown" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty BorderBrushMouseDownProperty = DependencyProperty.Register(nameof(BorderBrushMouseDown), new(Brushes.Transparent));
	/// <summary>
	/// Identifies the <see cref="BackgroundMouseOver" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty BackgroundMouseOverProperty = DependencyProperty.Register(nameof(BackgroundMouseOver), new(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20000000"))));
	/// <summary>
	/// Identifies the <see cref="BackgroundMouseDown" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty BackgroundMouseDownProperty = DependencyProperty.Register(nameof(BackgroundMouseDown), new(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#30000000"))));
	/// <summary>
	/// Identifies the <see cref="Opacity" /> dependency property. This field is read-only.
	/// </summary>
	new public static readonly DependencyProperty OpacityProperty = DependencyProperty.Register(nameof(Opacity), new(1.0));
	/// <summary>
	/// Identifies the <see cref="OpacityMouseOver" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty OpacityMouseOverProperty = DependencyProperty.Register(nameof(OpacityMouseOver), new(1.0));
	/// <summary>
	/// Identifies the <see cref="OpacityMouseDown" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty OpacityMouseDownProperty = DependencyProperty.Register(nameof(OpacityMouseDown), new(1.0));
	/// <summary>
	/// Identifies the <see cref="InvertImage" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty InvertImageProperty = DependencyProperty.Register(nameof(InvertImage));
	/// <summary>
	/// Identifies the <see cref="InvertImageMouseOver" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty InvertImageMouseOverProperty = DependencyProperty.Register(nameof(InvertImageMouseOver));
	/// <summary>
	/// Identifies the <see cref="InvertImageMouseDown" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty InvertImageMouseDownProperty = DependencyProperty.Register(nameof(InvertImageMouseDown));
	/// <summary>
	/// Identifies the <see cref="CornerRadius" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius));
	/// <summary>
	/// Gets or sets a brush that describes the border color when the mouse is over the button.
	/// </summary>
	public Brush? BorderBrushMouseOver
	{
		get => this.GetValue<Brush>(BorderBrushMouseOverProperty);
		set => SetValue(BorderBrushMouseOverProperty, value);
	}
	/// <summary>
	/// Gets or sets a brush that describes the border color when the button is pressed.
	/// </summary>
	public Brush? BorderBrushMouseDown
	{
		get => this.GetValue<Brush>(BorderBrushMouseDownProperty);
		set => SetValue(BorderBrushMouseDownProperty, value);
	}
	/// <summary>
	/// Gets or sets a brush that describes the background when the mouse is over the button.
	/// </summary>
	public Brush? BackgroundMouseOver
	{
		get => this.GetValue<Brush>(BackgroundMouseOverProperty);
		set => SetValue(BackgroundMouseOverProperty, value);
	}
	/// <summary>
	/// Gets or sets a brush that describes the background when the button is pressed.
	/// </summary>
	public Brush? BackgroundMouseDown
	{
		get => this.GetValue<Brush>(BackgroundMouseDownProperty);
		set => SetValue(BackgroundMouseDownProperty, value);
	}
	/// <summary>
	/// Gets or sets the opacity of the button.
	/// </summary>
	new public double Opacity
	{
		get => this.GetValue<double>(OpacityProperty);
		set => SetValue(OpacityProperty, value);
	}
	/// <summary>
	/// Gets or sets the opacity when the mouse is over the button.
	/// </summary>
	public double OpacityMouseOver
	{
		get => this.GetValue<double>(OpacityMouseOverProperty);
		set => SetValue(OpacityMouseOverProperty, value);
	}
	/// <summary>
	/// Gets or sets the opacity when the button is pressed.
	/// </summary>
	public double OpacityMouseDown
	{
		get => this.GetValue<double>(OpacityMouseDownProperty);
		set => SetValue(OpacityMouseDownProperty, value);
	}
	/// <summary>
	/// Gets or sets a <see cref="bool" /> value indicating whether the image content of the button should be inverted.
	/// </summary>
	public bool InvertImage
	{
		get => this.GetValue<bool>(InvertImageProperty);
		set => SetValue(InvertImageProperty, value);
	}
	/// <summary>
	/// Gets or sets a <see cref="bool" /> value indicating whether the image content of the button should be inverted when the mouse is over the button.
	/// </summary>
	public bool InvertImageMouseOver
	{
		get => this.GetValue<bool>(InvertImageMouseOverProperty);
		set => SetValue(InvertImageMouseOverProperty, value);
	}
	/// <summary>
	/// Gets or sets a <see cref="bool" /> value indicating whether the image content of the button should be inverted when the button is pressed.
	/// </summary>
	public bool InvertImageMouseDown
	{
		get => this.GetValue<bool>(InvertImageMouseDownProperty);
		set => SetValue(InvertImageMouseDownProperty, value);
	}
	/// <summary>
	/// Gets or sets the corner radius of the button.
	/// </summary>
	public CornerRadius CornerRadius
	{
		get => this.GetValue<CornerRadius>(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, value);
	}

	static UiFlatButton()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiFlatButton), new FrameworkPropertyMetadata(typeof(UiFlatButton)));
	}

	/// <summary>
	/// Called when a control is clicked by the mouse or the keyboard.
	/// </summary>
	protected override void OnClick()
	{
		this.FindParent<ContextMenu>(UITreeType.Logical)?.IsOpen = false;

		base.OnClick();
	}
}