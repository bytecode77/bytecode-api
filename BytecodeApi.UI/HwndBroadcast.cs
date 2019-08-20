using System;
using System.Windows;
using System.Windows.Interop;

namespace BytecodeApi.UI
{
	/// <summary>
	/// Class to send and receive HWND broadcast window messages from/to other processes.
	/// </summary>
	public class HwndBroadcast : IDisposable
	{
		private readonly uint WindowMessage;
		private int SendCount;
		private HwndSource HwndSource;
		/// <summary>
		/// Occurs when <see cref="Notify" /> is called by another running instance using the same name.
		/// </summary>
		public event EventHandler Notified;

		/// <summary>
		/// Initializes a new instance of the <see cref="HwndBroadcast" /> class with the specified name.
		/// </summary>
		/// <param name="name">A <see cref="string" /> representing the name that identifies the broadcast window messages.</param>
		public HwndBroadcast(string name)
		{
			Check.ArgumentNull(name, nameof(name));
			Check.ArgumentEx.StringNotEmpty(name, nameof(name));

			WindowMessage = Native.RegisterWindowMessage("BAPI_HWND_BROADCAST_" + name);
		}
		/// <summary>
		/// Releases all resources used by the current instance of the <see cref="HwndBroadcast" /> class.
		/// </summary>
		public void Dispose()
		{
			HwndSource?.Dispose();
		}

		/// <summary>
		/// Registers a <see cref="Window" /> object that identifies as the main application window.
		/// </summary>
		/// <param name="window">The <see cref="Window" /> object identifying as the main application window.</param>
		public void RegisterWindow(Window window)
		{
			Check.ArgumentNull(window, nameof(window));

			RegisterWindow(new WindowInteropHelper(window).EnsureHandle());
		}
		/// <summary>
		/// Registers a window handle (HWND) that identifies as the main application window.
		/// </summary>
		/// <param name="handle">A <see cref="IntPtr" /> representing window handle (HWND).</param>
		public void RegisterWindow(IntPtr handle)
		{
			Check.ArgumentEx.Handle(handle, nameof(handle));

			if (HwndSource == null)
			{
				HwndSource = HwndSource.FromHwnd(handle);
				HwndSource.AddHook(WndProc);
			}
			else
			{
				throw Throw.InvalidOperation("Window was already registered.");
			}
		}
		/// <summary>
		/// Sends a notification to other running instances. The <see cref="Notified" /> event will be triggered in all instances using the same name, except the current.
		/// </summary>
		public void Notify()
		{
			SendCount++;
			Native.SendNotifyMessage((IntPtr)(-1), WindowMessage, 0, 0);
		}
		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == WindowMessage && SendCount-- <= 0)
			{
				SendCount = 0;
				OnNotified(EventArgs.Empty);
			}

			return IntPtr.Zero;
		}

		/// <summary>
		/// Raises the <see cref="Notified" /> event.
		/// </summary>
		/// <param name="e">The event data for the <see cref="Notified" /> event.</param>
		protected virtual void OnNotified(EventArgs e)
		{
			Notified?.Invoke(this, e);
		}
	}
}