using System.IO;
using System.IO.Compression;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="ZipArchive" /> and related objects.
	/// </summary>
	public static class CompressionExtensions
	{
		/// <summary>
		/// Adds a <see cref="ZipArchiveEntry" /> to this <see cref="ZipArchive" /> with the specified name and content.
		/// </summary>
		/// <param name="archive">The <see cref="ZipArchive" /> where the <see cref="ZipArchiveEntry" /> is created in.</param>
		/// <param name="name">A <see cref="string" /> value speficying the name for the entry.</param>
		/// <param name="content">A <see cref="byte" />[] speficying the content for the entry.</param>
		public static void CreateEntry(this ZipArchive archive, string name, byte[] content)
		{
			archive.CreateEntry(name, content, CompressionLevel.Optimal);
		}
		/// <summary>
		/// Adds a <see cref="ZipArchiveEntry" /> to this <see cref="ZipArchive" /> with the specified name, content and <see cref="CompressionLevel" />.
		/// </summary>
		/// <param name="archive">The <see cref="ZipArchive" /> where the <see cref="ZipArchiveEntry" /> is created in.</param>
		/// <param name="name">A <see cref="string" /> value speficying the name for the entry.</param>
		/// <param name="content">A <see cref="byte" />[] speficying the content for the entry.</param>
		/// <param name="compressionLevel">The <see cref="CompressionLevel" /> to apply to the data in <paramref name="content" />.</param>
		public static void CreateEntry(this ZipArchive archive, string name, byte[] content, CompressionLevel compressionLevel)
		{
			Check.ArgumentNull(archive, nameof(archive));
			Check.ArgumentNull(name, nameof(name));
			Check.ArgumentNull(content, nameof(content));

			ZipArchiveEntry entry = archive.CreateEntry(name, compressionLevel);
			using Stream stream = entry.Open();
			stream.Write(content);
		}
		/// <summary>
		/// Extracts the content of this <see cref="ZipArchiveEntry" /> into a <see cref="byte" />[].
		/// </summary>
		/// <param name="entry">The <see cref="ZipArchiveEntry" /> object that holds the compressed data.</param>
		/// <returns>
		/// A <see cref="byte" />[] with the uncompressed data from this <see cref="ZipArchiveEntry" />.
		/// </returns>
		public static byte[] GetContent(this ZipArchiveEntry entry)
		{
			Check.ArgumentNull(entry, nameof(entry));

			using Stream stream = entry.Open();
			using MemoryStream memoryStream = new MemoryStream();
			stream.CopyTo(memoryStream);
			return memoryStream.ToArray();
		}
	}
}