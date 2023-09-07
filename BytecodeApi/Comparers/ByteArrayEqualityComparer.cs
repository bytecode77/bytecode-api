using BytecodeApi.Extensions;

namespace BytecodeApi.Comparers;

/// <summary>
/// Represents an equality comparison operation that compares the contents of <see cref="byte" />[] objects.
/// </summary>
public sealed class ByteArrayEqualityComparer : IEqualityComparer<byte[]>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteArrayEqualityComparer" /> class.
	/// </summary>
	public ByteArrayEqualityComparer()
	{
	}

	/// <summary>
	/// Determines whether the specified <see cref="byte" />[] objects have the same data.
	/// </summary>
	/// <param name="x">The first <see cref="byte" />[] object to compare.</param>
	/// <param name="y">The second <see cref="byte" />[] object to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="byte" />[] objects have the same data or if <paramref name="x" /> and <paramref name="y" /> are both <see langword="null" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(byte[]? x, byte[]? y)
	{
		return x == null && y == null || x?.Compare(y) == true;
	}
	/// <summary>
	/// Returns a hash code for the <see cref="byte" />[].
	/// </summary>
	/// <returns>
	/// The hash code for the <see cref="byte" />[] instance.
	/// </returns>
	public int GetHashCode([DisallowNull] byte[] obj)
	{
		return obj?.GetHashCode() ?? 0;
	}
}