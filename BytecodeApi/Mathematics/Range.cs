using System;

namespace BytecodeApi.Mathematics
{
	/// <summary>
	/// Represents a generic range of two numeric values that implement <see cref="IComparable" />.
	/// </summary>
	/// <typeparam name="T">The type of the two numeric values.</typeparam>
	public struct Range<T> : IEquatable<Range<T>> where T : struct, IComparable
	{
		/// <summary>
		/// Gets or sets the minimum value of this <see cref="Range{T}" />.
		/// </summary>
		public T Min { get; set; }
		/// <summary>
		/// Gets or sets the maximum value of this <see cref="Range{T}" />.
		/// </summary>
		public T Max { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Range{T}" /> structure to the values indicated by <paramref name="min" /> and <paramref name="max" />.
		/// </summary>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value.</param>
		public Range(T min, T max)
		{
			Min = min;
			Max = max;
		}

		/// <summary>
		/// Determines if this <see cref="Range{T}" /> intersects with <paramref name="range" />.
		/// </summary>
		/// <param name="range">A <see cref="Range{T}" /> to intersect.</param>
		/// <returns>
		/// <see langword="true" />, if there is any intersection;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IntersectsWith(Range<T> range)
		{
			return Min.CompareTo(range.Max) <= 0 && range.Min.CompareTo(Max) <= 0;
		}
		/// <summary>
		/// Determines if <paramref name="range" /> lies within this <see cref="Range{T}" />.
		/// </summary>
		/// <param name="range">A <see cref="Range{T}" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="range" /> lies within this <see cref="Range{T}" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsInside(Range<T> range)
		{
			return Min.CompareTo(range.Min) <= 0 && range.Max.CompareTo(Max) <= 0;
		}
		/// <summary>
		/// Determines if <paramref name="value" /> lies within this <see cref="Range{T}" />.
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> lies within this <see cref="Range{T}" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsInside(T value)
		{
			return value.CompareTo(Min) >= 0 && value.CompareTo(Max) <= 0;
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Min + ", " + Max + "]";
		}
		/// <summary>
		/// Determines whether the specified <see cref="object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Equals(object obj)
		{
			return obj is Range<T> range && Equals(range);
		}
		/// <summary>
		/// Determines whether this instance is equal to another <see cref="Range{T}" />.
		/// </summary>
		/// <param name="other">The <see cref="Range{T}" /> to compare to this instance.</param>
		/// <returns>
		/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(Range<T> other)
		{
			return Min.CompareTo(other.Min) == 0 && Max.CompareTo(other.Max) == 0;
		}
		/// <summary>
		/// Returns a hash code for this <see cref="Range{T}" />.
		/// </summary>
		/// <returns>
		/// The hash code for this <see cref="Range{T}" /> instance.
		/// </returns>
		public override int GetHashCode()
		{
			return Min.GetHashCode() ^ Max.GetHashCode();
		}

		/// <summary>
		/// Compares two <see cref="Range{T}" /> instances for equality.
		/// </summary>
		/// <param name="a">The first <see cref="Range{T}" /> to compare.</param>
		/// <param name="b">The second <see cref="Range{T}" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="Range{T}" /> are equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator ==(Range<T> a, Range<T> b)
		{
			return Equals(a, b);
		}
		/// <summary>
		/// Compares two <see cref="Range{T}" /> instances for inequality.
		/// </summary>
		/// <param name="a">The first <see cref="Range{T}" /> to compare.</param>
		/// <param name="b">The second <see cref="Range{T}" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="Range{T}" /> are not equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator !=(Range<T> a, Range<T> b)
		{
			return !(a == b);
		}
	}
}