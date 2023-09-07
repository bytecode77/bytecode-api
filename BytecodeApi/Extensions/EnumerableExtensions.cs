using BytecodeApi.Mathematics;
using System.Collections;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for querying data structures that implement <see cref="IEnumerable" />.
/// </summary>
public static class EnumerableExtensions
{
	/// <summary>
	/// Determines whether a sequence contains no elements.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">The <see cref="IEnumerable{T}" /> to check for emptiness.</param>
	/// <returns>
	/// <see langword="true" />, if the source sequence is empty;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool None<TSource>(this IEnumerable<TSource> source)
	{
		Check.ArgumentNull(source);

		return !Enumerable.Any(source);
	}
	/// <summary>
	/// Determines whether a sequence that satisfies a specified condition contains no elements.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">The <see cref="IEnumerable{T}" /> to check for emptiness.</param>
	/// <param name="predicate">A function to test each element for a condition.</param>
	/// <returns>
	/// <see langword="true" />, if the source sequence that satisfies a specified condition is empty;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(predicate);

		return !source.Any(predicate);
	}
	/// <summary>
	/// Searches for the specified item in the sequence and returns the index of its first occurrence.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">The <see cref="IEnumerable{T}" /> to search in.</param>
	/// <param name="item">The item to search for in <paramref name="source" />.</param>
	/// <returns>
	/// The index of <paramref name="item" />, if found in <paramref name="source" />; If not found, returns -1.
	/// </returns>
	public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource item)
	{
		return source.IndexOf(itm => Equals(itm, item));
	}
	/// <summary>
	/// Searches for the sequence until a specified condition is met and returns the index of its first occurrence.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">The <see cref="IEnumerable{T}" /> to search in.</param>
	/// <param name="predicate">A function to test each element for a condition.</param>
	/// <returns>
	/// The index of the first occurrence, where a specified condition met; If not found, returns -1.
	/// </returns>
	public static int IndexOf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(predicate);

		int index = 0;
		foreach (TSource item in source)
		{
			if (predicate(item)) return index;
			index++;
		}

		return -1;
	}
	/// <summary>
	/// Returns <see langword="true" />, if all values of <paramref name="source" /> are equal or if the <see cref="IEnumerable{T}" /> has no elements.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">The <see cref="IEnumerable{T}" /> to check.</param>
	/// <returns>
	/// <see langword="true" />, if all values of <paramref name="source" /> are equal or if the <see cref="IEnumerable{T}" /> has no elements;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool AllValuesEqual<TSource>(this IEnumerable<TSource> source)
	{
		Check.ArgumentNull(source);

		bool hasFirstItem = false;
		TSource? firstItem = default;

		foreach (TSource item in source)
		{
			if (!hasFirstItem)
			{
				firstItem = item;
				hasFirstItem = true;
			}
			else if (!Equals(item, firstItem))
			{
				return false;
			}
		}

		return true;
	}
	/// <summary>
	/// Converts all <see cref="bool" /> values to a nullable <see cref="bool" /> value representing an indeterminate indicator.
	/// </summary>
	/// <param name="values">A collection of <see cref="bool" /> of values to be processed.</param>
	/// <returns>
	/// <see langword="true" />, if all values of <paramref name="values" /> are equal to <see langword="true" />;
	/// <see langword="false" />, if all values of <paramref name="values" /> are equal to <see langword="false" /> or <paramref name="values" /> has no elements;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static bool? ToIndeterminate(this IEnumerable<bool> values)
	{
		Check.ArgumentNull(values);

		bool hasTrue = false;
		bool hasFalse = false;

		foreach (bool value in values)
		{
			if (value)
			{
				hasTrue = true;
			}
			else
			{
				hasFalse = true;
			}

			if (hasTrue && hasFalse)
			{
				return null;
			}
		}

		return hasTrue;
	}
	/// <summary>
	/// Converts all <see cref="bool" /> values to a nullable <see cref="bool" /> value representing an indeterminate indicator.
	/// </summary>
	/// <param name="values">A collection of <see cref="bool" /> of values to be processed.</param>
	/// <returns>
	/// <see langword="true" />, if all values of <paramref name="values" /> are equal to <see langword="true" />;
	/// <see langword="false" />, if all values of <paramref name="values" /> are equal to <see langword="false" /> or <paramref name="values" /> has no elements;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static bool? ToIndeterminate(this IEnumerable<bool?> values)
	{
		Check.ArgumentNull(values);

		bool hasTrue = false;
		bool hasFalse = false;
		bool hasNull = false;

		foreach (bool? value in values)
		{
			if (value == true)
			{
				hasTrue = true;
			}
			else if (value == false)
			{
				hasFalse = true;
			}
			else
			{
				hasNull = true;
			}

			if (hasNull || hasTrue && hasFalse)
			{
				return null;
			}
		}

		return hasTrue;
	}

	/// <summary>
	/// Returns the first element of a sequence, or <see langword="null" />, if the sequence contains no elements.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}" /> to process.</param>
	/// <returns>
	/// The first elements in <paramref name="source" />, or <see langword="null" />, if <paramref name="source" /> is empty.
	/// </returns>
	public static TSource? FirstOrNull<TSource>(this IEnumerable<TSource> source) where TSource : struct
	{
		Check.ArgumentNull(source);

		foreach (TSource item in source)
		{
			return item;
		}

		return null;
	}
	/// <summary>
	/// Returns the first element of a sequence that satisfies a condition, or <see langword="null" />, if the sequence contains no elements.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}" /> to process.</param>
	/// <param name="predicate">A function to test each element for a condition.</param>
	/// <returns>
	/// The first elements in <paramref name="source" />, or <see langword="null" />, if <paramref name="source" /> is empty or if no element passes the test specified by <paramref name="predicate" />.
	/// </returns>
	public static TSource? FirstOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) where TSource : struct
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(predicate);

		foreach (TSource item in source)
		{
			if (predicate(item))
			{
				return item;
			}
		}

		return null;
	}
	/// <summary>
	/// Returns the only element of a sequence, or <see langword="null" />, if the sequence is empty. This method throws an exception, if there is more than one element in the sequence.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}" /> to process.</param>
	/// <returns>
	/// The single element of the input sequence, of <see langword="null" />, if the sequence contains no elements.
	/// </returns>
	public static TSource? SingleOrNull<TSource>(this IEnumerable<TSource> source) where TSource : struct
	{
		Check.ArgumentNull(source);

		using IEnumerator<TSource> enumerator = source.GetEnumerator();

		if (!enumerator.MoveNext())
		{
			return null;
		}

		TSource result = enumerator.Current;

		if (!enumerator.MoveNext())
		{
			return result;
		}

		throw Throw.InvalidOperation("Sequence contains more than one element.");
	}
	/// <summary>
	/// Returns the only element of a sequence that satisfies a condition, or <see langword="null" />, if the sequence is empty. This method throws an exception, if there is more than one element in the sequence satisfies the condition.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}" /> to process.</param>
	/// <param name="predicate">A function to test each element for a condition.</param>
	/// <returns>
	/// The single element of the input sequence that satisfies the condition, of <see langword="null" />, if the sequence contains no elements.
	/// </returns>
	public static TSource? SingleOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) where TSource : struct
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(predicate);

		TSource? result = null;
		int count = 0;

		foreach (TSource element in source)
		{
			if (predicate(element))
			{
				result = element;

				if (++count > 1)
				{
					break;
				}
			}
		}

		return count switch
		{
			0 => null,
			1 => result,
			_ => throw Throw.InvalidOperation("Sequence contains more than one element.")
		};
	}
	/// <summary>
	/// Gets the value associated with the specified key from this <see cref="IDictionary{TKey, TValue}" />, or returns a default value if it was not found.
	/// </summary>
	/// <typeparam name="TKey">The type of the key of this <see cref="IDictionary{TKey, TValue}" />.</typeparam>
	/// <typeparam name="TValue">The type of the value of this <see cref="IDictionary{TKey, TValue}" />.</typeparam>
	/// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}" /> to search.</param>
	/// <param name="key">The key of the value to get.</param>
	/// <returns>
	/// The value associated with the specified key, or a default value if it was not found.
	/// </returns>
	public static TValue? ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TKey : notnull
	{
		return dictionary.ValueOrDefault(key, default);
	}
	/// <summary>
	/// Gets the value associated with the specified key from this <see cref="IDictionary{TKey, TValue}" />, or returns a default value if it was not found.
	/// </summary>
	/// <typeparam name="TKey">The type of the key of this <see cref="IDictionary{TKey, TValue}" />.</typeparam>
	/// <typeparam name="TValue">The type of the value of this <see cref="IDictionary{TKey, TValue}" />.</typeparam>
	/// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}" /> to search.</param>
	/// <param name="key">The key of the value to get.</param>
	/// <param name="defaultValue">The value that is used if the key was not found.</param>
	/// <returns>
	/// The value associated with the specified key, or a default value if it was not found.
	/// </returns>
	public static TValue? ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue? defaultValue) where TKey : notnull
	{
		Check.ArgumentNull(dictionary);

		return dictionary.TryGetValue(key, out TValue? value) ? value : defaultValue;
	}

	/// <summary>
	/// Performs an <see cref="Action{T}" /> on each element of a sequence and returns the elements after invoking <paramref name="action" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}" /> to process.</param>
	/// <param name="action">The action to perform on each element of <paramref name="source" />.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that contains all elements from the input sequence.
	/// </returns>
	public static IEnumerable<TSource> Each<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(action);

		foreach (TSource item in source)
		{
			action(item);
			yield return item;
		}
	}
	/// <summary>
	/// Filters the elements of an <see cref="IEnumerable" /> based on a specified type. Objects must be of type <typeparamref name="TResult" />. Objects of classes that inherit <typeparamref name="TResult" /> are not returned.
	/// </summary>
	/// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
	/// <param name="source">The <see cref="IEnumerable" /> whose elements to filter.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that contains elements from the input sequence of type <typeparamref name="TResult" />. Objects of classes that inherit <typeparamref name="TResult" /> are not returned.
	/// </returns>
	public static IEnumerable<TResult> OfExactType<TResult>(this IEnumerable source)
	{
		foreach (object? item in source)
		{
			if (CSharp.IsType<TResult>(item))
			{
				yield return (TResult)item;
			}
		}
	}
	/// <summary>
	/// Returns elements from a sequence as long as a specified condition is <see langword="true" />, however including the last element, where the condition is <see langword="false" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}" /> to return elements from.</param>
	/// <param name="predicate">A function to test each element for a condition.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that contains the elements from the input sequence that occur before the element at which the test no longer passes, however including the last element, where the condition is <see langword="false" />.
	/// </returns>
	public static IEnumerable<TSource> TakeWhileInclusive<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(predicate);

		foreach (TSource item in source)
		{
			yield return item;
			if (!predicate(item)) break;
		}
	}
	/// <summary>
	/// Returns elements from a sequence as long as a specified condition is <see langword="true" />, however including the last element, where the condition is <see langword="false" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}" /> to return elements from.</param>
	/// <param name="predicate">A function to test each element for a condition.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that contains the elements from the input sequence that occur before the element at which the test no longer passes, however including the last element, where the condition is <see langword="false" />.
	/// </returns>
	public static IEnumerable<TSource> TakeWhileInclusive<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(predicate);

		int index = 0;

		foreach (TSource item in source)
		{
			yield return item;
			if (!predicate(item, index++)) break;
		}
	}
	/// <summary>
	/// Bypasses elements in a sequence as long as a specified condition is <see langword="true" /> and then returns the remaining elements, excluding the first element, where the condition is <see langword="true" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}" /> to return elements from.</param>
	/// <param name="predicate">A function to test each element for a condition.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that contains the elements from the input sequence starting after the first element in the linear series that does not pass the test specified by predicate.
	/// </returns>
	public static IEnumerable<TSource> SkipWhileExclusive<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(predicate);

		bool take = false;

		foreach (TSource item in source)
		{
			if (take) yield return item;
			if (!predicate(item)) take = true;
		}
	}
	/// <summary>
	/// Bypasses elements in a sequence as long as a specified condition is <see langword="true" /> and then returns the remaining elements, excluding the first element, where the condition is <see langword="true" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}" /> to return elements from.</param>
	/// <param name="predicate">A function to test each element for a condition.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that contains the elements from the input sequence starting after the first element in the linear series that does not pass the test specified by predicate.
	/// </returns>
	public static IEnumerable<TSource> SkipWhileExclusive<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(predicate);

		bool take = false;
		int index = 0;

		foreach (TSource item in source)
		{
			if (take) yield return item;
			if (!predicate(item, index++)) take = true;
		}
	}
	/// <summary>
	/// Projects each element of a sequence to an <see cref="IEnumerable{T}" /> and flattens the resulting sequences into one sequence.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">A sequence of values to project.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.
	/// </returns>
	public static IEnumerable<TSource> SelectMany<TSource>(this IEnumerable<IEnumerable<TSource>> source)
	{
		Check.ArgumentNull(source);

		return source.SelectMany(itm => itm);
	}
	/// <summary>
	/// Filters a sequence of values and returns only values which are not <see langword="null" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}" /> to return elements from.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that contains elements from the input sequence that are not <see langword="null" />.
	/// </returns>
	public static IEnumerable<TSource> ExceptNull<TSource>(this IEnumerable<TSource?> source) where TSource : class
	{
		Check.ArgumentNull(source);

		foreach (TSource? item in source)
		{
			if (item != null)
			{
				yield return item;
			}
		}
	}
	/// <summary>
	/// Filters a sequence of values and returns only values which are not <see langword="null" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">An <see cref="IEnumerable{T}" /> to return elements from.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that contains elements from the input sequence that are not <see langword="null" />.
	/// </returns>
	public static IEnumerable<TSource> ExceptNull<TSource>(this IEnumerable<TSource?> source) where TSource : struct
	{
		Check.ArgumentNull(source);

		foreach (TSource? item in source)
		{
			if (item != null)
			{
				yield return item.Value;
			}
		}
	}
	/// <summary>
	/// Produces the set difference of a sequence and one element.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of the input sequence and the second element.</typeparam>
	/// <param name="first">An <see cref="IEnumerable{T}" /> whose elements that are not equal to <paramref name="second" /> will be returned.</param>
	/// <param name="second">The second element, which will be removed from the returned sequence, if it also occurs in the first sequence.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that contains the set difference of the elements from the input sequence and the second element.
	/// </returns>
	public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, TSource second)
	{
		return first.Except(second, null);
	}
	/// <summary>
	/// Produces the set difference of a sequence and one element by using the specified <see cref="IEqualityComparer{T}" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of the input sequence and the second element.</typeparam>
	/// <param name="first">An <see cref="IEnumerable{T}" /> whose elements that are not equal to <paramref name="second" /> will be returned.</param>
	/// <param name="second">The second element, which will be removed from the returned sequence, if it also occurs in the first sequence.</param>
	/// <param name="comparer">An <see cref="IComparer{T}" /> to compare the elements.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that contains the set difference of the elements from the input sequence and the second element.
	/// </returns>
	public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, TSource second, IEqualityComparer<TSource>? comparer)
	{
		Check.ArgumentNull(first);

		return first.Except(new[] { second }, comparer);
	}
	/// <summary>
	/// Sorts the elements of a sequence in ascending order.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">A sequence of values to sort.</param>
	/// <returns>
	/// An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted.
	/// </returns>
	public static IOrderedEnumerable<TSource> Sort<TSource>(this IEnumerable<TSource> source)
	{
		return source.Sort(null);
	}
	/// <summary>
	/// Sorts the elements of a sequence in ascending order using a specified comparer.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">A sequence of values to sort.</param>
	/// <param name="comparer">An <see cref="IComparer{T}" /> to compare the elements.</param>
	/// <returns>
	/// An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted.
	/// </returns>
	public static IOrderedEnumerable<TSource> Sort<TSource>(this IEnumerable<TSource> source, IComparer<TSource>? comparer)
	{
		Check.ArgumentNull(source);

		return source.OrderBy(itm => itm, comparer);
	}
	/// <summary>
	/// Sorts the elements of a sequence in descending order.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">A sequence of values to sort.</param>
	/// <returns>
	/// An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted in descending order.
	/// </returns>
	public static IOrderedEnumerable<TSource> SortDescending<TSource>(this IEnumerable<TSource> source)
	{
		return source.SortDescending(null);
	}
	/// <summary>
	/// Sorts the elements of a sequence in descending order using a specified comparer.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">A sequence of values to sort.</param>
	/// <param name="comparer">An <see cref="IComparer{T}" /> to compare the elements.</param>
	/// <returns>
	/// An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted in descending order.
	/// </returns>
	public static IOrderedEnumerable<TSource> SortDescending<TSource>(this IEnumerable<TSource> source, IComparer<TSource>? comparer)
	{
		Check.ArgumentNull(source);

		return source.OrderByDescending(itm => itm, comparer);
	}
	/// <summary>
	/// Randomizes the order of the elements of a sequence.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
	/// <param name="source">A sequence of values to sort.</param>
	/// <returns>
	/// An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted in randomized order.
	/// </returns>
	public static IOrderedEnumerable<TSource> SortRandom<TSource>(this IEnumerable<TSource> source)
	{
		Check.ArgumentNull(source);

		return source.OrderBy(itm => MathEx.Random.Next());
	}
	/// <summary>
	/// Exchanges keys with values in this <see cref="IDictionary{TKey, TValue}" /> and returns a new <see cref="Dictionary{TKey, TValue}" />, where keys and values are swapped.
	/// </summary>
	/// <typeparam name="TKey">The type of the key of this <see cref="IDictionary{TKey, TValue}" />.</typeparam>
	/// <typeparam name="TValue">The type of the value of this <see cref="IDictionary{TKey, TValue}" />.</typeparam>
	/// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}" /> to process.</param>
	/// <returns>
	/// A new <see cref="Dictionary{TKey, TValue}" />, where keys and values are swapped.
	/// </returns>
	public static Dictionary<TValue, TKey> Swap<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TValue : notnull
	{
		return Swap(dictionary, null);
	}
	/// <summary>
	/// Exchanges keys with values in this <see cref="IDictionary{TKey, TValue}" /> using a specified <see cref="IEqualityComparer{T}" /> and returns a new <see cref="Dictionary{TKey, TValue}" />, where keys and values are swapped.
	/// </summary>
	/// <typeparam name="TKey">The type of the key of this <see cref="IDictionary{TKey, TValue}" />.</typeparam>
	/// <typeparam name="TValue">The type of the value of this <see cref="IDictionary{TKey, TValue}" />.</typeparam>
	/// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}" /> to process.</param>
	/// <param name="comparer">An <see cref="IComparer{T}" /> to compare the elements.</param>
	/// <returns>
	/// A new <see cref="Dictionary{TKey, TValue}" />, where keys and values are swapped.
	/// </returns>
	public static Dictionary<TValue, TKey> Swap<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEqualityComparer<TValue>? comparer) where TValue : notnull
	{
		Check.ArgumentNull(dictionary);

		return dictionary.ToDictionary(itm => itm.Value, itm => itm.Key, comparer);
	}
	/// <summary>
	/// Ensures that all elements from the input sequence are disposed at the end of the query. This method must be called first in the LINQ query.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements to dispose. <typeparamref name="TSource" /> must inherit <see cref="IDisposable" />.</typeparam>
	/// <param name="source">A sequence of objects to dispose.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that contains all elements from the input sequence.
	/// </returns>
	public static IEnumerable<TSource> AsDisposable<TSource>(this IEnumerable<TSource> source) where TSource : IDisposable
	{
		Check.ArgumentNull(source);

		foreach (TSource item in source)
		{
			try
			{
				yield return item;
			}
			finally
			{
				(item as IDisposable)?.Dispose();
			}
		}
	}

	/// <summary>
	/// Adds the elements of the specified collection to the end of <paramref name="source" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements to add.</typeparam>
	/// <param name="source">The <see cref="ICollection{T}" /> to add the elements from <paramref name="collection" /> to.</param>
	/// <param name="collection">The collection of elements to be addded to <paramref name="source" />.</param>
	public static void AddRange<TSource>(this ICollection<TSource> source, IEnumerable<TSource> collection)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(collection);

		foreach (TSource item in collection)
		{
			source.Add(item);
		}
	}
	/// <summary>
	/// Removes all elements that satisfy a specified condition.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements to remove.</typeparam>
	/// <param name="source">The <see cref="ICollection{T}" /> to remove elements from.</param>
	/// <param name="predicate">A function to test each element for a condition.</param>
	public static void RemoveAll<TSource>(this ICollection<TSource> source, Func<TSource, bool> predicate)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(predicate);

		foreach (TSource item in source.Where(itm => predicate(itm)).ToArray())
		{
			source.Remove(item);
		}
	}
	/// <summary>
	/// Removes all elements that occur in the specified collection.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements to remove.</typeparam>
	/// <param name="source">The <see cref="ICollection{T}" /> to remove elements from.</param>
	/// <param name="collection">The <see cref="ICollection{T}" /> with all elements to remove from <paramref name="source" />.</param>
	public static void RemoveRange<TSource>(this ICollection<TSource> source, IEnumerable<TSource> collection)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(collection);

		foreach (TSource item in collection.ToArray())
		{
			source.Remove(item);
		}
	}
	/// <summary>
	/// Performs the specified <see cref="Action" /> on each element of this <see cref="IEnumerable{T}" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements to process.</typeparam>
	/// <param name="source">A sequence of values to process.</param>
	/// <param name="action">The action to perform on each element of <paramref name="source" />.</param>
	public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
	{
		Check.ArgumentNull(source);
		Check.ArgumentNull(action);

		foreach (TSource item in source)
		{
			action(item);
		}
	}
}