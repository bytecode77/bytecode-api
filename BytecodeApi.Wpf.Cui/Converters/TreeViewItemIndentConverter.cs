using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BytecodeApi.Wpf.Cui.Converters;

public sealed class TreeViewItemIndentConverter : ConverterBase<TreeViewItem>
{
	public override object? Convert(TreeViewItem? value)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return new Thickness(GetTreeViewItemLevel(value) * 18, 0, 0, 0);
		}
	}

	private static int GetTreeViewItemLevel(TreeViewItem item)
	{
		int level = 0;

		for (DependencyObject? parent = VisualTreeHelper.GetParent(item); parent != null; parent = VisualTreeHelper.GetParent(parent))
		{
			if (parent is TreeViewItem)
			{
				level++;
			}
		}

		return level;
	}
}