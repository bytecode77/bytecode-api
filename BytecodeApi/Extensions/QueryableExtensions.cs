using System.ComponentModel;
using System.Linq.Expressions;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="IQueryable{T}" /> objects.
/// </summary>
public static class QueryableExtensions
{
	/// <summary>
	/// Sorts the elements of a sequence in ascending or descending order, depending on the specified <see cref="ListSortDirection" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
	/// <param name="source">A sequence of values to sort.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="direction">Specifies the direction to sort (ascending or descending).</param>
	/// <returns>
	/// An <see cref="IOrderedQueryable{T}" /> whose elements are sorted according to the specified direction.
	/// </returns>
	public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, ListSortDirection direction)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(keySelector);

		return direction switch
		{
			ListSortDirection.Ascending => source.OrderBy(keySelector),
			ListSortDirection.Descending => source.OrderByDescending(keySelector),
			_ => throw Throw.InvalidEnumArgument(nameof(direction), direction)
		};
	}
	/// <summary>
	/// Performs a subsequent ordering of a sequence in ascending or descending order, depending on the specified <see cref="ListSortDirection" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
	/// <param name="source">A sequence of values to sort.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="direction">Specifies the direction to sort (ascending or descending).</param>
	/// <returns>
	/// An <see cref="IOrderedQueryable{T}" /> whose elements are sorted according to the specified direction.
	/// </returns>
	public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, ListSortDirection direction)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(keySelector);

		return direction switch
		{
			ListSortDirection.Ascending => source.ThenBy(keySelector),
			ListSortDirection.Descending => source.ThenByDescending(keySelector),
			_ => throw Throw.InvalidEnumArgument(nameof(direction), direction)
		};
	}
}