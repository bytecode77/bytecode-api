using BytecodeApi.Extensions;
using BytecodeApi.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace BytecodeApi
{
	/// <summary>
	/// Provides <see langword="static" /> methods and properties serving as a general object manipulation helper class.
	/// </summary>
	public static class CSharp
	{
		/// <summary>
		/// Represents a <see cref="ReadOnlyDictionary{TKey, TValue}" /> with all C# built-in types. The key contains the class name of the type. The value contains the equivalent <see cref="Type" /> representation of the type.
		/// <para>This <see cref="ReadOnlyDictionary{TKey, TValue}" /> contains following keys: Void, Boolean, Byte, SByte, Char, Decimal, Double, Single, Int32, UInt32, Int64, UInt64, Object, Int16, UInt16, String</para>
		/// </summary>
		public static readonly ReadOnlyDictionary<string, Type> BuiltInTypes = new Dictionary<string, Type>
		{
			["Void"] = typeof(void),
			["Boolean"] = typeof(bool),
			["Byte"] = typeof(byte),
			["SByte"] = typeof(sbyte),
			["Char"] = typeof(char),
			["Decimal"] = typeof(decimal),
			["Double"] = typeof(double),
			["Single"] = typeof(float),
			["Int32"] = typeof(int),
			["UInt32"] = typeof(uint),
			["Int64"] = typeof(long),
			["UInt64"] = typeof(ulong),
			["Object"] = typeof(object),
			["Int16"] = typeof(short),
			["UInt16"] = typeof(ushort),
			["String"] = typeof(string)
		}.ToReadOnlyDictionary();
		/// <summary>
		/// Represents a <see cref="ReadOnlyDictionary{TKey, TValue}" /> with all C# built-in types. The key contains the class name of the type. The value contains the equivalent <see cref="string" /> representation of the type name (e.g. "Int32" and "int").
		/// <para>This <see cref="ReadOnlyDictionary{TKey, TValue}" /> contains following keys: Void, Boolean, Byte, SByte, Char, Decimal, Double, Single, Int32, UInt32, Int64, UInt64, Object, Int16, UInt16, String</para>
		/// </summary>
		public static readonly ReadOnlyDictionary<string, string> BuiltInTypeNames = new Dictionary<string, string>
		{
			["Void"] = "void",
			["Boolean"] = "bool",
			["Byte"] = "byte",
			["SByte"] = "sbyte",
			["Char"] = "char",
			["Decimal"] = "decimal",
			["Double"] = "double",
			["Single"] = "float",
			["Int32"] = "int",
			["UInt32"] = "uint",
			["Int64"] = "long",
			["UInt64"] = "ulong",
			["Object"] = "object",
			["Int16"] = "short",
			["UInt16"] = "ushort",
			["String"] = "string"
		}.ToReadOnlyDictionary();

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
			catch (Exception ex)
			{
				return throws ? throw ex : false;
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
			catch (Exception ex)
			{
				return throws ? throw ex : defaultValue;
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
		/// Attempts to invoke an <see cref="Action" /> up to a defined number of times until <paramref name="action" /> successfully returned without throwing an exception. If <paramref name="action" /> throws an exception on the last time, the exception is rethrown. Between each call of <paramref name="action" /> that throws an exception, a delay of <paramref name="delay" /> is waited.
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
		/// Attempts to invoke a <see cref="Func{TResult}" /> up to a defined number of times until <paramref name="func" /> successfully returns a value without throwing an exception. If <paramref name="func" /> throws an exception on the last time, the exception is rethrown. Between each call of <paramref name="func" /> that throws an exception, a delay of <paramref name="delay" /> is waited.
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
		/// Invokes a <see cref="Func{TResult}" /> until the result of <paramref name="func" /> is <see langword="true" /> or <paramref name="timeout" /> has been reached. Between each call of <paramref name="func" /> that returns <see langword="false" />, a delay of <paramref name="delay" /> is waited. If <paramref name="func" /> does not return <see langword="true" /> in this timeframe, <see langword="false" /> is returned, otherwuse <see langword="true" />.
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
	}
}