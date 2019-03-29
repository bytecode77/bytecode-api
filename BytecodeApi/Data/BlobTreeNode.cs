using BytecodeApi.Extensions;
using System.Collections.Generic;

namespace BytecodeApi.Data
{
	/// <summary>
	/// Represents a tree node within a <see cref="BlobTree" />.
	/// </summary>
	public class BlobTreeNode
	{
		/// <summary>
		/// Gets or sets the name of the <see cref="BlobTreeNode" />.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Gets the collection of nodes associated with this <see cref="BlobTreeNode" />.
		/// </summary>
		public BlobTreeNodeCollection Nodes { get; private set; }
		/// <summary>
		/// Gets the collection of <see cref="Blob" /> objects associated with this <see cref="BlobTreeNode" />.
		/// </summary>
		public BlobCollection Blobs { get; private set; }
		/// <summary>
		/// Gets or sets the object that contains data about the <see cref="BlobTreeNode" />.
		/// </summary>
		public object Tag { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BlobTreeNode" /> class.
		/// </summary>
		public BlobTreeNode()
		{
			Nodes = new BlobTreeNodeCollection();
			Blobs = new BlobCollection();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="BlobTreeNode" /> class with the specified name.
		/// </summary>
		/// <param name="name">The name of the <see cref="BlobTreeNode" />.</param>
		public BlobTreeNode(string name) : this()
		{
			Name = name;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="BlobTreeNode" /> class with the specified name and elemets.
		/// </summary>
		/// <param name="name">The name of the <see cref="BlobTreeNode" />.</param>
		/// <param name="nodes">A collection of nodes to add to the <see cref="BlobTreeNode" />.</param>
		/// <param name="blobs">A collection of blobs to add to the <see cref="BlobTreeNode" />.</param>
		public BlobTreeNode(string name, IEnumerable<BlobTreeNode> nodes, IEnumerable<Blob> blobs) : this(name)
		{
			Nodes.AddRange(nodes);
			Blobs.AddRange(blobs);
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Name + ", Nodes: " + Nodes.Count + ", Blobs: " + Blobs.Count + "]";
		}
	}
}