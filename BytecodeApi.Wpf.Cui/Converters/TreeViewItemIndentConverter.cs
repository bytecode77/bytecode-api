using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BytecodeApi.Wpf.Cui.Converters;

/// <summary>
/// Represents the converter that converts <see cref="TreeViewItem" /> values to <see cref="Thickness" /> values representing the indentation based on the hierarchical level in the tree.
/// </summary>
public sealed class TreeViewItemIndentConverter : ConverterBase<TreeViewItem>
{
	/// <summary>
	/// Converts the specified <see cref="TreeViewItem" /> value to a <see cref="Thickness" /> value representing the indentation based on the hierarchical level in the tree.
	/// </summary>
	/// <param name="value">The <see cref="TreeViewItem" /> value to convert.</param>
	/// <returns>
	/// A <see cref="Thickness" /> value with the left indentation set based on the hierarchical level in the tree.
	/// </returns>
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