using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace BytecodeApi.UI.Controls
{
	/// <summary>
	/// Represents an <see cref="Adorner" /> for watermark elements.
	/// </summary>
	public class WatermarkAdorner : Adorner
	{
		private readonly ContentPresenter ContentPresenter;
		private Control Control => (Control)AdornedElement;
		/// <summary>
		/// Gets the number of visual child elements within this element.
		/// </summary>
		protected override int VisualChildrenCount => 1;

		/// <summary>
		/// Initializes a new instance of the <see cref="WatermarkAdorner" /> class with the specified element and watermark.
		/// </summary>
		/// <param name="uiElement">The <see cref="UIElement" /> to apply the watermark to.</param>
		/// <param name="watermark">The <see cref="object" /> that represents the watermark. This is typically a <see cref="TextBlock" /> element.</param>
		public WatermarkAdorner(UIElement uiElement, object watermark) : base(uiElement)
		{
			Check.ArgumentNull(uiElement, nameof(uiElement));

			IsHitTestVisible = false;

			ContentPresenter = new ContentPresenter
			{
				Content = watermark,
				Opacity = .5,
				Margin = new Thickness(Control.Margin.Left + Control.Padding.Left, Control.Margin.Top + Control.Padding.Top, 0, 0)
			};

			if (Control is ItemsControl && !(Control is ComboBox))
			{
				ContentPresenter.VerticalAlignment = VerticalAlignment.Center;
				ContentPresenter.HorizontalAlignment = HorizontalAlignment.Center;
			}

			SetBinding(VisibilityProperty, new Binding("IsVisible")
			{
				Source = uiElement,
				Converter = new BooleanToVisibilityConverter()
			});
		}

		/// <summary>
		/// Overrides <see cref="Visual.GetVisualChild" />, and returns a child at the specified index from a collection of child elements.
		/// </summary>
		/// <param name="index">The zero-based index of the requested child element in the collection.</param>
		/// <returns>
		/// The requested child element. This should not return <see langword="null" />;
		/// if the provided index is out of range, an exception is thrown.
		/// </returns>
		protected override Visual GetVisualChild(int index)
		{
			return ContentPresenter;
		}
		/// <summary>
		/// Implements any custom measuring behavior for the adorner.
		/// </summary>
		/// <param name="constraint">A size to constrain the adorner to.</param>
		/// <returns>
		/// A <see cref="Size" /> object representing the amount of layout space needed by the adorner.
		/// </returns>
		protected override Size MeasureOverride(Size constraint)
		{
			ContentPresenter.Measure(Control.RenderSize);
			return Control.RenderSize;
		}
		/// <summary>
		/// Positions child elements and determines a size for a <see cref="FrameworkElement" /> derived class.
		/// </summary>
		/// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
		/// <returns>
		/// The actual size used.
		/// </returns>
		protected override Size ArrangeOverride(Size finalSize)
		{
			ContentPresenter.Arrange(new Rect(finalSize));
			return finalSize;
		}
	}
}