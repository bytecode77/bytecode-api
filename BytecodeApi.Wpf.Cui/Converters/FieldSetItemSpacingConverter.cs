using System.Windows;

namespace BytecodeApi.Wpf.Cui.Converters;

public sealed class FieldSetItemSpacingConverter : ConverterBase<double>
{
	public override object? Convert(double value)
	{
		return new Thickness(0, 0, 0, value);
	}
}