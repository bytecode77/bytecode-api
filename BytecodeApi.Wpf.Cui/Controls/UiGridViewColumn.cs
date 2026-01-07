using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a column in a <see cref="UiListView" /> with extended properties.
/// </summary>
public class UiGridViewColumn : GridViewColumn
{
	/// <summary>
	/// Identifies the <see cref="SortProperty" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty SortPropertyProperty = DependencyProperty.Register(nameof(SortProperty));
	/// <summary>
	/// Identifies the <see cref="IsVisible" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(nameof(IsVisible), new(true, IsVisible_Changed));
	/// <summary>
	/// Gets or sets a <see cref="string" /> value that corresponds to the <see cref="UiListView.SortColumn" /> property when sorting by this column.
	/// </summary>
	public string? SortProperty
	{
		get => this.GetValue<string?>(SortPropertyProperty);
		set => SetValue(SortPropertyProperty, value);
	}
	/// <summary>
	/// Gets or sets a <see cref="bool" /> value that indicates whether the column is visible.
	/// </summary>
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