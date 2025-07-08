using System.Diagnostics;
using System.Reflection;

namespace BytecodeApi;

/// <summary>
/// Provides <see langword="static" /> methods and properties serving as a general object manipulation helper class.
/// </summary>
public static class CSharp
{
	/// <summary>
	/// Returns the converted version of <paramref name="obj" />, if it is of the specified type; otherwise, returns <see langword="default" />(<typeparamref name="T" />).
	/// </summary>
	/// <typeparam name="T">The type to which to convert <paramref name="obj" /> to.</typeparam>
	/// <param name="obj">The <see cref="object" /> to be converted.</param>
	/// <returns>
	/// The converted version of <paramref name="obj" />, if it is of the specified type; otherwise, returns <see langword="default" />(<typeparamref name="T" />).
	/// </returns>
	public static T? CastOrDefault<T>(object? obj)
	{
		return obj is T castedObject ? castedObject : default;
	}
	/// <summary>
	/// Performs an <see cref="Action" /> and disposes <paramref name="obj" />, if <paramref name="obj" /> is an <see cref="IDisposable" />. This is useful if the given <see cref="object" /> only indirectly inherits <see cref="IDisposable" /> and therefore the <see langword="using" /> keyword cannot be used.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to be disposed. If <paramref name="obj" /> is not an <see cref="IDisposable" />, it will not be disposed.</param>
	/// <param name="action">The <see cref="Action" /> to be performed before the <see cref="IDisposable.Dispose" /> method is called. This is equivalent to the body of the <see langword="using" /> statement.</param>
	public static void Using(object? obj, Action action)
	{
		Check.ArgumentNull(action);

		try
		{
			action();
		}
		finally
		{
			(obj as IDisposable)?.Dispose();
		}
	}
	/// <summary>
	/// Performs an <see cref="Action" /> and disposes all objects in the specified array that are <see cref="IDisposable" />.
	/// </summary>
	/// <param name="objects">An array of objects to be disposed.</param>
	/// <param name="action">The <see cref="Action" /> to be performed before the <see cref="IDisposable.Dispose" /> method is called. This is equivalent to the body of the <see langword="using" /> statement.</param>
	public static void Using(object?[] objects, Action action)
	{
		Check.ArgumentNull(objects);
		Check.ArgumentNull(action);

		try
		{
			action();
		}
		finally
		{
			foreach (object? obj in objects)
			{
				(obj as IDisposable)?.Dispose();
			}
		}
	}
	/// <summary>
	/// Copies the contents of properties and fields of an <see cref="object" /> to another <see cref="object" /> of a different <see cref="Type" /> by comparing property and field names. A new instance of <typeparamref name="TDest" /> is created.
	/// <para>Values are only copied, if the property or field is of equivalent type. This includes conversion between mixed <see cref="Nullable" /> values (e.g. <see cref="int" /> and <see cref="int" />?), and between <see cref="Enum" /> and numeric values. Differing types are attempted to convert (e.g. <see cref="int" /> and <see cref="long" />). If conversion fails, the default value of the destination type is used.</para>
	/// </summary>
	/// <typeparam name="TDest">The type of the <see cref="object" /> to copy the contents to.</typeparam>
	/// <param name="obj">The <see cref="object" /> to copy the contents from.</param>
	/// <returns>
	/// The new instance of <typeparamref name="TDest" /> this method creates, with properties and fields copied from <paramref name="obj" />.
	/// </returns>
	public static TDest ConvertObject<TDest>(object obj) where TDest : class
	{
		return ConvertObject<TDest>(obj, ConvertObjectOptions.None);
	}
	/// <summary>
	/// Copies the contents of properties and fields of an <see cref="object" /> to another <see cref="object" /> of a different <see cref="Type" /> by comparing property and field names. A new instance of <typeparamref name="TDest" /> is created.
	/// <para>Values are only copied, if the property or field is of equivalent type. This includes conversion between mixed <see cref="Nullable" /> values (e.g. <see cref="int" /> and <see cref="int" />?), and between <see cref="Enum" /> and numeric values. Differing types are attempted to convert (e.g. <see cref="int" /> and <see cref="long" />). If conversion fails, the default value of the destination type is used.</para>
	/// </summary>
	/// <typeparam name="TDest">The type of the <see cref="object" /> to copy the contents to.</typeparam>
	/// <param name="obj">The <see cref="object" /> to copy the contents from.</param>
	/// <param name="flags">The <see cref="ConvertObjectOptions" /> flags that specify comparison and copy behavior.</param>
	/// <returns>
	/// The new instance of <typeparamref name="TDest" /> this method creates, with properties and fields copied from <paramref name="obj" />.
	/// </returns>
	public static TDest ConvertObject<TDest>(object obj, ConvertObjectOptions flags) where TDest : class
	{
		Check.ArgumentNull(obj);

		TDest dest = Activator.CreateInstance<TDest>();
		ConvertObject(obj, dest, flags);
		return dest;
	}
	/// <summary>
	/// Copies the contents of properties and fields of an <see cref="object" /> to another <see cref="object" /> of a different <see cref="Type" /> by comparing property and field names.
	/// <para>Values are only copied, if the property or field is of equivalent type. This includes conversion between mixed <see cref="Nullable" /> values (e.g. <see cref="int" /> and <see cref="int" />?), and between <see cref="Enum" /> and numeric values. Differing types are attempted to convert (e.g. <see cref="int" /> and <see cref="long" />). If conversion fails, the default value of the destination type is used.</para>
	/// </summary>
	/// <typeparam name="TDest">The type of the <see cref="object" /> to copy the contents to.</typeparam>
	/// <param name="obj">The <see cref="object" /> to copy the contents from.</param>
	/// <param name="dest">The <see cref="object" /> to copy the contents to.</param>
	public static void ConvertObject<TDest>(object obj, TDest dest) where TDest : class
	{
		ConvertObject(obj, dest, ConvertObjectOptions.None);
	}
	/// <summary>
	/// Copies the contents of properties and fields of an <see cref="object" /> to another <see cref="object" /> of a different <see cref="Type" /> by comparing property and field names.
	/// <para>Values are only copied, if the property or field is of equivalent type. This includes conversion between mixed <see cref="Nullable" /> values (e.g. <see cref="int" /> and <see cref="int" />?), and between <see cref="Enum" /> and numeric values. Differing types are attempted to convert (e.g. <see cref="int" /> and <see cref="long" />). If conversion fails, the default value of the destination type is used.</para>
	/// </summary>
	/// <typeparam name="TDest">The type of the <see cref="object" /> to copy the contents to.</typeparam>
	/// <param name="obj">The <see cref="object" /> to copy the contents from.</param>
	/// <param name="dest">The <see cref="object" /> to copy the contents to.</param>
	/// <param name="flags">The <see cref="ConvertObjectOptions" /> flags that specify comparison and copy behavior.</param>
	public static void ConvertObject<TDest>(object obj, TDest dest, ConvertObjectOptions flags) where TDest : class
	{
		Check.ArgumentNull(obj);
		Check.ArgumentNull(dest);

		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
		if (flags.HasFlag(ConvertObjectOptions.IgnoreCase)) bindingFlags |= BindingFlags.IgnoreCase;
		if (flags.HasFlag(ConvertObjectOptions.NonPublic)) bindingFlags |= BindingFlags.NonPublic;
		if (flags.HasFlag(ConvertObjectOptions.Static)) bindingFlags |= BindingFlags.Static;

		if (!flags.HasFlag(ConvertObjectOptions.IgnoreProperties))
		{
			foreach (PropertyInfo sourceProperty in obj.GetType().GetProperties(bindingFlags))
			{
				if (dest.GetType().GetProperty(sourceProperty.Name, bindingFlags) is PropertyInfo destProperty && destProperty.SetMethod != null)
				{
					Process
					(
						sourceProperty.PropertyType,
						destProperty.PropertyType,
						() => sourceProperty.GetValue(obj),
						value => destProperty.SetValue(dest, value)
					);
				}

				if (flags.HasFlag(ConvertObjectOptions.PropertiesToFields) && dest.GetType().GetField(sourceProperty.Name, bindingFlags) is FieldInfo destField)
				{
					Process
					(
						sourceProperty.PropertyType,
						destField.FieldType,
						() => sourceProperty.GetValue(obj),
						value => destField.SetValue(dest, value)
					);
				}
			}
		}

		if (!flags.HasFlag(ConvertObjectOptions.IgnoreFields))
		{
			foreach (FieldInfo sourceField in obj.GetType().GetFields(bindingFlags))
			{
				if (dest.GetType().GetField(sourceField.Name, bindingFlags) is FieldInfo destField)
				{
					Process
					(
						sourceField.FieldType,
						destField.FieldType,
						() => sourceField.GetValue(obj),
						value => destField.SetValue(dest, value)
					);
				}

				if (flags.HasFlag(ConvertObjectOptions.FieldsToProperties) && dest.GetType().GetProperty(sourceField.Name, bindingFlags) is PropertyInfo destProperty && destProperty.SetMethod != null)
				{
					Process
					(
						sourceField.FieldType,
						destProperty.PropertyType,
						() => sourceField.GetValue(obj),
						value => destProperty.SetValue(dest, value)
					);
				}
			}
		}

		static void Process(Type sourceType, Type destType, Func<object?> getValue, Action<object?> setValue)
		{
			GetEffectiveType(ref sourceType);
			GetEffectiveType(ref destType);

			if (sourceType == destType)
			{
				setValue(getValue());
				return;
			}
			else
			{
				try
				{
					setValue(Convert.ChangeType(getValue(), destType));
					return;
				}
				catch
				{
				}
			}

			setValue(null);
		}
		static void GetEffectiveType(ref Type type)
		{
			if (Nullable.GetUnderlyingType(type) is Type nullable)
			{
				type = nullable;
			}

			if (type.IsEnum)
			{
				type = type.GetEnumUnderlyingType();
			}
		}
	}

