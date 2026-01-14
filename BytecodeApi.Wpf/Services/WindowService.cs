using BytecodeApi.Extensions;
using BytecodeApi.Interop;
using BytecodeApi.Wpf.Extensions;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace BytecodeApi.Wpf.Services;

/// <summary>
/// Helper class for the WPF <see cref="Window" />.
/// </summary>
public static class WindowService
{
	/// <summary>
	/// Identifies the <see cref="WindowService" />.ShowIcon dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty ShowIconProperty = DependencyProperty.RegisterAttached<Window, bool>("ShowIcon", new FrameworkPropertyMetadata(true, ShowIcon_Changed));
	/// <summary>
	/// Identifies the <see cref="WindowService" />.DisableMinimizeButton dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty DisableMinimizeButtonProperty = DependencyProperty.RegisterAttached<Window, bool>("DisableMinimizeButton", new FrameworkPropertyMetadata(DisableMinimizeButton_Changed));
	/// <summary>
	/// Identifies the <see cref="WindowService" />.DisableMaximizeButton dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty DisableMaximizeButtonProperty = DependencyProperty.RegisterAttached<Window, bool>("DisableMaximizeButton", new FrameworkPropertyMetadata(DisableMaximizeButton_Changed));
	/// <summary>
	/// Identifies the <see cref="WindowService" />.TitleBarBrush dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty TitleBarBrushProperty = DependencyProperty.RegisterAttached<Window, SolidColorBrush>("TitleBarBrush", new FrameworkPropertyMetadata(TitleBarBrush_Changed));
	/// <summary>
	/// Identifies the <see cref="WindowService" />.BorderBrush dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.RegisterAttached<Window, SolidColorBrush>("BorderBrush", new FrameworkPropertyMetadata(BorderBrush_Changed));
	/// <summary>
	/// Identifies the <see cref="WindowService" />.IsAcrylicEnabled dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty IsAcrylicEnabledProperty = DependencyProperty.RegisterAttached<Window, bool>("IsAcrylicEnabled", new FrameworkPropertyMetadata(IsAcrylicEnabled_Changed));
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether this <see cref="Window" /> displays an icon.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to check.</param>
	/// <returns>
	/// <see langword="true" />, if the icon is visible;
	/// <see langword="false" />, if the icon is hidden.
	/// </returns>
	public static bool GetShowIcon(DependencyObject dependencyObject)
	{
		Check.ArgumentNull(dependencyObject);

		return dependencyObject.GetValue<bool>(ShowIconProperty);
	}
	/// <summary>
	/// Shows or hides the icon on this <see cref="Window" />.
	/// <para>This method can only be called during window initialization, i.e. in XAML.</para>
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to set the visibility of the icon.</param>
	/// <param name="value"><see langword="true" /> to show the icon; <see langword="false" /> to hide the icon.</param>
	public static void SetShowIcon(DependencyObject dependencyObject, bool value)
	{
		Check.ArgumentNull(dependencyObject);

		dependencyObject.SetValue(ShowIconProperty, value);
	}
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether the minimize button of this <see cref="Window" /> is disabled.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to process.</param>
	/// <returns>
	/// <see langword="true" />, if the minimize button is disabled;
	/// <see langword="false" />, if the minimize button is enabled.
	/// </returns>
	public static bool GetDisableMinimizeButton(DependencyObject dependencyObject)
	{
		Check.ArgumentNull(dependencyObject);

		return dependencyObject.GetValue<bool>(DisableMinimizeButtonProperty);
	}
	/// <summary>
	/// Enables or disables the minimize button of this <see cref="Window" />.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to configure.</param>
	/// <param name="value"><see langword="true" /> to disable the minimize button; <see langword="false" /> to enable the minimize button.</param>
	public static void SetDisableMinimizeButton(DependencyObject dependencyObject, bool value)
	{
		Check.ArgumentNull(dependencyObject);

		dependencyObject.SetValue(DisableMinimizeButtonProperty, value);
	}
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether the maximize button of this <see cref="Window" /> is disabled.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to process.</param>
	/// <returns>
	/// <see langword="true" />, if the maximize button is disabled;
	/// <see langword="false" />, if the maximize button is enabled.
	/// </returns>
	public static bool GetDisableMaximizeButton(DependencyObject dependencyObject)
	{
		Check.ArgumentNull(dependencyObject);

		return dependencyObject.GetValue<bool>(DisableMaximizeButtonProperty);
	}
	/// <summary>
	/// Enables or disables the maximize button of this <see cref="Window" />.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to configure.</param>
	/// <param name="value"><see langword="true" /> to disable the maximize button; <see langword="false" /> to enable the maximize button.</param>
	public static void SetDisableMaximizeButton(DependencyObject dependencyObject, bool value)
	{
		Check.ArgumentNull(dependencyObject);

		dependencyObject.SetValue(DisableMaximizeButtonProperty, value);
	}
	/// <summary>
	/// Gets the color of the title bar of this <see cref="Window" />.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to check.</param>
	/// <returns>
	/// A <see cref="SolidColorBrush" /> with the color of the title bar of this <see cref="Window" />.
	/// </returns>
	public static SolidColorBrush GetTitleBarBrush(DependencyObject dependencyObject)
	{
		Check.ArgumentNull(dependencyObject);

		return dependencyObject.GetValue<SolidColorBrush>(TitleBarBrushProperty);
	}
	/// <summary>
	/// Sets the color of the title bar of this <see cref="Window" />.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to change the title bar color of.</param>
	/// <param name="value">A <see cref="SolidColorBrush" /> with the color of the title bar.</param>
	public static void SetTitleBarBrush(DependencyObject dependencyObject, SolidColorBrush value)
	{
		Check.ArgumentNull(dependencyObject);

		dependencyObject.SetValue(TitleBarBrushProperty, value);
	}
	/// <summary>
	/// Gets the border color of this <see cref="Window" />.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to check.</param>
	/// <returns>
	/// A <see cref="SolidColorBrush" /> with the border color of this <see cref="Window" />.
	/// </returns>
	public static SolidColorBrush GetBorderBrush(DependencyObject dependencyObject)
	{
		Check.ArgumentNull(dependencyObject);

		return dependencyObject.GetValue<SolidColorBrush>(BorderBrushProperty);
	}
	/// <summary>
	/// Sets the border color of this <see cref="Window" />.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to change the border color of.</param>
	/// <param name="value">A <see cref="SolidColorBrush" /> with the border color for this <see cref="Window" />.</param>
	public static void SetBorderBrush(DependencyObject dependencyObject, SolidColorBrush value)
	{
		Check.ArgumentNull(dependencyObject);

		dependencyObject.SetValue(BorderBrushProperty, value);
	}
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether this <see cref="Window" /> uses acrylic blur.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to check.</param>
	/// <returns>
	/// <see langword="true" />, if acrylic blur is enabled;
	/// <see langword="false" />, if acrylic blur is disabled.
	/// </returns>
	public static bool GetIsAcrylicEnabled(DependencyObject dependencyObject)
	{
		Check.ArgumentNull(dependencyObject);

		return dependencyObject.GetValue<bool>(IsAcrylicEnabledProperty);
	}
	/// <summary>
	/// Enables or disables acrylic blur on this <see cref="Window" />.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="Window" /> to enable or disable acrylic blur on.</param>
	/// <param name="value"><see langword="true" /> to enable acrylic blur; <see langword="false" /> to disable acrylic blur.</param>
	public static void SetIsAcrylicEnabled(DependencyObject dependencyObject, bool value)
	{
		Check.ArgumentNull(dependencyObject);

		dependencyObject.SetValue(IsAcrylicEnabledProperty, value);
	}

