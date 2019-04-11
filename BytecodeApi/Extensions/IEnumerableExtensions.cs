using BytecodeApi.Mathematics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for querying data structures that implement <see cref="IEnumerable" />.
	/// </summary>
	public static class IEnumerableExtensions
	{
		public static bool Any(this IEnumerable source)
		{
			Check.ArgumentNull(source, nameof(source));

			if (source is ICollection collection)
			{
				return collection.Count > 0;
			}
			else
			{
				bool any = false;
				IEnumerator enumerator = source.GetEnumerator();

				CSharp.Using(enumerator, () =>
				{
					any = enumerator.MoveNext();
				});

				return any;
			}
		}
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
			Check.ArgumentNull(source, nameof(source));

			return Enumerable.Any(source);
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
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(predicate, nameof(predicate));

			return !source.Any(predicate);
		}
		public static bool None(this IEnumerable source)
		{
			Check.ArgumentNull(source, nameof(source));

			return !source.Any();
		}
		/// <summary>
		/// Returns the number of elements in a sequence.
		/// </summary>
		/// <param name="source">A sequence that contains elements to be counted.</param>
		/// <returns>
		/// The number of elements in the input sequence.
		/// </returns>
		public static int Count(this IEnumerable source)
		{
			Check.ArgumentNull(source, nameof(source));

			if (source is ICollection collection)
			{
				return collection.Count;
			}
			else
			{
				int count = 0;
				IEnumerator enumerator = source.GetEnumerator();

				CSharp.Using(enumerator, () =>
				{
					while (enumerator.MoveNext()) count++;
				});

				return count;
			}
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
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(predicate, nameof(predicate));

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
			Check.ArgumentNull(source, nameof(source));

			TSource firstItem = default;

			foreach (TSource item in source)
			{
				if (Equals(firstItem, default(TSource))) firstItem = item;
				else if (!Equals(item, firstItem)) return false;
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
			Check.ArgumentNull(values, nameof(values));

			bool hasTrue = false;
			bool hasFalse = false;

			foreach (bool value in values)
			{
				if (value) hasTrue = true;
				else hasFalse = true;

				if (hasTrue && hasFalse) return null;
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
			Check.ArgumentNull(values, nameof(values));

			bool hasTrue = false;
			bool hasFalse = false;
			bool hasNull = false;

			foreach (bool? value in values)
			{
				if (value == true) hasTrue = true;
				else if (value == false) hasFalse = true;
				else hasNull = true;

				if (hasNull || hasTrue && hasFalse) return null;
			}

			return hasTrue;
		}
		/// <summary>
		/// Computes the sum of the sequence of <see cref="TimeSpan" /> values.
		/// </summary>
		/// <param name="source">A sequence of <see cref="TimeSpan" /> values that are used to calculate a sum.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> with the sum of the projected values.
		/// </returns>
		public static TimeSpan Sum(this IEnumerable<TimeSpan> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new TimeSpan(source.Sum(itm => itm.Ticks));
		}
		/// <summary>
		/// Computes the sum of the sequence of <see cref="TimeSpan" /> values. Values equal to <see langword="null" /> are excluded from the calculation and treated as <see cref="TimeSpan.Zero" />.
		/// </summary>
		/// <param name="source">A sequence of <see cref="TimeSpan" /> values that are used to calculate a sum.</param>
		/// <returns>
		/// A new <see cref="TimeSpan" /> with the sum of the projected values.
		/// </returns>
		public static TimeSpan Sum(this IEnumerable<TimeSpan?> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new TimeSpan(source.Sum(itm => itm?.Ticks ?? 0));
		}
		/// <summary>
		/// Computes the average of a sequence of <see cref="TimeSpan" /> values.
		/// </summary>
		/// <param name="source">A sequence of <see cref="TimeSpan" /> values that are used to calculate a sum.</param>
		/// <returns>
		/// The average of the sequence of <paramref name="source" />.
		/// </returns>
		public static TimeSpan Average(this IEnumerable<TimeSpan> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new TimeSpan((long)source.Average(itm => itm.Ticks));
		}

		/// <summary>
		/// Returns the first element of a sequence or throws an exception of the type <typeparamref name="TException" />, if <paramref name="source" /> does not have any elements.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TException">The type of the exception that is instantiated and thrown.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}" /> to return an element from.</param>
		/// <returns>
		/// The first element of a sequence, if <paramref name="source" /> has any;
		/// otherwise, instantiates and throws an exception of the type <typeparamref name="TException" />.
		/// </returns>
		public static TSource FirstOrException<TSource, TException>(this IEnumerable<TSource> source) where TSource : class where TException : Exception
		{
			Check.ArgumentNull(source, nameof(source));

			return source.FirstOrDefault() ?? throw Activator.CreateInstance<TException>();
		}
		/// <summary>
		/// Returns the first element of a sequence that satisfies a specified condition or throws an exception of the type <typeparamref name="TException" />, if no such element is found.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TException">The type of the exception that is instantiated and thrown.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>
		/// The first element of a sequence that satisfies a specified condition, if any item satisfied;
		/// otherwise, instantiates and throws an exception of the type <typeparamref name="TException" />.
		/// </returns>
		public static TSource FirstOrException<TSource, TException>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) where TSource : class where TException : Exception
		{
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(predicate, nameof(predicate));

			return source.FirstOrDefault(predicate) ?? throw Activator.CreateInstance<TException>();
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
		public static TValue ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
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
		public static TValue ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			Check.ArgumentNull(dictionary, nameof(dictionary));

			return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
		}

		/// <summary>
		/// Creates a <see cref="string" /> from this collection of characters.
		/// </summary>
		/// <param name="source">The collection of characters to create the <see cref="string" /> from.</param>
		/// <returns>
		/// A new <see cref="string" />, created from this collection of characters.
		/// </returns>
		public static string AsString(this IEnumerable<char> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return AsString(source.ToArray());
		}
		/// <summary>
		/// Creates a <see cref="string" /> from this <see cref="char" />[].
		/// </summary>
		/// <param name="source">The <see cref="char" />[] to create the <see cref="string" /> from.</param>
		/// <returns>
		/// A new <see cref="string" />, created from this <see cref="char" />[].
		/// </returns>
		public static string AsString(this char[] source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new string(source);
		}
		/// <summary>
		/// Creates a <see cref="string" /> from this collection of objects. All objects in <paramref name="source" /> are converted to <see cref="string" /> by calling <see cref="object.ToString" />. Then, the resulting strings are concatenated.
		/// </summary>
		/// <param name="source">The collection of objects to concatenate.</param>
		/// <returns>
		/// A new <see cref="string" />, created from this collection of objects, where <see cref="object.ToString" /> was called on each object prior to concatenation.
		/// </returns>
		public static string AsString(this IEnumerable<object> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return source.Select(itm => itm?.ToString()).AsString();
		}
		/// <summary>
		/// Creates a <see cref="string" /> from this collection of strings, by concatenating all values in <paramref name="source" />.
		/// </summary>
		/// <param name="source">The collection strings to concatenate.</param>
		/// <returns>
		/// A new <see cref="string" />, created from this collection of strings, where all values in <paramref name="source" /> are concatenated.
		/// </returns>
		public static string AsString(this IEnumerable<string> source)
		{
			return source.AsString(null);
		}
		/// <summary>
		/// Creates a <see cref="string" /> from this collection of strings, by concatenating all values in <paramref name="source" /> using the specified separator.
		/// </summary>
		/// <param name="source">The collection strings to concatenate.</param>
		/// <param name="separator">A <see cref="string" /> specifying the separator to concatenate the values in <paramref name="source" /> with, or <see langword="null" /> to not use a separator.</param>
		/// <returns>
		/// A new <see cref="string" />, created from this collection of strings, where all values in <paramref name="source" /> are concatenated using the specified separator.
		/// </returns>
		public static string AsString(this IEnumerable<string> source, string separator)
		{
			Check.ArgumentNull(source, nameof(source));

			return string.Join(separator, source);
		}
		/// <summary>
		/// Creates a multiline <see cref="string" /> from this collection of strings, by concatenating all values with a CRLF.
		/// </summary>
		/// <param name="source">The <see cref="string" />[] to concatenate.</param>
		/// <returns>
		/// A new <see cref="string" /> that represents this <see cref="string" /> collection as multiline text, where each element is concatenated with a CRLF.
		/// </returns>
		public static string AsMultilineString(this IEnumerable<string> source)
		{
			return source.AsMultilineString(false);
		}
		/// <summary>
		/// Creates a multiline <see cref="string" /> from this collection of strings, by concatenating all values with a CRLF.
		/// </summary>
		/// <param name="source">The <see cref="string" />[] to concatenate.</param>
		/// <param name="lastLine"><see langword="true" /> to add an additional CRLF at the end.</param>
		/// <returns>
		/// A new <see cref="string" /> that represents this <see cref="string" /> collection as multiline text, where each element is concatenated with a CRLF.
		/// </returns>
		public static string AsMultilineString(this IEnumerable<string> source, bool lastLine)
		{
			Check.ArgumentNull(source, nameof(source));

			return source.AsString("\r\n") + (lastLine ? "\r\n" : null);
		}

		/// <summary>
		/// Bypasses a specified number of elements at the end of a sequence and then returns the preceding elements.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}" /> to return elements from.</param>
		/// <param name="count">The number of elements to remove from the end of <paramref name="source" /> before returning the preceding elements.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> that contains the preceding elements that occur before the removed elements in the input sequence.
		/// </returns>
		public static IEnumerable<TSource> SkipLast<TSource>(this IEnumerable<TSource> source, int count)
		{
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));

			return source.Reverse().Skip(count).Reverse();
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
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(predicate, nameof(predicate));

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
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(predicate, nameof(predicate));

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
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(predicate, nameof(predicate));

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
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(predicate, nameof(predicate));

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
			Check.ArgumentNull(source, nameof(source));

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
		public static IEnumerable<TSource> ExceptNull<TSource>(this IEnumerable<TSource> source) where TSource : class
		{
			Check.ArgumentNull(source, nameof(source));

			return source.Where(itm => itm != null);
		}
		/// <summary>
		/// Returns all distinct elements of a sequence according to a specified key selector function.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}" /> to return elements from.</param>
		/// <param name="keySelector">A <see cref="Func{T, TResult}" /> to extract the key for each element.</param>
		/// <returns>
		/// An <see cref="IEnumerator{T}" /> with all distinct elements of <paramref name="source" />.
		/// </returns>
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.DistinctBy(keySelector, null);
		}
		/// <summary>
		/// Returns all distinct elements of a sequence according to a specified key selector function by using a specified <see cref="IEqualityComparer{T}" />.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}" /> to return elements from.</param>
		/// <param name="keySelector">A <see cref="Func{T, TResult}" /> to extract the key for each element.</param>
		/// <param name="comparer">An <see cref="IComparer{T}" /> to compare the elements.</param>
		/// <returns>
		/// An <see cref="IEnumerator{T}" /> with all distinct elements of <paramref name="source" />.
		/// </returns>
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(keySelector, nameof(keySelector));

			return source.GroupBy(keySelector, comparer).Select(x => x.First());
		}
		/// <summary>
		/// Concatenates a sequence and one element, where the single element is put after all elements of this sequence.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements to concatenate.</typeparam>
		/// <param name="first">The first sequence to concatenate.</param>
		/// <param name="second">The element to concatenate to the first sequence.</param>
		/// <returns>
		/// An <see cref="IEnumerator{T}" /> that contains the concatenated elements of the source
		/// </returns>
		public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, TSource second)
		{
			Check.ArgumentNull(first, nameof(first));

			return first.Concat(Singleton.List(second));
		}
		/// <summary>
		/// Produces the set union of a sequence and one element.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequence and the second element.</typeparam>
		/// <param name="first">An <see cref="IEnumerable{T}" /> whose distinct elements form the first set for the union.</param>
		/// <param name="second">The second element, which forms the second set for the union.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> that contains the elements from the input sequence and the second element, excluding duplicates.
		/// </returns>
		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, TSource second)
		{
			return first.Union(second, null);
		}
		/// <summary>
		/// Produces the set union of a sequence and one element by using a specified <see cref="IEqualityComparer{T}" />.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequence and the second element.</typeparam>
		/// <param name="first">An <see cref="IEnumerable{T}" /> whose distinct elements form the first set for the union.</param>
		/// <param name="second">The second element, which forms the second set for the union.</param>
		/// <param name="comparer">An <see cref="IComparer{T}" /> to compare the elements.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> that contains the elements from the input sequence and the second element, excluding duplicates.
		/// </returns>
		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, TSource second, IEqualityComparer<TSource> comparer)
		{
			Check.ArgumentNull(first, nameof(first));

			return first.Union(Singleton.List(second), comparer);
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
		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, TSource second, IEqualityComparer<TSource> comparer)
		{
			Check.ArgumentNull(first, nameof(first));

			return first.Except(Singleton.List(second), comparer);
		}
		/// <summary>
		/// Splits up a sequence into chunks with the specified size. Each chunk is an <see cref="IEnumerable{T}" />, containing a maximum number of elements according to <paramref name="chunkSize" />. The last chunk may contain less elements than specified <paramref name="chunkSize" />.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements to chunk.</typeparam>
		/// <param name="source">A sequence of values to chunk.</param>
		/// <param name="chunkSize">The chunk size. The last chunk may contain less elements than specified <paramref name="chunkSize" />. All previous chunks contain exactly the amount of chunks specified in <paramref name="chunkSize" />.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}" /> with an <see cref="IEnumerable{T}" /> for each returned chunk.
		/// </returns>
		public static IEnumerable<IEnumerable<TSource>> Chunk<TSource>(this IEnumerable<TSource> source, int chunkSize)
		{
			//IMPORTANT: Pulls everything into memory. Consider [][] or yield return
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentOutOfRangeEx.Greater0(chunkSize, nameof(chunkSize));

			return source
				.Select((itm, i) => new
				{
					Index = i,
					Value = itm
				})
				.GroupBy(itm => itm.Index / chunkSize)
				.Select(itm => itm.Select(itm2 => itm2.Value));
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
		public static IOrderedEnumerable<TSource> Sort<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
		{
			Check.ArgumentNull(source, nameof(source));

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
		public static IOrderedEnumerable<TSource> SortDescending<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
		{
			Check.ArgumentNull(source, nameof(source));

			return source.OrderByDescending(itm => itm, comparer);
		}
		/// <summary>
		/// Randomized the order of the elements of a sequence.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <param name="source">A sequence of values to sort.</param>
		/// <returns>
		/// An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted in randomized order.
		/// </returns>
		public static IOrderedEnumerable<TSource> SortRandom<TSource>(this IEnumerable<TSource> source)
		{
			Check.ArgumentNull(source, nameof(source));

			lock (MathEx._Random)
			{
				return source.OrderBy(itm => MathEx._Random.Next());
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
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(collection, nameof(collection));

			foreach (TSource item in collection) source.Add(item);
		}
		/// <summary>
		/// Removes all elements that satisfy a specified condition.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements to remove.</typeparam>
		/// <param name="source">The <see cref="ICollection{T}" /> to remove elements from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		public static void RemoveAll<TSource>(this ICollection<TSource> source, Func<TSource, bool> predicate)
		{
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(predicate, nameof(predicate));

			foreach (TSource item in source.Where(itm => predicate(itm)).ToList()) source.Remove(item);
		}
		/// <summary>
		/// Removes all elements that occur in the specified collection.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements to remove.</typeparam>
		/// <param name="source">The <see cref="ICollection{T}" /> to remove elements from.</param>
		/// <param name="collection">The <see cref="ICollection{T}" /> with all elements to remove from <paramref name="source" />.</param>
		public static void RemoveRange<TSource>(this ICollection<TSource> source, IEnumerable<TSource> collection)
		{
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(collection, nameof(collection));

			foreach (TSource item in collection.ToList()) source.Remove(item);
		}
		/// <summary>
		/// Performs the specified <see cref="Action" /> on each element of this <see cref="IEnumerable{T}" />.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements to process.</typeparam>
		/// <param name="source">A sequence of values to process.</param>
		/// <param name="action">The action to perform on each element of <paramref name="source" />.</param>
		public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			Check.ArgumentNull(source, nameof(source));
			Check.ArgumentNull(action, nameof(action));

			foreach (TSource item in source) action(item);
		}
	}
}