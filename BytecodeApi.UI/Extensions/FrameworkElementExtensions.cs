using System.Windows;

namespace BytecodeApi.UI.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="FrameworkElement" /> objects.
	/// </summary>
	public static class FrameworkElementExtensions
	{
		/// <summary>
		/// Searches for a resource with the specified key, and throws an exception, if the requested resource is not found.
		/// </summary>
		/// <typeparam name="T">The return type of the resource.</typeparam>
		/// <param name="frameworkElement">The <see cref="FrameworkElement" /> to search in.</param>
		/// <param name="key">The key identifier for the requested resource.</param>
		/// <returns>
		/// The requested resource. If no resource with the specified key was found, an exception is thrown. An <see cref="DependencyProperty.UnsetValue" /> value might also be returned in the exception case.
		/// </returns>
		public static T FindResource<T>(this FrameworkElement frameworkElement, object key)
		{
			Check.ArgumentNull(frameworkElement, nameof(frameworkElement));
			Check.ArgumentNull(key, nameof(key));

			return CSharp.CastOrDefault<T>(frameworkElement.FindResource(key));
		}
		/// <summary>
		/// Searches for a resource with the specified key, and returns that resource, if found and of type specified type.
		/// </summary>
		/// <typeparam name="T">The return type of the resource.</typeparam>
		/// <param name="frameworkElement">The <see cref="FrameworkElement" /> to search in.</param>
		/// <param name="key">The key identifier of the resource to be found.</param>
		/// <returns>
		/// The found resource, or <see langword="default" />(<typeparamref name="T" />), if no resource with the provided key is found or it does not match the specified type.
		/// </returns>
		public static T TryFindResource<T>(this FrameworkElement frameworkElement, object key)
		{
			Check.ArgumentNull(frameworkElement, nameof(frameworkElement));
			Check.ArgumentNull(key, nameof(key));

			return CSharp.CastOrDefault<T>(frameworkElement.TryFindResource(key));
		}
		/// <summary>
		/// Attempts to bring the top left corner of this <see cref="FrameworkElement" /> into view.
		/// </summary>
		/// <param name="frameworkElement">The <see cref="FrameworkElement" /> to be processed.</param>
		public static void ScrollToTop(this FrameworkElement frameworkElement)
		{
			Check.ArgumentNull(frameworkElement, nameof(frameworkElement));

			frameworkElement.BringIntoView(new Rect());
		}
	}
}