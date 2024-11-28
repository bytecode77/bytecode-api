using System.Media;
using System.Runtime.InteropServices;
using System.Windows;

namespace BytecodeApi.Win32;

/// <summary>
/// Provides a set of <see langword="static" /> methods for Windows desktop interaction.
/// </summary>
public static class Desktop
{
	/// <summary>
	/// Gets the screen DPI. A value of 96 corresponds to 100% font scaling.
	/// </summary>
	public static Point Dpi
	{
		get
		{
			using System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHwnd(0);
			nint desktop = graphics.GetHdc();
			return new(Native.GetDeviceCaps(desktop, 88), Native.GetDeviceCaps(desktop, 90));
		}
	}
	/// <summary>
	/// Gets the current mouse position in screen coordinates.
	/// </summary>
	public static Point MousePosition
	{
		get
		{
			Point dpi = Dpi;

			return new(
				System.Windows.Forms.Control.MousePosition.X * 96.0 / dpi.X,
				System.Windows.Forms.Control.MousePosition.Y * 96.0 / dpi.Y);
		}
	}
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether the left mouse button is pressed.
	/// </summary>
	public static bool IsLeftMouseButtonPressed => Native.GetAsyncKeyState(1) != 0;
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether the right mouse button is pressed.
	/// </summary>
	public static bool IsRightMouseButtonPressed => Native.GetAsyncKeyState(2) != 0;
	/// <summary>
	/// Gets a <see cref="Rect" />[] that represent the bounds of all screens.
	/// </summary>
	public static Rect[] Screens
	{
		get
		{
			Point dpi = Dpi;

			return System.Windows.Forms.Screen.AllScreens
				.Select(screen => new Rect(
					screen.Bounds.X * 96.0 / dpi.X,
					screen.Bounds.Y * 96.0 / dpi.Y,
					screen.Bounds.Width * 96.0 / dpi.X,
					screen.Bounds.Height * 96.0 / dpi.Y))
				.ToArray();
		}
	}
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether the workstation is locked.
	/// </summary>
	public static bool IsWorkstationLocked
	{
		get
		{
			nint desktop = 0;

			try
			{
				desktop = Native.OpenInputDesktop(0, false, 256);
				if (desktop == 0)
				{
					desktop = Native.OpenDesktop("Default", 0, false, 256);
				}

				return desktop != 0 ? !Native.SwitchDesktop(desktop) : throw Throw.Win32();
			}
			finally
			{
				if (desktop != 0) Native.CloseDesktop(desktop);
			}
		}
	}
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether the screensaver is running.
	/// </summary>
	public static bool IsScreensaverRunning
	{
		get
		{
			bool running = false;
			return Native.SystemParametersInfo(114, 0, ref running, 0) ? running : throw Throw.Win32();
		}
	}

	/// <summary>
	/// Plays the <see cref="SystemSounds.Beep" /> sound.
	/// </summary>
	public static void Beep()
	{
		Beep(true);
	}
	/// <summary>
	/// Plays the <see cref="SystemSounds.Beep" /> or the <see cref="SystemSounds.Hand" /> sound, depending on the <paramref name="success" /> parameter.
	/// </summary>
	/// <param name="success"><see langword="true" /> to play <see cref="SystemSounds.Beep" />; <see langword="false" /> to play <see cref="SystemSounds.Hand" />.</param>
	public static void Beep(bool success)
	{
		(success ? SystemSounds.Beep : SystemSounds.Hand).Play();
	}
	/// <summary>
	/// Captures the entire virutal screen and returns a <see cref="System.Drawing.Bitmap" /> with the image.
	/// </summary>
	/// <param name="allScreens"><see langword="true" /> to capture all screens, <see langword="false" /> to only capture the primary screen.</param>
	/// <returns>
	/// A <see cref="System.Drawing.Bitmap" /> with the image of the captured screen.
	/// </returns>
	public static System.Drawing.Bitmap CaptureScreen(bool allScreens)
	{
		Point dpi = Dpi;
		int left = allScreens ? (int)(SystemParameters.VirtualScreenLeft * dpi.X / 96.0) : 0;
		int top = allScreens ? (int)(SystemParameters.VirtualScreenTop * dpi.Y / 96.0) : 0;
		int width = (int)((allScreens ? SystemParameters.VirtualScreenWidth : SystemParameters.PrimaryScreenWidth) * dpi.X / 96.0);
		int height = (int)((allScreens ? SystemParameters.VirtualScreenHeight : SystemParameters.PrimaryScreenHeight) * dpi.Y / 96.0);

		System.Drawing.Bitmap bitmap = new(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
		using System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
		graphics.CopyFromScreen(left, top, 0, 0, bitmap.Size);

		return bitmap;
	}
	/// <summary>
	/// Locks the workstation.
	/// </summary>
	public static void LockWorkstation()
	{
		Native.LockWorkStation();
	}
	/// <summary>
	/// Turns on the Windows screensaver using a HWND broadcast message, if a screensaver is configured.
	/// </summary>
	public static void TurnOnScreenSaver()
	{
		Native.SendMessage(0xffff, 0x112, 0xf140, 0);
	}
	/// <summary>
	/// Changes the Windows wallpaper to an image from the specified file and throws an exception, if changing the wallpaper failed.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path to an image file.</param>
	public static void ChangeWallpaper(string path)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);

		if (Native.SystemParametersInfo(20, 0, path, 1) != 1)
		{
			throw Throw.Win32();
		}
	}
}

file static class Native
{
	[DllImport("user32.dll", EntryPoint = "OpenDesktopA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	public static extern nint OpenDesktop(string desktop, int flags, bool inherit, uint desiredAccess);
	[DllImport("user32", SetLastError = true)]
	public static extern nint OpenInputDesktop(uint flags, bool inherit, uint desiredAccess);
	[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	public static extern nint CloseDesktop(nint desktop);
	[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SwitchDesktop(nint desktop);
	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern int SystemParametersInfo(int action, int param1, string param2, int winIni);
	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SystemParametersInfo(uint action, uint param1, ref bool param2, int winIni);
	[DllImport("user32.dll")]
	public static extern void LockWorkStation();
	[DllImport("user32.dll")]
	public static extern nint SendMessage(nint handle, uint msg, int wParam, int lParam);
	[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
	public static extern int GetDeviceCaps(nint dc, int index);
	[DllImport("user32.dll")]
	public static extern short GetAsyncKeyState(int vKey);
}