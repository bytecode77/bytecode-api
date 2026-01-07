using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="bool" /> objects.
/// </summary>
public static class BooleanExtensions
{
	extension(bool value)
	{
		/// <summary>
		/// Converts the value of this <see cref="bool" /> to its equivalent <see cref="string" /> representation using the invariant culture.
		/// </summary>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="bool" />.
		/// </returns>
		public string ToStringInvariant()
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="bool" /> value is <see langword="false" />, otherwise its original value.
		/// </summary>
		/// <returns>
		/// <see langword="null" />, if this <see cref="bool" /> value is <see langword="false" />;
		/// otherwise, its original value.
		/// </returns>
		public bool? ToNullIfDefault()
		{
			return value != default ? value : null;
		}
	}
}