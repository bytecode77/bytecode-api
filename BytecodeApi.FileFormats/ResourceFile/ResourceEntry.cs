namespace BytecodeApi.FileFormats.ResourceFile
{
	/// <summary>
	/// Represents a named resource entry of a PE file.
	/// </summary>
	/// <typeparam name="TData">The data type of the resource entry</typeparam>
	public sealed class ResourceEntry<TData>
	{
		/// <summary>
		/// Gets or sets the type of the resource entry.
		/// </summary>
		public ResourceType Type { get; set; }
		/// <summary>
		/// Gets or sets the name of the resource entry.
		/// </summary>
		public int Name { get; set; }
		/// <summary>
		/// Gets or sets the data representation of the resource entry.
		/// </summary>
		public TData Data { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ResourceEntry{TData}" /> class.
		/// </summary>
		/// <param name="type">The type of the <see cref="ResourceEntry{TData}" />.</param>
		/// <param name="name">The name of the <see cref="ResourceEntry{TData}" />.</param>
		/// <param name="data">The data representation of the <see cref="ResourceEntry{TData}" />.</param>
		public ResourceEntry(ResourceType type, int name, TData data)
		{
			Type = type;
			Name = name;
			Data = data;
		}
	}
}