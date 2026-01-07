# BytecodeApi.CsvParser

Library for CSV parsing and writing.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.CsvParser

<details>
<summary>CsvFile</summary>

The `CsvFile` class can read and write CSV files:

```
CsvFile csv = CsvFile.FromFile(@"A:\path\to\file.csv", hasHeaderRow: true, delimiter: ";");

if (csv.HasErrors)
{
	Console.WriteLine("There were parsing errors.");
}

if (!csv.IsColumnCountConsistent)
{
	Console.WriteLine("Not all rows have the same amount of columns.");
}

// The headers. This value is null, if hasHeaderRow is set to false.
string[]? headers = csv.Headers;

foreach (CsvRow row in csv.Rows)
{
	foreach (CsvCell cell in row)
	{
		Console.WriteLine(cell.Value);
	}
}
```

To auto detect the delimiter, simply pass `null`:

```
CsvFile csv = CsvFile.FromFile(@"A:\path\to\file.csv", hasHeaderRow: true, delimiter: null);
```

After modifying the `CsvFile` object, it can be written:

```
csv.Save(@"A:\path\to\file.csv", alwaysQuote: false);
```

</details>

<details>
<summary>CsvIterator</summary>

Sometimes, parsing extremely large CSV files is a requirement. The `CsvIterator` provides streaming capabilities:

The iterator can also detect the delimiter, if the `delimiter` parameter is not provided:

```
IEnumerable<CsvRow> iterator = CsvIterator.FromFile(@"A:\path\to\file.csv");

foreach (CsvRow row in iterator)
{
	// ...
}
```

And to stream an `IEnumerable<CsvRow>` back, use `ToFile` or `ToStream`:

```
CsvIterator.ToFile(@"A:\path\to\file.csv", GetRows(), ";");

IEnumerable<CsvRow> GetRows()
{
	yield return new CsvRow("col1", "col2", "col3");
	// ...
}
```

</details>

## Changelog

### 4.0.0 (15.09.2025)

* **change:** Targeting .NET 9.0
* **new:** `byte[] CsvFile.Save()` method overload

### 3.0.0 (08.09.2023)

* Initial release