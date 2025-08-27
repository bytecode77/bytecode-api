using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Exception" /> objects.
/// </summary>
public static class ExceptionExtensions
{
	/// <summary>
	/// Gets a <see cref="string" /> representing the complete stack trace including all inner exceptions for this <see cref="Exception" /> object.
	/// </summary>
	/// <param name="exception">The <see cref="Exception" /> to evaluate.</param>
	/// <returns>
	/// The complete stack trace including all inner exceptions for this <see cref="Exception" /> object.
	/// </returns>
	public static string GetFullStackTrace(this Exception exception)
	{
		return exception.GetFullStackTrace(out _);
	}
	/// <summary>
	/// Gets a <see cref="string" /> representing the complete stack trace including all inner exceptions for this <see cref="Exception" /> object.
	/// </summary>
	/// <param name="exception">The <see cref="Exception" /> to evaluate.</param>
	/// <param name="count">When this method returns, a <see cref="int" /> value indicating the amount of cascaded exceptions (inner exceptions).</param>
	/// <returns>
	/// The complete stack trace including all inner exceptions for this <see cref="Exception" /> object.
	/// </returns>
	public static string GetFullStackTrace(this Exception exception, out int count)
	{
		Check.ArgumentNull(exception);

		count = 0;
		StringBuilder stackTrace = new();
		Exception? ex = exception;

		do
		{
			stackTrace.AppendLine($"{ex.GetType()}: {ex.Message}");
			stackTrace.AppendLine(ex.StackTrace);
			stackTrace.AppendLine();
			count++;
		}
		while ((ex = ex.InnerException) != null);

		return stackTrace.ToString().TrimEnd();
	}
	/// <summary>
	/// Tries to find an inner exception of this <see cref="Exception" /> that matches the specified type.
	/// </summary>
	/// <typeparam name="T">The explicit type of the inner exception to search for.</typeparam>
	/// <param name="exception">The <see cref="Exception" /> to traverse from.</param>
	/// <returns>
	/// The closest first inner exception of this <see cref="Exception" /> that matches the specified type, if found;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static T? FindInnerException<T>(this Exception exception) where T : Exception
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
	/// <param name="exception">The <see cref="Exception" /> to append <paramref name="stackTrace" /> to.</param>
	/// <param name="stackTrace">The <see cref="StackTrace" /> to append to <paramref name="exception" />.</param>
	public static void AppendStackTrace(this Exception exception, StackTrace stackTrace)
	{
		exception.AppendStackTrace(stackTrace.ToString());
	}
	/// <summary>
	/// Appends the specified stack trace to this <see cref="Exception" />.
	/// </summary>
	/// <param name="exception">The <see cref="Exception" /> to append <paramref name="stackTrace" /> to.</param>
	/// <param name="stackTrace">The stack trace to append to <paramref name="exception" />.</param>
	public static void AppendStackTrace(this Exception exception, string stackTrace)
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