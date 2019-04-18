using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace BytecodeApi.UI
{
	/// <summary>
	/// Class for registration of system-wide hotkeys in UI applications.
	/// </summary>
	public class HotKey : IDisposable
	{
		private readonly int HotKeyId;
		private IntPtr Handle;
		/// <summary>
		/// Get the key associated with this hotkey.
		/// </summary>
		public Key Key { get; private set; }
		/// <summary>
		/// Get the modifier keys associated with this hotkey.
		/// </summary>
		public ModifierKeys Modifiers { get; private set; }
		/// <summary>
		/// Occurs when the hotkey was pressed.
		/// </summary>
		public event EventHandler<HotKeyEventArgs> Pressed;

		/// <summary>
		/// Initializes a new instance of the <see cref="HotKey" /> class with the specified key and modifier.
		/// </summary>
		/// <param name="key">The key associated with this hotkey.</param>
		/// <param name="modifiers">The modifier keys associated with this hotkey.</param>
		public HotKey(Key key, ModifierKeys modifiers)
		{
			Check.Argument(key != Key.None, nameof(modifiers), "Key not defined.");
			Check.Argument(modifiers != ModifierKeys.None, nameof(modifiers), "Modifier keys not defined.");

			Key = key;
			Modifiers = modifiers;
			HotKeyId = GetHashCode();
		}
		/// <summary>
		/// Releases all resources used by the current instance of the <see cref="HotKey" /> class.
		/// </summary>
		public void Dispose()
		{
			if (Handle != IntPtr.Zero)
			{
				ComponentDispatcher.ThreadPreprocessMessage -= ComponentDispatcher_ThreadPreprocessMessage;
				Native.UnregisterHotKey(Handle, HotKeyId);
				Handle = IntPtr.Zero;
			}
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
			Check.Argument(handle != IntPtr.Zero && handle != (IntPtr)(-1), nameof(handle), "Invalid handle.");

			if (Handle == IntPtr.Zero)
			{
				Handle = handle;
				if (!Native.RegisterHotKey(Handle, HotKeyId, Modifiers, (System.Windows.Forms.Keys)KeyInterop.VirtualKeyFromKey(Key))) throw Throw.Win32("Hotkey already in use.");
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
				OnPressed(new HotKeyEventArgs(Key, Modifiers));
				handled = true;
			}
		}

		/// <summary>
		/// Raises the <see cref="Pressed" /> event.
		/// </summary>
		/// <param name="e">The event data for the <see cref="Pressed" /> event.</param>
		protected void OnPressed(HotKeyEventArgs e)
		{
			Pressed?.Invoke(this, e);
		}
	}
}