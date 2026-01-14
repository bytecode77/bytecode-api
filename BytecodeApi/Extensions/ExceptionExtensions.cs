using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Exception" /> objects.
/// </summary>
public static class ExceptionExtensions
{
	extension(Exception exception)
	{
		/// <summary>
		/// Gets a <see cref="string" /> representing the complete stack trace including all inner exceptions for this <see cref="Exception" /> object.
		/// </summary>
		public string FullStackTrace
		{
			get
			{
				Check.ArgumentNull(exception);

				StringBuilder stackTrace = new();
				Exception? ex = exception;

				do
				{
					stackTrace.AppendLine($"{ex.GetType()}: {ex.Message}");
					stackTrace.AppendLine(ex.StackTrace);
					stackTrace.AppendLine();
				}
				while ((ex = ex.InnerException) != null);

				return stackTrace.ToString().TrimEnd();
			}
		}
		/// <summary>
		/// Gets the number of cascaded exceptions (inner exceptions) for this <see cref="Exception" /> object, including this <see cref="Exception" />.
		/// </summary>
		public int ChildExceptionCount
		{
			get
			{
				Check.ArgumentNull(exception);

				int count = 0;
				Exception? ex = exception;

				do
				{
					count++;
				}
				while ((ex = ex.InnerException) != null);

				return count;
			}
		}

		/// <summary>
		/// Tries to find an inner exception of this <see cref="Exception" /> that matches the specified type.
		/// </summary>
		/// <typeparam name="T">The explicit type of the inner exception to search for.</typeparam>
		/// <returns>
		/// The closest first inner exception of this <see cref="Exception" /> that matches the specified type, if found;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public T? FindInnerException<T>() where T : Exception
		{
			Check.ArgumentNull(exception);

			Exception? ex = exception;

			do
			{
				if (ex is T innerException)
				{
					return innerException;
				}
			}
			while ((ex = ex.InnerException) != null);

			return null;
		}
		/// <summary>
		/// Appends the specified <see cref="StackTrace" /> to this <see cref="Exception" />.
		/// </summary>
		/// <param name="stackTrace">The <see cref="StackTrace" /> to append to this <see cref="Exception" />.</param>
		public void AppendStackTrace(StackTrace stackTrace)
		{
			exception.AppendStackTrace(stackTrace.ToString());
		}
		/// <summary>
		/// Appends the specified stack trace to this <see cref="Exception" />.
		/// </summary>
		/// <param name="stackTrace">The stack trace to append to this <see cref="Exception" />.</param>
		public void AppendStackTrace(string stackTrace)
		{
			Check.ArgumentNull(exception);
			Check.ArgumentNull(stackTrace);

			if (typeof(Exception).GetField("_stackTraceString", BindingFlags.NonPublic | BindingFlags.Instance) is FieldInfo field)
			{
				field.SetValue(exception, $"{exception.StackTrace}\r\n{stackTrace}");
			}
			else
			{
				throw Throw.InvalidOperation("Field '_stackTraceString' not found.");
			}
		}
	}
}