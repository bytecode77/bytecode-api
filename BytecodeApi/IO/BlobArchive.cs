using BytecodeApi.Data;
using BytecodeApi.Extensions;
using System.IO;
using System.IO.Compression;

namespace BytecodeApi.IO
{
	//FEATURE: Decompress to BlobTree
	/// <summary>
	/// Class to create and decompress ZIP archive files from <see cref="Blob" /> objects.
	/// </summary>
	public static class BlobArchive
	{
		/// <summary>
		/// Creates a ZIP archive from the specified collection of <see cref="Blob" /> objects and returns a <see cref="byte" />[] representing the compressed archive.
		/// </summary>
		/// <param name="blobs">A collection of <see cref="Blob" /> objects to create the ZIP archive from.</param>
		/// <returns>
		/// A <see cref="byte" />[] representing the compressed ZIP archive with all <see cref="Blob" /> objects from the specified collection.
		/// </returns>
		public static byte[] Create(BlobCollection blobs)
		{
			Check.ArgumentNull(blobs, nameof(blobs));

			using (MemoryStream memoryStream = new MemoryStream())
			{
				Create(blobs, memoryStream);
				return memoryStream.ToArray();
			}
		}
		/// <summary>
		/// Creates a ZIP archive from the specified collection of <see cref="Blob" /> objects and writes the compressed archive to <paramref name="stream" />.
		/// </summary>
		/// <param name="blobs">A collection of <see cref="Blob" /> objects to create the ZIP archive from.</param>
		/// <param name="stream">The <see cref="Stream" /> to write the compressed archive to.</param>
		public static void Create(BlobCollection blobs, Stream stream)
		{
			Create(blobs, stream, false);
		}
		/// <summary>
		/// Creates a ZIP archive from the specified collection of <see cref="Blob" /> objects and writes the compressed archive to <paramref name="stream" />.
		/// </summary>
		/// <param name="blobs">A collection of <see cref="Blob" /> objects to create the ZIP archive from.</param>
		/// <param name="stream">The <see cref="Stream" /> to write the compressed archive to.</param>
		/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
		public static void Create(BlobCollection blobs, Stream stream, bool leaveOpen)
		{
			Check.ArgumentNull(blobs, nameof(blobs));
			Check.ArgumentNull(stream, nameof(stream));

			using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create, leaveOpen))
			{
				foreach (Blob blob in blobs)
				{
					archive.CreateEntry(blob.Name, blob.Content);
				}
			}
		}
		/// <summary>
		/// Creates a ZIP archive from the specified <see cref="BlobTree" /> and returns a <see cref="byte" />[] representing the compressed archive.
		/// </summary>
		/// <param name="blobs">A tree of <see cref="Blob" /> objects to create the ZIP archive from.</param>
		/// <returns>
		/// A <see cref="byte" />[] representing the compressed ZIP archive with all <see cref="Blob" /> objects from the specified tree.
		/// </returns>
		public static byte[] Create(BlobTree blobs)
		{
			Check.ArgumentNull(blobs, nameof(blobs));

			using (MemoryStream memoryStream = new MemoryStream())
			{
				Create(blobs, memoryStream);
				return memoryStream.ToArray();
			}
		}
		/// <summary>
		/// Creates a ZIP archive from the specified <see cref="BlobTree" /> and writes the compressed archive to <paramref name="stream" />.
		/// </summary>
		/// <param name="blobs">A tree of <see cref="Blob" /> objects to create the ZIP archive from.</param>
		/// <param name="stream">The <see cref="Stream" /> to write the compressed archive to.</param>
		public static void Create(BlobTree blobs, Stream stream)
		{
			Create(blobs, stream, false);
		}
		/// <summary>
		/// Creates a ZIP archive from the specified <see cref="BlobTree" /> and writes the compressed archive to <paramref name="stream" />.
		/// </summary>
		/// <param name="blobs">A tree of <see cref="Blob" /> objects to create the ZIP archive from.</param>
		/// <param name="stream">The <see cref="Stream" /> to write the compressed archive to.</param>
		/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
		public static void Create(BlobTree blobs, Stream stream, bool leaveOpen)
		{
			Check.ArgumentNull(blobs, nameof(blobs));
			Check.ArgumentNull(stream, nameof(stream));

			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create, leaveOpen))
				{
					CreateEntries("", blobs.Root);

					void CreateEntries(string path, BlobTreeNode node)
					{
						foreach (BlobTreeNode childNode in node.Nodes)
						{
							CreateEntries(Path.Combine(path, childNode.Name), childNode);
						}

						foreach (Blob blob in node.Blobs)
						{
							archive.CreateEntry(Path.Combine(path, blob.Name), blob.Content);
						}
					}
				}
			}
		}
	}
}