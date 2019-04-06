using BytecodeApi;
using BytecodeApi.Extensions;
using BytecodeApi.FileFormats.Coff;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTest
{
	[TestClass]
	public class FileFormatsTests
	{
		[TestMethod]
		public void BytecodeApi_FileFormats_Coff()
		{
			byte[] file = File.ReadAllBytes(@"C:\Windows\explorer.exe");
			PEImage image = PEImage.FromBinary(file);

			Assert.IsTrue(image.ToBinary().Compare(file));
		}
	}
}