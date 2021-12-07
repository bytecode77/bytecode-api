using BytecodeApi.Extensions;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BytecodeApi.IO.Interop
{
	/// <summary>
	/// Provides support for global keystrokes capturing.
	/// </summary>
	public sealed class GlobalKeyboardHook : IDisposable
	{
		private IntPtr User32Library;
		private IntPtr Hook;
		private Native.HookProc HookProcedure;
		/// <summary>
		/// Occurs when a key is pressed. Keystrokes are captured globally using SetWindowsHookEx.
		/// </summary>
		public event KeyboardHookEventHandler KeyPressed;

		/// <summary>
		/// Initializes a new instance of the <see cref="GlobalKeyboardHook" /> class and starts capturing.
		/// </summary>
		public GlobalKeyboardHook()
		{
			User32Library = Native.LoadLibrary("user32.dll");
			HookProcedure = KeyboardHookProc;
			Hook = Native.SetWindowsHookEx(13, HookProcedure, User32Library, 0);
		}
		/// <summary>
		/// Releases all resources used by the current instance of the <see cref="GlobalKeyboardHook" /> class.
		/// </summary>
		public void Dispose()
		{
			if (Hook != IntPtr.Zero)
			{
				Native.UnhookWindowsHookEx(Hook);
				Hook = IntPtr.Zero;
				HookProcedure -= KeyboardHookProc;
			}

			if (User32Library != IntPtr.Zero)
			{
				Native.FreeLibrary(User32Library);
				User32Library = IntPtr.Zero;
			}
		}

		private IntPtr KeyboardHookProc(int code, IntPtr wParam, IntPtr lParam)
		{
			//TODO: Bug: Doesn't capture sticky keys correctly (^^, ´´, ``) and dead keys / AltGr combinations (i.e. []{}@)
			bool handled = false;

			if (code >= 0 && wParam == (IntPtr)0x100)
			{
				Native.KeyboardInput input = Marshal.PtrToStructure<Native.KeyboardInput>(lParam);
				bool isShift = (Native.GetKeyState(0x10) & 0x80) == 0x80;
				bool isCapslock = Native.GetKeyState(0x14) != 0;

				byte[] keyState = new byte[256];
				Native.GetKeyboardState(keyState);

				char key;
				byte[] translatedKey = new byte[4];
				switch (Native.ToAscii(input.KeyCode, input.ScanCode, keyState, translatedKey, input.Flags))
				{
					case 1:
						key = BitConverter.ToChar(translatedKey, 0);
						if (isCapslock ^ isShift && key.IsLetter()) key = key.ToUpper();
						break;
					case 2:
						key = BitConverter.ToChar(translatedKey, 1);
						if (isCapslock ^ isShift && key.IsLetter()) key = key.ToUpper();
						break;
					default:
						key = '\0';
						break;
				}

				KeyboardHookEventArgs e = new KeyboardHookEventArgs((Keys)input.KeyCode, input.ScanCode, key);
				KeyPressed?.Invoke(this, e);
				handled |= e.Handled;
			}

			return handled ? (IntPtr)1 : Native.CallNextHookEx(Hook, code, wParam, lParam);
		}
	}
}