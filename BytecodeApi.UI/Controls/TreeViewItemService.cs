using BytecodeApi.UI.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.UI.Controls
{
	/// <summary>
	/// Helper class for the WPF <see cref="TreeView" /> control that checks whether a MouseOver event occurrs over an item directly. This is relevant, when MouseOver effects need to be implemented for a <see cref="TreeViewItem" /> excluding its children.
	/// </summary>
	public static class TreeViewItemService
	{
		private static TreeViewItem CurrentItem;
		private static readonly RoutedEvent UpdateOverItemEvent = EventManager.RegisterRoutedEvent("UpdateOverItem", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TreeViewItemService));
		private static readonly DependencyPropertyKey IsMouseDirectlyOverItemKey = DependencyProperty.RegisterAttachedReadOnly("IsMouseDirectlyOverItem", typeof(bool), typeof(TreeViewItemService), new FrameworkPropertyMetadata(null, new CoerceValueCallback(CalculateIsMouseDirectlyOverItem)));
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
		/// <param name="obj">The <see cref="TreeViewItem" /> object to check.</param>
		/// <returns>
		/// <see langword="true" />, if the mouse is directly hovering a <see cref="TreeViewItem" />;
		/// <see langword="false" />, if it is hovering a child or no <see cref="TreeViewItem" /> at all.
		/// </returns>
		public static bool GetIsMouseDirectlyOverItem(DependencyObject obj)
		{
			Check.ArgumentNull(obj, nameof(obj));

			return obj.GetValue<bool>(IsMouseDirectlyOverItemProperty);
		}
		private static object CalculateIsMouseDirectlyOverItem(DependencyObject item, object value)
		{
			return item == CurrentItem;
		}
		private static void OnUpdateOverItem(object sender, RoutedEventArgs e)
		{
			CurrentItem = sender as TreeViewItem;
			CurrentItem.InvalidateProperty(IsMouseDirectlyOverItemProperty);
			e.Handled = true;
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
	}
}