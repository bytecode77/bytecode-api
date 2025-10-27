using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace BytecodeApi.Wpf.Cui.Controls;

public sealed class SortAdorner : Adorner
{
	private static readonly Geometry AscendingGeometry = Geometry.Parse("M 0,6 L0,5 L4,1 L8,5 L8,6 L4,2 z");
	private static readonly Geometry DescendingGeometry = Geometry.Parse("M 0,1 L0,2 L4,6 L8,2 L8,1 L4,5 z");
	public ListSortDirection Direction { get; private init; }

	public SortAdorner(UIElement element, ListSortDirection direction) : base(element)
	{
		Direction = direction;
	}

	protected override void OnRender(DrawingContext drawingContext)
	{
		base.OnRender(drawingContext);

		if (AdornedElement.RenderSize.Width > 15)
		{
			drawingContext.PushTransform(new TranslateTransform(AdornedElement.RenderSize.Width / 2 - 4, 0));
			drawingContext.DrawGeometry(Brushes.Black, null, Direction == ListSortDirection.Ascending ? AscendingGeometry : DescendingGeometry);
			drawingContext.Pop();
		}
	}
}