namespace BytecodeApi.Data;

/// <summary>
/// Provides information about the pagination of a collection of items.
/// </summary>
public sealed class Pagination
{
	/// <summary>
	/// Gets or sets the zero based page index.
	/// The default value is 0.
	/// </summary>
	public int Page { get; set; }
	/// <summary>
	/// Gets or sets the number of items included in a page.
	/// The default value is 10.
	/// </summary>
	public int PageSize { get; set; }
	/// <summary>
	/// Gets or sets the total number of items, or <see langword="null" />, if the total number of items is unknown.
	/// </summary>
	public int? TotalCount { get; set; }
	/// <summary>
	/// Gets the total number of pages, or <see langword="null" />, if the total number of items is unknown.
	/// </summary>
	public int? PageCount => TotalCount is null or < 0 || PageSize <= 0 ? null : (TotalCount.Value + PageSize - 1) / PageSize;
	/// <summary>
	/// Gets the number of items to skip in order to get the items for the current page.
	/// </summary>
	public int Skip => Page * PageSize;

	/// <summary>
	/// Initializes a new instance of the <see cref="Pagination" /> class.
	/// </summary>
	public Pagination()
	{
		PageSize = 10;
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="Pagination" /> class.
	/// </summary>
	/// <param name="page">The zero based page index.</param>
	/// <param name="pageSize">The number of items included in a page.</param>
	public Pagination(int page, int pageSize) : this()
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(page);
		Check.ArgumentOutOfRangeEx.Greater0(pageSize);

		Page = page;
		PageSize = pageSize;
	}
}