	/// <summary>
	/// Invokes an <see cref="Action" /> and handles any exception. Returns <see langword="true" /> on successful execution and <see langword="false" />, if an exception was thrown.
	/// </summary>
	/// <param name="action">The <see cref="Action" /> to be invoked.</param>
	/// <returns>
	/// <see langword="true" />, on successful execution and
	/// <see langword="false" />, if an exception was thrown.
	/// </returns>
	public static bool Try(Action action)
	{
		Check.ArgumentNull(action);

		try
		{
			action();
			return true;
		}
		catch
		{
			return false;
		}
	}
	/// <summary>
	/// Schedules a <see cref="Task" /> and handles any exception. Returns <see langword="true" /> on successful execution and <see langword="false" />, if an exception was thrown.
	/// </summary>
	/// <param name="task">The <see cref="Task" /> to be scheduled.</param>
	/// <returns>
	/// <see langword="true" />, on successful execution and
	/// <see langword="false" />, if an exception was thrown.
	/// </returns>
	public static async Task<bool> Try(Func<Task> task)
	{
		Check.ArgumentNull(task);

		try
		{
			await task();
			return true;
		}
		catch
		{
			return false;
		}
	}
	/// <summary>
	/// Schedules a <see cref="Task" /> and handles any exception. Returns <see langword="true" /> on successful execution and <see langword="false" />, if an exception was thrown.
	/// </summary>
	/// <param name="task">The <see cref="Task" /> to be scheduled.</param>
	/// <returns>
	/// <see langword="true" />, on successful execution and
	/// <see langword="false" />, if an exception was thrown.
	/// </returns>
	public static async Task<bool> Try(Task task)
	{
		Check.ArgumentNull(task);

		try
		{
			await task;
			return true;
		}
		catch
		{
			return false;
		}
	}
	/// <summary>
	/// Invokes a <see cref="Func{TResult}" /> and handles any exception. Returns the result of <paramref name="func" /> on successful execution and <see langword="default" />(<typeparamref name="T" />) if an exception was thrown.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="func" />.</typeparam>
	/// <param name="func">The <see cref="Func{TResult}" /> to be invoked.</param>
	/// <returns>
	/// The result of <paramref name="func" />, on successful execution and
	/// <see langword="default" />(<typeparamref name="T" />), if an exception was thrown.
	/// </returns>
	public static T? Try<T>(Func<T> func)
	{
		return Try(func, default(T));
	}
	/// <summary>
	/// Invokes a <see cref="Func{TResult}" /> and handles any exception. Returns the result of <paramref name="func" /> on successful execution and <paramref name="defaultValue" />, if an exception was thrown.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="func" />.</typeparam>
	/// <param name="func">The <see cref="Func{TResult}" /> to be invoked.</param>
	/// <param name="defaultValue">The default value that is returned if an exception was thrown.</param>
	/// <returns>
	/// The result of <paramref name="func" />, on successful execution and
	/// <paramref name="defaultValue" />, if an exception was thrown.
	/// </returns>
	public static T Try<T>(Func<T> func, T defaultValue)
	{
		Check.ArgumentNull(func);

		try
		{
			return func();
		}
		catch
		{
			return defaultValue;
		}
	}
	/// <summary>
	/// Invokes a <see cref="Func{TResult}" /> and handles any exception. Returns the result of <paramref name="func" /> on successful execution and invokes and returns <paramref name="defaultValue" />, if an exception was thrown.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="func" />.</typeparam>
	/// <param name="func">The <see cref="Func{TResult}" /> to be invoked.</param>
	/// <param name="defaultValue">The <see cref="Func{TResult}" /> that is invoked and whose result is returned, if an exception was thrown.</param>
	/// <returns>
	/// The result of <paramref name="func" />, on successful execution and
	/// The result of <paramref name="defaultValue" />, if an exception was thrown.
	/// </returns>
	public static T Try<T>(Func<T> func, Func<T> defaultValue)
	{
		Check.ArgumentNull(func);
		Check.ArgumentNull(defaultValue);

		try
		{
			return func();
		}
		catch
		{
			return defaultValue();
		}
	}
	/// <summary>
	/// Schedules a <see cref="Task{TResult}" /> and handles any exception. Returns the result of <paramref name="task" /> on successful execution and <see langword="default" />(<typeparamref name="T" />) if an exception was thrown.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="task" />.</typeparam>
	/// <param name="task">The <see cref="Task{TResult}" /> to be scheduled.</param>
	/// <returns>
	/// The result of <paramref name="task" />, on successful execution and
	/// <see langword="default" />(<typeparamref name="T" />), if an exception was thrown.
	/// </returns>
	public static Task<T?> Try<T>(Func<Task<T?>> task)
	{
		return Try(task, default(T));
	}
	/// <summary>
	/// Schedules a <see cref="Task{TResult}" /> and handles any exception. Returns the result of <paramref name="task" /> on successful execution and <paramref name="defaultValue" />, if an exception was thrown.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="task" />.</typeparam>
	/// <param name="task">The <see cref="Task{TResult}" /> to be scheduled.</param>
	/// <param name="defaultValue">The default value that is returned if an exception was thrown.</param>
	/// <returns>
	/// The result of <paramref name="task" />, on successful execution and
	/// <paramref name="defaultValue" />, if an exception was thrown.
	/// </returns>
	public static async Task<T> Try<T>(Func<Task<T>> task, T defaultValue)
	{
		Check.ArgumentNull(task);

		try
		{
			return await task();
		}
		catch
		{
			return defaultValue;
		}
	}
	/// <summary>
	/// Schedules a <see cref="Task{TResult}" /> and handles any exception. Returns the result of <paramref name="task" /> on successful execution and invokes and returns <paramref name="defaultValue" />, if an exception was thrown.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="task" />.</typeparam>
	/// <param name="task">The <see cref="Task{TResult}" /> to be scheduled.</param>
	/// <param name="defaultValue">The <see cref="Func{TResult}" /> that is invoked and whose result is returned, if an exception was thrown.</param>
	/// <returns>
	/// The result of <paramref name="task" />, on successful execution and
	/// The result of <paramref name="defaultValue" />, if an exception was thrown.
	/// </returns>
	public static async Task<T> Try<T>(Func<Task<T>> task, Func<T> defaultValue)
	{
		Check.ArgumentNull(task);
		Check.ArgumentNull(defaultValue);

		try
		{
			return await task();
		}
		catch
		{
			return defaultValue();
		}
	}
	/// <summary>
	/// Schedules a <see cref="Task{TResult}" /> and handles any exception. Returns the result of <paramref name="task" /> on successful execution and <see langword="default" />(<typeparamref name="T" />) if an exception was thrown.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="task" />.</typeparam>
	/// <param name="task">The <see cref="Task{TResult}" /> to be scheduled.</param>
	/// <returns>
	/// The result of <paramref name="task" />, on successful execution and
	/// <see langword="default" />(<typeparamref name="T" />), if an exception was thrown.
	/// </returns>
	public static Task<T?> Try<T>(Task<T?> task)
	{
		return Try(task, default(T));
	}
	/// <summary>
	/// Schedules a <see cref="Task{TResult}" /> and handles any exception. Returns the result of <paramref name="task" /> on successful execution and <paramref name="defaultValue" />, if an exception was thrown.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="task" />.</typeparam>
	/// <param name="task">The <see cref="Task{TResult}" /> to be scheduled.</param>
	/// <param name="defaultValue">The default value that is returned if an exception was thrown.</param>
	/// <returns>
	/// The result of <paramref name="task" />, on successful execution and
	/// <paramref name="defaultValue" />, if an exception was thrown.
	/// </returns>
	public static async Task<T> Try<T>(Task<T> task, T defaultValue)
	{
		Check.ArgumentNull(task);

		try
		{
			return await task;
		}
		catch
		{
			return defaultValue;
		}
	}
	/// <summary>
	/// Schedules a <see cref="Task{TResult}" /> and handles any exception. Returns the result of <paramref name="task" /> on successful execution and invokes and returns <paramref name="defaultValue" />, if an exception was thrown.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="task" />.</typeparam>
	/// <param name="task">The <see cref="Task{TResult}" /> to be scheduled.</param>
	/// <param name="defaultValue">The <see cref="Func{TResult}" /> that is invoked and whose result is returned, if an exception was thrown.</param>
	/// <returns>
	/// The result of <paramref name="task" />, on successful execution and
	/// The result of <paramref name="defaultValue" />, if an exception was thrown.
	/// </returns>
	public static async Task<T> Try<T>(Task<T> task, Func<T> defaultValue)
	{
		Check.ArgumentNull(task);
		Check.ArgumentNull(defaultValue);

		try
		{
			return await task;
		}
		catch
		{
			return defaultValue();
		}
	}
	/// <summary>
	/// Attempts to invoke an <see cref="Action" /> up to a defined number of times until <paramref name="action" /> successfully returned without throwing an exception. If <paramref name="action" /> throws an exception on the last time, the exception is rethrown.
	/// </summary>
	/// <param name="action">The <see cref="Action" /> to be invoked.</param>
	/// <param name="attempts">A <see cref="int" /> value indicating how many times <paramref name="action" /> is attempted before the final exception is rethrown.</param>
	public static void Retry(Action action, int attempts)
	{
		Retry(action, attempts, TimeSpan.Zero);
	}
	/// <summary>
	/// Attempts to invoke an <see cref="Action" /> up to a defined number of times until <paramref name="action" /> successfully returned without throwing an exception. If <paramref name="action" /> throws an exception on the last time, the exception is rethrown. Between each call of <paramref name="action" /> that throws an exception, a delay is waited.
	/// </summary>
	/// <param name="action">The <see cref="Action" /> to be invoked.</param>
	/// <param name="attempts">A <see cref="int" /> value indicating how many times <paramref name="action" /> is attempted before the final exception is rethrown.</param>
	/// <param name="delay">A <see cref="TimeSpan" /> value representing the wait period between each call of <paramref name="action" /> that throws an exception.</param>
	public static void Retry(Action action, int attempts, TimeSpan delay)
	{
		Check.ArgumentNull(action);
		Check.ArgumentOutOfRangeEx.Greater0(attempts);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(delay);

		for (; ; attempts--)
		{
			try
			{
				action();
				return;
			}
			catch when (attempts <= 1)
			{
				throw;
			}
			catch
			{
				Thread.Sleep(delay);
			}
		}
	}
	/// <summary>
	/// Attempts to schedule a <see cref="Task" /> up to a defined number of times until <paramref name="task" /> successfully returned without throwing an exception. If <paramref name="task" /> throws an exception on the last time, the exception is rethrown.
	/// </summary>
	/// <param name="task">The <see cref="Task" /> to be scheduled.</param>
	/// <param name="attempts">A <see cref="int" /> value indicating how many times <paramref name="task" /> is attempted before the final exception is rethrown.</param>
	public static Task Retry(Func<Task> task, int attempts)
	{
		return Retry(task, attempts, TimeSpan.Zero);
	}
	/// <summary>
	/// Attempts to schedule a <see cref="Task" /> up to a defined number of times until <paramref name="task" /> successfully returned without throwing an exception. If <paramref name="task" /> throws an exception on the last time, the exception is rethrown. Between each call of <paramref name="task" /> that throws an exception, a delay is waited.
	/// </summary>
	/// <param name="task">The <see cref="Task" /> to be scheduled.</param>
	/// <param name="attempts">A <see cref="int" /> value indicating how many times <paramref name="task" /> is attempted before the final exception is rethrown.</param>
	/// <param name="delay">A <see cref="TimeSpan" /> value representing the wait period between each call of <paramref name="task" /> that throws an exception.</param>
	public static async Task Retry(Func<Task> task, int attempts, TimeSpan delay)
	{
		Check.ArgumentNull(task);
		Check.ArgumentOutOfRangeEx.Greater0(attempts);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(delay);

		for (; ; attempts--)
		{
			try
			{
				await task();
				return;
			}
			catch when (attempts <= 1)
			{
				throw;
			}
			catch
			{
				await Task.Delay(delay);
			}
		}
	}
	/// <summary>
	/// Attempts to invoke a <see cref="Func{TResult}" /> up to a defined number of times until <paramref name="func" /> successfully returns a value without throwing an exception. If <paramref name="func" /> throws an exception on the last time, the exception is rethrown.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="func" />.</typeparam>
	/// <param name="func">The <see cref="Func{TResult}" /> to be invoked.</param>
	/// <param name="attempts">A <see cref="int" /> value indicating how many times <paramref name="func" /> is attempted before the final exception is rethrown.</param>
	/// <returns>
	/// The result of <paramref name="func" />, if <paramref name="func" /> successfully returned a value in the given number of attempts without throwing an exception;
	/// otherwise, rethrows the exception.
	/// </returns>
	public static T Retry<T>(Func<T> func, int attempts)
	{
		return Retry(func, attempts, TimeSpan.Zero);
	}
	/// <summary>
	/// Attempts to invoke a <see cref="Func{TResult}" /> up to a defined number of times until <paramref name="func" /> successfully returns a value without throwing an exception. If <paramref name="func" /> throws an exception on the last time, the exception is rethrown. Between each call of <paramref name="func" /> that throws an exception, a delay is waited.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="func" />.</typeparam>
	/// <param name="func">The <see cref="Func{TResult}" /> to be invoked.</param>
	/// <param name="attempts">A <see cref="int" /> value indicating how many times <paramref name="func" /> is attempted before the final exception is rethrown.</param>
	/// <param name="delay">A <see cref="TimeSpan" /> value representing the wait period between each call of <paramref name="func" /> that throws an exception.</param>
	/// <returns>
	/// The result of <paramref name="func" />, if <paramref name="func" /> successfully returned a value in the given number of attempts without throwing an exception;
	/// otherwise, rethrows the exception.
	/// </returns>
	public static T Retry<T>(Func<T> func, int attempts, TimeSpan delay)
	{
		Check.ArgumentNull(func);
		Check.ArgumentOutOfRangeEx.Greater0(attempts);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(delay);

		for (; ; attempts--)
		{
			try
			{
				return func();
			}
			catch when (attempts <= 1)
			{
				throw;
			}
			catch
			{
				Thread.Sleep(delay);
			}
		}
	}
	/// <summary>
	/// Attempts to schedule a <see cref="Task{TResult}" /> up to a defined number of times until <paramref name="task" /> successfully returns a value without throwing an exception. If <paramref name="task" /> throws an exception on the last time, the exception is rethrown.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="task" />.</typeparam>
	/// <param name="task">The <see cref="Task{TResult}" /> to be scheduled.</param>
	/// <param name="attempts">A <see cref="int" /> value indicating how many times <paramref name="task" /> is attempted before the final exception is rethrown.</param>
	/// <returns>
	/// The result of <paramref name="task" />, if <paramref name="task" /> successfully returned a value in the given number of attempts without throwing an exception;
	/// otherwise, rethrows the exception.
	/// </returns>
	public static Task<T> Retry<T>(Func<Task<T>> task, int attempts)
	{
		return Retry(task, attempts, TimeSpan.Zero);
	}
	/// <summary>
	/// Attempts to schedule a <see cref="Task{TResult}" /> up to a defined number of times until <paramref name="task" /> successfully returns a value without throwing an exception. If <paramref name="task" /> throws an exception on the last time, the exception is rethrown. Between each call of <paramref name="task" /> that throws an exception, a delay is waited.
	/// </summary>
	/// <typeparam name="T">The return type of <paramref name="task" />.</typeparam>
	/// <param name="task">The <see cref="Task{TResult}" /> to be scheduled.</param>
	/// <param name="attempts">A <see cref="int" /> value indicating how many times <paramref name="task" /> is attempted before the final exception is rethrown.</param>
	/// <param name="delay">A <see cref="TimeSpan" /> value representing the wait period between each call of <paramref name="task" /> that throws an exception.</param>
	/// <returns>
	/// The result of <paramref name="task" />, if <paramref name="task" /> successfully returned a value in the given number of attempts without throwing an exception;
	/// otherwise, rethrows the exception.
	/// </returns>
	public static async Task<T> Retry<T>(Func<Task<T>> task, int attempts, TimeSpan delay)
	{
		Check.ArgumentNull(task);
		Check.ArgumentOutOfRangeEx.Greater0(attempts);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(delay);

		for (; ; attempts--)
		{
			try
			{
				return await task();
			}
			catch when (attempts <= 1)
			{
				throw;
			}
			catch
			{
				await Task.Delay(delay);
			}
		}
	}
	/// <summary>
	/// Invokes a <see cref="Func{TResult}" /> until the result of <paramref name="func" /> is <see langword="true" /> or <paramref name="timeout" /> has been reached. If <paramref name="func" /> does not return <see langword="true" /> in this timeframe, <see langword="false" /> is returned, otherwise, <see langword="true" />.
	/// </summary>
	/// <param name="func">The <see cref="Func{TResult}" /> to be tested.</param>
	/// <param name="timeout">A <see cref="TimeSpan" /> value representing the total time for <paramref name="func" /> to be tested.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="func" /> returned <see langword="true" /> in the specified <paramref name="timeout" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool Timeout(Func<bool> func, TimeSpan timeout)
	{
		return Timeout(func, timeout, TimeSpan.Zero);
	}
	/// <summary>
	/// Invokes a <see cref="Func{TResult}" /> until the result of <paramref name="func" /> is <see langword="true" /> or <paramref name="timeout" /> has been reached. If <paramref name="func" /> does not return <see langword="true" /> in this timeframe, <see langword="false" /> is returned, otherwise, <see langword="true" />. Between each call of <paramref name="func" /> that returns <see langword="false" />, a delay is waited.
	/// </summary>
	/// <param name="func">The <see cref="Func{TResult}" /> to be tested.</param>
	/// <param name="timeout">A <see cref="TimeSpan" /> value representing the total time for <paramref name="func" /> to be tested.</param>
	/// <param name="delay">A <see cref="TimeSpan" /> value representing the wait period between each call of <paramref name="func" /> that returns <see langword="false" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="func" /> returned <see langword="true" /> in the specified <paramref name="timeout" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool Timeout(Func<bool> func, TimeSpan timeout, TimeSpan delay)
	{
		Check.ArgumentNull(func);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(timeout);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(delay);

		for (Stopwatch stopwatch = Stopwatch.StartNew(); stopwatch.Elapsed < timeout; Thread.Sleep(delay))
		{
			if (func())
			{
				return true;
			}
		}

		return false;
	}
	/// <summary>
	/// Schedules a <see cref="Task{TResult}" /> until the result of <paramref name="task" /> is <see langword="true" /> or <paramref name="timeout" /> has been reached. If <paramref name="task" /> does not return <see langword="true" /> in this timeframe, <see langword="false" /> is returned, otherwise, <see langword="true" />.
	/// </summary>
	/// <param name="task">The <see cref="Task{TResult}" /> to be scheduled.</param>
	/// <param name="timeout">A <see cref="TimeSpan" /> value representing the total time for <paramref name="task" /> to be tested.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="task" /> returned <see langword="true" /> in the specified <paramref name="timeout" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static Task<bool> Timeout(Func<Task<bool>> task, TimeSpan timeout)
	{
		return Timeout(task, timeout, TimeSpan.Zero);
	}
	/// <summary>
	/// Schedules a <see cref="Task{TResult}" /> until the result of <paramref name="task" /> is <see langword="true" /> or <paramref name="timeout" /> has been reached. If <paramref name="task" /> does not return <see langword="true" /> in this timeframe, <see langword="false" /> is returned, otherwise, <see langword="true" />. Between each call of <paramref name="task" /> that returns <see langword="false" />, a delay is waited.
	/// </summary>
	/// <param name="task">The <see cref="Task{TResult}" /> to be scheduled.</param>
	/// <param name="timeout">A <see cref="TimeSpan" /> value representing the total time for <paramref name="task" /> to be tested.</param>
	/// <param name="delay">A <see cref="TimeSpan" /> value representing the wait period between each call of <paramref name="task" /> that returns <see langword="false" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="task" /> returned <see langword="true" /> in the specified <paramref name="timeout" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static async Task<bool> Timeout(Func<Task<bool>> task, TimeSpan timeout, TimeSpan delay)
	{
		Check.ArgumentNull(task);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(timeout);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(delay);

		for (Stopwatch stopwatch = Stopwatch.StartNew(); stopwatch.Elapsed < timeout; await Task.Delay(delay))
		{
			if (await task())
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Returns <see langword="true" />, if <paramref name="obj" /> is an <see cref="object" /> of the specified <see cref="Type" />. If <paramref name="obj" /> is not of the specified <see cref="Type" />, or <paramref name="obj" /> is of a <see cref="Type" /> that inherits <paramref name="type" />, <see langword="false" /> is returned.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to check.</param>
	/// <param name="type">The <see cref="Type" /> to compare to the type of <paramref name="obj" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="obj" /> is an <see cref="object" /> of the specified <see cref="Type" />;
	/// <see langword="false" />, If <paramref name="obj" /> is not of the specified <see cref="Type" />, or <paramref name="obj" /> is of a <see cref="Type" /> that inherits <paramref name="type" />.
	/// </returns>
	public static bool IsType([NotNullWhen(true)] object? obj, Type type)
	{
		Check.ArgumentNull(type);

		return obj?.GetType() == type;
	}
	/// <summary>
	/// Returns <see langword="true" />, if <paramref name="obj" /> is an <see cref="object" /> of the specified type. If <paramref name="obj" /> is not of the specified type, or <paramref name="obj" /> is of a type that inherits <typeparamref name="T" />, <see langword="false" /> is returned.
	/// </summary>
	/// <typeparam name="T">The type to compare to the type of <paramref name="obj" />.</typeparam>
	/// <param name="obj">The <see cref="object" /> to check.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="obj" /> is an <see cref="object" /> of the specified type;
	/// <see langword="false" />, If <paramref name="obj" /> is not of the specified type, or <paramref name="obj" /> is of a type that inherits <typeparamref name="T" />.
	/// </returns>
	public static bool IsType<T>([NotNullWhen(true)] object? obj)
	{
		return obj?.GetType() == typeof(T);
	}
	/// <summary>
	/// Returns <see langword="true" />, if <paramref name="objA" /> and <paramref name="objB" /> are of the same <see cref="Type" />, of if both objects are <see langword="null" />.
	/// </summary>
	/// <param name="objA">The first <see cref="object" /> to compare.</param>
	/// <param name="objB">The second <see cref="object" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="objA" /> and <paramref name="objB" /> are of the same <see cref="Type" />, of if both objects are <see langword="null" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool TypeEquals(object? objA, object? objB)
	{
		return objA?.GetType() == objB?.GetType();
	}

	/// <summary>
	/// Invokes an <see cref="Action" /> and measures the time until <paramref name="action" /> returned.
	/// </summary>
	/// <param name="action">The <see cref="Action" /> to be invoked.</param>
	/// <returns>
	/// A <see cref="TimeSpan" /> value with the time <paramref name="action" /> took to return.
	/// </returns>
	public static TimeSpan MeasureTime(Action action)
	{
		Check.ArgumentNull(action);

		Stopwatch stopwatch = Stopwatch.StartNew();
		action();
		stopwatch.Stop();

		return stopwatch.Elapsed;
	}
	/// <summary>
	/// Schedules a <see cref="Task" /> and measures the time until <paramref name="task" /> finished.
	/// </summary>
	/// <param name="task">The <see cref="Task" /> to be scheduled.</param>
	/// <returns>
	/// A <see cref="TimeSpan" /> value with the time <paramref name="task" /> took to finish.
	/// </returns>
	public static async Task<TimeSpan> MeasureTime(Func<Task> task)
	{
		Check.ArgumentNull(task);

		Stopwatch stopwatch = Stopwatch.StartNew();
		await task();
		stopwatch.Stop();

		return stopwatch.Elapsed;
	}
	/// <summary>
	/// Schedules a <see cref="Task" /> and measures the time until <paramref name="task" /> finished.
	/// </summary>
	/// <param name="task">The <see cref="Task" /> to be scheduled.</param>
	/// <returns>
	/// A <see cref="TimeSpan" /> value with the time <paramref name="task" /> took to finish.
	/// </returns>
	public static async Task<TimeSpan> MeasureTime(Task task)
	{
		Check.ArgumentNull(task);

		Stopwatch stopwatch = Stopwatch.StartNew();
		await task;
		stopwatch.Stop();

		return stopwatch.Elapsed;
	}

	/// <summary>
	/// Runs the specified <see cref="Task" /> synchronously and waits for the task to finish.
	/// </summary>
	/// <param name="task">The task to run.</param>
	public static void RunTask(Func<Task> task)
	{
		Task.Run(async () => await task()).Wait();
	}
	/// <summary>
	/// Runs the specified <see cref="Task" /> synchronously and waits for the task to finish.
	/// </summary>
	/// <param name="task">The task to run.</param>
	public static void RunTask(Task task)
	{
		Task.Run(async () => await task).Wait();
	}
	/// <summary>
	/// Runs the specified <see cref="Task{TResult}" /> synchronously and waits for the task to finish.
	/// </summary>
	/// <typeparam name="T">The type of the <see cref="Task{TResult}" />.</typeparam>
	/// <param name="task">The task to run.</param>
	/// <returns>
	/// The value that <paramref name="task" /> returned.
	/// </returns>
	public static T RunTask<T>(Func<Task<T>> task)
	{
		T result = default!;
		Task.Run(async () => result = await task()).Wait();
		return result;
	}
	/// <summary>
	/// Runs the specified <see cref="Task{TResult}" /> synchronously and waits for the task to finish.
	/// </summary>
	/// <typeparam name="T">The type of the <see cref="Task{TResult}" />.</typeparam>
	/// <param name="task">The task to run.</param>
	/// <returns>
	/// The value that <paramref name="task" /> returned.
	/// </returns>
	public static T RunTask<T>(Task<T> task)
	{
		T result = default!;
		Task.Run(async () => result = await task).Wait();
		return result;
	}
}