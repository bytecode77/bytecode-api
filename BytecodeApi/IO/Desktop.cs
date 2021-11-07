using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Media;
using System.Windows;

namespace BytecodeApi.IO
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for Windows desktop interaction.
	/// </summary>
	public static class Desktop
	{
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
		/// Captures the entire virutal screen and returns a <see cref="Bitmap" /> with the image.
		/// </summary>
		/// <param name="allScreens"><see langword="true" /> to capture all screens, <see langword="false" /> to only capture the primary screen.</param>
		/// <returns>
		/// A <see cref="Bitmap" /> with the image of the captured screen.
		/// </returns>
		public static Bitmap CaptureScreen(bool allScreens)
		{
			System.Drawing.Size dpi = ApplicationBase.Session.Dpi;
			int left = allScreens ? (int)(SystemParameters.VirtualScreenLeft * dpi.Width / 96) : 0;
			int top = allScreens ? (int)(SystemParameters.VirtualScreenTop * dpi.Height / 96) : 0;
			int width = (int)((allScreens ? SystemParameters.VirtualScreenWidth : SystemParameters.PrimaryScreenWidth) * dpi.Width / 96);
			int height = (int)((allScreens ? SystemParameters.VirtualScreenHeight : SystemParameters.PrimaryScreenHeight) * dpi.Height / 96);

			Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.CopyFromScreen(left, top, 0, 0, bitmap.Size);
			}

			return bitmap;
		}
		/// <summary>
		/// Turns on the Windows screensaver using a HWND broadcast message, if a screensaver is configured.
		/// </summary>
		public static void TurnOnScreenSaver()
		{
			Native.SendMessage((IntPtr)0xffff, 0x112, 0xf140, 0);
		}
		/// <summary>
		/// Locks the workstation.
		/// </summary>
		public static void LockWorkstation()
		{
			Native.LockWorkStation();
		}
		/// <summary>
		/// Changes the Windows wallpaper to an image from the specified file.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path to an image file.</param>
		/// <returns>
		/// <see langword="true" /> on success;
		/// otherwise, <see langword="false" />
		/// </returns>
		public static bool ChangeWallpaper(string path)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.FileNotFound(path);

			return Native.SystemParametersInfo(20, 0, path, 1) == 1;
		}
		/// <summary>
		/// Refreshes the system tray area and removes icons of processes that no longer run.
		/// </summary>
		public static void RefreshSystemTray()
		{
			//TODO: Bug: Only works if "always show all icons" option is enabled in Windows
			IntPtr systemTray = Native.FindWindowEx(Native.FindWindow("Shell_TrayWnd", null), IntPtr.Zero, "TrayNotifyWnd", null);
			IntPtr sysPager = Native.FindWindowEx(systemTray, IntPtr.Zero, "SysPager", null);
			IntPtr notificationArea = Native.FindWindowEx(sysPager, IntPtr.Zero, "ToolbarWindow32", null);

			if (notificationArea != IntPtr.Zero)
			{
				if (Native.GetClientRect(notificationArea, out Native.Rect rect))
				{
					for (int x = 0; x < rect.Right; x += 5)
					{
						for (int y = 0; y < rect.Bottom; y += 5)
						{
							Native.SendMessage(notificationArea, 0x0200, 0, (y << 16) + x);
						}
					}
				}
			}
		}
	}
}