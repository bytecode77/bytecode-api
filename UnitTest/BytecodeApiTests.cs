using BytecodeApi;
using BytecodeApi.Extensions;
using BytecodeApi.IO;
using BytecodeApi.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class BytecodeApiTests
	{
		[TestMethod]
		public void BytecodeApi_IO_Compression()
		{
			byte[] data = MathEx.RandomNumberGenerator.GetBytes(10000);
			Assert.IsTrue(Compression.Decompress(Compression.Compress(data)).Compare(data), "Compression and decompression did not succeed.");
		}
		[TestMethod]
		public void BytecodeApi_ApplicationBase_OperatingSystem_FrameworkVersion_UpToDate()
		{
			// If convertible to Int32, indicates a fallback version number. Example: "461310" instead of "4.7.1".
			Assert.IsNull(ApplicationBase.OperatingSystem.FrameworkVersion.ToInt32OrNull());
		}
	}
}