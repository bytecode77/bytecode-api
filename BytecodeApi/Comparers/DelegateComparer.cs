using System.Collections;

namespace BytecodeApi.Comparers;

/// <summary>
/// Represents a comparison operation that compares objects using a specified <see cref="Comparison{T}" /> delegate.
/// </summary>
/// <typeparam name="T">The type of objects to compare.</typeparam>
public sealed class DelegateComparer<T> : IComparer, IComparer<T>
{
	/// <summary>
	/// The <see cref="Comparison{T}" /> delegate that is used to compare objects.
	/// </summary>
	public Comparison<T> Comparison { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="DelegateComparer{T}" /> class with the specified <see cref="Comparison{T}" /> delegate.
	/// </summary>
	/// <param name="comparison">The <see cref="Comparison{T}" /> delegate that is used to compare objects.</param>
	public DelegateComparer(Comparison<T> comparison)
	{
		Check.ArgumentNull(comparison);

		Comparison = comparison;
	}

	/// <summary>
	/// Compares two objects and returns an indication of their relative sort order. Specified <see cref="object" /> parameters that are not of the specified type are treated as <see langword="null" />.
	/// </summary>
	/// <param name="x">An <see cref="object" /> to compare to <paramref name="y" />.</param>
	/// <param name="y">An <see cref="object" /> to compare to <paramref name="x" />.</param>
	/// <returns>
	/// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />.
	/// </returns>
	public int Compare(object? x, object? y)
	{
		return Compare(CSharp.CastOrDefault<T>(x), CSharp.CastOrDefault<T>(y));
	}
	/// <summary>
	/// Compares two objects and returns an indication of their relative sort order.
	/// </summary>
	/// <param name="x">An <see cref="object" /> to compare to <paramref name="y" />.</param>
	/// <param name="y">An <see cref="object" /> to compare to <paramref name="x" />.</param>
	/// <returns>
	/// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />.
	/// </returns>
	public int Compare(T? x, T? y)
	{
		if (x == null && y == null)
		{
			return 0;
		}
		else if (x == null)
		{
			return -1;
		}
		else if (y == null)
		{
			return 1;
		}
		else
		{
			return Comparison(x, y);
		}
	}
}