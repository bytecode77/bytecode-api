using System;
using System.Windows.Input;

namespace BytecodeApi.UI
{
	/// <summary>
	/// Provides data for <see cref="HotKey" /> events.
	/// </summary>
	public class HotKeyEventArgs : EventArgs
	{
		/// <summary>
		/// Get the key associated with the hotkey event.
		/// </summary>
		public Key Key { get; private set; }
		/// <summary>
		/// Get the modifier keys associated with the hotkey event.
		/// </summary>
		public ModifierKeys Modifiers { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HotKeyEventArgs" /> class using the specified properties equivalent to the properties of <see cref="HotKey" />.
		/// </summary>
		/// <param name="key">The key associated with the hotkey event.</param>
		/// <param name="modifiers">The modifier keys associated with the hotkey event.</param>
		public HotKeyEventArgs(Key key, ModifierKeys modifiers)
		{
			Key = key;
			Modifiers = modifiers;
		}
	}
}