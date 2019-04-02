using BytecodeApi.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BytecodeApi
{
	/// <summary>
	/// Proides <see langword="static" /> methods and properties serving as a general object manipulation helper class.
	/// </summary>
	public static class EnumEx
	{
		/// <summary>
		/// Retrieves an array of the values of the constants in the specified <see langword="enum" /> value, casted to <typeparamref name="T" />.
		/// </summary>
		/// <typeparam name="T">The element type of the returned <see cref="Array" />.</typeparam>
		/// <returns>
		/// A new array of the values of the constants in the specified <see langword="enum" /> value, casted to <typeparamref name="T" />.
		/// </returns>
		public static T[] GetValues<T>() where T : struct
		{
			return Enum.GetValues(typeof(T)).ToArray<T>();
		}
		/// <summary>
		/// Gets a lookup <see cref="Dictionary{TKey, TValue}" /> with all distinct <see langword="enum" /> values and descriptions for the specified enum type. The description is taken from the <see cref="DescriptionAttribute.Description" /> attribute. If the attribute was not found, the <see cref="string" /> representation of this <see langword="enum" /> is used.
		/// </summary>
		/// <typeparam name="T">The type of the <see cref="Enum" /> to be processed.</typeparam>
		/// <returns>
		/// A new <see cref="Dictionary{TKey, TValue}" /> with all <see langword="enum" /> values and descriptions for the specified enum type.
		/// </returns>
		public static Dictionary<T, string> GetDescriptionLookup<T>() where T : struct
		{
			return GetValues<T>().Distinct().ToDictionary(item => item, item => (item as Enum).GetDescription());
		}
		/// <summary>
		/// Tries to find an <see langword="enum" /> value by the description found in the <see cref="DescriptionAttribute" /> attribute of the <see langword="enum" /> value. If the <see langword="enum" /> value was not found, <see langword="null" /> is returned.
		/// </summary>
		/// <typeparam name="T">The element type of the returned <see cref="Enum" />.</typeparam>
		/// <param name="description">The description that is searched in the <see cref="DescriptionAttribute.Description" /> property.</param>
		/// <returns>
		/// The <see langword="enum" /> value, if found;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static T? FindValueByDescription<T>(string description) where T : struct
		{
			return Enum
				.GetValues(typeof(T))
				.Cast<Enum>()
				.FirstOrDefault(value => value.GetDescription() == description) as T?;
		}
	}
}