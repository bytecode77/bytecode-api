using System.Runtime.InteropServices;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Stream" /> objects.
/// </summary>
public static class StreamExtensions
{
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
		Check.ArgumentNull(stream);

		byte[] buffer = new byte[Marshal.SizeOf<T>()];
		stream.ReadExactly(buffer);
		return ConvertEx.ToStructure<T>(buffer);
	}
	/// <summary>
	/// Writes a structure of the specified type to the stream.
	/// </summary>
	/// <typeparam name="T">The type of the structure to write.</typeparam>
	/// <param name="stream">The <see cref="Stream" /> to write to.</param>
	/// <param name="structure">The structure to write to <paramref name="stream" />.</param>
	public static void WriteStructure<T>(this Stream stream, T structure) where T : struct
	{
		Check.ArgumentNull(stream);

		stream.Write(ConvertEx.FromStructure(structure));
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
		Check.ArgumentNull(stream);
		Check.ArgumentNull(sequence);
		Check.ArgumentEx.ArrayElementsRequired(sequence);

		long position = stream.Position;

		try
		{
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
		}
		finally
		{
			stream.Seek(position, SeekOrigin.Begin);
		}

		return -1;
	}
}