using BytecodeApi.Extensions;
using System.Collections;
using System.Diagnostics;

namespace BytecodeApi.Data;

/// <summary>
/// Represents an entry in a hierarchical tree structure.
/// </summary>
/// <typeparam name="T">The type of the value and its children.</typeparam>
[DebuggerDisplay($"{nameof(TreeNode<T>)}: Value = {{Value}}, Children: {{Children.Count}}")]
public class TreeNode<T> : IEnumerable<TreeNode<T>>, IEquatable<TreeNode<T>>
{
	private readonly List<TreeNode<T>> Children;
	/// <summary>
	/// Gets or sets the value that is associated with this node.
	/// </summary>
	public T Value { get; set; }
	/// <summary>
	/// Gets the child node at the specified index.
	/// </summary>
	/// <param name="index">The index at which to retrieve the child node.</param>
	public TreeNode<T> this[int index]
	{
		get
		{
			Check.IndexOutOfRange(index, Count);
			return Children[index];
		}
	}
	/// <summary>
	/// Gets the number of child nodes.
	/// </summary>
	public int Count => Children.Count;

	/// <summary>
	/// Gets the parent node, or <see langword="null" />, if this node is the root node.
	/// </summary>
	public TreeNode<T>? Parent { get; private set; }
	/// <summary>
	/// Gets the root node. If this node is already the root node, <see langword="this" /> is returned.
	/// </summary>
	public TreeNode<T>? Root => Parent == null ? this : Parent.Root;
	/// <summary>
	/// Gets the sibling that is to the left of this node. If this node is already the root node, or if this node is the first child, <see langword="null" /> is returned.
	/// </summary>
	public TreeNode<T>? Left
	{
		get
		{
			if (Parent == null)
			{
				return null;
			}
			else
			{
				int index = Parent.Children.IndexOf(this);
				return index <= 0 ? null : Parent.Children[index - 1];
			}
		}
	}
	/// <summary>
	/// Gets the sibling that is to the right of this node. If this node is already the root node, or if this node is the last child, <see langword="null" /> is returned.
	/// </summary>
	public TreeNode<T>? Right
	{
		get
		{
			if (Parent == null)
			{
				return null;
			}
			else
			{
				int index = Parent.Children.IndexOf(this);
				return index < 0 || index >= Parent.Children.Count - 1 ? null : Parent.Children[index + 1];
			}
		}
	}
	/// <summary>
	/// Gets the depth of this node, where 0 represents the root node, and 1 represents a node within the root, and so on.
	/// </summary>
	public int Level => Parent == null ? 0 : Parent.Level + 1;

	/// <summary>
	/// Initializes a new instance of the <see cref="TreeNode{T}" /> class.
	/// </summary>
	/// <param name="value">The value that is associated with this node.</param>
	public TreeNode(T value)
	{
		Value = value;
		Children = new();
	}

	/// <summary>
	/// Creates a new child node with the specified value.
	/// </summary>
	/// <param name="value">The value that is associated with the new child node.</param>
	/// <returns>
	/// The newly created child node.
	/// </returns>
	public TreeNode<T> Add(T value)
	{
		return Add(new TreeNode<T>(value));
	}
	/// <summary>
	/// Adds the specified child node. If <paramref name="node" /> is already part of a tree, it will be removed from its current parent node.
	/// </summary>
	/// <param name="node">The child node to be added.</param>
	/// <returns>
	/// A reference to <paramref name="node" />.
	/// </returns>
	public TreeNode<T> Add(TreeNode<T> node)
	{
		Check.ArgumentNull(node);

		if (node.Parent != null)
		{
			node.Parent.Remove(node);
		}

		node.Parent = this;
		Children.Add(node);
		return node;
	}
	/// <summary>
	/// Removes the child node from this <see cref="TreeNode{T}" />.
	/// </summary>
	/// <param name="node">The <see cref="TreeNode{T}" /> to remove.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="node" /> is successfully removed;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Remove(TreeNode<T> node)
	{
		if (Children.Remove(node))
		{
			node.Parent = null;
			return true;
		}
		else
		{
			return false;
		}
	}
	/// <summary>
	/// Removes all child nodes that satisfy a specified condition.
	/// </summary>
	/// <param name="predicate">A function to test each child node for a condition.</param>
	/// <returns>
	/// The number of removed child nodes.
	/// </returns>
	public int RemoveAll(Predicate<TreeNode<T>> predicate)
	{
		Check.ArgumentNull(predicate);

		List<TreeNode<T>> removed = new();

		foreach (TreeNode<T> node in Children)
		{
			if (predicate(node))
			{
				node.Parent = null;
				removed.Add(node);
			}
		}

		Children.RemoveRange(removed);
		return removed.Count;
	}
	/// <summary>
	/// Removes all child nodes where the value satisfies a specified condition.
	/// </summary>
	/// <param name="predicate">A function to test each child node's value for a condition.</param>
	/// <returns>
	/// The number of removed child nodes.
	/// </returns>
	public int RemoveAll(Predicate<T> predicate)
	{
		Check.ArgumentNull(predicate);

		List<TreeNode<T>> removed = new();

		foreach (TreeNode<T> node in Children)
		{
			if (predicate(node.Value))
			{
				node.Parent = null;
				removed.Add(node);
			}
		}

		Children.RemoveRange(removed);
		return removed.Count;
	}
	/// <summary>
	/// Removes a range of child nodes.
	/// </summary>
	/// <param name="nodes">A collection of nodes to remove.</param>
	public void RemoveRange(IEnumerable<TreeNode<T>> nodes)
	{
		Check.ArgumentNull(nodes);

		foreach (TreeNode<T> node in nodes.ToArray())
		{
			Remove(node);
		}
	}
	/// <summary>
	/// Removes all child nodes.
	/// </summary>
	public void Clear()
	{
		foreach (TreeNode<T> node in Children)
		{
			node.Parent = null;
		}

		Children.Clear();
	}

