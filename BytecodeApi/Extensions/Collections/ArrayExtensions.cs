using System;
using System.Linq;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Array" /> objects.
	/// </summary>
	public static class ArrayExtensions
	{
		/// <summary>
		/// Indicates whether this <see cref="Array" /> is <see langword="null" />, has no elements or all elements are equal to <see langword="null" />.
		/// </summary>
		/// <param name="array">The <see cref="Array" /> to test.</param>
		/// <returns>
		/// <see langword="true" />, if this <see cref="Array" /> is <see langword="null" />, has no elements or all elements are equal to <see langword="null" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsNullOrEmpty(this Array array)
		{
			return array == null || array.Length == 0 || array.Cast<object>().All(item => item == null);
		}
	}
}