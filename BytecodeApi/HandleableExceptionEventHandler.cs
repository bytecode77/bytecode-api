namespace BytecodeApi;

/// <summary>
/// Represents the method that is called when an unhandled exception occurs that can be handled by user code.
/// </summary>
/// <param name="sender">The object that raised the event.</param>
/// <param name="e">A <see cref="HandleableExceptionEventArgs" /> that contains the event data.</param>
public delegate void HandleableExceptionEventHandler(object sender, HandleableExceptionEventArgs e);