	/// <summary>
	/// Retrieves all ancestor nodes, excluding this node.
	/// </summary>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that iterates all ancestor nodes, excluding this node.
	/// </returns>
	public IEnumerable<TreeNode<T>> Ancestors()
	{
		TreeNode<T>? current = Parent;

		while (current != null)
		{
			yield return current;
			current = current.Parent;
		}
	}
	/// <summary>
	/// Retrieves all ancestor nodes, including this node.
	/// </summary>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that iterates all ancestor nodes, including this node.
	/// </returns>
	public IEnumerable<TreeNode<T>> AncestorsAndSelf()
	{
		yield return this;

		foreach (TreeNode<T> node in Ancestors())
		{
			yield return node;
		}
	}
	/// <summary>
	/// Retrieves all descendant nodes, excluding this node.
	/// </summary>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that iterates all descendant nodes, excluding this node.
	/// </returns>
	public IEnumerable<TreeNode<T>> Descendants()
	{
		foreach (TreeNode<T> child in Children)
		{
			yield return child;

			foreach (TreeNode<T> node in child.Descendants())
			{
				yield return node;
			}
		}
	}
	/// <summary>
	/// Retrieves all descendant nodes, including this node.
	/// </summary>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that iterates all descendant nodes, including this node.
	/// </returns>
	public IEnumerable<TreeNode<T>> DescendantsAndSelf()
	{
		yield return this;

		foreach (TreeNode<T> node in Descendants())
		{
			yield return node;
		}
	}
	/// <summary>
	/// Retrieves all sibling nodes from the same parent, excluding this node. If this node represents the root of the tree, an empty sequence is returned.
	/// </summary>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that iterates all sibling nodes from the same parent, excluding this node.
	/// </returns>
	public IEnumerable<TreeNode<T>> Siblings()
	{
		foreach (TreeNode<T> sibling in SiblingsAndSelf())
		{
			if (sibling != this)
			{
				yield return sibling;
			}
		}
	}
	/// <summary>
	/// Retrieves all sibling nodes from the same parent, including this node. If this node represents the root of the tree, an empty sequence is returned.
	/// </summary>
	/// <returns>
	/// An <see cref="IEnumerable{T}" /> that iterates all sibling nodes from the same parent, including this node.
	/// </returns>
	public IEnumerable<TreeNode<T>> SiblingsAndSelf()
	{
		if (Parent != null)
		{
			foreach (TreeNode<T> sibling in Parent)
			{
				yield return sibling;
			}
		}
	}

	/// <summary>
	/// Returns the name of this <see cref="TreeNode{T}" />.
	/// </summary>
	/// <returns>
	/// The name of this <see cref="TreeNode{T}" />.
	/// </returns>
	public override string ToString()
	{
		return Value?.ToString() ?? "";
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is TreeNode<T> node && Equals(node);
	}
	/// <summary>
	/// Determines whether <see cref="Value" /> of this instance is equal to that of another <see cref="TreeNode{T}" />.
	/// </summary>
	/// <param name="other">The <see cref="TreeNode{T}" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if <see cref="Value" /> of this instance is equal to that of the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals([NotNullWhen(true)] TreeNode<T>? other)
	{
		return
			other != null &&
			CSharp.TypeEquals(this, other) &&
			Equals(Value, other.Value);
	}
	/// <summary>
	/// Returns a hash code for this <see cref="TreeNode{T}" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="TreeNode{T}" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Value);
	}

	/// <summary>
	/// Returns an enumerator that iterates through the <see cref="TreeNode{T}" />.
	/// </summary>
	/// <returns>
	/// An enumerator that can be used to iterate through the <see cref="TreeNode{T}" />.
	/// </returns>
	public IEnumerator<TreeNode<T>> GetEnumerator()
	{
		return Children.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return Children.GetEnumerator();
	}
}