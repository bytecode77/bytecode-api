using BytecodeApi;
using BytecodeApi.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class BytecodeApiTests
	{
		[TestMethod]
		public void FrameworkVersionUpToDate()
		{
			// If convertible to Int32, indicates a fallback version number. Example: "461310" instead of "4.7.1".
			Assert.IsNull(ApplicationBase.OperatingSystem.FrameworkVersion.ToInt32OrNull());
		}
	}
}