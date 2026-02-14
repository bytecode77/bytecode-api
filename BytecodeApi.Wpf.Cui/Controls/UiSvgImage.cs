using BytecodeApi.Extensions;
using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents an image control that displays SVG images.
/// </summary>
public class UiSvgImage : Control
{
	/// <summary>
	/// Identifies the <see cref="Source" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), new(Source_Changed));
	/// <summary>
	/// Identifies the <see cref="Stretch" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof(Stretch), new(Stretch.Uniform));
	/// <summary>
	/// Identifies the <see cref="InheritForeground" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty InheritForegroundProperty = DependencyProperty.Register(nameof(InheritForeground), new(Source_Changed));
	private static readonly DependencyPropertyKey ImageSourcePropertyKey = DependencyProperty.RegisterReadOnly(nameof(ImageSource), new FrameworkPropertyMetadata(null));
	/// <summary>
	/// Identifies the <see cref="ImageSource" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty ImageSourceProperty = ImageSourcePropertyKey.DependencyProperty;
	/// <summary>
	/// Gets or sets the source URI of the SVG image. This can also be a non-SVG image, such as a PNG.
	/// </summary>
	public string? Source
	{
		get => this.GetValue<string?>(SourceProperty);
		set => SetValue(SourceProperty, value);
	}
	/// <summary>
	/// Gets or sets a <see cref="Stretch" /> value that indicates how the image is stretched to fill the allocated space.
	/// </summary>
	public Stretch Stretch
	{
		get => this.GetValue<Stretch>(StretchProperty);
		set => SetValue(StretchProperty, value);
	}
	/// <summary>
	/// Gets or sets a <see cref="bool" /> value indicating whether the SVG image should inherit the control's foreground color.
	/// <para>If set to <see langword="true" />, the SVG image colors are replaced with the control's foreground color.</para>
	/// <para>If set to <see langword="false" />, the original colors of the SVG image are retained.</para>
	/// </summary>
	public bool InheritForeground
	{
		get => this.GetValue<bool>(InheritForegroundProperty);
		set => SetValue(InheritForegroundProperty, value);
	}
	/// <summary>
	/// Gets the <see cref="System.Windows.Media.ImageSource" /> representation of the SVG image.
	/// </summary>
	public ImageSource? ImageSource
	{
		get => this.GetValue<ImageSource?>(ImageSourceProperty);
		private set => SetValue(ImageSourcePropertyKey, value);
	}

	static UiSvgImage()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiSvgImage), new FrameworkPropertyMetadata(typeof(UiSvgImage)));
		ForegroundProperty.OverrideMetadata(typeof(UiSvgImage), new FrameworkPropertyMetadata(Brushes.Black, Source_Changed));
	}

	private static void Source_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		UiSvgImage svgImage = (UiSvgImage)dependencyObject;

		if (svgImage.Source.IsNullOrEmpty())
		{
			svgImage.ImageSource = null;
			return;
		}

		try
		{
			if (svgImage.InheritForeground)
			{
				svgImage.ImageSource = SvgSource.ImageSource(svgImage.Source, (svgImage.Foreground as SolidColorBrush)?.Color ?? Colors.Black);
			}
			else
			{
				svgImage.ImageSource = SvgSource.ImageSource(svgImage.Source);
			}
		}
		catch
		{
			// Allow assigning non-svg images.
			svgImage.ImageSource = new BitmapImage(new(svgImage.Source, UriKind.Relative));
		}
	}
}