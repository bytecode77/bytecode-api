using System.IO;
using System.Linq;

namespace BytecodeApi.Data
{
	/// <summary>
	/// Represents a hierarchical collection of <see cref="Blob" /> objects that represent a tree, such as a directory structure.
	/// </summary>
	public class BlobTree
	{
		/// <summary>
		/// Gets the root node of this <see cref="BlobTree" />.
		/// </summary>
		public BlobTreeNode Root { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BlobTree" /> class.
		/// </summary>
		public BlobTree()
		{
			Root = new BlobTreeNode();
		}
		/// <summary>
		/// Creates a hierarchical <see cref="BlobTree" /> object from the specified directory by traversing it recursively.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path to a directory from which to create the <see cref="BlobTree" />.</param>
		/// <returns>
		/// The <see cref="BlobTree" /> this method creates, representing the entire content of the specified directory.
		/// </returns>
		public static BlobTree FromDirectory(string path)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.DirectoryNotFound(path);

			return new BlobTree { Root = ReadDirectory(path.TrimEnd('/', '\\')) };

			BlobTreeNode ReadDirectory(string directory)
			{
				BlobTreeNode node = new BlobTreeNode(Path.GetFileName(directory));

				foreach (DirectoryInfo subDirectory in new DirectoryInfo(directory).GetDirectories()) node.Nodes.Add(ReadDirectory(subDirectory.FullName));
				foreach (FileInfo file in new DirectoryInfo(directory).GetFiles()) node.Blobs.Add(Blob.FromFile(file.FullName));

				return node;
			}
		}

		/// <summary>
		/// Writes the contents of all <see cref="Blob" /> objects to the specified directory and creates a directory structure based on the hierarchical <see cref="BlobTreeNode" /> structure of <see cref="Root" />. <see cref="BlobTreeNode.Name" /> represents the directory names, <see cref="Blob.Name" /> represents the filenames and <see cref="Blob.Content" /> represents the file contents.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path to a directory to which this <see cref="BlobTree" /> is written to.</param>
		public void SaveToDirectory(string path)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.DirectoryNotFound(path);

			CheckNames(Root);
			Save(Root, path);

			void CheckNames(BlobTreeNode node)
			{
				foreach (BlobTreeNode childNode in node.Nodes)
				{
					if (!Validate.FileName(childNode.Name)) throw Throw.InvalidOperation("Blob tree node with the name '" + childNode.Name + "' has illegal filename characters.");
					CheckNames(childNode);
				}

				Blob blob = node.Blobs.FirstOrDefault(b => !Validate.FileName(b.Name));
				if (blob != null) throw BlobCollection.CreateIllegalFilenameException(blob);
			}
			void Save(BlobTreeNode node, string nodePath)
			{
				Directory.CreateDirectory(nodePath);
				foreach (BlobTreeNode childNode in node.Nodes) Save(childNode, Path.Combine(nodePath, childNode.Name));
				node.Blobs.SaveToDirectory(nodePath);
			}
		}
	}
}