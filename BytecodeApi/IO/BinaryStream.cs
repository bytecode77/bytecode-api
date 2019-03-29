using BytecodeApi.Extensions;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace BytecodeApi.IO
{
	/// <summary>
	/// Reads and writes primitive data types as binary values in a specific encoding. Reading and writing methods are supported based on the capabilities of the underlying <see cref="Stream" />.
	/// </summary>
	public class BinaryStream : IDisposable
	{
		private readonly BinaryReader Reader;
		private readonly BinaryWriter Writer;
		private readonly Encoding Encoding;
		private readonly bool LeaveOpen;
		/// <summary>
		/// Returns the underlying <see cref="Stream" />.
		/// </summary>
		public Stream BaseStream { get; private set; }
		/// <summary>
		/// Gets the total amount of bytes read from the underlying <see cref="Stream" /> of this instance.
		/// </summary>
		public long BytesRead { get; private set; }
		/// <summary>
		/// Gets the total amount of bytes written to the underlying <see cref="Stream" /> of this instance.
		/// </summary>
		public long BytesWritten { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryStream" /> class using the <see cref="Encoding.UTF8" /> encoding.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to use for reading and writing. This stream can be readable, writable, or both.</param>
		public BinaryStream(Stream stream) : this(stream, Encoding.UTF8)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryStream" /> class using the specified character encoding.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to use for reading and writing. This stream can be readable, writable, or both.</param>
		/// <param name="encoding">The encoding that is used for read and write operations on the base stream.</param>
		public BinaryStream(Stream stream, Encoding encoding) : this(stream, encoding, false)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryStream" /> class using the specified character encoding.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to use for reading and writing. This stream can be readable, writable, or both.</param>
		/// <param name="encoding">The encoding that is used for read and write operations on the base stream.</param>
		/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
		public BinaryStream(Stream stream, Encoding encoding, bool leaveOpen)
		{
			Check.ArgumentNull(stream, nameof(stream));
			Check.ArgumentNull(encoding, nameof(encoding));

			BaseStream = stream;
			Encoding = encoding;
			LeaveOpen = leaveOpen;
			if (stream.CanRead) Reader = new BinaryReader(BaseStream, Encoding, LeaveOpen);
			if (stream.CanWrite) Writer = new BinaryWriter(BaseStream, Encoding, LeaveOpen);
		}
		/// <summary>
		/// Releases all resources used by the underlying instance of the <see cref="Stream" />.
		/// </summary>
		public void Dispose()
		{
			Writer?.Dispose();
			Reader?.Dispose();
			if (!LeaveOpen) BaseStream.Dispose();
		}

		/// <summary>
		/// Reads the specified number of bytes from the stream, starting from a specified point in the <see cref="byte" />[].
		/// </summary>
		/// <param name="buffer">The buffer to read data into.</param>
		/// <param name="index">The starting point in the buffer at which to begin reading into the buffer.</param>
		/// <param name="count">The number of bytes to read.</param>
		/// <returns>
		/// The number of bytes read into <paramref name="buffer" />. This might be less than the number of bytes requested if that many bytes are not available, or it might be zero if the end of the stream is reached.
		/// </returns>
		public int Read(byte[] buffer, int index, int count)
		{
			AssertCanRead();
			Check.ArgumentNull(buffer, nameof(buffer));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(index, nameof(index));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));
			Check.ArgumentEx.OffsetAndLengthOutOfBounds(index, count, buffer.Length);

			int read = Reader.Read(buffer, index, count);
			BytesRead += read;
			return read;
		}
		/// <summary>
		/// Reads the specified number of characters from the stream, starting from a specified point in the <see cref="char" />[].
		/// </summary>
		/// <param name="buffer">The buffer to read data into.</param>
		/// <param name="index">The starting point in the buffer at which to begin reading into the buffer.</param>
		/// <param name="count">The number of characters to read.</param>
		/// <returns>
		/// The number of characters read into <paramref name="buffer" />. This might be less than the number of characters requested if that many characters are not available, or it might be zero if the end of the stream is reached.
		/// </returns>
		public int Read(char[] buffer, int index, int count)
		{
			AssertCanRead();
			Check.ArgumentNull(buffer, nameof(buffer));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(index, nameof(index));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));
			Check.ArgumentEx.OffsetAndLengthOutOfBounds(index, count, buffer.Length);

			int read = Reader.Read(buffer, index, count);
			BytesRead += Encoding.GetByteCount(buffer, index, count);
			return read;
		}
		/// <summary>
		/// Reads a <see cref="bool" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if the byte that has been read is nonzero;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool ReadBoolean()
		{
			AssertCanRead();

			bool value = Reader.ReadBoolean();
			BytesRead++;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="byte" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="byte" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public byte ReadByte()
		{
			AssertCanRead();

			byte value = Reader.ReadByte();
			BytesRead++;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="sbyte" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="sbyte" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public sbyte ReadSByte()
		{
			AssertCanRead();

			sbyte value = Reader.ReadSByte();
			BytesRead++;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="char" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="char" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public char ReadChar()
		{
			AssertCanRead();

			char value = Reader.ReadChar();
			BytesRead += Encoding.GetByteCount(Singleton.Array(value));
			return value;
		}
		/// <summary>
		/// Reads a <see cref="decimal" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="decimal" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public decimal ReadDecimal()
		{
			AssertCanRead();

			decimal value = Reader.ReadDecimal();
			BytesRead += 16;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="double" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="double" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public double ReadDouble()
		{
			AssertCanRead();

			double value = Reader.ReadDouble();
			BytesRead += 8;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="float" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="float" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public float ReadSingle()
		{
			AssertCanRead();

			float value = Reader.ReadSingle();
			BytesRead += 4;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="int" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="int" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public int ReadInt32()
		{
			AssertCanRead();

			int value = Reader.ReadInt32();
			BytesRead += 4;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="uint" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="uint" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public uint ReadUInt32()
		{
			AssertCanRead();

			uint value = Reader.ReadUInt32();
			BytesRead += 4;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="long" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="long" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public long ReadInt64()
		{
			AssertCanRead();

			long value = Reader.ReadInt64();
			BytesRead += 8;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="ulong" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="ulong" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public ulong ReadUInt64()
		{
			AssertCanRead();

			ulong value = Reader.ReadUInt64();
			BytesRead += 8;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="short" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="short" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public short ReadInt16()
		{
			AssertCanRead();

			short value = Reader.ReadInt16();
			BytesRead += 2;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="ushort" /> value from underlying <see cref="Stream" />.
		/// </summary>
		/// <returns>
		/// The next <see cref="ushort" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public ushort ReadUInt16()
		{
			AssertCanRead();

			ushort value = Reader.ReadUInt16();
			BytesRead += 2;
			return value;
		}
		/// <summary>
		/// Reads a <see cref="string" /> value from underlying <see cref="Stream" />. The <see cref="string" /> is prefixed with the length, encoded as an integer seven bits at a time.
		/// </summary>
		/// <returns>
		/// The next <see cref="string" /> read from the underlying <see cref="Stream" />.
		/// </returns>
		public string ReadString()
		{
			AssertCanRead();

			string value = Reader.ReadString();
			BytesRead += Encoding.GetByteCount(value) + Compute7BitEncodedIntSize(value.Length);
			return value;
		}
		/// <summary>
		/// Reads the specified number of bytes from the underlying <see cref="Stream" /> into a <see cref="byte" />[].
		/// </summary>
		/// <param name="count">A <see cref="int" /> value specifying the number of bytes to read.</param>
		/// <returns>
		/// The <see cref="byte" />[] read from the underlying <see cref="Stream" />. This might be less than the number of bytes requested if the end of the stream is reached.
		/// </returns>
		public byte[] ReadBytes(int count)
		{
			AssertCanRead();
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));

			byte[] value = Reader.ReadBytes(count);
			BytesRead += count;
			return value;
		}
		/// <summary>
		/// Reads the specified number of characters from the underlying <see cref="Stream" /> into a <see cref="char" />[].
		/// </summary>
		/// <param name="count">A <see cref="int" /> value specifying the number of characters to read.</param>
		/// <returns>
		/// The <see cref="char" />[] read from the underlying <see cref="Stream" />. This might be less than the number of characters requested if the end of the stream is reached.
		/// </returns>
		public char[] ReadChars(int count)
		{
			AssertCanRead();
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));

			char[] value = Reader.ReadChars(count);
			BytesRead += Encoding.GetByteCount(value);
			return value;
		}
		/// <summary>
		/// Reads a structure of the specified type from the stream.
		/// </summary>
		/// <typeparam name="T">The type of the structure to read.</typeparam>
		/// <returns>
		/// A new structure of the specified type that was read from the stream.
		/// </returns>
		public T ReadStructure<T>() where T : struct
		{
			AssertCanRead();

			T value = BaseStream.ReadStructure<T>();
			BytesRead += Marshal.SizeOf(typeof(T));
			return value;
		}
		/// <summary>
		/// Writes a region of a <see cref="byte" />[] to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="buffer">The <see cref="byte" />[] containing the data to write.</param>
		/// <param name="index">The starting point in the buffer at which to begin writing.</param>
		/// <param name="count">The number of bytes to write.</param>
		public void Write(byte[] buffer, int index, int count)
		{
			AssertCanWrite();
			Check.ArgumentNull(buffer, nameof(buffer));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(index, nameof(index));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));
			Check.ArgumentEx.OffsetAndLengthOutOfBounds(index, count, buffer.Length);

			Writer.Write(buffer, index, count);
			BytesWritten += count;
		}
		/// <summary>
		/// Writes a region of a <see cref="char" />[] to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="buffer">The <see cref="char" />[] containing the data to write.</param>
		/// <param name="index">The starting point in the buffer at which to begin writing.</param>
		/// <param name="count">The number of characters to write.</param>
		public void Write(char[] buffer, int index, int count)
		{
			AssertCanWrite();
			Check.ArgumentNull(buffer, nameof(buffer));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(index, nameof(index));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));
			Check.ArgumentEx.OffsetAndLengthOutOfBounds(index, count, buffer.Length);

			Writer.Write(buffer, index, count);
			BytesWritten += Encoding.GetByteCount(buffer, index, count);
		}
		/// <summary>
		/// Writes a one-byte <see cref="bool" /> value to the underlying <see cref="Stream" />, with 0 representing <see langword="false" /> and 1 representing <see langword="true" />.
		/// </summary>
		/// <param name="value">The <see cref="bool" /> value to write (0 or 1).</param>
		public void Write(bool value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten++;
		}
		/// <summary>
		/// Writes a <see cref="byte" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="byte" /> value to write.</param>
		public void Write(byte value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten++;
		}
		/// <summary>
		/// Writes a <see cref="sbyte" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="sbyte" /> value to write.</param>
		public void Write(sbyte value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten++;
		}
		/// <summary>
		/// Writes a <see cref="char" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The non-surrogate <see cref="char" /> value to write.</param>
		public void Write(char value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten += Encoding.GetByteCount(Singleton.Array(value));
		}
		/// <summary>
		/// Writes a <see cref="decimal" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="decimal" /> value to write.</param>
		public void Write(decimal value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten += 16;
		}
		/// <summary>
		/// Writes a <see cref="double" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="double" /> value to write.</param>
		public void Write(double value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten += 8;
		}
		/// <summary>
		/// Writes a <see cref="float" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="float" /> value to write.</param>
		public void Write(float value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten += 4;
		}
		/// <summary>
		/// Writes a <see cref="int" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value to write.</param>
		public void Write(int value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten += 4;
		}
		/// <summary>
		/// Writes a <see cref="uint" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="uint" /> value to write.</param>
		public void Write(uint value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten += 4;
		}
		/// <summary>
		/// Writes a <see cref="long" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to write.</param>
		public void Write(long value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten += 8;
		}
		/// <summary>
		/// Writes a <see cref="ulong" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="ulong" /> value to write.</param>
		public void Write(ulong value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten += 8;
		}
		/// <summary>
		/// Writes a <see cref="short" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="short" /> value to write.</param>
		public void Write(short value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten += 2;
		}
		/// <summary>
		/// Writes a <see cref="ushort" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="ushort" /> value to write.</param>
		public void Write(ushort value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten += 2;
		}
		/// <summary>
		/// Writes a length-prefixed <see cref="string" /> value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="value">The <see cref="string" /> value to write.</param>
		public void Write(string value)
		{
			AssertCanWrite();

			Writer.Write(value);
			BytesWritten += Encoding.GetByteCount(value) + Compute7BitEncodedIntSize(value.Length);
		}
		/// <summary>
		/// Writes a <see cref="byte" />[] value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="buffer">The <see cref="byte" />[] containing the data to write.</param>
		public void Write(byte[] buffer)
		{
			AssertCanWrite();
			Check.ArgumentNull(buffer, nameof(buffer));

			Writer.Write(buffer);
			BytesWritten += buffer.Length;
		}
		/// <summary>
		/// Writes a <see cref="char" />[] value to the underlying <see cref="Stream" />.
		/// </summary>
		/// <param name="chars">The <see cref="char" />[] containing the data to write.</param>
		public void Write(char[] chars)
		{
			AssertCanWrite();
			Check.ArgumentNull(chars, nameof(chars));

			Writer.Write(chars);
			BytesWritten += Encoding.GetByteCount(chars);
		}
		/// <summary>
		/// Writes a structure of the specified type to the underlying <see cref="Stream" />.
		/// </summary>
		/// <typeparam name="T">The type of the structure to write.</typeparam>
		/// <param name="structure">The structure to write.</param>
		public void WriteStructure<T>(T structure) where T : struct
		{
			AssertCanWrite();

			BaseStream.WriteStructure(structure);
			BytesWritten += Marshal.SizeOf(typeof(T));
		}
		/// <summary>
		/// Clears all buffers for this stream and causes any buffered data to be written to the underlying <see cref="Stream" />.
		/// </summary>
		public void Flush()
		{
			Writer?.Flush();
			BaseStream.Flush();
		}

		private static int Compute7BitEncodedIntSize(int value)
		{
			int size = 0;
			uint remaining = (uint)value;

			while (remaining >= 0x80)
			{
				remaining >>= 7;
				size++;
			}

			return size + 1;
		}
		private void AssertCanRead()
		{
			Check.Argument(Reader != null, null, "The base stream is not readable");
		}
		private void AssertCanWrite()
		{
			Check.Argument(Reader != null, null, "The base stream is not writable");
		}
	}
}