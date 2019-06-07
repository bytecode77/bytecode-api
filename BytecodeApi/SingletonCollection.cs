using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BytecodeApi
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for creation of collection types containing one item.
	/// </summary>
	public static class SingletonCollection
	{
		/// <summary>
		/// Creates an <see cref="System.Array" /> with <paramref name="obj" /> as the single element.
		/// </summary>
		/// <typeparam name="TSource">The type of the <see cref="object" /> to create the <see cref="System.Array" /> from.</typeparam>
		/// <param name="obj">The <see cref="object" /> to be inserted into the new <see cref="System.Array" />.</param>
		/// <returns>
		/// A new <see cref="System.Array" /> with <paramref name="obj" /> as the single element.
		/// </returns>
		public static TSource[] Array<TSource>(TSource obj)
		{
			return new[] { obj };
		}
		/// <summary>
		/// Creates a <see cref="System.Collections.Generic.List{T}" /> with <paramref name="obj" /> as the single element.
		/// </summary>
		/// <typeparam name="TSource">The type of the <see cref="object" /> to create the <see cref="System.Collections.Generic.List{T}" /> from.</typeparam>
		/// <param name="obj">The <see cref="object" /> to be inserted into the new <see cref="System.Collections.Generic.List{T}" />.</param>
		/// <returns>
		/// A new <see cref="System.Collections.Generic.List{T}" /> with <paramref name="obj" /> as the single element.
		/// </returns>
		public static List<TSource> List<TSource>(TSource obj)
		{
			return new List<TSource> { obj };
		}
		/// <summary>
		/// Creates a <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" /> with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <param name="keyValuePair">The <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" />.</param>
		/// <returns>
		/// A new <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" /> with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </returns>
		public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(KeyValuePair<TKey, TValue> keyValuePair)
		{
			return Dictionary(keyValuePair.Key, keyValuePair.Value);
		}
		/// <summary>
		/// Creates a <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" /> that uses the specified equality comparer for the set type with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <param name="keyValuePair">The <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" />.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}" /> implementation to use when comparing values in the set, or <see langword="null" /> to use the default <see cref="IEqualityComparer{T}" /> implementation for the set type.</param>
		/// <returns>
		/// A new <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" /> with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </returns>
		public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(KeyValuePair<TKey, TValue> keyValuePair, IEqualityComparer<TKey> comparer)
		{
			return Dictionary(keyValuePair.Key, keyValuePair.Value, comparer);
		}
		/// <summary>
		/// Creates a <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" /> with the <see cref="KeyValuePair{TKey, TValue}" /> from the <paramref name="key" /> and <paramref name="value" /> parameters as the single element.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <param name="key">The key for the <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" />.</param>
		/// <param name="value">The value for the <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" />.</param>
		/// <returns>
		/// A new <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" /> with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </returns>
		public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(TKey key, TValue value)
		{
			return Dictionary(key, value, null);
		}
		/// <summary>
		/// Creates a <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" /> that uses the specified equality comparer for the set type with the <see cref="KeyValuePair{TKey, TValue}" /> from the <paramref name="key" /> and <paramref name="value" /> parameters as the single element.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <param name="key">The key for the <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" />.</param>
		/// <param name="value">The value for the <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" />.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}" /> implementation to use when comparing values in the set, or <see langword="null" /> to use the default <see cref="IEqualityComparer{T}" /> implementation for the set type.</param>
		/// <returns>
		/// A new <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" /> with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </returns>
		public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(TKey key, TValue value, IEqualityComparer<TKey> comparer)
		{
			return new Dictionary<TKey, TValue>(comparer) { [key] = value };
		}
		/// <summary>
		/// Creates a <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" /> with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <param name="keyValuePair">The <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" />.</param>
		/// <returns>
		/// A new <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" /> with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </returns>
		public static ReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(KeyValuePair<TKey, TValue> keyValuePair)
		{
			return ReadOnlyDictionary(keyValuePair.Key, keyValuePair.Value);
		}
		/// <summary>
		/// Creates a <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" /> that uses the specified equality comparer for the set type with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <param name="keyValuePair">The <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" />.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}" /> implementation to use when comparing values in the set, or <see langword="null" /> to use the default <see cref="IEqualityComparer{T}" /> implementation for the set type.</param>
		/// <returns>
		/// A new <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" /> with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </returns>
		public static ReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(KeyValuePair<TKey, TValue> keyValuePair, IEqualityComparer<TKey> comparer)
		{
			return ReadOnlyDictionary(keyValuePair.Key, keyValuePair.Value, comparer);
		}
		/// <summary>
		/// Creates a <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" /> with the <see cref="KeyValuePair{TKey, TValue}" /> from the <paramref name="key" /> and <paramref name="value" /> parameters as the single element.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <param name="key">The key for the <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" />.</param>
		/// <param name="value">The value for the <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" />.</param>
		/// <returns>
		/// A new <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" /> with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </returns>
		public static ReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(TKey key, TValue value)
		{
			return ReadOnlyDictionary(key, value, null);
		}
		/// <summary>
		/// Creates a <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" /> that uses the specified equality comparer for the set type with the <see cref="KeyValuePair{TKey, TValue}" /> from the <paramref name="key" /> and <paramref name="value" /> parameters as the single element.
		/// </summary>
		/// <typeparam name="TKey">The type of the key of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <typeparam name="TValue">The type of the value of the specified <see cref="KeyValuePair{TKey, TValue}" />.</typeparam>
		/// <param name="key">The key for the <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" />.</param>
		/// <param name="value">The value for the <see cref="KeyValuePair{TKey, TValue}" /> to be inserted into the new <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" />.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}" /> implementation to use when comparing values in the set, or <see langword="null" /> to use the default <see cref="IEqualityComparer{T}" /> implementation for the set type.</param>
		/// <returns>
		/// A new <see cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}" /> with the specified <see cref="KeyValuePair{TKey, TValue}" /> as the single element.
		/// </returns>
		public static ReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(TKey key, TValue value, IEqualityComparer<TKey> comparer)
		{
			return new ReadOnlyDictionary<TKey, TValue>(Dictionary(key, value, comparer));
		}
		/// <summary>
		/// Creates a <see cref="System.Collections.Generic.HashSet{T}" /> with <paramref name="obj" /> as the single element.
		/// </summary>
		/// <typeparam name="TSource">The type of the <see cref="object" /> to create the <see cref="System.Collections.Generic.HashSet{T}" /> from.</typeparam>
		/// <param name="obj">The <see cref="object" /> to be inserted into the new <see cref="System.Collections.Generic.HashSet{T}" />.</param>
		/// <returns>
		/// A new <see cref="System.Collections.Generic.HashSet{T}" /> with <paramref name="obj" /> as the single element.
		/// </returns>
		public static HashSet<TSource> HashSet<TSource>(TSource obj)
		{
			return HashSet(obj, null);
		}
		/// <summary>
		/// Creates a <see cref="System.Collections.Generic.HashSet{T}" /> that uses the specified equality comparer for the set type with <paramref name="obj" /> as the single element.
		/// </summary>
		/// <typeparam name="TSource">The type of the <see cref="object" /> to create the <see cref="System.Collections.Generic.HashSet{T}" /> from.</typeparam>
		/// <param name="obj">The <see cref="object" /> to be inserted into the new <see cref="System.Collections.Generic.HashSet{T}" />.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}" /> implementation to use when comparing values in the set, or <see langword="null" /> to use the default <see cref="IEqualityComparer{T}" /> implementation for the set type.</param>
		/// <returns>
		/// A new <see cref="System.Collections.Generic.HashSet{T}" /> with <paramref name="obj" /> as the single element.
		/// </returns>
		public static HashSet<TSource> HashSet<TSource>(TSource obj, IEqualityComparer<TSource> comparer)
		{
			return new HashSet<TSource>(comparer) { obj };
		}
		/// <summary>
		/// Creates an <see cref="System.Collections.ObjectModel.ObservableCollection{T}" /> with <paramref name="obj" /> as the single element.
		/// </summary>
		/// <typeparam name="TSource">The type of the <see cref="object" /> to create the <see cref="System.Collections.ObjectModel.ObservableCollection{T}" /> from.</typeparam>
		/// <param name="obj">The <see cref="object" /> to be inserted into the new <see cref="System.Collections.ObjectModel.ObservableCollection{T}" />.</param>
		/// <returns>
		/// A new <see cref="System.Collections.ObjectModel.ObservableCollection{T}" /> with <paramref name="obj" /> as the single element.
		/// </returns>
		public static ObservableCollection<TSource> ObservableCollection<TSource>(TSource obj)
		{
			return new ObservableCollection<TSource> { obj };
		}
		/// <summary>
		/// Creates an <see cref="System.Collections.ObjectModel.ReadOnlyObservableCollection{T}" /> with <paramref name="obj" /> as the single element.
		/// </summary>
		/// <typeparam name="TSource">The type of the <see cref="object" /> to create the <see cref="System.Collections.ObjectModel.ReadOnlyObservableCollection{T}" /> from.</typeparam>
		/// <param name="obj">The <see cref="object" /> to be inserted into the new <see cref="System.Collections.ObjectModel.ReadOnlyObservableCollection{T}" />.</param>
		/// <returns>
		/// A new <see cref="System.Collections.ObjectModel.ReadOnlyObservableCollection{T}" /> with <paramref name="obj" /> as the single element.
		/// </returns>
		public static ReadOnlyObservableCollection<TSource> ReadOnlyObservableCollection<TSource>(TSource obj)
		{
			return new ReadOnlyObservableCollection<TSource>(ObservableCollection(obj));
		}
	}
}