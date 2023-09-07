using System.Collections;

namespace BytecodeApi.Comparers;

/// <summary>
/// Represents a comparison operation that compares the result of the Count() method of <see cref="IEnumerable{T}" /> objects. Each call of <see cref="EnumerableCountComparer{T}.Compare(object, object)" /> invokes the Count() method on the <see cref="IEnumerable{T}" />.
/// </summary>
/// <typeparam name="TSource">The type of the elements of the compared collections.</typeparam>
public sealed class EnumerableCountComparer<TSource> : IComparer, IComparer<IEnumerable<TSource>>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EnumerableCountComparer{T}" /> class.
	/// </summary>
	public EnumerableCountComparer()
	{
	}

	/// <summary>
	/// Compares the result of the Count() method of two <see cref="IEnumerable{T}" /> objects and returns an indication of their relative sort order. Specified <see cref="object" /> parameters that are not <see cref="IEnumerable{T}" /> objects are treated as <see langword="null" />.
	/// </summary>
	/// <param name="x">An <see cref="IEnumerable{T}" /> to compare the result of the Count() method to the result of the Count() method of <paramref name="y" />.</param>
	/// <param name="y">An <see cref="IEnumerable{T}" /> to compare the result of the Count() method to the result of the Count() method of <paramref name="x" />.</param>
	/// <returns>
	/// A signed integer that indicates the relative values of the result of the Count() method of <paramref name="x" /> and the result of the Count() method of <paramref name="y" />.
	/// </returns>
	public int Compare(object? x, object? y)
	{
		return Compare(x as IEnumerable<TSource>, y as IEnumerable<TSource>);
	}
	/// <summary>
	/// Compares the result of the Count() method of two <see cref="IEnumerable{T}" /> objects and returns an indication of their relative sort order.
	/// </summary>
	/// <param name="x">An <see cref="IEnumerable{T}" /> to compare the result of the Count() method to the result of the Count() method of <paramref name="y" />.</param>
	/// <param name="y">An <see cref="IEnumerable{T}" /> to compare the result of the Count() method to the result of the Count() method of <paramref name="x" />.</param>
	/// <returns>
	/// A signed integer that indicates the relative values of the result of the Count() method of <paramref name="x" /> and the result of the Count() method of <paramref name="y" />.
	/// </returns>
	public int Compare(IEnumerable<TSource>? x, IEnumerable<TSource>? y)
	{
		if (x == null && y == null)
		{
			return 0;
		}
		else if (x == null)
		{
			return -1;
		}
		else if (y == null)
		{
			return 1;
		}
		else
		{
			return x.Count().CompareTo(y.Count());
		}
	}
}