	private static void ShowIcon_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		if (dependencyObject is Window window)
		{
			if (!GetShowIcon(dependencyObject))
			{
				ApplyWindowChange(window, handle =>
				{
					Native.SetWindowLong(handle, -20, Native.GetWindowLong(handle, -20) | 1);
					Native.SetWindowPos(handle, 0, 0, 0, 0, 0, 0x27);
					Native.SendMessage(handle, 0x80, 1, 0);
					Native.SendMessage(handle, 0x80, 0, 0);
				});
			}
		}
	}
	private static void DisableMinimizeButton_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		if (dependencyObject is Window window)
		{
			if (GetDisableMinimizeButton(dependencyObject))
			{
				ApplyWindowChange(window, handle => Native.SetWindowLong(handle, -16, Native.GetWindowLong(handle, -16) & ~0x20000));
			}
		}
	}
	private static void DisableMaximizeButton_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		if (dependencyObject is Window window)
		{
			if (GetDisableMaximizeButton(dependencyObject))
			{
				ApplyWindowChange(window, handle =>
				{
					Native.SetWindowLong(handle, -16, Native.GetWindowLong(handle, -16) & ~0x10000);
				});
			}
		}
	}
	private static void TitleBarBrush_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		if (dependencyObject is Window window)
		{
			SolidColorBrush titleBarBrush = GetTitleBarBrush(dependencyObject);

			ApplyWindowChange(window, handle =>
			{
				uint color = (uint)titleBarBrush.Color.B << 16 | (uint)titleBarBrush.Color.G << 8 | titleBarBrush.Color.R;
				Native.DwmSetWindowAttribute(handle, 35, ref color, 4);
			});
		}
	}
	private static void BorderBrush_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		if (dependencyObject is Window window)
		{
			SolidColorBrush borderBrush = GetBorderBrush(dependencyObject);

			ApplyWindowChange(window, handle =>
			{
				uint color = (uint)borderBrush.Color.B << 16 | (uint)borderBrush.Color.G << 8 | borderBrush.Color.R;
				Native.DwmSetWindowAttribute(handle, 34, ref color, 4);
			});
		}
	}
	private static void IsAcrylicEnabled_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		if (dependencyObject is Window window)
		{
			Check.InvalidOperation(window.WindowStyle == WindowStyle.None, "Window.WindowStyle must be set to WindowStyle.None.");
			Check.InvalidOperation(window.AllowsTransparency, "Window.AllowsTransparency must be set to true.");

			ApplyWindowChange(window, handle =>
			{
				Native.AccentPolicy accent = new()
				{
					AccentState = GetIsAcrylicEnabled(dependencyObject) ? 3 : 0
				};

				using HGlobal accentPtr = HGlobal.FromStructure(accent);

				Native.WindowCompositionAttributeData data = new()
				{
					Attribute = 19,
					SizeOfData = Marshal.SizeOf<Native.AccentPolicy>(),
					Data = accentPtr.Handle
				};

				if (Native.SetWindowCompositionAttribute(handle, ref data) == 0)
				{
					throw Throw.Win32("SetWindowCompositionAttribute failed.");
				}
			});
		}
	}

	private static void ApplyWindowChange(Window window, Action<nint> apply)
	{
		nint handle = new WindowInteropHelper(window).Handle;

		if (handle != 0)
		{
			apply(handle);
		}
		else
		{
			window.SourceInitialized += Window_SourceInitialized;

			void Window_SourceInitialized(object? sender, EventArgs e)
			{
				window.SourceInitialized -= Window_SourceInitialized;
				nint handle = new WindowInteropHelper(window).Handle;

				if (handle != 0)
				{
					apply(handle);
				}
			}
		}
	}
}

file static class Native
{
	[DllImport("user32.dll")]
	public static extern int GetWindowLong(nint hwnd, int index);
	[DllImport("user32.dll")]
	public static extern int SetWindowLong(nint hwnd, int index, int newStyle);
	[DllImport("user32.dll")]
	public static extern bool SetWindowPos(nint hwnd, nint hwndInsertAfter, int x, int y, int width, int height, uint flags);
	[DllImport("user32.dll")]
	public static extern nint SendMessage(nint hwnd, uint msg, nint wParam, nint lParam);
	[DllImport("user32.dll")]
	public static extern int SetWindowCompositionAttribute(nint handle, ref WindowCompositionAttributeData data);
	[DllImport("dwmapi.dll", PreserveSig = true)]
	public static extern int DwmSetWindowAttribute(nint hwnd, uint attribute, ref uint attributeValue, int attributeSize);

	[StructLayout(LayoutKind.Sequential)]
	public struct WindowCompositionAttributeData
	{
		public int Attribute;
		public nint Data;
		public int SizeOfData;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct AccentPolicy
	{
		public int AccentState;
		public int AccentFlags;
		public int GradientColor;
		public int AnimationId;
	}
}