namespace BytecodeApi.Win32.Interop;

/// <summary>
/// Provides data for <see cref="CommandPrompt" /> events.
/// </summary>
public sealed class CommandPromptEventArgs : EventArgs
{
	/// <summary>
	/// Gets a value indicating whether the message was read from the standard error stream or the standard output stream.
	/// </summary>
	public bool IsError { get; private init; }
	/// <summary>
	/// Gets a <see cref="string" /> with the message that was read from the output stream.
	/// </summary>
	public string Message { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="CommandPromptEventArgs" /> class using the specified message.
	/// </summary>
	/// <param name="isError"><see langword="true" />, if the message was read from the standard error stream; <see langword="false" />, if it was read from the standard output stream.</param>
	/// <param name="message">A <see cref="string" /> with the message that was read from the output stream.</param>
	public CommandPromptEventArgs(bool isError, string message)
	{
		Check.ArgumentNull(message);

		IsError = isError;
		Message = message;
	}
}