using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Services;

/// <summary>
/// Helper class for the WPF <see cref="TreeView" /> control.
/// </summary>
public static class TreeViewItemService
{
	private static TreeViewItem? CurrentItem;
	private static readonly RoutedEvent UpdateOverItemEvent = EventManager.RegisterRoutedEvent("UpdateOverItem", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TreeViewItemService));
	private static readonly DependencyPropertyKey IsMouseDirectlyOverItemKey = DependencyProperty.RegisterAttachedReadOnly<bool>("IsMouseDirectlyOverItem", new FrameworkPropertyMetadata(null, CalculateIsMouseDirectlyOverItem));
	/// <summary>
	/// Identifies the <see cref="TreeViewItemService" />.IsMouseDirectlyOverItem dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty IsMouseDirectlyOverItemProperty = IsMouseDirectlyOverItemKey.DependencyProperty;

	static TreeViewItemService()
	{
		EventManager.RegisterClassHandler(typeof(TreeViewItem), UIElement.MouseEnterEvent, new MouseEventHandler(OnMouseTransition), true);
		EventManager.RegisterClassHandler(typeof(TreeViewItem), UIElement.MouseLeaveEvent, new MouseEventHandler(OnMouseTransition), true);
		EventManager.RegisterClassHandler(typeof(TreeViewItem), UpdateOverItemEvent, new RoutedEventHandler(OnUpdateOverItem));
	}

	/// <summary>
	/// Returns <see langword="true" />, if the mouse is directly hovering a <see cref="TreeViewItem" /> and <see langword="false" />, if it is hovering a child or no <see cref="TreeViewItem" /> at all.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="TreeViewItem" /> object to check.</param>
	/// <returns>
	/// <see langword="true" />, if the mouse is directly hovering a <see cref="TreeViewItem" />;
	/// <see langword="false" />, if it is hovering a child or no <see cref="TreeViewItem" /> at all.
	/// </returns>
	public static bool GetIsMouseDirectlyOverItem(DependencyObject dependencyObject)
	{
		Check.ArgumentNull(dependencyObject);

		return dependencyObject.GetValue<bool>(IsMouseDirectlyOverItemProperty);
	}
	private static object CalculateIsMouseDirectlyOverItem(DependencyObject dependencyObject, object value)
	{
		return dependencyObject == CurrentItem;
	}
	private static void OnMouseTransition(object sender, MouseEventArgs e)
	{
		lock (IsMouseDirectlyOverItemProperty)
		{
			if (CurrentItem != null)
			{
				DependencyObject oldItem = CurrentItem;
				CurrentItem = null;
				oldItem.InvalidateProperty(IsMouseDirectlyOverItemProperty);
			}

			Mouse.DirectlyOver?.RaiseEvent(new RoutedEventArgs(UpdateOverItemEvent));
		}
	}
	private static void OnUpdateOverItem(object sender, RoutedEventArgs e)
	{
		CurrentItem = sender as TreeViewItem;
		CurrentItem?.InvalidateProperty(IsMouseDirectlyOverItemProperty);
		e.Handled = true;
	}
}