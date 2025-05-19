using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BytecodeApi;

internal static class Check
{
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Argument(bool condition, string? parameterName, string message)
	{
		if (!condition)
		{
			throw new ArgumentException(message, parameterName);
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ArgumentNull<T>([NotNull] T? parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null)
	{
		if (parameter == null)
		{
			throw new ArgumentNullException(parameterName);
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void InvalidOperation(bool condition, string message)
	{
		if (!condition)
		{
			throw new InvalidOperationException(message);
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ArgumentOutOfRange(bool condition, string? parameterName, string message)
	{
		if (!condition)
		{
			throw new ArgumentOutOfRangeException(parameterName, message);
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void IndexOutOfRange(int index, int count)
	{
		if (index < 0 || index >= count)
		{
			throw new IndexOutOfRangeException();
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Format(bool condition, string message)
	{
		if (!condition)
		{
			throw new FormatException(message);
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void InvalidCast(bool condition, string? parameterName)
	{
		if (!condition)
		{
			throw new InvalidCastException(string.Format(CultureInfo.InvariantCulture, ExceptionMessages.InvalidCast, parameterName));
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Overflow(bool condition)
	{
		if (!condition)
		{
			throw new OverflowException();
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void DivideByZero<T>(T parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null) where T : INumber<T>
	{
		if (T.IsZero(parameter))
		{
			throw new DivideByZeroException(string.Format(CultureInfo.InvariantCulture, ExceptionMessages.DivideByZero, parameterName));
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void DivideByZero(long parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null)
	{
		if (parameter == 0)
		{
			throw new DivideByZeroException(string.Format(CultureInfo.InvariantCulture, ExceptionMessages.DivideByZero, parameterName));
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void FileNotFound(string? path)
	{
		if (!File.Exists(path))
		{
			throw new FileNotFoundException(null, path);
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void DirectoryNotFound(string? path)
	{
		if (!Directory.Exists(path))
		{
			throw new DirectoryNotFoundException(string.Format(CultureInfo.InvariantCulture, ExceptionMessages.DirectoryNotFound, path));
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void FileOrDirectoryNotFound(string? path)
	{
		if (!File.Exists(path) && !Directory.Exists(path))
		{
			throw new FileNotFoundException(null, path);
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void TargetParameterCount(int count1, int count2)
	{
		if (count1 != count2)
		{
			throw new TargetParameterCountException();
		}
	}
	[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ObjectDisposed<T>(bool disposed)
	{
		if (disposed)
		{
			throw new ObjectDisposedException(typeof(T).FullName);
		}
	}

	public static class ArgumentEx
	{
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void StringNotEmpty(string? parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null)
		{
			if (parameter == "")
			{
				throw new ArgumentException(ExceptionMessages.Argument.StringNotEmpty, parameterName);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void StringNotEmptyOrWhiteSpace(string parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null)
		{
			if (parameter == "" || parameter.Any(char.IsWhiteSpace))
			{
				throw new ArgumentException(ExceptionMessages.Argument.StringNotEmptyOrWhiteSpace, parameterName);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void OffsetAndLengthOutOfBounds(int offset, int count, int length)
		{
			if (offset + count > length)
			{
				throw new ArgumentException(ExceptionMessages.Argument.OffsetAndLengthOutOfBounds);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ArrayElementsRequired<T>(IEnumerable<T?> parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null)
		{
			if (!parameter.Any())
			{
				throw new ArgumentException(ExceptionMessages.Argument.ArrayElementsRequired, parameterName);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void EnumerableElementsRequired(bool any, string? parameterName)
		{
			if (!any)
			{
				throw new ArgumentException(ExceptionMessages.Argument.EnumerableElementsRequired, parameterName);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ArrayValuesNotNull<T>(IEnumerable<T?> parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null)
		{
			if (parameter.Any(itm => itm == null))
			{
				throw new ArgumentException(ExceptionMessages.Argument.ArrayValuesNotNull, parameterName);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ArrayValuesNotStringEmpty(IEnumerable<string> parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null)
		{
			if (parameter.Any(itm => itm == ""))
			{
				throw new ArgumentException(ExceptionMessages.Argument.ArrayValuesNotStringEmpty, parameterName);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Handle(nint handle, [CallerArgumentExpression(nameof(handle))] string? parameterName = null)
		{
			if (handle is 0 or -1)
			{
				throw new ArgumentException(ExceptionMessages.Argument.InvalidHandle, parameterName);
			}
		}
	}
	public static class ArgumentOutOfRangeEx
	{
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GreaterEqual0<T>(T parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null) where T : INumber<T>
		{
			if (parameter < T.Zero)
			{
				throw new ArgumentOutOfRangeException(parameterName, ExceptionMessages.ArgumentOutOfRange.GreaterEqual0);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GreaterEqual0(TimeSpan parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null)
		{
			if (parameter < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException(parameterName, ExceptionMessages.ArgumentOutOfRange.GreaterEqual0);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Greater0<T>(T parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null) where T : INumber<T>
		{
			if (parameter <= T.Zero)
			{
				throw new ArgumentOutOfRangeException(parameterName, ExceptionMessages.ArgumentOutOfRange.Greater0);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Greater0(TimeSpan parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null)
		{
			if (parameter <= TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException(parameterName, ExceptionMessages.ArgumentOutOfRange.Greater0);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Between0And1<T>(T parameter, [CallerArgumentExpression(nameof(parameter))] string? parameterName = null) where T : INumber<T>
		{
			if (parameter < T.Zero || parameter > T.One)
			{
				throw new ArgumentOutOfRangeException(parameterName, ExceptionMessages.ArgumentOutOfRange.Between0And1);
			}
		}
		[DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GreaterEqualValue<T>(T parameter1, T parameter2, [CallerArgumentExpression(nameof(parameter1))] string parameter1Name = null!, [CallerArgumentExpression(nameof(parameter2))] string parameter2Name = null!) where T : struct, IComparable
		{
			if (parameter1.CompareTo(parameter2) < 0)
			{
				throw new ArgumentOutOfRangeException(parameter1Name, string.Format(CultureInfo.InvariantCulture, ExceptionMessages.ArgumentOutOfRange.GreaterEqualValue, parameter2Name));
			}
		}
	}
}