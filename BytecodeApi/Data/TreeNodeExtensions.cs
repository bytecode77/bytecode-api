namespace BytecodeApi.Data;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="TreeNode{T}" /> objects.
/// </summary>
public static class TreeNodeExtensions
{
	/// <summary>
	/// Retrieves the value from each element in <paramref name="nodes" />.
	/// </summary>
	/// <typeparam name="T">The type of the <see cref="TreeNode{T}" />.</typeparam>
	/// <param name="nodes">A sequence of <see cref="TreeNode{T}" /> to query.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> whose elements represent the value of the input sequence.
	/// </returns>
	public static IEnumerable<T> Values<T>(this IEnumerable<TreeNode<T>> nodes)
	{
		foreach (TreeNode<T> node in nodes)
		{
			yield return node.Value;
		}
	}
}