using BytecodeApi.Extensions;
using System.ComponentModel;

namespace BytecodeApi;

/// <summary>
/// Provides <see langword="static" /> methods that extend the <see cref="Enum" /> class.
/// </summary>
public static class EnumEx
{
	/// <summary>
	/// Returns an array of the values of the constants in the specified <see cref="Enum" /> type.
	/// </summary>
	/// <param name="enumType">The type of the <see cref="Enum" /> to be processed.</param>
	/// <returns>
	/// A new array of the values of the constants in the specified <see cref="Enum" /> type.
	/// </returns>
	public static Enum[] GetValues(Type enumType)
	{
		Check.ArgumentNull(enumType);
		Check.Argument(enumType.IsEnum, nameof(enumType), "Type provided must be an Enum.");

		return Enum.GetValues(enumType).ToArray<Enum>();
	}
	/// <summary>
	/// Returns an array of the values of the constants in the specified <see cref="Enum" /> type, casted to <typeparamref name="T" />.
	/// </summary>
	/// <typeparam name="T">The element type of the returned <see cref="Array" />.</typeparam>
	/// <returns>
	/// A new array of the values of the constants in the specified <see cref="Enum" /> type, casted to <typeparamref name="T" />.
	/// </returns>
	public static T[] GetValues<T>() where T : struct, Enum
	{
		return Enum.GetValues(typeof(T)).ToArray<T>();
	}
	/// <summary>
	/// Gets a lookup <see cref="Dictionary{TKey, TValue}" /> with all distinct <see langword="enum" /> values and descriptions for the specified <see cref="Enum" /> type. The description is taken from the <see cref="DescriptionAttribute.Description" /> attribute, or <see langword="null" />, if the attribute was not found.
	/// </summary>
	/// <param name="enumType">The type of the <see cref="Enum" /> to be processed.</param>
	/// <returns>
	/// A new <see cref="Dictionary{TKey, TValue}" /> with all <see langword="enum" /> values and descriptions for the specified <see cref="Enum" /> type.
	/// </returns>
	public static Dictionary<Enum, string?> GetDescriptionLookup(Type enumType)
	{
		Check.ArgumentNull(enumType);
		Check.Argument(enumType.IsEnum, nameof(enumType), "Type provided must be an Enum.");

		return GetValues(enumType).Distinct().ToDictionary(item => item, item => item.GetDescription());
	}
	/// <summary>
	/// Gets a lookup <see cref="Dictionary{TKey, TValue}" /> with all distinct <see langword="enum" /> values and descriptions for the specified <see cref="Enum" /> type. The description is taken from the <see cref="DescriptionAttribute.Description" /> attribute, or <see langword="null" />, if the attribute was not found.
	/// </summary>
	/// <typeparam name="T">The type of the <see cref="Enum" /> to be processed.</typeparam>
	/// <returns>
	/// A new <see cref="Dictionary{TKey, TValue}" /> with all <see langword="enum" /> values and descriptions for the specified <see cref="Enum" /> type.
	/// </returns>
	public static Dictionary<T, string?> GetDescriptionLookup<T>() where T : struct, Enum
	{
		return GetValues<T>().Distinct().ToDictionary(item => item, item => item.GetDescription());
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
	public static T? FindValueByDescription<T>(string? description) where T : struct, Enum
	{
		return GetValues(typeof(T)).FirstOrDefault(value => value.GetDescription() == description) as T?;
	}
}