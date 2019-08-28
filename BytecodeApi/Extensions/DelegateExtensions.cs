using System;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with delegate objects, such as <see cref="Action" /> and <see cref="Func{TResult}" />.
	/// </summary>
	public static class DelegateExtensions
	{
		/// <summary>
		/// Converts a <see cref="Func{TResult}" /> to an <see cref="Action" />.
		/// </summary>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{TResult}" /> to wrap inside an <see cref="Action" />.</param>
		/// <returns>
		/// A new <see cref="Action" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action AsAction<TResult>(this Func<TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return () => func();
		}
		/// <summary>
		/// Converts a <see cref="Func{T, TResult}" /> to an <see cref="Action{T}" />.
		/// </summary>
		/// <typeparam name="T">The parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T, TResult}" /> to wrap inside an <see cref="Action{T}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T> AsAction<T, TResult>(this Func<T, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return arg => func(arg);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, TResult}" /> to an <see cref="Action{T1, T2}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, TResult}" /> to wrap inside an <see cref="Action{T1, T2}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2> AsAction<T1, T2, TResult>(this Func<T1, T2, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2) => func(arg1, arg2);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, TResult}" /> to an <see cref="Action{T1, T2, T3}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3> AsAction<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3) => func(arg1, arg2, arg3);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, TResult}" /> to an <see cref="Action{T1, T2, T3, T4}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4> AsAction<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4) => func(arg1, arg2, arg3, arg4);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5> AsAction<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5) => func(arg1, arg2, arg3, arg4, arg5);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5, T6}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T6">The sixth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5, T6}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5, T6}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5, T6> AsAction<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5, arg6) => func(arg1, arg2, arg3, arg4, arg5, arg6);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5, T6, T7}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T6">The sixth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T7">The seventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5, T6, T7}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5, T6, T7}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7> AsAction<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T6">The sixth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T7">The seventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T8">The eighth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8> AsAction<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T6">The sixth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T7">The seventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T8">The eighth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T9">The ninth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> AsAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T6">The sixth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T7">The seventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T8">The eighth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T9">The ninth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T10">The tenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> AsAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T6">The sixth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T7">The seventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T8">The eighth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T9">The ninth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T10">The tenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T11">The eleventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> AsAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T6">The sixth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T7">The seventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T8">The eighth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T9">The ninth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T10">The tenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T11">The eleventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T12">The twelfth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> AsAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T6">The sixth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T7">The seventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T8">The eighth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T9">The ninth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T10">The tenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T11">The eleventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T12">The twelfth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T13">The thirteenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> AsAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T6">The sixth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T7">The seventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T8">The eighth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T9">The ninth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T10">The tenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T11">The eleventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T12">The twelfth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T13">The thirteenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T14">The fourteenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> AsAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T6">The sixth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T7">The seventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T8">The eighth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T9">The ninth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T10">The tenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T11">The eleventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T12">The twelfth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T13">The thirteenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T14">The fourteenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T15">The fifteenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> AsAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
		}
		/// <summary>
		/// Converts a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}" /> to an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}" />.
		/// </summary>
		/// <typeparam name="T1">The first parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T2">The second parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T3">The third parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T4">The fourth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T5">The fifth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T6">The sixth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T7">The seventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T8">The eighth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T9">The ninth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T10">The tenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T11">The eleventh parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T12">The twelfth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T13">The thirteenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T14">The fourteenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T15">The fifteenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="T16">The sixteenth parameter type of <paramref name="func" /> that is passed to the returned <see cref="Action" />.</typeparam>
		/// <typeparam name="TResult">The result type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}" /> to wrap inside an <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}" /> object that wraps <paramref name="func" />.
		/// </returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> AsAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
		}
	}
}