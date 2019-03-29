using System.IO;
using System.Runtime.InteropServices;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Stream" /> objects.
	/// </summary>
	public static class StreamExtensions
	{
		/// <summary>
		/// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to read from.</param>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified <see cref="byte" />[] with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
		/// <returns>
		/// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or 0, if the end of the stream has been reached.
		/// </returns>
		public static int Read(this Stream stream, byte[] buffer)
		{
			Check.ArgumentNull(stream, nameof(stream));
			Check.ArgumentNull(buffer, nameof(buffer));

			return stream.Read(buffer, 0, buffer.Length);
		}
		/// <summary>
		/// Sets the position within the current stream to an offset starting at the beginning.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to process.</param>
		/// <param name="offset">A byte offset relative to the beginning of the stream.</param>
		/// <returns>
		/// The new position within the current stream.
		/// </returns>
		public static long Seek(this Stream stream, long offset)
		{
			Check.ArgumentNull(stream, nameof(stream));

			return stream.Seek(offset, SeekOrigin.Begin);
		}
		/// <summary>
		/// Writes a sequence of bytes to the current <see cref="Stream" /> and advances the current position within this <see cref="Stream" /> by the number of bytes written.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to write to.</param>
		/// <param name="buffer">A <see cref="byte" />[] that is copied to this <see cref="Stream" />.</param>
		public static void Write(this Stream stream, byte[] buffer)
		{
			Check.ArgumentNull(stream, nameof(stream));
			Check.ArgumentNull(buffer, nameof(buffer));

			stream.Write(buffer, 0, buffer.Length);
		}
		/// <summary>
		/// Reads a structure of the specified type from the stream.
		/// </summary>
		/// <typeparam name="T">The type of the structure to read.</typeparam>
		/// <param name="stream">The <see cref="Stream" /> to read from.</param>
		/// <returns>
		/// A new structure of the specified type that was read from the stream.
		/// </returns>
		public static T ReadStructure<T>(this Stream stream) where T : struct
		{
			Check.ArgumentNull(stream, nameof(stream));

			byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
			stream.Read(buffer);
			return ConvertEx.ArrayToStructure<T>(buffer);
		}
		/// <summary>
		/// Writes a structure of the specified type to the stream.
		/// </summary>
		/// <typeparam name="T">The type of the structure to write.</typeparam>
		/// <param name="stream">The <see cref="Stream" /> to write to.</param>
		/// <param name="structure">The structure to write to <paramref name="stream" />.</param>
		public static void WriteStructure<T>(this Stream stream, T structure) where T : struct
		{
			Check.ArgumentNull(stream, nameof(stream));

			stream.Write(ConvertEx.StructureToArray(structure));
		}
		/// <summary>
		/// Searches this <see cref="Stream" /> for the first occurrence of <paramref name="sequence" />. If not found, returns -1.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to be searched.</param>
		/// <param name="sequence">The <see cref="byte" />[] to search for.</param>
		/// <returns>
		/// The index of the first occurrence of <paramref name="sequence" /> and -1, if not found.
		/// </returns>
		public static long FindSequence(this Stream stream, byte[] sequence)
		{
			Check.ArgumentNull(stream, nameof(stream));
			Check.ArgumentNull(sequence, nameof(sequence));
			Check.ArgumentEx.ArrayElementsRequired(sequence, nameof(sequence));

			int readByte;
			int sequenceIndex = 0;

			for (long index = 0; (readByte = stream.ReadByte()) != -1; index++)
			{
				if (readByte == sequence[sequenceIndex])
				{
					sequenceIndex++;
					if (sequenceIndex == sequence.Length)
					{
						return index + 1 - sequence.Length;
					}
				}
				else
				{
					sequenceIndex = 0;
				}
			}

			return -1;
		}
	}
}