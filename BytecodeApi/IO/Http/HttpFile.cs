namespace BytecodeApi.IO.Http
{
	internal sealed class HttpFile
	{
		public string FormName { get; set; }
		public string FileName { get; set; }
		public byte[] Content { get; set; }

		public HttpFile(string formName, string fileName, byte[] content)
		{
			FormName = formName;
			FileName = fileName;
			Content = content;
		}
	}
}