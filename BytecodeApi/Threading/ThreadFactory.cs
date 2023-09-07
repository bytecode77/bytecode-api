using BytecodeApi.Extensions;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace BytecodeApi.Threading;

/// <summary>
/// Provides support for creating <see cref="Thread" /> objects.
/// </summary>
[SupportedOSPlatform("windows")]
public static class ThreadFactory
{
	/// <summary>
	/// Creates a new background <see cref="Thread" /> with the STA apartment state and starts it. <see cref="ThreadAbortException" /> exceptions are swallowed.
	/// </summary>
	/// <param name="action">The <see cref="Action" /> to be invoked from the new <see cref="Thread" />.</param>
	/// <returns>
	/// The <see cref="Thread" /> this method creates.
	/// </returns>
	public static Thread StartThread(Action action)
	{
		return StartThread(action, null);
	}
	/// <summary>
	/// Creates a new background <see cref="Thread" /> with the STA apartment state and starts it. <see cref="ThreadAbortException" /> exceptions are swallowed.
	/// </summary>
	/// <param name="action">The <see cref="Action" /> to be invoked from the new <see cref="Thread" />.</param>
	/// <param name="exceptionHandler">An <see cref="Action{T}" /> that is called by the exception handler. If <see langword="null" />, the exception is rethrown. Use <see langword="delegate" /> { } to swallow exceptions. The stack trace prior to thread creation will be appended.</param>
	/// <returns>
	/// The <see cref="Thread" /> this method creates.
	/// </returns>
	public static Thread StartThread(Action action, Action<Exception>? exceptionHandler)
	{
		return StartThread(action, exceptionHandler, ThreadPriority.Normal);
	}
	/// <summary>
	/// Creates a new background <see cref="Thread" /> with the STA apartment state and starts it. <see cref="ThreadAbortException" /> exceptions are swallowed.
	/// </summary>
	/// <param name="action">The <see cref="Action" /> to be invoked from the new <see cref="Thread" />.</param>
	/// <param name="exceptionHandler">An <see cref="Action{T}" /> that is called by the exception handler. If <see langword="null" />, the exception is rethrown. Use <see langword="delegate" /> { } to swallow exceptions. The stack trace prior to thread creation will be appended.</param>
	/// <param name="priority">The <see cref="ThreadPriority" /> for the new <see cref="Thread" />.</param>
	/// <returns>
	/// The <see cref="Thread" /> this method creates.
	/// </returns>
	public static Thread StartThread(Action action, Action<Exception>? exceptionHandler, ThreadPriority priority)
	{
		Check.ArgumentNull(action);

		StackTrace stackTrace = new();
		Thread thread = new(() =>
		{
			try
			{
				action();
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex) when (exceptionHandler != null)
			{
				ex.AppendStackTrace(stackTrace);
				exceptionHandler(ex);
			}
		})
		{
			IsBackground = true,
			Priority = priority
		};

		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
		return thread;
	}
}