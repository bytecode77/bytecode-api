using BytecodeApi.Extensions;
using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BytecodeApi.Wpf.Services;

/// <summary>
/// Helper class for the WPF <see cref="MenuItem" /> control.
/// </summary>
public static class MenuItemService
{
	/// <summary>
	/// Identifies the <see cref="MenuItemService" />.ImageSource dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty ImageSourceProperty = DependencyPropertyEx.RegisterAttached<MenuItem, string>("ImageSource", new FrameworkPropertyMetadata(ImageSource_Changed));
	/// <summary>
	/// Gets the image source of this <see cref="MenuItem.Icon" />.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="MenuItem" /> to get the image source from.</param>
	/// <returns>
	/// The image source of this <see cref="MenuItem.Icon" />.
	/// </returns>
	public static string? GetImageSource(DependencyObject dependencyObject)
	{
		Check.ArgumentNull(dependencyObject);

		return dependencyObject.GetValue<string?>(ImageSourceProperty);
	}
	/// <summary>
	/// Sets the image source of this <see cref="MenuItem.Icon" />.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="MenuItem" /> to assign the image source to.</param>
	/// <param name="value">The image source of this <see cref="MenuItem.Icon" />.</param>
	public static void SetImageSource(DependencyObject dependencyObject, string? value)
	{
		Check.ArgumentNull(dependencyObject);

		dependencyObject.SetValue(ImageSourceProperty, value);
	}

	private static void ImageSource_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		if (dependencyObject is MenuItem menuItem)
		{
			if (GetImageSource(dependencyObject) is string uri)
			{
				menuItem.Icon = new Image
				{
					Source = new BitmapImage((Packs.Application + uri).ToUri(UriKind.Absolute))
				};
			}
			else
			{
				menuItem.Icon = null;
			}
		}
	}
}