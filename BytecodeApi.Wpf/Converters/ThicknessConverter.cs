using System.Windows;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="Thickness" />? values. The <see cref="Convert(Thickness?, double?)" /> method returns an <see cref="object" /> based on the specified <see cref="ThicknessConverterMethod" /> parameter.
/// </summary>
public sealed class ThicknessConverter : ConverterBase<Thickness?, double?>
{
	/// <summary>
	/// Specifies the method that is used to convert the <see cref="Thickness" />? value.
	/// </summary>
	public ThicknessConverterMethod Method { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ThicknessConverter" /> class with the specified conversion method.
	/// </summary>
	/// <param name="method">The method that is used to convert the <see cref="Thickness" />? value.</param>
	public ThicknessConverter(ThicknessConverterMethod method)
	{
		Method = method;
	}

	/// <summary>
	/// Converts the <see cref="Thickness" />? value based on the specified <see cref="ThicknessConverterMethod" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="Thickness" />? value to convert.</param>
	/// <param name="parameter">A parameter <see cref="double" /> that specifies the offset used in the <see cref="ThicknessConverterMethod" /> methods.</param>
	/// <returns>
	/// A <see cref="Thickness" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(Thickness? value, double? parameter)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return Method switch
			{
				ThicknessConverterMethod.SetLeft => new Thickness(parameter ?? 0, value.Value.Top, value.Value.Right, value.Value.Bottom),
				ThicknessConverterMethod.SetTop => new Thickness(value.Value.Left, parameter ?? 0, value.Value.Right, value.Value.Bottom),
				ThicknessConverterMethod.SetRight => new Thickness(value.Value.Left, value.Value.Top, parameter ?? 0, value.Value.Bottom),
				ThicknessConverterMethod.SetBottom => new Thickness(value.Value.Left, value.Value.Top, value.Value.Right, parameter ?? 0),
				ThicknessConverterMethod.SetHorizontal => new Thickness(parameter ?? 0, value.Value.Top, parameter ?? 0, value.Value.Bottom),
				ThicknessConverterMethod.SetVertical => new Thickness(value.Value.Left, parameter ?? 0, value.Value.Right, parameter ?? 0),
				ThicknessConverterMethod.AddLeft => new Thickness(value.Value.Left + (parameter ?? 0), value.Value.Top, value.Value.Right, value.Value.Bottom),
				ThicknessConverterMethod.AddTop => new Thickness(value.Value.Left, value.Value.Top + (parameter ?? 0), value.Value.Right, value.Value.Bottom),
				ThicknessConverterMethod.AddRight => new Thickness(value.Value.Left, value.Value.Top, value.Value.Right + (parameter ?? 0), value.Value.Bottom),
				ThicknessConverterMethod.AddBottom => new Thickness(value.Value.Left, value.Value.Top, value.Value.Right, value.Value.Bottom + (parameter ?? 0)),
				ThicknessConverterMethod.AddHorizontal => new Thickness(value.Value.Left + (parameter ?? 0), value.Value.Top, value.Value.Right + (parameter ?? 0), value.Value.Bottom),
				ThicknessConverterMethod.AddVertical => new Thickness(value.Value.Left, value.Value.Top + (parameter ?? 0), value.Value.Right, value.Value.Bottom + (parameter ?? 0)),
				ThicknessConverterMethod.KeepLeft => new Thickness(value.Value.Left, parameter ?? 0, parameter ?? 0, parameter ?? 0),
				ThicknessConverterMethod.KeepTop => new Thickness(parameter ?? 0, value.Value.Top, parameter ?? 0, parameter ?? 0),
				ThicknessConverterMethod.KeepRight => new Thickness(parameter ?? 0, parameter ?? 0, value.Value.Right, parameter ?? 0),
				ThicknessConverterMethod.KeepBottom => new Thickness(parameter ?? 0, parameter ?? 0, parameter ?? 0, value.Value.Bottom),
				ThicknessConverterMethod.Inverse => new Thickness(-value.Value.Left, -value.Value.Top, -value.Value.Right, -value.Value.Bottom),
				ThicknessConverterMethod.InverseLeft => new Thickness(-value.Value.Left, value.Value.Top, value.Value.Right, value.Value.Bottom),
				ThicknessConverterMethod.InverseTop => new Thickness(value.Value.Left, -value.Value.Top, value.Value.Right, value.Value.Bottom),
				ThicknessConverterMethod.InverseRight => new Thickness(value.Value.Left, value.Value.Top, -value.Value.Right, value.Value.Bottom),
				ThicknessConverterMethod.InverseBottom => new Thickness(value.Value.Left, value.Value.Top, value.Value.Right, -value.Value.Bottom),
				ThicknessConverterMethod.InverseHorizontal => new Thickness(-value.Value.Left, value.Value.Top, -value.Value.Right, value.Value.Bottom),
				ThicknessConverterMethod.InverseVertical => new Thickness(value.Value.Left, -value.Value.Top, value.Value.Right, -value.Value.Bottom),
				_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
			};
		}
	}
}