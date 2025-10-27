using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiDataGrid : DataGrid
{
	public static readonly DependencyProperty ShowRowNumbersProperty = DependencyPropertyEx.Register(nameof(ShowRowNumbers));
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