using System;
using System.Globalization;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Guid" /> objects.
	/// </summary>
	public static class GuidExtensions
	{
		/// <summary>
		/// Returns a <see cref="string" /> representation of the value of this <see cref="Guid" /> instance, according to the provided format specifier.
		/// </summary>
		/// <param name="guid">The unique identifier.</param>
		/// <param name="format">The <see cref="GuidFormat" /> that is used as a format specifier.</param>
		/// <returns>
		/// The value of this <see cref="Guid" /> represented as a <see cref="string" /> in the specified format.
		/// </returns>
		public static string ToString(this Guid guid, GuidFormat format)
		{
			return guid.ToString(((char)format).ToString(), CultureInfo.InvariantCulture);
		}
	}
}