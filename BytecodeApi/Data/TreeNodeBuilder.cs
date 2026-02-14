namespace BytecodeApi.Data;

/// <summary>
/// Helper class that creates and populates a <see cref="TreeNode{T}" /> structure.
/// </summary>
public static class TreeNodeBuilder
{
	/// <summary>
	/// Creates a new <see cref="TreeNode{T}" /> structure.
	/// </summary>
	/// <typeparam name="T">The type of the value of the <see cref="TreeNode{T}" /> structure.</typeparam>
	/// <param name="value">The value that is associated with the root node.</param>
	/// <returns>
	/// A <see cref="TreeNodeBuilder{T}" /> that can be used to populate the <see cref="TreeNode{T}" /> structure.
	/// </returns>
	public static TreeNodeBuilder<T> BeginTree<T>(T value)
	{
		return new(value);
	}
}

/// <summary>
/// Helper class that creates and populates a <see cref="TreeNode{T}" /> structure.
/// </summary>
/// <typeparam name="T">The type of the value of the <see cref="TreeNode{T}" /> structure.</typeparam>
public sealed class TreeNodeBuilder<T>
{
	private readonly Stack<TreeNode<T>> Stack;
	private bool EndTreeCalled;

	internal TreeNodeBuilder(T value)
	{
		Stack = new();
		Stack.Push(new TreeNode<T>(value));
	}
	/// <summary>
	/// Creates a new child node with the specified value.
	/// </summary>
	/// <param name="value">The value that is associated with the new child node.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public TreeNodeBuilder<T> Begin(T value)
	{
		Check.InvalidOperation(!EndTreeCalled, $"{nameof(TreeNodeBuilder<T>)}.{nameof(TreeNodeBuilder<T>.EndTree)} was already called.");

		Stack.Push(Stack.Peek().Add(value));
		return this;
	}
	/// <summary>
	/// Closes the current node and goes back to the parent node. A call to <see cref="Begin(T)" /> will proceed to create nodes in the parent node.
	/// </summary>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public TreeNodeBuilder<T> End()
	{
		Check.InvalidOperation(!EndTreeCalled, $"{nameof(TreeNodeBuilder<T>)}.{nameof(TreeNodeBuilder<T>.EndTree)} was already called.");
		Check.InvalidOperation(Stack.Count > 1, $"There is no {nameof(TreeNode<T>)} on the stack to close. Did you call {nameof(TreeNodeBuilder<T>)}.{nameof(TreeNodeBuilder<T>.End)} too many times?");

		Stack.Pop();
		return this;
	}
	/// <summary>
	/// Closes the final node and returns the <see cref="TreeNode{T}" />.
	/// </summary>
	/// <returns>
	/// The <see cref="TreeNode{T}" /> that this instance created.
	/// </returns>
	public TreeNode<T> EndTree()
	{
		Check.InvalidOperation(!EndTreeCalled, $"{nameof(TreeNodeBuilder<T>)}.{nameof(TreeNodeBuilder<T>.EndTree)} can be be called only once.");
		Check.InvalidOperation(Stack.Count == 1, Stack.Count == 0 ? $"There is no {nameof(TreeNode<T>)} on the stack to return. Did you call {nameof(TreeNodeBuilder<T>)}.{nameof(TreeNodeBuilder<T>.End)} too many times?" : $"The tree is not completed. Call {nameof(TreeNodeBuilder<T>)}.{nameof(TreeNodeBuilder<T>.End)} to close the last {nameof(TreeNode<T>)}.");

		EndTreeCalled = true;
		return Stack.Pop();
	}
}