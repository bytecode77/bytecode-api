namespace BytecodeApi.IO.FileSystem
{
	/// <summary>
	/// Represents an alternate data stream entry of a file or directory.
	/// </summary>
	public class AlternateDataStream
	{
		/// <summary>
		/// Gets the name of the alternate data stream without the leading colon.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the size of the alternate data stream.
		/// </summary>
		public long Size { get; private set; }
		/// <summary>
		/// Gets the type of the alternate data stream.
		/// </summary>
		public AlternateDataStreamType Type { get; private set; }
		/// <summary>
		/// Gets the alternate data stream attributes.
		/// </summary>
		public AlternateDataStreamAttributes Attributes { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AlternateDataStream" /> class with the specified name, size, type and attributes.
		/// </summary>
		/// <param name="name">The name of the alternate data stream without the leading colon.</param>
		/// <param name="size">The size of the alternate data stream.</param>
		/// <param name="type">The type of the alternate data stream.</param>
		/// <param name="attributes">The alternate data stream attributes.</param>
		public AlternateDataStream(string name, long size, AlternateDataStreamType type, AlternateDataStreamAttributes attributes)
		{
			Name = name;
			Size = size;
			Type = type;
			Attributes = attributes;
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Name + ", Size: " + Size + "]";
		}
	}
}