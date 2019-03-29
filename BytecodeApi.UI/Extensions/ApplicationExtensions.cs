using System.Windows;
using System.Windows.Media;

namespace BytecodeApi.UI.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Application" /> objects.
	/// </summary>
	public static class ApplicationExtensions
	{
		/// <summary>
		/// Searches for a user interface (UI) resource, such as a <see cref="Style" /> or <see cref="Brush" />, with the specified key, and throws an exception, if the requested resource is not found (see XAML Resources) or is not of the specified type.
		/// </summary>
		/// <typeparam name="T">The return type of the resource.</typeparam>
		/// <param name="application">The <see cref="Application" /> to search in.</param>
		/// <param name="key">The name of the resource to find.</param>
		/// <returns>
		/// The requested resource object. If the requested resource is not found or is not of the specified type, a <see cref="ResourceReferenceKeyNotFoundException" /> is thrown.
		/// </returns>
		public static T FindResource<T>(this Application application, object key)
		{
			Check.ArgumentNull(application, nameof(application));
			Check.ArgumentNull(key, nameof(key));

			return CSharp.CastOrDefault<T>(application.FindResource(key));
		}
		/// <summary>
		/// Searches for a resource with the specified key, and returns that resource, if found and of type specified type.
		/// </summary>
		/// <typeparam name="T">The return type of the resource.</typeparam>
		/// <param name="application">The <see cref="Application" /> to search in.</param>
		/// <param name="key">The name of the resource to find.</param>
		/// <returns>
		/// The found resource, or <see langword="null" />, if no resource with the provided key is found or it does not match the specified type.
		/// </returns>
		public static T TryFindResource<T>(this Application application, object key)
		{
			Check.ArgumentNull(application, nameof(application));
			Check.ArgumentNull(key, nameof(key));

			return CSharp.CastOrDefault<T>(application.TryFindResource(key));
		}
	}
}