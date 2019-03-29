namespace BytecodeApi.IO.FileSystem
{
	/// <summary>
	/// Specifies the type of an alternate data stream.
	/// </summary>
	public enum AlternateDataStreamType
	{
		/// <summary>
		/// The stream type is unknown.
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// The stream contains standard data.
		/// </summary>
		Data = 1,
		/// <summary>
		/// The stream contains extended attribute data.
		/// </summary>
		ExtendedAttributes = 2,
		/// <summary>
		/// The stream contains security data.
		/// </summary>
		SecurityData = 3,
		/// <summary>
		/// The stream contains an alternate data stream.
		/// </summary>
		AlternateDataStream = 4,
		/// <summary>
		/// The stream contains hard link information.
		/// </summary>
		Link = 5,
		/// <summary>
		/// The stream contains property data.
		/// </summary>
		PropertyData = 6,
		/// <summary>
		/// The stream contains object identifiers.
		/// </summary>
		ObjectId = 7,
		/// <summary>
		/// The stream contains reparse points.
		/// </summary>
		ReparseData = 8,
		/// <summary>
		/// The stream contains a sparse file.
		/// </summary>
		SparseBlock = 9,
		/// <summary>
		/// The stream contains transactional data.
		/// </summary>
		TransactionData = 10
	}
}