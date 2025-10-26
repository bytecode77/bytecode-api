using BytecodeApi.Extensions;
using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiSvgImage : Control
{
	public static readonly DependencyProperty SourceProperty = DependencyPropertyEx.Register(nameof(Source), new(Source_Changed));
	public static readonly DependencyProperty StretchProperty = DependencyPropertyEx.Register(nameof(Stretch), new(Stretch.Uniform));
	public static readonly DependencyProperty InheritForegroundProperty = DependencyPropertyEx.Register(nameof(InheritForeground), new(Source_Changed));
	private static readonly DependencyPropertyKey ImageSourcePropertyKey = DependencyPropertyEx.RegisterReadOnly(nameof(ImageSource), new FrameworkPropertyMetadata(null));
	public static readonly DependencyProperty ImageSourceProperty = ImageSourcePropertyKey.DependencyProperty;
	public string? Source
	{
		get => this.GetValue<string?>(SourceProperty);
		set => SetValue(SourceProperty, value);
	}
	public Stretch Stretch
	{
		get => this.GetValue<Stretch>(StretchProperty);
		set => SetValue(StretchProperty, value);
	}
	public bool InheritForeground
	{
		get => this.GetValue<bool>(InheritForegroundProperty);
		set => SetValue(InheritForegroundProperty, value);
	}
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