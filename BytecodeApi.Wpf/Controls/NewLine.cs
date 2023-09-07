using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BytecodeApi.Wpf.Controls;

/// <summary>
/// Represents a line break in a <see cref="WrapPanel" />.
/// </summary>
public class NewLine : FrameworkElement
{
	/// <summary>
	/// Initializes a new instance of the <see cref="NewLine" /> class.
	/// </summary>
	public NewLine()
	{
		Height = 0;

		BindingOperations.SetBinding(this, WidthProperty, new Binding()
		{
			RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(WrapPanel), 1),
			Path = new PropertyPath(nameof(ActualWidth))
		});
	}
}