using System;
using System.Collections.Generic;

namespace BytecodeApi.Comparers
{
	/// <summary>
	/// Represents a custom comparison operation that uses an <see cref="EqualityComparer{TCompareType}" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the source element to compare.</typeparam>
	/// <typeparam name="TCompareType">The type of the element to compare to source.</typeparam>
	public sealed class CustomComparer<TSource, TCompareType> : IEqualityComparer<TSource> where TSource : class
	{
		private readonly Func<TSource, TCompareType> CompareFunc;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomComparer{TSource, TCompareType}" /> class with the specified comparing function.
		/// </summary>
		/// <param name="compareFunc">The <see cref="Func{TSource, TCompareType}" /> to be used by the <see cref="Equals" /> method.</param>
		public CustomComparer(Func<TSource, TCompareType> compareFunc)
		{
			Check.ArgumentNull(compareFunc, nameof(compareFunc));

			CompareFunc = compareFunc;
		}

		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>
		/// <see langword="true" />, if the specified objects are equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(TSource x, TSource y)
		{
			if (x == null) return y == null;
			else if (y == null) return false;
			else return EqualityComparer<TCompareType>.Default.Equals(CompareFunc(x), CompareFunc(y));
		}
		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <param name="obj">The object for which a hash code is to be returned.</param>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		public int GetHashCode(TSource obj)
		{
			return EqualityComparer<TCompareType>.Default.GetHashCode(CompareFunc(obj));
		}
	}
}