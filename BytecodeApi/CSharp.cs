using BytecodeApi.Extensions;
using BytecodeApi.Text;
using BytecodeApi.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace BytecodeApi
{
	/// <summary>
	/// Provides <see langword="static" /> methods and properties serving as a general object manipulation helper class.
	/// </summary>
	public static class CSharp
	{
		internal const string DebuggerDisplayString = "{DebuggerDisplay,nq}";

		/// <summary>
		/// Returns the converted version of <paramref name="obj" />, if it is of the specified type; otherwise, returns <see langword="default" />(<typeparamref name="T" />).
		/// </summary>
		/// <typeparam name="T">The type to which to convert <paramref name="obj" /> to.</typeparam>
		/// <param name="obj">The <see cref="object" /> to be converted.</param>
		/// <returns>
		/// The converted version of <paramref name="obj" />, if it is of the specified type; otherwise, returns <see langword="default" />(<typeparamref name="T" />).
		/// </returns>
		public static T CastOrDefault<T>(object obj)
		{
			return obj is T castedObject ? castedObject : default;
		}
		/// <summary>
		/// Exchanges the references of two <see cref="object" /> instances.
		/// </summary>
		/// <typeparam name="T">The type of the <see cref="object" /> instances to be exchanged.</typeparam>
		/// <param name="objA">The <see cref="object" /> to be exchanged.</param>
		/// <param name="objB">The <see cref="object" /> to be exchanged.</param>
		public static void Swap<T>(ref T objA, ref T objB)
		{
			T swap = objA;
			objA = objB;
			objB = swap;
		}
		/// <summary>
		/// Performs an <see cref="Action" /> and disposes <paramref name="obj" />, if <paramref name="obj" /> is an <see cref="IDisposable" />. This is useful if the given <see cref="object" /> only indirectly inherits <see cref="IDisposable" /> and therefore the <see langword="using" /> keyword cannot be used.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to be disposed. If <paramref name="obj" /> is not an <see cref="IDisposable" />, it will not be disposed.</param>
		/// <param name="action">The <see cref="Action" /> to be performed before the <see cref="IDisposable.Dispose" /> method is called. This is equivalent to the body of the <see langword="using" /> statement.</param>
		public static void Using(object obj, Action action)
		{
			Check.ArgumentNull(action, nameof(action));

			try
			{
				action();
			}
			finally
			{
				if (obj is IDisposable disposable) disposable.Dispose();
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
			Check.ArgumentNull(obj, nameof(obj));

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
			Check.ArgumentNull(obj, nameof(obj));
			Check.ArgumentNull(dest, nameof(dest));

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

			static void Process(Type sourceType, Type destType, Func<object> getValue, Action<object> setValue)
			{
				GetEffectiveType(ref sourceType);
				GetEffectiveType(ref destType);
				bool changed = false;

				if (sourceType == destType)
				{
					setValue(getValue());
					changed = true;
				}
				else
				{
					try
					{
						setValue(Convert.ChangeType(getValue(), destType));
						changed = true;
					}
					catch { }
				}

				if (!changed) setValue(null);
			}
			static void GetEffectiveType(ref Type type)
			{
				if (Nullable.GetUnderlyingType(type) is Type nullable) type = nullable;
				if (type.IsEnum) type = type.GetEnumUnderlyingType();
			}
		}

		/// <summary>
		/// Calculates the hashcode for a set of objects by using XOR (i.e. a ^ b ^ c ...). This is a helper method for GetHashCode method implementations that do not require value specific handling.
		/// </summary>
		/// <param name="objects">A set of objects, where <see cref="object.GetHashCode" /> is called on each <see cref="object" /> that is not <see langword="null" />.</param>
		/// <returns>
		/// The combined hashcode of all objects in the given set.
		/// </returns>
		public static int GetHashCode(params object[] objects)
		{
			Check.ArgumentNull(objects, nameof(objects));

			unchecked
			{
				int hashCode = 0;
				foreach (object obj in objects) hashCode ^= obj?.GetHashCode() ?? 0;

				return hashCode;
			}
		}
		/// <summary>
		/// Builds a <see cref="string" /> for the <see cref="DebuggerDisplayAttribute" /> from a set of objects.
		/// </summary>
		/// <param name="type">The type of the class or structure where the <see cref="DebuggerDisplayAttribute" /> is used.</param>
		/// <param name="str">A composite format <see cref="string" />.</param>
		/// <param name="objects">An <see cref="object" /> array that contains zero or more objects to format.</param>
		/// <returns>
		/// A formatted <see cref="string" /> that displays debug information. Each <see cref="object" /> is treated individually, depending on its type.
		/// </returns>
		public static string DebuggerDisplay(Type type, string str, params object[] objects)
		{
			Check.ArgumentNull(str, nameof(str));
			Check.ArgumentNull(objects, nameof(objects));

			object[] args = new object[objects.Length];
			for (int i = 0; i < args.Length; i++)
			{
				if (objects[i] == null || objects[i] is QuotedString quotedStringObject && quotedStringObject == null) args[i] = "null";
				else if (objects[i] is Type typeObject) args[i] = typeObject.ToCSharpName();
				else if (objects[i] is Type[] typeArrayObject) args[i] = typeArrayObject.Select(t => t.ToCSharpName()).AsString(", ");
				else if (objects[i] is Array arrayObject) args[i] = arrayObject.GetType().ToCSharpName().SubstringUntil("[]", true) + "[" + arrayObject.Length + "]";
				else args[i] = objects[i];
			}

			return type.ToCSharpName() + ": " + str.FormatInvariant(args);
		}
		/// <summary>
		/// Builds a <see cref="string" /> for the <see cref="DebuggerDisplayAttribute" /> from a set of objects.
		/// </summary>
		/// <typeparam name="T">The type of the class or structure where the <see cref="DebuggerDisplayAttribute" /> is used.</typeparam>
		/// <param name="str">A composite format <see cref="string" />.</param>
		/// <param name="objects">An <see cref="object" /> array that contains zero or more objects to format.</param>
		/// <returns>
		/// A formatted <see cref="string" /> that displays debug information. Each <see cref="object" /> is treated individually, depending on its type.
		/// </returns>
		public static string DebuggerDisplay<T>(string str, params object[] objects)
		{
			return DebuggerDisplay(typeof(T), str, objects);
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
			return Try(action, false);
		}
		/// <summary>
		/// Invokes an <see cref="Action" /> and handles any exception. Returns <see langword="true" /> on successful execution and <see langword="false" />, if an exception was thrown.
		/// </summary>
		/// <param name="action">The <see cref="Action" /> to be invoked.</param>
		/// <param name="throws"><see langword="true" /> to throw the exception anyway, effectively bypassing the function logic and just executing <paramref name="action" /> without exception handling.</param>
		/// <returns>
		/// <see langword="true" />, on successful execution and
		/// <see langword="false" />, if an exception was thrown and <paramref name="throws" /> equals to <see langword="false" />.
		/// </returns>
		public static bool Try(Action action, bool throws)
		{
			Check.ArgumentNull(action, nameof(action));

			try
			{
				action();
				return true;
			}
			catch
			{
				if (throws) throw; else return false;
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
		public static T Try<T>(Func<T> func)
		{
			return Try(func, default);
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
			return Try(func, defaultValue, false);
		}
		/// <summary>
		/// Invokes a <see cref="Func{TResult}" /> and handles any exception. Returns the result of <paramref name="func" /> on successful execution and <paramref name="defaultValue" />, if an exception was thrown.
		/// </summary>
		/// <typeparam name="T">The return type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{TResult}" /> to be invoked.</param>
		/// <param name="defaultValue">The default value that is returned if an exception was thrown.</param>
		/// <param name="throws"><see langword="true" /> to throw the exception anyway, effectively bypassing the function logic and just executing <paramref name="func" /> without exception handling.</param>
		/// <returns>
		/// The result of <paramref name="func" />, on successful execution and
		/// <paramref name="defaultValue" />, if an exception was thrown and <paramref name="throws" /> equals to <see langword="false" />.
		/// </returns>
		public static T Try<T>(Func<T> func, T defaultValue, bool throws)
		{
			Check.ArgumentNull(func, nameof(func));

			try
			{
				return func();
			}
			catch
			{
				if (throws) throw; else return defaultValue;
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
			Check.ArgumentNull(action, nameof(action));
			Check.ArgumentOutOfRangeEx.Greater0(attempts, nameof(attempts));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(delay, nameof(delay));

			for (; ; attempts--)
			{
				try
				{
					action();
					return;
				}
				catch
				{
					if (attempts <= 1) throw; else Thread.Sleep(delay);
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
			Check.ArgumentNull(func, nameof(func));
			Check.ArgumentOutOfRangeEx.Greater0(attempts, nameof(attempts));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(delay, nameof(delay));

			for (; ; attempts--)
			{
				try
				{
					return func();
				}
				catch
				{
					if (attempts <= 1) throw; else Thread.Sleep(delay);
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
			Check.ArgumentNull(func, nameof(func));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(timeout, nameof(timeout));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(delay, nameof(delay));

			for (Stopwatch stopwatch = ThreadFactory.StartStopwatch(); stopwatch.Elapsed < timeout; Thread.Sleep(delay))
			{
				if (func()) return true;
			}

			return false;
		}

		/// <summary>
		/// Compares <paramref name="obj" /> with the objects in the specified collection and returns <see langword="true" />, if <paramref name="obj" /> is equal to any of the values.
		/// </summary>
		/// <typeparam name="T">The type of the <see cref="object" /> and the value collection.</typeparam>
		/// <param name="obj">The <see cref="object" /> to compare with the objects in <paramref name="values" />.</param>
		/// <param name="values">The value collection to be compared to <paramref name="obj" />.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="obj" /> is equal to any of the values in the specified collection;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool EqualsAny<T>(T obj, IEnumerable<T> values)
		{
			Check.ArgumentNull(values, nameof(values));

			return values.Any(itm => Equals(itm, obj));
		}
		/// <summary>
		/// Compares <paramref name="obj" /> with the objects in the specified collection and returns <see langword="true" />, if <paramref name="obj" /> is equal to any of the values.
		/// </summary>
		/// <typeparam name="T">The type of the <see cref="object" /> and the value collection.</typeparam>
		/// <param name="obj">The <see cref="object" /> to compare with the objects in <paramref name="values" />.</param>
		/// <param name="values">The value collection to be compared to <paramref name="obj" />.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="obj" /> is equal to any of the values in the specified collection;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool EqualsAny<T>(T obj, params T[] values)
		{
			return EqualsAny(obj, (IEnumerable<T>)values);
		}
		/// <summary>
		/// Compares <paramref name="obj" /> with the objects in the specified collection and returns <see langword="true" />, if <paramref name="obj" /> is equal to none of the values.
		/// </summary>
		/// <typeparam name="T">The type of the <see cref="object" /> and the value collection.</typeparam>
		/// <param name="obj">The <see cref="object" /> to compare with the objects in <paramref name="values" />.</param>
		/// <param name="values">The value collection to be compared to <paramref name="obj" />.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="obj" /> is equal to none of the values in the specified collection;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool EqualsNone<T>(T obj, IEnumerable<T> values)
		{
			return !EqualsAny(obj, values);
		}
		/// <summary>
		/// Compares <paramref name="obj" /> with the objects in the specified collection and returns <see langword="true" />, if <paramref name="obj" /> is equal to none of the values.
		/// </summary>
		/// <typeparam name="T">The type of the <see cref="object" /> and the value collection.</typeparam>
		/// <param name="obj">The <see cref="object" /> to compare with the objects in <paramref name="values" />.</param>
		/// <param name="values">The value collection to be compared to <paramref name="obj" />.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="obj" /> is equal to none of the values in the specified collection;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool EqualsNone<T>(T obj, params T[] values)
		{
			return EqualsNone(obj, (IEnumerable<T>)values);
		}
		/// <summary>
		/// Compares <paramref name="obj" /> with the objects in the specified collection and returns <see langword="true" />, if <paramref name="obj" /> is equal to all of the values.
		/// </summary>
		/// <typeparam name="T">The type of the <see cref="object" /> and the value collection.</typeparam>
		/// <param name="obj">The <see cref="object" /> to compare with the objects in <paramref name="values" />.</param>
		/// <param name="values">The value collection to be compared to <paramref name="obj" />.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="obj" /> is equal to all of the values in the specified collection;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool EqualsAll<T>(T obj, IEnumerable<T> values)
		{
			Check.ArgumentNull(values, nameof(values));

			return values.All(itm => Equals(itm, obj));
		}
		/// <summary>
		/// Compares <paramref name="obj" /> with the objects in the specified collection and returns <see langword="true" />, if <paramref name="obj" /> is equal to all of the values.
		/// </summary>
		/// <typeparam name="T">The type of the <see cref="object" /> and the value collection.</typeparam>
		/// <param name="obj">The <see cref="object" /> to compare with the objects in <paramref name="values" />.</param>
		/// <param name="values">The value collection to be compared to <paramref name="obj" />.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="obj" /> is equal to all of the values in the specified collection;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool EqualsAll<T>(T obj, params T[] values)
		{
			return EqualsAll(obj, (IEnumerable<T>)values);
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
		public static bool IsType(object obj, Type type)
		{
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
		public static bool IsType<T>(object obj)
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
		public static bool TypeEquals(object objA, object objB)
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
			Stopwatch stopwatch = ThreadFactory.StartStopwatch();
			action();
			stopwatch.Stop();

			return stopwatch.Elapsed;
		}
	}
}