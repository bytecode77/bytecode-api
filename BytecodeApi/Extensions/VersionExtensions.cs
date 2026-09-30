namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Version" /> objects.
/// </summary>
public static class VersionExtensions
{
	extension(Version version)
	{
		/// <summary>
		/// Determines whether this instance is equal to another <see cref="Version" />.
		/// </summary>
		/// <param name="other">The <see cref="Version" /> to compare to this instance.</param>
		/// <param name="fieldCount">The number of fields to compare.</param>
		/// <returns>
		/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(Version other, int fieldCount)
		{
			Check.ArgumentOutOfRange(fieldCount is >= 0 and <= 4, nameof(fieldCount), "Field count must be between 0 and 4.");

			return version.CompareTo(other, fieldCount) == 0;
		}
		/// <summary>
		/// Compares this instance to a specified <see cref="Version" /> and returns a comparison of their relative values.
		/// </summary>
		/// <param name="other">A <see cref="Version" /> to compare with this instance.</param>
		/// <param name="fieldCount">The number of fields to compare.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared.
		/// </returns>
		public int CompareTo(Version other, int fieldCount)
		{
			Check.ArgumentOutOfRange(fieldCount is >= 0 and <= 4, nameof(fieldCount), "Field count must be between 0 and 4.");

			int result;

			if (fieldCount >= 1 && (result = version.Major.CompareTo(other.Major)) != 0) return result;
			if (fieldCount >= 2 && (result = version.Minor.CompareTo(other.Minor)) != 0) return result;
			if (fieldCount >= 3 && (result = version.Build.CompareTo(other.Build)) != 0) return result;
			if (fieldCount >= 4 && (result = version.Revision.CompareTo(other.Revision)) != 0) return result;

			return 0;
		}
	}
}