using BytecodeApi.Extensions;
using BytecodeApi.UI.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace BytecodeApi.UI.Controls
{
	/// <summary>
	/// Helper class to display watermark elements on top of <see cref="TextBox" />, <see cref="ComboBox" /> and <see cref="ItemsControl" /> elements.
	/// </summary>
	public static class WatermarkService
	{
		private static readonly Dictionary<object, ItemsControl> ItemsControls = new Dictionary<object, ItemsControl>();
		/// <summary>
		/// Identifies the <see cref="WatermarkService" />.Watermark dependency property. This field is read-only.
		/// </summary>
		public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached("Watermark", typeof(object), typeof(WatermarkService), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(Changed)));
		/// <summary>
		/// Gets the watermark of this <see cref="DependencyObject" />.
		/// </summary>
		/// <param name="dependencyObject">The <see cref="DependencyObject" /> to get the watermark from.</param>
		/// <returns>
		/// The watermark of this <see cref="DependencyObject" />.
		/// </returns>
		public static object GetWatermark(DependencyObject dependencyObject)
		{
			Check.ArgumentNull(dependencyObject, nameof(dependencyObject));

			return dependencyObject.GetValue(WatermarkProperty);
		}
		/// <summary>
		/// Sets the watermark of this <see cref="DependencyObject" />.
		/// </summary>
		/// <param name="dependencyObject">The <see cref="DependencyObject" /> to set the watermark of.</param>
		/// <param name="value">The <see cref="object" /> that represents the watermark. This is typically a <see cref="TextBlock" /> element.</param>
		public static void SetWatermark(DependencyObject dependencyObject, object value)
		{
			Check.ArgumentNull(dependencyObject, nameof(dependencyObject));

			dependencyObject.SetValue(WatermarkProperty, value);
		}

		private static void Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			Control control = (Control)dependencyObject;
			control.Loaded += Control_Loaded;

			if (dependencyObject is ComboBox)
			{
				control.GotKeyboardFocus += Control_GotKeyboardFocus;
				control.LostKeyboardFocus += Control_Loaded;
			}
			else if (dependencyObject is TextBox textBoxDependencyObject)
			{
				control.GotKeyboardFocus += Control_GotKeyboardFocus;
				control.LostKeyboardFocus += Control_Loaded;
				textBoxDependencyObject.TextChanged += Control_GotKeyboardFocus;
			}

			if (dependencyObject is ItemsControl itemsControlDependencyObject && !(dependencyObject is ComboBox))
			{
				ItemsControl itemsControl = itemsControlDependencyObject;
				itemsControl.ItemContainerGenerator.ItemsChanged += Control_ItemsChanged;
				ItemsControls.Add(itemsControl.ItemContainerGenerator, itemsControl);

				DependencyPropertyDescriptor
					.FromProperty(ItemsControl.ItemsSourceProperty, itemsControl.GetType())
					.AddValueChanged(itemsControl, Control_ItemsSourceChanged);
			}
		}
		private static void Control_GotKeyboardFocus(object sender, RoutedEventArgs e)
		{
			Control control = (Control)sender;
			SetWatermarkVisibility(control, GetWatermarkSuggestedVisibility(control));
		}
		private static void Control_Loaded(object sender, RoutedEventArgs e)
		{
			Control control = (Control)sender;
			if (GetWatermarkSuggestedVisibility(control)) SetWatermarkVisibility(control, true);
		}
		private static void Control_ItemsSourceChanged(object sender, EventArgs e)
		{
			ItemsControl itemsControl = (ItemsControl)sender;
			SetWatermarkVisibility(itemsControl, itemsControl.ItemsSource == null || GetWatermarkSuggestedVisibility(itemsControl));
		}
		private static void Control_ItemsChanged(object sender, ItemsChangedEventArgs e)
		{
			if (ItemsControls.TryGetValue(sender, out ItemsControl control)) SetWatermarkVisibility(control, GetWatermarkSuggestedVisibility(control));
		}
		private static bool GetWatermarkSuggestedVisibility(Control control)
		{
			return (control as ComboBox)?.Text.IsNullOrEmpty() == true || (control as TextBox)?.Text.IsNullOrEmpty() == true || (control as ItemsControl)?.Items.Count == 0;
		}
		private static void SetWatermarkVisibility(Control control, bool visibility)
		{
			AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);

			if (visibility)
			{
				layer?.Add(new WatermarkAdorner(control, GetWatermark(control)));
			}
			else
			{
				layer
					?.GetAdorners(control)
					?.OfType<WatermarkAdorner>()
					.ForEach(adorner =>
					{
						adorner.Hide(true);
						layer.Remove(adorner);
					});
			}
		}
	}
}