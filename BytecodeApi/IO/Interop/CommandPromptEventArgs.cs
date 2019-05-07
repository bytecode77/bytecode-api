using System;

namespace BytecodeApi.IO.Interop
{
	/// <summary>
	/// Provides data for <see cref="CommandPrompt" /> events.
	/// </summary>
	public sealed class CommandPromptEventArgs : EventArgs
	{
		/// <summary>
		/// Gets a value indicating whether the message was read from the standard error stream or the standard output stream.
		/// </summary>
		public bool IsError { get; private set; }
		/// <summary>
		/// Gets a <see cref="string" /> with the message that was read from the output stream.
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandPromptEventArgs" /> class using the specified message.
		/// </summary>
		/// <param name="isError"><see langword="true" />, if the message was read from the standard error stream; <see langword="false" />, if it was read from the standard output stream.</param>
		/// <param name="message">A <see cref="string" /> with the message that was read from the output stream.</param>
		public CommandPromptEventArgs(bool isError, string message)
		{
			IsError = isError;
			Message = message;
		}
	}
}