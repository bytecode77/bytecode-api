using System.Windows;

namespace BytecodeApi.Wpf;

/// <summary>
/// Helper class to retrieve the DataContext property from <see cref="FrameworkElement" /> and <see cref="FrameworkContentElement" /> objects.
/// </summary>
public static class UIContext
{
	/// <summary>
	/// Returns the DataContext property, if the specified <see cref="object" /> instance is either a <see cref="FrameworkElement" /> or a <see cref="FrameworkContentElement" />. If found and can be casted to the specified type, the DataContext is returned, otherwise, <see langword="default" />(<typeparamref name="T" />).
	/// </summary>
	/// <typeparam name="T">The return type to cast the DataContext property to.</typeparam>
	/// <param name="obj">The <see cref="object" /> where the DataContext property looked for. This is typically a parameter from a WPF event handler.</param>
	/// <returns>
	/// <see cref="FrameworkElement.DataContext" />, if <paramref name="obj" /> is a <see cref="FrameworkElement" />;
	/// <see cref="FrameworkContentElement.DataContext" />, if <paramref name="obj" /> instance is a <see cref="FrameworkContentElement" />;
	/// otherwise, <see langword="default" />(<typeparamref name="T" />).
	/// </returns>
	public static T? Find<T>(object obj)
	{
		if (obj is FrameworkElement frameworkElement && frameworkElement.DataContext is T dataContext1)
		{
			return dataContext1;
		}
		else if (obj is FrameworkContentElement frameworkContentElement && frameworkContentElement.DataContext is T dataContext2)
		{
			return dataContext2;
		}
		else
		{
			return default;
		}
	}
}