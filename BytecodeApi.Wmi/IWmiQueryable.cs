namespace BytecodeApi.Wmi;

/// <summary>
/// Provides functionality to evaluate queries against WMI classes.
/// </summary>
public interface IWmiQueryable
{
	/// <summary>
	/// Specifies what columns to read from the WMI class. By default, all columns are read.
	/// </summary>
	/// <param name="columns">A <see cref="string" />[] specifying what columns to read from the WMI class.</param>
	/// <returns>
	/// The query with the additional SELECT statement.
	/// </returns>
	IWmiQueryable Select(params string[] columns);
	/// <summary>
	/// Specifies a filter condition for the WMI WHERE clause.
	/// </summary>
	/// <param name="condition">A statement for the WMI WHERE clause.</param>
	/// <returns>
	/// The query with the additional WHERE statement.
	/// </returns>
	IWmiQueryable Where(string condition);
	/// <summary>
	/// Executes this query and reads the contents from the WMI class.
	/// </summary>
	/// <returns>
	/// A new <see cref="WmiObject" />[] with the contents from the WMI class.
	/// </returns>
	WmiObject[] ToArray();
	/// <summary>
	/// Executes this query and reads the first element from the WMI class.
	/// </summary>
	/// <returns>
	/// A new <see cref="WmiObject" /> with the first element from the WMI class.
	/// </returns>
	WmiObject First();
	/// <summary>
	/// Executes this query and reads the first element from the WMI class, and returns <see langword="null" />, if the query returned no elements.
	/// </summary>
	/// <returns>
	/// A new <see cref="WmiObject" /> with the first element from the WMI class, or <see langword="null" />, if the query returned no elements.
	/// </returns>
	WmiObject? FirstOrDefault();
}