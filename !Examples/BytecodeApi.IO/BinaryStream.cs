using BytecodeApi.IO;
using System;
using System.IO;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		using (MemoryStream memoryStream = new MemoryStream())
		{
			// BinaryStream is a wrapper that uses both BinaryReader and BinaryWriter, based on whether the stream is readable/writable.
			// This abstraction layer can read and write any default data type.
			// If the stream does not support writing, the Write*() methods will throw an exception.

			using (BinaryStream binaryStream = new BinaryStream(memoryStream))
			{
				// Write 3 integers
				binaryStream.Write(123);
				binaryStream.Write(456);
				binaryStream.Write(789);

				// Seek to beginning and read 3 integers
				binaryStream.BaseStream.Seek(0, SeekOrigin.Begin);
				int a = binaryStream.ReadInt32(); // 123
				int b = binaryStream.ReadInt32(); // 456

				// The BinaryStream keeps track of read & written byte count:
				Console.WriteLine("Bytes read: " + binaryStream.BytesRead);
				Console.WriteLine("Bytes written: " + binaryStream.BytesWritten);
			}
		}

		Console.ReadKey();
	}
}