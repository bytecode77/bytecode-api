using BytecodeApi.Extensions;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace BytecodeApi.Threading
{
	/// <summary>
	/// Provides support for creating <see cref="Thread" /> and other multi threading related objects.
	/// </summary>
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
		public static Thread StartThread(Action action, Action<Exception> exceptionHandler)
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
		public static Thread StartThread(Action action, Action<Exception> exceptionHandler, ThreadPriority priority)
		{
			Check.ArgumentNull(action, nameof(action));

			StackTrace stackTrace = new StackTrace();
			Thread thread = new Thread(() =>
			{
				try
				{
					action();
				}
				catch (ThreadAbortException)
				{
				}
				catch (Exception ex)
				{
					if (exceptionHandler == null)
					{
						throw;
					}
					else
					{
						ex.AppendStackTrace(stackTrace);
						exceptionHandler(ex);
					}
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
		/// <summary>
		/// Creates and starts a new <see cref="DispatcherTimer" /> with the specified <see cref="Action" /> and interval.
		/// </summary>
		/// <param name="action">The <see cref="Action" /> to be invoked by the new <see cref="DispatcherTimer" />.</param>
		/// <param name="interval">The interval in which <paramref name="action" /> is invoked.</param>
		/// <returns>
		/// The <see cref="DispatcherTimer" /> this method creates.
		/// </returns>
		public static DispatcherTimer StartDispatcherTimer(Action action, TimeSpan interval)
		{
			return StartDispatcherTimer(action, interval, false);
		}
		/// <summary>
		/// Creates and starts a new <see cref="DispatcherTimer" /> with the specified <see cref="Action" /> and interval.
		/// </summary>
		/// <param name="action">The <see cref="Action" /> to be invoked by the new <see cref="DispatcherTimer" />.</param>
		/// <param name="interval">The interval in which <paramref name="action" /> is invoked.</param>
		/// <param name="executeNow"><see langword="true" /> to execute <paramref name="action" /> immediately; <see langword="false" /> to execute it on the first interval.</param>
		/// <returns>
		/// The <see cref="DispatcherTimer" /> this method creates.
		/// </returns>
		public static DispatcherTimer StartDispatcherTimer(Action action, TimeSpan interval, bool executeNow)
		{
			return StartDispatcherTimer(action, interval, executeNow, false);
		}
		/// <summary>
		/// Creates and starts a new <see cref="DispatcherTimer" /> with the specified <see cref="Action" /> and interval.
		/// </summary>
		/// <param name="action">The <see cref="Action" /> to be invoked by the new <see cref="DispatcherTimer" />.</param>
		/// <param name="interval">The interval in which <paramref name="action" /> is invoked.</param>
		/// <param name="executeNow"><see langword="true" /> to execute <paramref name="action" /> immediately; <see langword="false" /> to execute it on the first interval.</param>
		/// <param name="ignoreExceptions"><see langword="true" /> to ignore all exceptions.</param>
		/// <returns>
		/// The <see cref="DispatcherTimer" /> this method creates.
		/// </returns>
		public static DispatcherTimer StartDispatcherTimer(Action action, TimeSpan interval, bool executeNow, bool ignoreExceptions)
		{
			Check.ArgumentNull(action, nameof(action));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(interval, nameof(interval));

			DispatcherTimer timer = new DispatcherTimer { Interval = interval };
			timer.Tick += delegate { ExecuteAction(); };
			timer.Start();
			if (executeNow) ExecuteAction();
			return timer;

			void ExecuteAction() => CSharp.Try(action, !ignoreExceptions);
		}
		/// <summary>
		/// Creates a new <see cref="Stopwatch" /> and starts counting.
		/// </summary>
		/// <returns>
		/// The <see cref="Stopwatch" /> this method creates.
		/// </returns>
		public static Stopwatch StartStopwatch()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}
	}
}