using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="TimeOnly" /> objects.
/// </summary>
public static class TimeOnlyExtensions
{
	extension(TimeOnly timeOnly)
	{
		/// <summary>
		/// Gets a <see cref="TimeOnly" /> object that is set to the current date and time on this computer.
		/// </summary>
		public static TimeOnly Now => DateTime.Now.ToTimeOnly();
		/// <summary>
		/// Gets a <see cref="TimeOnly" /> object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).
		/// </summary>
		public static TimeOnly UtcNow => DateTime.UtcNow.ToTimeOnly();

		/// <summary>
		/// Converts the value of this <see cref="TimeOnly" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="TimeOnly" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="TimeOnly" />.
		/// </returns>
		public string ToStringInvariant(string format)
		{
			return timeOnly.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="TimeOnly" /> object is <see langword="default" />(<see cref="TimeOnly" />), otherwise its original value.
		/// </summary>
		/// <returns>
		/// <see langword="null" />, if this <see cref="TimeOnly" /> object is <see langword="default" />(<see cref="TimeOnly" />);
		/// otherwise, its original value.
		/// </returns>
		public TimeOnly? ToNullIfDefault()
		{
			return timeOnly == default ? null : timeOnly;
		}
	}
}