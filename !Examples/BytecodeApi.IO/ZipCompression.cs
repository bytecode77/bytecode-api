using BytecodeApi.Data;
using BytecodeApi.IO;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// ZipCompression can be used to create ZIP files from collections or a tree of blobs.
		// The result can be stored in byte[] or directly written into a file or Stream.

		// In this example, we create a collection of blobs. ZipCompression also accepts BlobTree objects.
		BlobCollection blobs = new BlobCollection();
		blobs.Add(new Blob("file1.txt", new byte[1000]));
		blobs.Add(new Blob("file2.txt", new byte[2000]));

		// The result of compression is a ZIP file:
		// The hierarchy of a BlobTree is mapped to directories and files.
		byte[] zipFile = ZipCompression.Compress(blobs);

		// Decompress a ZIP file into a BlobTree.
		// This ZIP file does not need to be created by the ZipCompression class. Any ZIP file can be decompressed into a BlobTree.
		BlobTree decompressed = ZipCompression.Decompress(zipFile);
	}
}