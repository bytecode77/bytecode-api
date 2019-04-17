using BytecodeApi.Extensions;
using System.Collections;
using System.Collections.Generic;

namespace BytecodeApi.Comparers
{
	/// <summary>
	/// Represents a <see cref="string" /> comparison operation that compares strings using specified <see cref="SpecialStringComparisons" /> flags.
	/// </summary>
	public sealed class SpecialStringComparer : IComparer, IComparer<string>
	{
		/// <summary>
		/// Gets the <see cref="SpecialStringComparisons" /> flags specifying what comparison properties apply.
		/// </summary>
		public SpecialStringComparisons Comparison { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SpecialStringComparer" /> class with the specified comparison flags.
		/// </summary>
		/// <param name="comparison">The <see cref="SpecialStringComparisons" /> flags specifying what comparison properties apply.</param>
		public SpecialStringComparer(SpecialStringComparisons comparison)
		{
			Comparison = comparison;
		}

		/// <summary>
		/// Compares two <see cref="string" /> objects and returns an indication of their relative sort order. Specified <see cref="object" /> parameters that are not <see cref="string" /> objects are treated as <see langword="null" />.
		/// </summary>
		/// <param name="x">A <see cref="string" /> to compare to <paramref name="y" />.</param>
		/// <param name="y">A <see cref="string" /> to compare to <paramref name="x" />.</param>
		/// <returns>
		/// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />.
		/// </returns>
		public int Compare(object x, object y)
		{
			return Compare(x as string, y as string);
		}
		/// <summary>
		/// Compares two <see cref="string" /> objects and returns an indication of their relative sort order.
		/// </summary>
		/// <param name="x">A <see cref="string" /> to compare to <paramref name="y" />.</param>
		/// <param name="y">A <see cref="string" /> to compare to <paramref name="x" />.</param>
		/// <returns>
		/// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />.
		/// </returns>
		public int Compare(string x, string y)
		{
			return x.CompareTo(y, Comparison);
		}
	}
}