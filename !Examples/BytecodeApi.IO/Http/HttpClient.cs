using BytecodeApi.IO.Http;
using System;
using System.IO;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Create HTTP client
		HttpClient http = new HttpClient(true, "BytecodeApi.IO.Http.HttpClient");

		// The HTTP client wraps common HTTP functionality (GET, POST, etc.)
		// and returns the content as a string, byte[], Stream or file.

		// Note, that example.com does not actually support following examples.

		string example = http
			.Get("https://example.com/")            // Create GET request
			.ReadString();                          // Execute request and store result as string

		byte[] binaryExample = http
			.Get("https://example.com/")
			.AddQueryParameter("param", "value")    // /?param=value
			.ReadBytes();                           // Read as byte[]

		http
			.Post("https://example.com/login")      // Posting into a form
			.AddQueryParameter("param", "value")    // /?param=value
			.AddPostValue("username", "admin")
			.AddPostValue("password", "123456")
			.AddPostValue("btnOk", "1")
			.ReadString();

		// Using callback for progress calculation:
		http
			.Post("https://example.com/large_file.zip")
			.ReadFile(Path.Combine(Path.GetTempPath(), "download_test"), (bytes, totalBytes) =>
			{
				Console.WriteLine(bytes + " bytes read since the last callback.");
				Console.WriteLine(totalBytes + " bytes read in total.");
			});
	}
}