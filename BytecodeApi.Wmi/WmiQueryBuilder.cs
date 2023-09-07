using BytecodeApi.Extensions;

namespace BytecodeApi.Wmi;

/// <summary>
/// Class to accumulate information about a WMI query before executing it.
/// </summary>
public sealed class WmiQueryBuilder : IWmiQueryable
{
	internal readonly WmiClass Class;
	internal string[]? Columns;
	internal readonly List<string> Conditions;

	internal WmiQueryBuilder(WmiClass @class)
	{
		Class = @class;
		Conditions = new();
	}
	internal WmiQueryBuilder(WmiQueryBuilder query) : this(@query.Class)
	{
		Columns = query.Columns;
		Conditions = query.Conditions.ToList();
	}

	/// <summary>
	/// Specifies what columns to read from the WMI class. By default, all columns are read.
	/// </summary>
	/// <param name="columns">A <see cref="string" />[] specifying what columns to read from the WMI class.</param>
	/// <returns>
	/// The query with the additional SELECT statement.
	/// </returns>
	public IWmiQueryable Select(params string[] columns)
	{
		Check.ArgumentNull(columns);
		Check.ArgumentEx.ArrayValuesNotNull(columns);
		Check.ArgumentEx.ArrayValuesNotStringEmpty(columns);
		Check.Argument(Columns == null, nameof(columns), "This query already has a SELECT statement.");

		return new WmiQueryBuilder(this)
		{
			Columns = columns
		};
	}
	/// <summary>
	/// Specifies a filter condition for the WMI WHERE clause.
	/// </summary>
	/// <param name="condition">A statement for the WMI WHERE clause.</param>
	/// <returns>
	/// The query with the additional WHERE statement.
	/// </returns>
	public IWmiQueryable Where(string condition)
	{
		Check.ArgumentNull(condition);
		Check.ArgumentEx.StringNotEmpty(condition);

		WmiQueryBuilder query = new(this);
		query.Conditions.Add(condition);
		return query;
	}
	/// <summary>
	/// Executes this query and reads the contents from the WMI class.
	/// </summary>
	/// <returns>
	/// A new <see cref="WmiObject" />[] with the contents from the WMI class.
	/// </returns>
	public WmiObject[] ToArray()
	{
		using WmiIterator iterator = new(this);
		return iterator.ToArray();
	}
	/// <summary>
	/// Executes this query and reads the first element from the WMI class.
	/// </summary>
	/// <returns>
	/// A new <see cref="WmiObject" /> with the first element from the WMI class.
	/// </returns>
	public WmiObject First()
	{
		using WmiIterator iterator = new(this);
		return iterator.First();
	}
	/// <summary>
	/// Executes this query and reads the first element from the WMI class, and returns <see langword="null" />, if the query returned no elements.
	/// </summary>
	/// <returns>
	/// A new <see cref="WmiObject" /> with the first element from the WMI class, or <see langword="null" />, if the query returned no elements.
	/// </returns>
	public WmiObject? FirstOrDefault()
	{
		using WmiIterator iterator = new(this);
		return iterator.FirstOrDefault();
	}
}