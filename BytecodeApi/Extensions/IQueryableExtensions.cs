using System;
using System.Linq;
using System.Linq.Expressions;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for querying data structures that implement <see cref="IQueryable" />.
	/// </summary>
	public static class IQueryableExtensions
	{
		/// <summary>
		/// Returns the first element of a sequence or throws an exception of the type <typeparamref name="TException" />, if <paramref name="source" /> does not have any elements.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TException">The type of the exception that is instantiated and thrown.</typeparam>
		/// <param name="source">An <see cref="IQueryable{T}" /> to return an element from.</param>
		/// <returns>
		/// The first element of a sequence, if <paramref name="source" /> has any;
		/// otherwise, instantiates and throws an exception of the type <typeparamref name="TException" />.
		/// </returns>
		public static TSource FirstOrException<TSource, TException>(this IQueryable<TSource> source) where TSource : class where TException : Exception
		{
			Check.ArgumentNull(source, nameof(source));

			return source.FirstOrDefault() ?? throw Activator.CreateInstance<TException>();
		}
		/// <summary>
		/// Returns the first element of a sequence that satisfies a specified condition or throws an exception of the type <typeparamref name="TException" />, if no such element is found.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TException">The type of the exception that is instantiated and thrown.</typeparam>
		/// <param name="source">An <see cref="IQueryable{T}" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>
		/// The first element of a sequence that satisfies a specified condition, if any item satisfied;
		/// otherwise, instantiates and throws an exception of the type <typeparamref name="TException" />.
		/// </returns>
		public static TSource FirstOrException<TSource, TException>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) where TSource : class where TException : Exception
		{
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(predicate, nameof(predicate));

			return source.FirstOrDefault(predicate) ?? throw Activator.CreateInstance<TException>();
		}
	}
}