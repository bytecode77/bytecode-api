using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiGridViewColumn : GridViewColumn
{
	public static readonly DependencyProperty SortPropertyProperty = DependencyPropertyEx.Register(nameof(SortProperty));
	public static readonly DependencyProperty IsVisibleProperty = DependencyPropertyEx.Register(nameof(IsVisible), new(true, IsVisible_Changed));
	public string? SortProperty
	{
		get => this.GetValue<string?>(SortPropertyProperty);
		set => SetValue(SortPropertyProperty, value);
	}
	public bool IsVisible
	{
		get => this.GetValue<bool>(IsVisibleProperty);
		set => SetValue(IsVisibleProperty, value);
	}
	private double HiddenWidth = double.NaN;

	private static void IsVisible_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		if (dependencyObject is UiGridViewColumn column)
		{
			if (column.IsVisible)
			{
				column.Width = column.HiddenWidth;
			}
			else
			{
				if (column.Width > 0)
				{
					column.HiddenWidth = column.Width;
				}

				column.Width = 0;
			}
		}
	}
}