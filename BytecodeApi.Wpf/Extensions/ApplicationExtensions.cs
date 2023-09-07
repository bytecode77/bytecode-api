using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Application" /> objects.
/// </summary>
public static class ApplicationExtensions
{
	/// <summary>
	/// Searches for a user interface (UI) resource, such as a <see cref="Style" /> or <see cref="Brush" />, with the specified key, and throws an exception, if the requested resource is not found (see XAML Resources).
	/// </summary>
	/// <typeparam name="T">The return type of the resource.</typeparam>
	/// <param name="application">The <see cref="Application" /> to search in.</param>
	/// <param name="key">The name of the resource to find.</param>
	/// <returns>
	/// The requested resource object. If the requested resource is not found, a <see cref="ResourceReferenceKeyNotFoundException" /> is thrown.
	/// </returns>
	public static T FindResource<T>(this Application application, object key)
	{
		Check.ArgumentNull(application);
		Check.ArgumentNull(key);

		return CSharp.CastOrDefault<T>(application.FindResource(key))!;
	}
	/// <summary>
	/// Searches for a resource with the specified key, and returns that resource, if found and of type specified type.
	/// </summary>
	/// <typeparam name="T">The return type of the resource.</typeparam>
	/// <param name="application">The <see cref="Application" /> to search in.</param>
	/// <param name="key">The name of the resource to find.</param>
	/// <returns>
	/// The found resource, or <see langword="default" />(<typeparamref name="T" />), if no resource with the provided key is found or it does not match the specified type.
	/// </returns>
	public static T? TryFindResource<T>(this Application application, object key)
	{
		Check.ArgumentNull(application);
		Check.ArgumentNull(key);

		return CSharp.CastOrDefault<T>(application.TryFindResource(key));
	}

	/// <summary>
	/// Invokes an empty <see cref="Action" /> on the <see cref="Dispatcher" /> of <see cref="Application.Current" /> while <paramref name="condition" /> evaluates to <see langword="true" />, thereby refreshing the UI. This is the WPF equivalent to <see cref="System.Windows.Forms.Application.DoEvents" />.
	/// </summary>
	/// <param name="application">The <see cref="Application" /> to perform the wait operation on.</param>
	/// <param name="condition">The <see cref="Func{TResult}" /> to be evaluated.</param>
	public static void DoEventsWhile(this Application application, Func<bool> condition)
	{
		application.DoEventsWhile(condition, TimeSpan.FromMilliseconds(1));
	}
	/// <summary>
	/// Invokes an empty <see cref="Action" /> on the <see cref="Dispatcher" /> of <see cref="Application.Current" /> while <paramref name="condition" /> evaluates to <see langword="true" />, thereby refreshing the UI. This is the WPF equivalent to <see cref="System.Windows.Forms.Application.DoEvents" />. The specified delay is waited between each call to <paramref name="condition" />.
	/// </summary>
	/// <param name="application">The <see cref="Application" /> to perform the wait operation on.</param>
	/// <param name="condition">The <see cref="Func{TResult}" /> to be evaluated.</param>
	/// <param name="delay">A <see cref="TimeSpan" /> that specifies the delay between each call to <paramref name="condition" />. The default value is 1 milliseconds.</param>
	public static void DoEventsWhile(this Application application, Func<bool> condition, TimeSpan delay)
	{
		Check.ArgumentNull(application);
		Check.ArgumentNull(condition);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(delay);

		do
		{
			Thread.Sleep(delay);
			application.Dispatcher.Invoke(delegate { }, DispatcherPriority.Background);
		}
		while (condition());
	}
}