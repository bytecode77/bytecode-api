using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a data grid control.
/// </summary>
public class UiDataGrid : DataGrid
{
	/// <summary>
	/// Identifies the <see cref="ShowRowNumbers" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty ShowRowNumbersProperty = DependencyPropertyEx.Register(nameof(ShowRowNumbers));
	/// <summary>
	/// Gets or sets a <see cref="bool" /> value that indicates whether row numbers are displayed in the row header.
	/// </summary>
	public bool ShowRowNumbers
	{
		get => this.GetValue<bool>(ShowRowNumbersProperty);
		set => SetValue(ShowRowNumbersProperty, value);
	}

	static UiDataGrid()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiDataGrid), new FrameworkPropertyMetadata(typeof(UiDataGrid)));
	}
}