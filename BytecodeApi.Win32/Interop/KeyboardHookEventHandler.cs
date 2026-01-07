namespace BytecodeApi.Win32.Interop;

/// <summary>
/// Represents the method that is called when a keystroke was captured by a <see cref="GlobalKeyboardHook" />.
/// </summary>
/// <param name="sender">The <see cref="GlobalKeyboardHook" /> object that raised the event.</param>
/// <param name="e">A <see cref="KeyboardHookEventArgs" /> that contains the event data.</param>
public delegate void KeyboardHookEventHandler(object sender, KeyboardHookEventArgs e);