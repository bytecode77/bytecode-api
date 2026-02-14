using System.Data;

namespace BytecodeApi.Wpf.Cui.Converters;

/// <summary>
/// Represents the converter that converts <see cref="DataRowView" /> values to their corresponding one-based row numbers.
/// </summary>
public sealed class DataGridRowNumberConverter : ConverterBase<DataRowView>
{
	/// <summary>
	/// Converts the <see cref="DataRowView" /> value to its corresponding one-based row number.
	/// </summary>
	/// <param name="value">The <see cref="DataRowView" />? value to convert.</param>
	/// <returns>
	/// The one-based row number.
	/// </returns>
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