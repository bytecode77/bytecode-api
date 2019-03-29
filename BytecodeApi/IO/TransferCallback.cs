namespace BytecodeApi.IO
{
	/// <summary>
	/// Represents the method that is called periodically while binary data is transferred.
	/// </summary>
	/// <param name="bytes">The number of bytes that have been transferred since the last call to this method.</param>
	/// <param name="totalBytes">The total number of bytes that have been transferred.</param>
	public delegate void TransferCallback(long bytes, long totalBytes);
}