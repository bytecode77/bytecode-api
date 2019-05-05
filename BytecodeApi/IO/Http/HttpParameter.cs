namespace BytecodeApi.IO.Http
{
	internal struct HttpParameter
	{
		public string Key { get; set; }
		public string Value { get; set; }

		public HttpParameter(string key, string value)
		{
			Key = key;
			Value = value;
		}
	}
}