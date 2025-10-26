using System.Data;

namespace BytecodeApi.Wpf.Cui.Converters;

public sealed class DataGridRowNumberConverter : ConverterBase<DataRowView>
{
	public override object? Convert(DataRowView? value)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return value.DataView.Table?.Rows.IndexOf(value.Row) + 1;
		}
	}
}