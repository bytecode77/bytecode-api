using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiGridViewColumn : GridViewColumn
{
	public static readonly DependencyProperty SortPropertyProperty = DependencyPropertyEx.Register(nameof(SortProperty));
	public static readonly DependencyProperty IsColumnVisibleProperty = DependencyPropertyEx.Register(nameof(IsColumnVisible), new(true, IsColumnVisible_Changed));
	public string? SortProperty
	{
		get => this.GetValue<string?>(SortPropertyProperty);
		set => SetValue(SortPropertyProperty, value);
	}
	public bool IsColumnVisible
	{
		get => this.GetValue<bool>(IsColumnVisibleProperty);
		set => SetValue(IsColumnVisibleProperty, value);
	}
	private double HiddenColumnWidth = double.NaN;

	private static void IsColumnVisible_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		if (dependencyObject is UiGridViewColumn column)
		{
			if (column.IsColumnVisible)
			{
				column.Width = column.HiddenColumnWidth;
			}
			else
			{
				if (column.Width > 0)
				{
					column.HiddenColumnWidth = column.Width;
				}

				column.Width = 0;
			}
		}
	}
}