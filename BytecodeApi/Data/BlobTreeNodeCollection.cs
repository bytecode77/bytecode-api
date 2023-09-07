using BytecodeApi.Extensions;
using System.Collections;

namespace BytecodeApi.Data;

/// <summary>
/// Represents a collection of <see cref="BlobTreeNode" /> objects.
/// </summary>
public sealed class BlobTreeNodeCollection : ICollection<BlobTreeNode>
{
	private readonly List<BlobTreeNode> Nodes;
	/// <summary>
	/// Gets the <see cref="BlobTreeNode" /> at the specified index.
	/// </summary>
	/// <param name="index">The index at which to retrieve the <see cref="BlobTreeNode" />.</param>
	public BlobTreeNode this[int index]
	{
		get
		{
			Check.IndexOutOfRange(index, Count);
			return Nodes[index];
		}
		set
		{
			Check.IndexOutOfRange(index, Count);
			Nodes[index] = value;
		}
	}
	/// <summary>
	/// Gets the <see cref="BlobTreeNode" /> with the specified case sensitive name and throws an exception, if it was not found.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the name of the <see cref="BlobTreeNode" />.</param>
	public BlobTreeNode this[string name]
	{
		get
		{
			Check.ArgumentNull(name);
			return Nodes.FirstOrDefault(b => b.Name == name) ?? throw Throw.KeyNotFound($"A node with the name '{name}' was not found.");
		}
	}
	/// <summary>
	/// Gets the number of elements contained in the <see cref="BlobTreeNodeCollection" />.
	/// </summary>
	public int Count => Nodes.Count;
	/// <summary>
	/// Gets a value indicating whether the <see cref="BlobTreeNodeCollection" /> is read-only.
	/// </summary>
	public bool IsReadOnly => false;

	/// <summary>
	/// Initializes a new instance of the <see cref="BlobTreeNodeCollection" /> class.
	/// </summary>
	public BlobTreeNodeCollection()
	{
		Nodes = new();
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="BlobTreeNodeCollection" /> class with the specified collection of nodes.
	/// </summary>
	/// <param name="nodes">A collection of <see cref="BlobTreeNode" /> objects to add to this <see cref="BlobTreeNodeCollection" />.</param>
	public BlobTreeNodeCollection(IEnumerable<BlobTreeNode> nodes) : this()
	{
		Check.ArgumentNull(nodes);

		Nodes.AddRange(nodes);
	}

	/// <summary>
	/// Computes the size, in bytes, of all <see cref="Blob" /> objects within this <see cref="BlobTreeNodeCollection" /> recursively.
	/// </summary>
	/// <returns>
	/// The size, in bytes, of all <see cref="Blob" /> objects within this <see cref="BlobTreeNodeCollection" /> recursively.
	/// </returns>
	public long ComputeSize()
	{
		return Nodes.Sum(node => node.ComputeSize());
	}

	/// <summary>
	/// Adds a <see cref="BlobTreeNode" /> to the end of the <see cref="BlobTreeNodeCollection" />.
	/// </summary>
	/// <param name="item">The <see cref="BlobTreeNode" /> to be added to the end of the <see cref="BlobTreeNodeCollection" />.</param>
	public void Add(BlobTreeNode item)
	{
		Check.ArgumentNull(item);

		Nodes.Add(item);
	}
	/// <summary>
	/// Removes the first occurrence of a specific <see cref="BlobTreeNode" /> from the <see cref="BlobTreeNodeCollection" />.
	/// </summary>
	/// <param name="item">The <see cref="BlobTreeNode" /> to remove from the <see cref="BlobTreeNodeCollection" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="item" /> is successfully removed;
	/// otherwise, <see langword="false" />.
	/// This method also returns <see langword="false" />, if <paramref name="item" /> was not found in the <see cref="BlobTreeNodeCollection" />.</returns>
	public bool Remove(BlobTreeNode item)
	{
		return Nodes.Remove(item);
	}
	/// <summary>
	/// Removes all elements from the <see cref="BlobTreeNodeCollection" />.
	/// </summary>
	public void Clear()
	{
		Nodes.Clear();
	}
	/// <summary>
	/// Determines whether an element is in the <see cref="BlobTreeNodeCollection" />.
	/// </summary>
	/// <param name="item">The <see cref="BlobTreeNode" /> to locate in the <see cref="BlobTreeNodeCollection" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="item" /> is found in the <see cref="BlobTreeNodeCollection" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Contains(BlobTreeNode item)
	{
		return Nodes.Contains(item);
	}
	void ICollection<BlobTreeNode>.CopyTo(BlobTreeNode[] array, int arrayIndex)
	{
		Check.ArgumentNull(array);
		Check.IndexOutOfRange(arrayIndex, array.Length - Count + 1);

		Nodes.CopyTo(array, arrayIndex);
	}
	/// <summary>
	/// Returns an enumerator that iterates through the <see cref="BlobTreeNodeCollection" />.
	/// </summary>
	/// <returns>
	/// An enumerator that can be used to iterate through the <see cref="BlobTreeNodeCollection" />.
	/// </returns>
	public IEnumerator<BlobTreeNode> GetEnumerator()
	{
		return Nodes.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return Nodes.GetEnumerator();
	}
}