using BytecodeApi.IO.FileSystem;
using System;
using System.IO;
using System.Threading;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Define a file that contains cached contents.
		// This is useful to store information that take long to retrieve and should
		// be kept a specific period of time before retrieving again

		// Example: List of all postal codes and street names, retrieved from a WebService
		// The list only changes occasionally and should be stored on the disk to increase application startup

		CacheFile cacheFile = CacheFile.CreateWithTimeout
		(
			Path.Combine(Path.GetTempPath(), "cachefile_example"),
			TimeSpan.FromSeconds(10),
			CacheFile_UpdateCallback
		);

		while (true)
		{
			// Read the file.

			// When retrieving for the first time, the update callback is invoked
			// After that, the callback update is only invoked, if the timeout of 10 seconds has passed

			byte[] file = cacheFile.ReadAllBytes();
			Console.WriteLine("File was read");
			Thread.Sleep(1000);
		}
	}

	private static void CacheFile_UpdateCallback(FileStream stream)
	{
		// This Callback is invoked when the cached file needs to be updated

		// Very complex function that retrieves a lot of data through a WebService:
		stream.Write(new byte[100], 0, 100);

		Console.WriteLine("UpdateCallback was invoked; Cached file was written to");
	}
}