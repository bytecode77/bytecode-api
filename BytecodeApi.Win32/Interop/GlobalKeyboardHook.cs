using BytecodeApi.Extensions;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace BytecodeApi.Win32.Interop;

/// <summary>
/// Provides support for global keystrokes capturing.
/// </summary>
public sealed class GlobalKeyboardHook : IDisposable
{
	private nint User32Library;
	private nint Hook;
	private HookProc? HookProcedure;
	private bool Disposed;
	/// <summary>
	/// Occurs when a key is pressed. Keystrokes are captured globally using SetWindowsHookEx.
	/// </summary>
	public event KeyboardHookEventHandler? KeyPressed;

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
	/// Cleans up Windows resources for this <see cref="GlobalKeyboardHook" />.
	/// </summary>
	~GlobalKeyboardHook()
	{
		Dispose(false);
	}
	/// <summary>
	/// Releases all resources used by the current instance of the <see cref="GlobalKeyboardHook" /> class.
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
	private void Dispose(bool disposing)
	{
		if (!Disposed)
		{
			if (disposing)
			{
				HookProcedure -= KeyboardHookProc;
			}

			Native.UnhookWindowsHookEx(Hook);
			Hook = 0;

			Native.FreeLibrary(User32Library);
			User32Library = 0;

			Disposed = true;
		}
	}

	private nint KeyboardHookProc(int code, nint wParam, nint lParam)
	{
		//TODO: Bug: Doesn't capture sticky keys correctly (^^, ´´, ``) and dead keys / AltGr combinations (i.e. []{}@)
		bool handled = false;

		if (code >= 0 && wParam == 0x100)
		{
			Native.KeyboardInput input = Marshal.PtrToStructure<Native.KeyboardInput>(lParam);
			bool isShift = (Native.GetKeyState(0x10) & 0x80) == 0x80;
			bool isCapslock = Native.GetKeyState(0x14) != 0;

			byte[] keyState = new byte[256];
			Native.GetKeyboardState(keyState);

			byte[] translatedKey = new byte[4];
			char key = Native.ToAscii(input.KeyCode, input.ScanCode, keyState, translatedKey, input.Flags) switch
			{
				1 => BitConverter.ToChar(translatedKey, 0),
				2 => BitConverter.ToChar(translatedKey, 1),
				_ => '\0'
			};

			if (isCapslock ^ isShift && key.IsLetter())
			{
				key = key.ToUpper();
			}

			KeyboardHookEventArgs e = new((Keys)input.KeyCode, input.ScanCode, key);
			KeyPressed?.Invoke(this, e);
			handled |= e.Handled;
		}

		return handled ? 1 : Native.CallNextHookEx(Hook, code, wParam, lParam);
	}
}

file static class Native
{
	[StructLayout(LayoutKind.Sequential)]
	public struct KeyboardInput
	{
		public int KeyCode;
		public int ScanCode;
		public int Flags;
		public int TimeStamp;
		public nint AdditionalInformation;
	}

	[DllImport("kernel32.dll")]
	public static extern nint LoadLibrary(string path);
	[DllImport("kernel32.dll", SetLastError = true)]
	[SuppressUnmanagedCodeSecurity]
	public static extern bool FreeLibrary(nint module);
	[DllImport("user32.dll", SetLastError = true)]
	public static extern nint SetWindowsHookEx(int hookId, HookProc callback, nint module, int threadId);
	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool UnhookWindowsHookEx(nint hook);
	[DllImport("user32.dll", SetLastError = true)]
	public static extern nint CallNextHookEx(nint hook, int code, nint wParam, nint lParam);
	[DllImport("user32.dll")]
	public static extern short GetKeyState(int virtualKey);
	[DllImport("user32.dll")]
	public static extern int GetKeyboardState(byte[] keyState);
	[DllImport("user32.dll")]
	public static extern int ToAscii(int virtualKey, int scanCode, byte[] keyState, byte[] translatedKey, int state);
}