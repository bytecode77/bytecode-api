using System;
using System.Windows.Forms;

namespace BytecodeApi.IO.Interop
{
	/// <summary>
	/// Provides data for the <see cref="GlobalKeyboardHook.KeyPressed" /> event.
	/// </summary>
	public sealed class KeyboardHookEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the keycode of the pressed key.
		/// </summary>
		public Keys KeyCode { get; private set; }
		/// <summary>
		/// Gets the scancode of the pressed key.
		/// </summary>
		public int ScanCode { get; private set; }
		/// <summary>
		/// Gets the key character, or '\0', if the key does not represent a printable character.
		/// </summary>
		public char KeyChar { get; private set; }
		/// <summary>
		/// Gets or sets a <see cref="bool" /> value indicating whether to cancel the keyboard event.
		/// </summary>
		public bool Handled { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyboardHookEventArgs" /> class.
		/// </summary>
		/// <param name="keyCode">The keycode of the pressed key.</param>
		/// <param name="scanCode">The scancode of the pressed key.</param>
		/// <param name="keyChar">The key character, or '\0', if the key does not represent a printable character.</param>
		public KeyboardHookEventArgs(Keys keyCode, int scanCode, char keyChar)
		{
			KeyCode = keyCode;
			ScanCode = scanCode;
			KeyChar = keyChar;
		}
	}
}