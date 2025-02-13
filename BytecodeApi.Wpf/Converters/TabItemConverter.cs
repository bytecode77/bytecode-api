using System.Windows.Controls;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="TabItem" />? values. The <see cref="Convert(TabItem?)" /> method returns an <see cref="object" /> based on the specified <see cref="TabItemConverterMethod" /> parameter.
/// </summary>
public sealed class TabItemConverter : ConverterBase<TabItem?>
{
	/// <summary>
	/// Specifies the method that is used to convert the <see cref="TabItem" />? value.
	/// </summary>
	public TabItemConverterMethod Method { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="TabItemConverter" /> class with the specified conversion method.
	/// </summary>
	/// <param name="method">The method that is used to convert the <see cref="TabItem" />? value.</param>
	public TabItemConverter(TabItemConverterMethod method)
	{
		Method = method;
	}

	/// <summary>
	/// Converts the <see cref="TabItem" />? value based on the specified <see cref="TabItemConverterMethod" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="TabItem" />? value to convert.</param>
	/// <returns>
	/// An <see cref="object" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(TabItem? value)
	{
		TabControl? tabControl = value?.Parent as TabControl;
		if (value == null || tabControl == null)
		{
			return null;
		}

		switch (Method)
		{
			case TabItemConverterMethod.Index:
				return tabControl.Items.IndexOf(value);
			case TabItemConverterMethod.IsFirst:
				return tabControl.Items.IndexOf(value) == 0;
			case TabItemConverterMethod.IsLast:
				return tabControl.Items.IndexOf(value) == tabControl.Items.Count - 1;
			case TabItemConverterMethod.PreviousHeader:
				{
					if (tabControl.Items.IndexOf(value) is int index and > 0)
					{
						return ((TabItem)tabControl.Items[index - 1]).Header;
					}
					else
					{
						return null;
					}
				}
			case TabItemConverterMethod.NextHeader:
				{
					if (tabControl.Items.IndexOf(value) is int index && index < tabControl.Items.Count - 1)
					{
						return ((TabItem)tabControl.Items[index + 1]).Header;
					}
					else
					{
						return null;
					}
				}
			default:
				throw Throw.InvalidEnumArgument(nameof(Method), Method);
		}
	}
}