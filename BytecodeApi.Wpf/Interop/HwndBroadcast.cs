using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace BytecodeApi.Wpf.Interop;

/// <summary>
/// Class to send and receive HWND broadcast window messages from/to other processes.
/// </summary>
public class HwndBroadcast : IDisposable
{
	private readonly uint WindowMessage;
	private int SendCount;
	private HwndSource? HwndSource;
	private bool Disposed;
	/// <summary>
	/// Occurs when <see cref="Notify" /> is called by another running instance using the same name.
	/// </summary>
	public event EventHandler? Notified;

	/// <summary>
	/// Initializes a new instance of the <see cref="HwndBroadcast" /> class with the specified name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> representing the name that identifies the broadcast window messages.</param>
	public HwndBroadcast(string name)
	{
		Check.ArgumentNull(name);
		Check.ArgumentEx.StringNotEmpty(name);

		WindowMessage = Native.RegisterWindowMessage("BAPI_HWND_BROADCAST_" + name);
	}
	/// <summary>
	/// Releases all resources used by the current instance of the <see cref="HwndBroadcast" /> class.
	/// </summary>
	public void Dispose()
	{
		if (!Disposed)
		{
			HwndSource?.RemoveHook(WndProc);
			HwndSource?.Dispose();
			HwndSource = null;

			Disposed = true;
		}
	}

	/// <summary>
	/// Registers a <see cref="Window" /> object that identifies as the main application window.
	/// </summary>
	/// <param name="window">The <see cref="Window" /> object identifying as the main application window.</param>
	public void RegisterWindow(Window window)
	{
		Check.ObjectDisposed<HwndBroadcast>(Disposed);
		Check.ArgumentNull(window);

		RegisterWindow(new WindowInteropHelper(window).EnsureHandle());
	}
	/// <summary>
	/// Registers a window handle (HWND) that identifies as the main application window.
	/// </summary>
	/// <param name="handle">A <see cref="nint" /> representing window handle (HWND).</param>
	public void RegisterWindow(nint handle)
	{
		Check.ObjectDisposed<HwndBroadcast>(Disposed);
		Check.ArgumentEx.Handle(handle);

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
		Check.ObjectDisposed<HwndBroadcast>(Disposed);

		SendCount++;
		Native.SendNotifyMessage(-1, WindowMessage, 0, 0);
	}
	private nint WndProc(nint hwnd, int msg, nint wParam, nint lParam, ref bool handled)
	{
		if (msg == WindowMessage && SendCount-- <= 0)
		{
			SendCount = 0;
			OnNotified(EventArgs.Empty);
		}

		return 0;
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

file static class Native
{
	[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	public static extern uint RegisterWindowMessage(string str);
	[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	public static extern bool SendNotifyMessage(nint handle, uint msg, int wParam, int lParam);
}