using System.Windows;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="FrameworkElement" /> objects.
/// </summary>
public static class FrameworkElementExtensions
{
	extension(FrameworkElement frameworkElement)
	{
		/// <summary>
		/// Searches for a resource with the specified key, and throws an exception, if the requested resource is not found.
		/// </summary>
		/// <typeparam name="T">The return type of the resource.</typeparam>
		/// <param name="key">The key identifier for the requested resource.</param>
		/// <returns>
		/// The requested resource. If no resource with the specified key was found, an exception is thrown. An <see cref="DependencyProperty.UnsetValue" /> value might also be returned in the exception case.
		/// </returns>
		public T FindResource<T>(object key)
		{
			Check.ArgumentNull(frameworkElement);
			Check.ArgumentNull(key);

			return CSharp.CastOrDefault<T>(frameworkElement.FindResource(key))!;
		}
		/// <summary>
		/// Searches for a resource with the specified key, and returns that resource, if found and of type specified type.
		/// </summary>
		/// <typeparam name="T">The return type of the resource.</typeparam>
		/// <param name="key">The key identifier of the resource to be found.</param>
		/// <returns>
		/// The found resource, or <see langword="default" />(<typeparamref name="T" />), if no resource with the provided key is found or it does not match the specified type.
		/// </returns>
		public T? TryFindResource<T>(object key)
		{
			Check.ArgumentNull(frameworkElement);
			Check.ArgumentNull(key);

			return CSharp.CastOrDefault<T>(frameworkElement.TryFindResource(key));
		}
		/// <summary>
		/// Attempts to bring the top left corner of this <see cref="FrameworkElement" /> into view.
		/// </summary>
		public void ScrollToTop()
		{
			Check.ArgumentNull(frameworkElement);

			frameworkElement.BringIntoView(new());
		}
	}
}