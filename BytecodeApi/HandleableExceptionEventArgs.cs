using System.ComponentModel;

namespace BytecodeApi;

/// <summary>
/// Provides data for events that are raised when an unhandled exception occurs that can be handled by user code.
/// </summary>
public sealed class HandleableExceptionEventArgs : HandledEventArgs
{
	/// <summary>
	/// Gets the unhandled exception that was raised.
	/// </summary>
	public Exception Exception { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="HandleableExceptionEventArgs" /> class.
	/// </summary>
	/// <param name="exception">The unhandled exception that was raised.</param>
	public HandleableExceptionEventArgs(Exception exception)
	{
		Check.ArgumentNull(exception);

		Exception = exception;
	}
}