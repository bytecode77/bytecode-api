using BytecodeApi.Wpf.Data;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

namespace BytecodeApi.Wpf.Interop;

/// <summary>
/// Class for registration of system-wide hotkeys in UI applications.
/// </summary>
public class HotKey : IDisposable
{
	private readonly int HotKeyId;
	private nint Handle;
	private bool Disposed;
	/// <summary>
	/// Gets the keyboard shortcut associated with this hotkey.
	/// </summary>
	public KeyboardShortcut Shortcut { get; private init; }
	/// <summary>
	/// Occurs when the hotkey was pressed.
	/// </summary>
	public event EventHandler<HotKeyEventArgs>? Pressed;

	/// <summary>
	/// Initializes a new instance of the <see cref="HotKey" /> class with the specified key and modifier.
	/// </summary>
	/// <param name="shortcut">The keyboard shortcut associated with this hotkey.</param>
	public HotKey(KeyboardShortcut shortcut)
	{
		Check.ArgumentNull(shortcut);
		Check.Argument(shortcut.Key != Key.None, nameof(shortcut), "Key not defined.");
		Check.Argument(shortcut.Modifiers != ModifierKeys.None, nameof(shortcut), "Modifier keys not defined.");

		Shortcut = shortcut;
		HotKeyId = GetHashCode();
	}
	/// <summary>
	/// Cleans up Windows resources for this <see cref="HotKey" />.
	/// </summary>
	~HotKey()
	{
		Dispose(false);
	}
	/// <summary>
	/// Releases all resources used by the current instance of the <see cref="HotKey" /> class.
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
	/// <summary>
	/// Releases all resources used by the current instance of the <see cref="HotKey" /> class.
	/// </summary>
	/// <param name="disposing"><see langword="true" />, if this method was called from <see cref="Dispose()" />; <see langword="false" />, if this method was called from the finalizer.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!Disposed)
		{
			if (disposing)
			{
				ComponentDispatcher.ThreadPreprocessMessage -= ComponentDispatcher_ThreadPreprocessMessage;
			}

			if (Handle != 0)
			{
				Native.UnregisterHotKey(Handle, HotKeyId);
				Handle = 0;
			}

			Disposed = true;
		}
	}

	/// <summary>
	/// Registers a <see cref="Window" /> object that identifies as the main application window.
	/// </summary>
	/// <param name="window">The <see cref="Window" /> object identifying as the main application window.</param>
	public void RegisterWindow(Window window)
	{
		Check.ObjectDisposed<HotKey>(Disposed);
		Check.ArgumentNull(window);

		RegisterWindow(new WindowInteropHelper(window).EnsureHandle());
	}
	/// <summary>
	/// Registers a window handle (HWND) that identifies as the main application window.
	/// </summary>
	/// <param name="handle">A <see cref="nint" /> representing window handle (HWND).</param>
	public void RegisterWindow(nint handle)
	{
		Check.ObjectDisposed<HotKey>(Disposed);
		Check.ArgumentEx.Handle(handle);

		if (Handle == 0)
		{
			Handle = handle;

			if (!Native.RegisterHotKey(Handle, HotKeyId, Shortcut.Modifiers, (Keys)KeyInterop.VirtualKeyFromKey(Shortcut.Key)))
			{
				throw Throw.Win32("Hotkey already in use.");
			}

			ComponentDispatcher.ThreadPreprocessMessage += ComponentDispatcher_ThreadPreprocessMessage;
		}
		else
		{
			throw Throw.InvalidOperation("Window was already registered.");
		}
	}
	private void ComponentDispatcher_ThreadPreprocessMessage(ref MSG msg, ref bool handled)
	{
		if (!handled && msg.message == 0x312 && (int)msg.wParam == HotKeyId)
		{
			OnPressed(new(Shortcut));
			handled = true;
		}
	}

	/// <summary>
	/// Raises the <see cref="Pressed" /> event.
	/// </summary>
	/// <param name="e">The event data for the <see cref="Pressed" /> event.</param>
	protected virtual void OnPressed(HotKeyEventArgs e)
	{
		Pressed?.Invoke(this, e);
	}
}

file static class Native
{
	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool RegisterHotKey(nint handle, int id, ModifierKeys modifierKeys, Keys key);
	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool UnregisterHotKey(nint handle, int id);
}