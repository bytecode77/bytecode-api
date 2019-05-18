using System;
using System.Collections.Generic;

namespace BytecodeApi.Comparers
{
	/// <summary>
	/// Represents an equality comparison operation that compares objects using a specified <see cref="Func{T1, T2, TResult}" /> delegate.
	/// </summary>
	/// <typeparam name="T">The type of objects to compare.</typeparam>
	public sealed class DelegateEqualityComparer<T> : IEqualityComparer<T>
	{
		/// <summary>
		/// The <see cref="Func{T1, T2, TResult}" /> delegate that is used to compare objects for equality.
		/// </summary>
		public Func<T, T, bool> Comparison { get; private set; }
		/// <summary>
		/// The <see cref="Func{T, TResult}" /> delegate that is used to compute the hash code.
		/// </summary>
		public Func<T, int> HashCode { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateEqualityComparer{T}" /> class with the specified comparison delegate.
		/// </summary>
		/// <param name="comparison">The <see cref="Func{T1, T2, TResult}" /> delegate that is used to compare objects for equality.</param>
		public DelegateEqualityComparer(Func<T, T, bool> comparison)
		{
			Check.ArgumentNull(comparison, nameof(comparison));

			Comparison = comparison;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateEqualityComparer{T}" /> class with the specified comparison and hash code delegate.
		/// </summary>
		/// <param name="comparison">The <see cref="Func{T1, T2, TResult}" /> delegate that is used to compare objects for equality.</param>
		/// <param name="hashCode">The <see cref="Func{T, TResult}" /> delegate that is used to compute the hash code. If <see langword="null" />, a default hash code algorithm is used.</param>
		public DelegateEqualityComparer(Func<T, T, bool> comparison, Func<T, int> hashCode) : this(comparison)
		{
			HashCode = hashCode;
		}

		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <param name="x">The first object of type <typeparamref name="T" /> to compare.</param>
		/// <param name="y">The second object of type <typeparamref name="T" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if the specified objects are equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(T x, T y)
		{
			return Comparison(x, y);
		}
		/// <summary>
		/// Returns a hash code for the specified object, based on the <see cref="HashCode" /> delegate. If the property is <see langword="null" />, a default hash code algorithm is used.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> for which a hash code is to be returned.</param>
		/// <returns>
		/// A hash code for the specified object.
		/// </returns>
		public int GetHashCode(T obj)
		{
			return HashCode?.Invoke(obj) ?? CSharp.GetHashCode(obj);
		}
	}
}