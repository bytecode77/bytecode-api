using BytecodeApi.Data;
using BytecodeApi.Extensions;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Blob:			An object that is composed of a name and binary content
		// BlobCollection:	A collection of blobs
		// BlobTree			A hierarchical structure of nodes and blobs

		// Blobs are not dedicated to be read and written from/to the file system.
		// However, this example also includes file & directory opetaions.

		// Create a blob
		Blob blob = new Blob("name", new byte[100]);
		blob.Tag = "some additional information"; // Any object can be associated with a blob to store additional information

		// Load blob from file
		blob = Blob.FromFile(@"C:\Windows\win.ini");

		// Load a directory into a blob tree:
		// (the example requires a location that is present on every OS and it shouldn't be too large...)
		BlobTree blobTree = BlobTree.FromDirectory(@"C:\Windows\System32\Sysprep");

		// Iterate through a blob tree and write to console
		Console.WriteLine("Iterate through BlobTree that was loaded from a directory:");
		PrintTree(blobTree.Root);

		// Find a node or blob by a backslash delimited path
		BlobTreeNode node = blobTree.Root.FindNode(@"ActionFiles"); // equivalent of a directory
		Blob specificBlob = blobTree.Root.FindBlob(@"ActionFiles\Cleanup.xml"); // equivalent of a file

		// Create a blob collection
		BlobCollection blobs = new BlobCollection();
		blobs.Add(new Blob("blob1", new byte[100]));
		blobs.Add(new Blob("blob2", new byte[100]));
		blobs.Add(new Blob("blob3", new byte[100]));

		// Save to directory
		// blobs.SaveToDirectory(@"C:\path\to\directory");

		// Load blob collection from directory. Unlike a blob tree, this collection willy only contains the files directly in this directory.
		// BlobCollection.FromDirectory(@"C:\path\to\directory");

		// Load all files recursively into a flat blob collection
		// BlobCollection.FromDirectory(@"C:\path\to\directory", true);

		Console.ReadKey();
	}

	private static void PrintTree(BlobTreeNode node, int consoleIndent = 0)
	{
		Console.Write("  ".Repeat(consoleIndent));
		Console.Write("* " + node.Name);
		Console.WriteLine(" (" + node.ComputeSize() + ")"); // Compute the size of all blobs recursively

		foreach (BlobTreeNode childNode in node.Nodes)
		{
			PrintTree(childNode, consoleIndent + 1);
		}

		foreach (Blob blob in node.Blobs)
		{
			Console.Write("  ".Repeat(consoleIndent + 1));
			Console.WriteLine("> " + blob.Name + " (" + blob.Content.Length + ")");
		}
	}
}