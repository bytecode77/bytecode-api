using BytecodeApi;
using BytecodeApi.Data;
using BytecodeApi.Extensions;
using BytecodeApi.IO;
using BytecodeApi.Mathematics;
using BytecodeApi.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace UnitTest
{
	[TestClass]
	public class BytecodeApiTests
	{
		[TestMethod]
		public void BytecodeApi_ApplicationBase_OperatingSystem_FrameworkVersionNumber_UpToDate()
		{
			Assert.IsNotNull(ApplicationBase.OperatingSystem.FrameworkVersionNumber);
		}
		[TestMethod]
		public void BytecodeApi_ApplicationBase_OperatingSystem_FrameworkVersionName_UpToDate()
		{
			Assert.IsNotNull(ApplicationBase.OperatingSystem.FrameworkVersionName);
		}
		[TestMethod]
		public void BytecodeApi_ConvertEx()
		{
			byte[] data = MathEx.RandomNumberGenerator.GetBytes(100);
			Assert.IsTrue(ConvertEx.FromHexadecimalString(ConvertEx.ToHexadecimalString(data)).Compare(data));
			Assert.IsTrue(ConvertEx.FromBase32String(ConvertEx.ToBase32String(data)).Compare(data));

			for (int i = 0; i < 1000; i++)
			{
				Assert.AreEqual(ConvertEx.FromExcelColumnString(ConvertEx.ToExcelColumnString(i)), i);
				Assert.AreEqual(ConvertEx.From7BitEncodedInt(ConvertEx.To7BitEncodedInt(i)), i);
			}

			Point point = ConvertEx.ToStructure<Point>(ConvertEx.FromStructure(new Point(1337, 4242)));
			Assert.IsTrue(point.X == 1337 && point.Y == 4242);
		}
		[TestMethod]
		public void BytecodeApi_IO_Compression()
		{
			byte[] data = MathEx.RandomNumberGenerator.GetBytes(10000);
			Assert.IsTrue(Compression.Decompress(Compression.Compress(data)).Compare(data));
		}
		[TestMethod]
		public void BytecodeApi_Text_Wording()
		{
			string str = Create.AlphaNumericString(100);
			Assert.AreEqual(Wording.Rot13(Wording.Rot13(str)), str);
		}
		[TestMethod]
		public void BytecodeApi_IO_ZipCompression()
		{
			byte[] data1 = MathEx.RandomNumberGenerator.GetBytes(10000);
			byte[] data2 = MathEx.RandomNumberGenerator.GetBytes(10000);

			BlobTree tree = new BlobTree();
			tree.Root.Blobs.Add(new Blob("file1.txt", data1));
			tree.Root.Blobs.Add(new Blob("file2.txt", data2));

			BlobTree decompressed = ZipCompression.Decompress(ZipCompression.Compress(tree));
			Assert.IsTrue(tree.Root.Blobs[0].Compare(decompressed.Root.Blobs[0]));
			Assert.IsTrue(tree.Root.Blobs[1].Compare(decompressed.Root.Blobs[1]));
		}
	}
}