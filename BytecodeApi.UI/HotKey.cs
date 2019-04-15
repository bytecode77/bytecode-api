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
		private readonly IntPtr Handle;
		private readonly int HotKeyId;
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
		/// Initializes a new instance of the <see cref="HotKey" /> class with the specified hotkey a window handle.
		/// </summary>
		/// <param name="key">The key associated with this hotkey.</param>
		/// <param name="modifiers">The modifier keys associated with this hotkey.</param>
		/// <param name="handle">A <see cref="IntPtr" /> representing window handle (HWND).</param>
		public HotKey(Key key, ModifierKeys modifiers, IntPtr handle)
		{
			Check.Argument(key != Key.None, nameof(modifiers), "Key not defined.");
			Check.Argument(modifiers != ModifierKeys.None, nameof(modifiers), "Modifier keys not defined.");
			Check.Argument(handle != IntPtr.Zero && handle != (IntPtr)(-1), nameof(handle), "Invalid handle.");

			Key = key;
			Modifiers = modifiers;
			HotKeyId = GetHashCode();
			Handle = handle;

			if (!Native.RegisterHotKey(Handle, HotKeyId, Modifiers, (System.Windows.Forms.Keys)KeyInterop.VirtualKeyFromKey(Key))) throw Throw.Win32("Hotkey already in use.");
			ComponentDispatcher.ThreadPreprocessMessage += ComponentDispatcher_ThreadPreprocessMessage;
		}
		/// <summary>
		/// Releases all resources used by the current instance of the <see cref="HotKey" /> class.
		/// </summary>
		public void Dispose()
		{
			ComponentDispatcher.ThreadPreprocessMessage -= ComponentDispatcher_ThreadPreprocessMessage;
			Native.UnregisterHotKey(Handle, HotKeyId);
		}
		/// <summary>
		/// Creates a <see cref="HotKey" /> from the specified hotkey and a <see cref="Window" />.
		/// </summary>
		/// <param name="key">The key associated with this hotkey.</param>
		/// <param name="modifiers">The modifier keys associated with this hotkey.</param>
		/// <param name="window">The <see cref="Window" /> object this hotkey is created for.</param>
		/// <returns>
		/// The <see cref="HotKey" /> this method creates.
		/// </returns>
		public static HotKey Create(Key key, ModifierKeys modifiers, Window window)
		{
			Check.Argument(key != Key.None, nameof(modifiers), "Key not defined.");
			Check.Argument(modifiers != ModifierKeys.None, nameof(modifiers), "Modifier keys not defined.");
			Check.ArgumentNull(window, nameof(window));

			return new HotKey(key, modifiers, new WindowInteropHelper(window).EnsureHandle());
		}

		private void ComponentDispatcher_ThreadPreprocessMessage(ref MSG msg, ref bool handled)
		{
			if (!handled && msg.message == 0x312 && (int)msg.wParam == HotKeyId)
			{
				OnPressed();
				handled = true;
			}
		}

		/// <summary>
		/// Raises the <see cref="Pressed" /> event.
		/// </summary>
		protected void OnPressed()
		{
			Pressed?.Invoke(this, new HotKeyEventArgs(Key, Modifiers));
		}
	}
}