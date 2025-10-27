using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Cui;

public static class SystemParametersEx
{
	public static void SetToolTipDelay(int milliseconds)
	{
		ToolTipService.InitialShowDelayProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(milliseconds));
	}
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