using BytecodeApi.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BytecodeApi.UI.Controls
{
	/// <summary>
	/// Helper class for the WPF <see cref="MenuItem" /> control that implements an icon that can be defined in XAML.
	/// </summary>
	public static class MenuItemService
	{
		/// <summary>
		/// Identifies the <see cref="MenuItemService" />.ImageSource dependency property. This field is read-only.
		/// </summary>
		public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.RegisterAttached("ImageSource", typeof(string), typeof(MenuItem), new FrameworkPropertyMetadata(Changed));
		/// <summary>
		/// Gets the image source of this <see cref="MenuItem.Icon" />.
		/// </summary>
		/// <param name="dependencyObject">The <see cref="MenuItem" /> to get the image source from.</param>
		/// <returns>
		/// The image source of this <see cref="MenuItem.Icon" />.
		/// </returns>
		public static object GetImageSource(DependencyObject dependencyObject)
		{
			Check.ArgumentNull(dependencyObject, nameof(dependencyObject));

			return dependencyObject.GetValue(ImageSourceProperty);
		}
		/// <summary>
		/// Gets the image source of this <see cref="MenuItem.Icon" />.
		/// </summary>
		/// <param name="dependencyObject">The <see cref="MenuItem" /> to assign the image source to.</param>
		/// <param name="value">The image source of this <see cref="MenuItem.Icon" />.</param>
		public static void SetImageSource(DependencyObject dependencyObject, object value)
		{
			Check.ArgumentNull(dependencyObject, nameof(dependencyObject));

			dependencyObject.SetValue(ImageSourceProperty, value);
		}

		private static void Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			string uri = GetImageSource(dependencyObject) as string;
			((MenuItem)dependencyObject).Icon = uri == null ? null : new Image { Source = new BitmapImage((Packs.Application + uri).ToUri(UriKind.Absolute)) };
		}
	}
}