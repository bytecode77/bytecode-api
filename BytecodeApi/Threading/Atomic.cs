using System;

namespace BytecodeApi.Threading
{
	/// <summary>
	/// Represents a wrapper for a value that can be accessed by multiple threads.
	/// </summary>
	/// <typeparam name="T">The type of the wrapped value. This can be either a reference or a value type.</typeparam>
	public sealed class Atomic<T>
	{
		private readonly object SyncRoot;
		private T _Value;
		/// <summary>
		/// Gets or sets the underlying value and performs a <see langword="lock" /> operation on this instance. To prevent race conditions in consecutive get and set operations, use the <see cref="Exchange" /> method. Consider using <see cref="Lock{TResult}(Func{T, TResult})" /> for method calls on instances of <see cref="Value" />.
		/// </summary>
		public T Value
		{
			get
			{
				lock (SyncRoot)
				{
					return _Value;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_Value = value;
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Atomic{T}" /> class with the default value of <typeparamref name="T" />.
		/// </summary>
		public Atomic()
		{
			SyncRoot = new object();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Atomic{T}" /> class with the specified value.
		/// </summary>
		/// <param name="value">The initial value to be assigned to this instance of <see cref="Atomic{T}" />.</param>
		public Atomic(T value) : this()
		{
			_Value = value;
		}

		/// <summary>
		/// Invokes an <see cref="Action" /> while the underlying value is locked. The parameter of <paramref name="action" /> is the underlying value.
		/// </summary>
		/// <param name="action">An <see cref="Action" /> to be invoked while the underlying value is locked</param>
		public void Lock(Action<T> action)
		{
			Check.ArgumentNull(action, nameof(action));

			lock (SyncRoot)
			{
				action(_Value);
			}
		}
		/// <summary>
		/// Invokes a <see cref="Func{TResult}" /> while the underlying value is locked. The parameter of <paramref name="func" /> is the underlying value and the return value will be returned by this method.
		/// </summary>
		/// <typeparam name="TResult">The return type of <paramref name="func" />.</typeparam>
		/// <param name="func">A <see cref="Func{TResult}" /> to be invoked while the underlying value is locked.</param>
		/// <returns>
		/// The result of <paramref name="func" />.
		/// </returns>
		public TResult Lock<TResult>(Func<T, TResult> func)
		{
			Check.ArgumentNull(func, nameof(func));

			lock (SyncRoot)
			{
				return func(_Value);
			}
		}
		/// <summary>
		/// Performs a retrieve and set operation of the underlying value, allowing the value to be exchanged. This operation is atomic and the value cannot change inbetween read and write operations.
		/// </summary>
		/// <param name="func">The <see cref="Func{T, T}" /> that retrieves and then sets the contents of <see cref="Value" />.</param>
		public void Exchange(Func<T, T> func)
		{
			Check.ArgumentNull(func, nameof(func));

			lock (SyncRoot)
			{
				_Value = func(_Value);
			}
		}

		/// <summary>
		/// Performs a <see langword="lock" /> operation on this instance and retrieves the underlying value.
		/// </summary>
		/// <param name="value">The <see cref="Atomic{T}" /> instance to retrieve the value from.</param>
		/// <returns>
		/// The underlying value of this <see cref="Atomic{T}" /> instance.
		/// </returns>
		public static implicit operator T(Atomic<T> value)
		{
			return value == null ? default : value.Value;
		}
	}
}