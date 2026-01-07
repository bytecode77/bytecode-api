namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Array" /> objects.
/// </summary>
public static class ArrayExtensions
{
	/// <summary>
	/// Indicates whether this <see cref="Array" /> is <see langword="null" />, has no elements, or all elements are equal to <see langword="null" />.
	/// </summary>
	/// <param name="array">The <see cref="Array" /> to test.</param>
	/// <returns>
	/// <see langword="true" />, if this <see cref="Array" /> is <see langword="null" />, has no elements, or all elements are equal to <see langword="null" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool IsNullOrEmpty([NotNullWhen(false)] this Array? array)
	{
		return array == null || array.Length == 0 || array.Cast<object?>().All(item => item == null);
	}
	/// <summary>
	/// Returns this <see cref="Array" />, or <see langword="null" />, if this <see cref="Array" /> is <see langword="null" />, has no elements, or all elements are equal to <see langword="null" />.
	/// </summary>
	/// <typeparam name="T">The type of the elements of <paramref name="array" />.</typeparam>
	/// <param name="array">The <see cref="Array" /> to test.</param>
	/// <returns>
	/// This <see cref="Array" />, or <see langword="null" />, if this <see cref="Array" /> is <see langword="null" />, has no elements, or all elements are equal to <see langword="null" />.
	/// </returns>
	public static T[]? ToNullIfEmpty<T>(this T[]? array)
	{
		if (array == null || array.Length == 0 || array.All(item => item == null))
		{
			return null;
		}
		else
		{
			return array;
		}
	}
}