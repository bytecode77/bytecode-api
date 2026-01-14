using System.ComponentModel;
using System.Reflection;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see langword="enum" /> objects.
/// </summary>
public static class EnumExtensions
{
	extension(Enum)
	{
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

			return Enum.GetValues(enumType).Cast<Enum>().Distinct().ToDictionary(item => item, item => item.GetDescription());
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
			return Enum.GetValues<T>().Distinct().ToDictionary(item => item, item => item.GetDescription());
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
			return Enum.GetValues<T>().FirstOrNull(value => value.GetDescription() == description);
		}
	}

	extension(Enum value)
	{
		/// <summary>
		/// Returns the <see cref="DescriptionAttribute.Description" /> of this <see langword="enum" /> value, or <see langword="null" />, if the attribute was not found.
		/// </summary>
		/// <returns>
		/// The <see cref="DescriptionAttribute.Description" /> of this <see langword="enum" /> value, or <see langword="null" />, if the attribute was not found.
		/// </returns>
		public string? GetDescription()
		{
			Check.ArgumentNull(value);

			return value
				.GetType()
				.GetField(value.ToString())
				?.GetCustomAttribute<DescriptionAttribute>()
				?.Description;
		}
	}
}