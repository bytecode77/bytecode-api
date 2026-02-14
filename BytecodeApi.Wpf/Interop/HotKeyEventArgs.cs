using BytecodeApi.Wpf.Data;

namespace BytecodeApi.Wpf.Interop;

/// <summary>
/// Provides data for <see cref="HotKey" /> events.
/// </summary>
public sealed class HotKeyEventArgs : EventArgs
{
	/// <summary>
	/// Gets the keyboard shortcut associated with the hotkey event.
	/// </summary>
	public KeyboardShortcut Shortcut { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="HotKeyEventArgs" /> class using the specified properties equivalent to the properties of <see cref="HotKey" />.
	/// </summary>
	/// <param name="shortcut">The keyboard shortcut associated with the hotkey event.</param>
	public HotKeyEventArgs(KeyboardShortcut shortcut)
	{
		Check.ArgumentNull(shortcut);

		Shortcut = shortcut;
	}
}