using BytecodeApi.Extensions;
using System.Collections;
using System.Management;

namespace BytecodeApi.Wmi;

internal class WmiIterator : IEnumerable<WmiObject>, IDisposable
{
	private readonly WmiQueryBuilder QueryBuilder;
	private readonly ManagementObjectSearcher Searcher;
	private readonly ManagementObjectCollection Objects;
	private bool Disposed;

	public WmiIterator(WmiQueryBuilder queryBuilder)
	{
		string columnsString = queryBuilder.Columns == null ? "*" : queryBuilder.Columns.AsString(", ");
		string whereString = queryBuilder.Conditions.None() ? "" : $" WHERE {queryBuilder.Conditions.Select(c => $"({c})").AsString(" AND ")}";

		QueryBuilder = queryBuilder;
		Searcher = new(queryBuilder.Class.Namespace.FullPath, $"SELECT {columnsString} FROM {queryBuilder.Class.Name}{whereString}");
		Objects = Searcher.Get();
	}
	public void Dispose()
	{
		if (!Disposed)
		{
			Searcher.Dispose();
			Objects.Dispose();

			Disposed = true;
		}
	}

	public IEnumerator<WmiObject> GetEnumerator()
	{
		return Objects
			.Cast<ManagementObject>()
			.AsDisposable()
			.Select(obj => new WmiObject
			(
				QueryBuilder.Class,
				obj.Properties
					.Cast<PropertyData>()
					.OrderBy(property => property.Name, StringComparer.OrdinalIgnoreCase)
					.Select(property => new WmiProperty(property.Name, property.Value))
			))
			.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}