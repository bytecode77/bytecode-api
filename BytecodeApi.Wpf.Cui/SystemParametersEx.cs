using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui;

/// <summary>
/// Helper class that extends <see cref="SystemParameters" /> with additional functionality.
/// </summary>
public static class SystemParametersEx
{
	/// <summary>
	/// Sets the global initial delay, in milliseconds, before a tooltip is displayed.
	/// </summary>
	/// <param name="milliseconds">The delay, in milliseconds, to wait before showing a tooltip.</param>
	public static void SetToolTipDelay(int milliseconds)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(milliseconds);

		ToolTipService.InitialShowDelayProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(milliseconds));
	}
	/// <summary>
	/// Sets the global menu drop alignment.
	/// </summary>
	/// <param name="leftAligned"><see langword="true" /> to align menu dropdowns to the left; <see langword="false" /> to align menu dropdowns to the right.</param>
	public static void SetMenuDropAlignment(bool leftAligned)
	{
		Update();
		SystemParameters.StaticPropertyChanged += delegate { Update(); };

		void Update()
		{
			try
			{
				if (SystemParameters.MenuDropAlignment != leftAligned && typeof(SystemParameters).GetField("_menuDropAlignment", BindingFlags.NonPublic | BindingFlags.Static) is FieldInfo field)
				{
					field.SetValue(null, leftAligned);
				}
			}
			catch
			{
			}
		}
	}
}