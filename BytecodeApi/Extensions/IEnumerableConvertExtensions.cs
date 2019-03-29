using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for conversion of <see cref="IEnumerable" /> objects to different collection types.
	/// </summary>
	public static class IEnumerableConvertExtensions
	{
		/// <summary>
		/// Converts this <see cref="IEnumerable" /> to an <see cref="Array" /> of the specified type.
		/// </summary>
		/// <typeparam name="TSource">The type of the returned <see cref="Array" />.</typeparam>
		/// <param name="source">The <see cref="IEnumerable" /> to convert.</param>
		/// <returns>
		/// A new <see cref="Array" /> that contains all values in this <see cref="IEnumerable" />.
		/// </returns>
		public static TSource[] ToArray<TSource>(this IEnumerable source)
		{
			Check.ArgumentNull(source, nameof(source));

			return source.Cast<TSource>().ToArray();
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable" /> to a <see cref="List{T}" /> of the specified type.
		/// </summary>
		/// <typeparam name="TSource">The type of the returned <see cref="List{T}" />.</typeparam>
		/// <param name="source">The <see cref="IEnumerable" /> to convert.</param>
		/// <returns>
		/// A new <see cref="List{T}" /> that contains all values in this <see cref="IEnumerable" />.
		/// </returns>
		public static List<TSource> ToList<TSource>(this IEnumerable source)
		{
			Check.ArgumentNull(source, nameof(source));

			return source.Cast<TSource>().ToList();
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable" /> to a <see cref="ReadOnlyCollection{T}" /> of the specified type.
		/// </summary>
		/// <typeparam name="TSource">The type of the returned <see cref="ReadOnlyCollection{T}" />.</typeparam>
		/// <param name="source">The <see cref="IEnumerable" /> to convert.</param>
		/// <returns>
		/// A new <see cref="ReadOnlyCollection{T}" /> that contains all values in this <see cref="IEnumerable" />.
		/// </returns>
		public static ReadOnlyCollection<TSource> ToReadOnlyCollection<TSource>(this IEnumerable<TSource> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return source.ToList().AsReadOnly();
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to a <see cref="Dictionary{TKey, TValue}" />.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the returned <see cref="Dictionary{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the returned <see cref="Dictionary{TKey, TValue}" />.</typeparam>
		/// <param name="source">The collection of <see cref="KeyValuePair{TKey, TValue}" /> objects to convert.</param>
		/// <returns>
		/// A new <see cref="Dictionary{TKey, TValue}" /> that contains all <see cref="KeyValuePair{TKey, TValue}" /> objects in this <see cref="IEnumerable" />.
		/// </returns>
		public static Dictionary<TKey, TValue> ToDictionary<TValue, TKey>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return source.ToDictionary(itm => itm.Key, itm => itm.Value);
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to a <see cref="Dictionary{TKey, TValue}" /> using the specified equality comparer.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the returned <see cref="Dictionary{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the returned <see cref="Dictionary{TKey, TValue}" />.</typeparam>
		/// <param name="source">The collection of <see cref="KeyValuePair{TKey, TValue}" /> objects to convert.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}" /> implementation to use when creating the new <see cref="Dictionary{TKey, TValue}" />.</param>
		/// <returns>
		/// A new <see cref="Dictionary{TKey, TValue}" /> that contains all <see cref="KeyValuePair{TKey, TValue}" /> objects in this <see cref="IEnumerable" />.
		/// </returns>
		public static Dictionary<TKey, TValue> ToDictionary<TValue, TKey>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> comparer)
		{
			Check.ArgumentNull(source, nameof(source));

			return source.ToDictionary(itm => itm.Key, itm => itm.Value, comparer);
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to a <see cref="Dictionary{TKey, TValue}" />, where Item1 of the <see cref="Tuple{T1, T2}" /> is representing the key and Item2 is representing the value.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the returned <see cref="Dictionary{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the returned <see cref="Dictionary{TKey, TValue}" />.</typeparam>
		/// <param name="source">The collection of <see cref="Tuple{T1, T2}" /> objects to convert, where Item1 is representing the key and Item2 is representing the value.</param>
		/// <returns>
		/// A new <see cref="Dictionary{TKey, TValue}" /> that contains all converted <see cref="Tuple{T1, T2}" /> objects in this <see cref="IEnumerable" />.
		/// </returns>
		public static Dictionary<TKey, TValue> ToDictionary<TValue, TKey>(this IEnumerable<Tuple<TKey, TValue>> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return source.ToDictionary(itm => itm.Item1, itm => itm.Item2);
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to a <see cref="Dictionary{TKey, TValue}" />using the specified equality comparer, where Item1 of the <see cref="Tuple{T1, T2}" /> is representing the key and Item2 is representing the value.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the returned <see cref="Dictionary{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the returned <see cref="Dictionary{TKey, TValue}" />.</typeparam>
		/// <param name="source">The collection of <see cref="Tuple{T1, T2}" /> objects to convert, where Item1 is representing the key and Item2 is representing the value.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}" /> implementation to use when creating the new <see cref="Dictionary{TKey, TValue}" />.</param>
		/// <returns>
		/// A new <see cref="Dictionary{TKey, TValue}" /> that contains all converted <see cref="Tuple{T1, T2}" /> objects in this <see cref="IEnumerable" />.
		/// </returns>
		public static Dictionary<TKey, TValue> ToDictionary<TValue, TKey>(this IEnumerable<Tuple<TKey, TValue>> source, IEqualityComparer<TKey> comparer)
		{
			Check.ArgumentNull(source, nameof(source));

			return source.ToDictionary(itm => itm.Item1, itm => itm.Item2, comparer);
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to a <see cref="ReadOnlyDictionary{TKey, TValue}" />.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the returned <see cref="ReadOnlyDictionary{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the returned <see cref="ReadOnlyDictionary{TKey, TValue}" />.</typeparam>
		/// <param name="source">The collection of <see cref="KeyValuePair{TKey, TValue}" /> objects to convert.</param>
		/// <returns>
		/// A new <see cref="ReadOnlyDictionary{TKey, TValue}" /> that contains all <see cref="KeyValuePair{TKey, TValue}" /> objects in this <see cref="IEnumerable" />.
		/// </returns>
		public static ReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TValue, TKey>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new ReadOnlyDictionary<TKey, TValue>(source.ToDictionary());
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to a <see cref="ReadOnlyDictionary{TKey, TValue}" /> using the specified equality comparer.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the returned <see cref="ReadOnlyDictionary{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the returned <see cref="ReadOnlyDictionary{TKey, TValue}" />.</typeparam>
		/// <param name="source">The collection of <see cref="KeyValuePair{TKey, TValue}" /> objects to convert.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}" /> implementation to use when creating the new <see cref="ReadOnlyDictionary{TKey, TValue}" />.</param>
		/// <returns>
		/// A new <see cref="ReadOnlyDictionary{TKey, TValue}" /> that contains all <see cref="KeyValuePair{TKey, TValue}" /> objects in this <see cref="IEnumerable" />.
		/// </returns>
		public static ReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TValue, TKey>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> comparer)
		{
			Check.ArgumentNull(source, nameof(source));

			return new ReadOnlyDictionary<TKey, TValue>(source.ToDictionary(comparer));
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to a <see cref="ReadOnlyDictionary{TKey, TValue}" />, where Item1 of the <see cref="Tuple{T1, T2}" /> is representing the key and Item2 is representing the value.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the returned <see cref="ReadOnlyDictionary{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the returned <see cref="ReadOnlyDictionary{TKey, TValue}" />.</typeparam>
		/// <param name="source">The collection of <see cref="Tuple{T1, T2}" /> objects to convert, where Item1 is representing the key and Item2 is representing the value.</param>
		/// <returns>
		/// A new <see cref="ReadOnlyDictionary{TKey, TValue}" /> that contains all converted <see cref="Tuple{T1, T2}" /> objects in this <see cref="IEnumerable" />.
		/// </returns>
		public static ReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TValue, TKey>(this IEnumerable<Tuple<TKey, TValue>> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new ReadOnlyDictionary<TKey, TValue>(source.ToDictionary());
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to a <see cref="ReadOnlyDictionary{TKey, TValue}" />using the specified equality comparer, where Item1 of the <see cref="Tuple{T1, T2}" /> is representing the key and Item2 is representing the value.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the returned <see cref="ReadOnlyDictionary{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the returned <see cref="ReadOnlyDictionary{TKey, TValue}" />.</typeparam>
		/// <param name="source">The collection of <see cref="Tuple{T1, T2}" /> objects to convert, where Item1 is representing the key and Item2 is representing the value.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}" /> implementation to use when creating the new <see cref="ReadOnlyDictionary{TKey, TValue}" />.</param>
		/// <returns>
		/// A new <see cref="ReadOnlyDictionary{TKey, TValue}" /> that contains all converted <see cref="Tuple{T1, T2}" /> objects in this <see cref="IEnumerable" />.
		/// </returns>
		public static ReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TValue, TKey>(this IEnumerable<Tuple<TKey, TValue>> source, IEqualityComparer<TKey> comparer)
		{
			Check.ArgumentNull(source, nameof(source));

			return new ReadOnlyDictionary<TKey, TValue>(source.ToDictionary(comparer));
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable" /> to a <see cref="HashSet{T}" /> of the specified type.
		/// </summary>
		/// <typeparam name="TSource">The type of the returned <see cref="HashSet{T}" />.</typeparam>
		/// <param name="source">The <see cref="IEnumerable" /> to convert.</param>
		/// <returns>
		/// A new <see cref="HashSet{T}" /> that contains all values in this <see cref="IEnumerable" />.
		/// </returns>
		public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new HashSet<TSource>(source.Cast<TSource>());
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable" /> to a <see cref="HashSet{T}" /> of the specified type using the specified equality comparer.
		/// </summary>
		/// <typeparam name="TSource">The type of the returned <see cref="HashSet{T}" />.</typeparam>
		/// <param name="source">The <see cref="IEnumerable" /> to convert.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}" /> implementation to use when creating the new <see cref="HashSet{T}" />.</param>
		/// <returns>
		/// A new <see cref="HashSet{T}" /> that contains all values in this <see cref="IEnumerable" />.
		/// </returns>
		public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable source, IEqualityComparer<TSource> comparer)
		{
			Check.ArgumentNull(source, nameof(source));

			return new HashSet<TSource>(source.Cast<TSource>(), comparer);
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to a <see cref="HashSet{T}" />.
		/// </summary>
		/// <typeparam name="TSource">The type of the <see cref="IEnumerable{T}" />.</typeparam>
		/// <param name="source">The <see cref="IEnumerable{T}" /> to convert.</param>
		/// <returns>
		/// A new <see cref="HashSet{T}" /> that contains all values in this <see cref="IEnumerable" />.
		/// </returns>
		public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new HashSet<TSource>(source);
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to a <see cref="HashSet{T}" /> using the specified equality comparer.
		/// </summary>
		/// <typeparam name="TSource">The type of the <see cref="IEnumerable{T}" />.</typeparam>
		/// <param name="source">The <see cref="IEnumerable{T}" /> to convert.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}" /> implementation to use when creating the new <see cref="HashSet{T}" />.</param>
		/// <returns>
		/// A new <see cref="HashSet{T}" /> that contains all values in this <see cref="IEnumerable" />.
		/// </returns>
		public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			Check.ArgumentNull(source, nameof(source));

			return new HashSet<TSource>(source, comparer);
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable" /> to an <see cref="ObservableCollection{T}" /> of the specified type.
		/// </summary>
		/// <typeparam name="TSource">The type of the returned <see cref="ObservableCollection{T}" />.</typeparam>
		/// <param name="source">The <see cref="IEnumerable" /> to convert.</param>
		/// <returns>
		/// A new <see cref="ObservableCollection{T}" /> that contains all values in this <see cref="IEnumerable" />.
		/// </returns>
		public static ObservableCollection<TSource> ToObservableCollection<TSource>(this IEnumerable source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new ObservableCollection<TSource>(source.Cast<TSource>());
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to an <see cref="ObservableCollection{T}" />.
		/// </summary>
		/// <typeparam name="TSource">The type of the <see cref="IEnumerable{T}" />.</typeparam>
		/// <param name="source">The <see cref="IEnumerable{T}" /> to convert.</param>
		/// <returns>
		/// A new <see cref="ObservableCollection{T}" /> that contains all values in this <see cref="IEnumerable{T}" />.
		/// </returns>
		public static ObservableCollection<TSource> ToObservableCollection<TSource>(this IEnumerable<TSource> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new ObservableCollection<TSource>(source);
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable" /> to an <see cref="System.Collections.ObjectModel.ReadOnlyObservableCollection{T}" /> of the specified type.
		/// </summary>
		/// <typeparam name="TSource">The type of the returned <see cref="System.Collections.ObjectModel.ReadOnlyObservableCollection{T}" />.</typeparam>
		/// <param name="source">The <see cref="IEnumerable" /> to convert.</param>
		/// <returns>
		/// A new <see cref="System.Collections.ObjectModel.ReadOnlyObservableCollection{T}" /> that contains all values in this <see cref="IEnumerable" />.
		/// </returns>
		public static ReadOnlyObservableCollection<TSource> ToReadOnlyObservableCollection<TSource>(this IEnumerable source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new ReadOnlyObservableCollection<TSource>(source.ToObservableCollection<TSource>());
		}
		/// <summary>
		/// Converts this <see cref="IEnumerable{T}" /> to an <see cref="System.Collections.ObjectModel.ReadOnlyObservableCollection{T}" />.
		/// </summary>
		/// <typeparam name="TSource">The type of the <see cref="IEnumerable{T}" />.</typeparam>
		/// <param name="source">The <see cref="IEnumerable{T}" /> to convert.</param>
		/// <returns>
		/// A new <see cref="System.Collections.ObjectModel.ReadOnlyObservableCollection{T}" /> that contains all values in this <see cref="IEnumerable{T}" />.
		/// </returns>
		public static ReadOnlyObservableCollection<TSource> ToReadOnlyObservableCollection<TSource>(this IEnumerable<TSource> source)
		{
			Check.ArgumentNull(source, nameof(source));

			return new ReadOnlyObservableCollection<TSource>(source.ToObservableCollection());
		}
	}
}