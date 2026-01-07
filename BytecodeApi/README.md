# BytecodeApi

Framework for C# development.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi

<details>
<summary>ApplicationBase</summary>

Get information about the application: (non-exhaustive list)

```
string pathToExe = ApplicationBase.Path;
string myVersion = ApplicationBase.Version;

string processId = ApplicationBase.Process.Id;
string processElevated = ApplicationBase.Process.IsElevated;
```

Restart this process with elevated privileges. `Environment.Exit` will be invoked, after the new process was successfully started.

```
ApplicationBase.RestartElevated("", () => Environment.Exit(0));
```
</details>

<details>
<summary>CachedProperty</summary>

Load the contents of a property once and keep it cached:

```
public static CachedProperty<string> MyCachedFile { get; set; } = new(() =>
{
	// The file is only read the first time when the getter is invoked.
	return File.ReadAllText(@"C:\large_file.txt");
});
```

Optionally, specify a duration after which the value is invalidated and the getter will be invoked again:

```
public static CachedProperty<string> MyCachedFile { get; set; } = new(() =>
{
	return File.ReadAllText(@"C:\large_file.txt");
}, TimeSpan.FromMinutes(10));
```
</details>

<details>
<summary>CSharp</summary>

Try / catch wrappers:

```
// Returns false, if an exception was thrown:
bool worked = CSharp.Try(() => MyFunction());

// Retrieves the string and null, if an exception was thrown:
string? value = CSharp.Try(() => RetrieveString());

// Retry 10 times:
UserDto user = CSharp.Retry(() => GetUser(1), 10);
```

Copy all properties of an object to an object of a different type:

```
UserDto userDto = CSharp.ConvertObject<UserDto>(userEntity, ConvertObjectOptions.IgnoreCase);
```
</details>

<details>
<summary>DateTimeEx</summary>

Convert unix timestamps:

```
int? unixTimeStamp = DateTimeEx.ToUnixTimeStamp(DateTime.Now);
DateTime dateTime = DateTimeEx.FromUnixTimeStamp(unixTimeStamp);
```

Arithmetics:

```
int age = CalculateAgeFromBirthday(new DateTime(1991, xx, xx));
```
</details>

<details>
<summary>EnumEx</summary>

Querying the `DescriptionAttribute`:

```
// Get values with descriptions
Dictionary<MyEnum, string> lookup = EnumEx.GetDescriptionLookup<MyEnum>();

// Find enum value by description
MyEnum? value = EnumEx.FindValueByDescription<MyEnum>("The first value");

public enum MyEnum
{
	[Description("The first value")]
	Value1,
	[Description("The second value")]
	Value2,
	[Description("The third value")]
	Value3
}
```
</details>

<details>
<summary>IndexedProperty & ReadOnlyIndexedProperty</summary>

Use `IndexedProperty` or `ReadOnlyIndexedProperty` to provide a property that has an indexer without the need to create a new class.

This indexed property can be backed by, e.g. a `Dictionary`, or the getter and setter can access underlying data from a custom source.

```
public ReadOnlyIndexedProperty<string, ConnectionString> ConnectionStrings { get; private set; } = new(name =>
{
	// Getter
	return GetConnectionStringByName(name);
});

ConnectionString myConnection = ConnectionStrings["Database1"];
```

```
public IndexedProperty<int, string> MyValueCollection { get; private set; } = new(id =>
{
	// Getter
	return "The value";
},
(id, value) =>
{
	// Setter
	// Store "value" under the index "id"
});

// Set
MyValueCollection[1] = "foo";
MyValueCollection[2] = "bar";

// Get
string fooString = MyValueCollection[1];
```
</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Data

<details>
<summary>Blob & BlobTree</summary>

A `Blob` is a type with a name and a `byte[]` content:

```
byte[] content = ...
Blob blob = new("my_data", content);

// or:
Blob blob = Blob.FromFile(@"C:\file.txt")
```

A `BlobTree` is a tree structure, similar to a directory & file structure.

```
BlobTree tree = BlobTree.FromDirectory(@"C:\directory_name");
```

Helper method, like `FromFile` and `FromDirectory` exist, but blobs are generic data types and are not tied or limited to files or directories.

Some more helpers around blobs exist:

```
// Find "blobname" in a specified path within the BlobTree:
Blob blob = tree.FindBlob(@"path\to\node\blobname");

// Write entire BlobTree as it is to disk:
tree.SaveToDirectory(@"C:\target_path");
```

The `ZipCompression` class is an adapter between ZIP file compression and `BlobCollection` & `BlobTree` structures.
</details>

<details>
<summary>TreeNode</summary>

A `TreeNode` is a generic, hierarchical data structure. Each `TreeNode` has a value and children.

```
TreeNode<string> tree = new("My root node");

tree.Add("child node 1");
tree.Add("child node 2");
TreeNode<string> child3 = tree.Add("child node 3");

child3.Add("child node 3 child 1");
```

To construct a `TreeNode` statically, use `TreeNodeBuilder`:

```
TreeNode<string> tree = TreeNodeBuilder
	.BeginTree("My root node")
		.Begin("child node 1")
		.End()
		.Begin("child node 2")
		.End()
		.Begin("child node 3")
			.Begin("child node 3 child 1")
			.End()
		.End()
	.EndTree();
```

The `TreeNode` class has various methods for iteration to flatten, access ancestors, siblings, children, etc. A link to its parent node allows navigation up the tree.
</details>

<details>
<summary>Money & Currency</summary>

The `Money` datatype wraps an amount with a currency:

```
Money m = new(9.99, Currency.EUR);
```

With the `CurrencyConverter` and user-provided exchange rates, `Money` objects can be converted into other currencies:

```
CurrencyConverter converter = new()
	.HasConversion(Currency.EUR, Currency.USD, 0.95)
	.HasConversion(Currency.EUR, Currency.CHF, 1.05);

Money convertedValue = converter.Convert(m, Currency.USD);
```
</details>

<details>
<summary>ObservableObject</summary>

The `ObservableObject` class provides a base class for observable objects. This is especially relevant in WPF & MVVM.

```
public class MyDto : ObservableObject
{
	private string _Name;
	public string Name
	{
		get => _Name;
		set => Set(ref _Name, value);
	}
}
```

The pattern is simple and does not bulge performance.
</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Extensions

<details>
<summary>Extension Methods</summary>

This namespace contains extensions for all default types and several common .NET classes.

Here are some examples. However, there are so many extension methods that it's not possible to provide examples for each of them here.

```
// StringExtensions:

string subStr = "Hello, world!".SubstringFrom(","); // " world!"

string[] lines = "line 1\r\nline2\r\nline 3".SplitToLines();

int? maybeInt32Value = "123".ToInt32OrNull();


// ByteArrayExtensions:

bool arraysEqual = byteArrayA.Compare(byteArrayB);

int index = byteArrayA.FindSequence(byteArrayB);
```
</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Interop

<details>
<summary>DynamicLibrary</summary>

Dynamic invocation of DLL functions:

```
DynamicLibrary user32 = new("user32.dll");
DynamicLibraryFunction<int> function = user32.GetFunction<int>("GetTickCount", CallingConvention.StdCall, CharSet.Auto);

int ticks = function.Call();
```
</details>

<details>
<summary>HGlobal</summary>

Safe HGLOBAL Wrapper:

```
// Allocate HGLOBAL with 1024 bytes.
using (HGlobal mem = new(1024))
{
}

// Disposed!
```

Helper methods:

```
HGlobal mem = HGlobal.FromArray(byteArray);
byte[] array = mem.ToArray();

HGlobal memFromStruct = HGlobal.FromStructure(myStruct);

```
</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.IO

<details>
<summary>AlternateDataStreamInfo</summary>

Iterate alternate data streams of a file:

```
AlternateDataStreamInfo adsInfo = new(@"C:\file.txt");

foreach (AlternateDataStream ads in adsInfo.Streams)
{
	Console.WriteLine(ads.Name);
	Console.WriteLine(ads.ReadAllText());
}
```
</details>

<details>
<summary>BinaryStream</summary>

The `BinaryStream` combines the capabilities of `BinaryReader` and `BinaryWriter` and keeps track of the read and written byte count.

```
using (FileStream fileStream = File.OpenWrite(@"C:\file.txt"))
{
	using BinaryStream stream = new(fileStream);

	// Write to underlying stream
	stream.Write(123);
	stream.Write("foo");

	stream.BaseStream.Seek(0, SeekOrigin.Begin);

	// Read from underlying stream
	int number = stream.ReadInt32();
	string str = stream.ReadString();
}
```
</details>

<details>
<summary>CliCommand</summary>

Execute a file and retrieve the exit code and console output:

```
CliResult result = CliCommand
	.FileName("netstat")
	.Arguments("-o")
	.Hidden()
	.Execute();

int exitCode = result.ExitCode;
string consoleOutput = result.Output;
```
</details>

<details>
<summary>Compression</summary>

The `Compression` class offers a quick way to compress and decompress `byte[]` values:

```
byte[] compressed = Compression.Compress(data);
byte[] decompressed = Compression.Decompress(compressed);
```
</details>

<details>
<summary>TempDirectory</summary>

Creates a file in the system temp directory with the `FileAttributes.Temporary` attribute:

```
string path = TempDirectory.CreateFile("file.txt", byteArray);

// path = C:\Users\john\AppData\Local\Temp\{27666d85-2ab5-4c30-aa70-f00fc07f03e8}\file.txt
```

A subdirectory ensures that the path is unique **and** the file name does not need to be changed.
</details>

<details>
<summary>ZipCompression</summary>

The `ZipCompression` class compresses and decompresses ZIP archives from `BlobTree` objects:

```
// Decompress ZIP file into hierarchical structure:
BlobTree decompressed = ZipCompression.Decompress(@"C:\file.zip");

// Create ZIP file from hierarchical structure:
byte[] zipFile = ZipCompression.Compress(decompressed);
```
</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Mathematics

<details>
<summary>BitCalculator</summary>

This class performs bitwise computations on numeric values.

Get n'th bit from integer number:

```
bool bit = BitCalculator.GetBit(number, 3);
```

Set n'th bit of integer number:

```
// true = bit set; false = bit not set
number = BitCalculator.SetBit(number, 3, true);
```
</details>

<details>
<summary>ByteSize</summary>

The `ByteSize` structure represents a size, in bytes:

```
ByteSize size = 10000;

// "9,77 KB"
string str = size.Format();
```
</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Text

<details>
<summary>StringDistance</summary>

String distance algorithms:

```
int distance1 = StringDistance.Levenshtein("hello", "holla");
int distance2 = StringDistance.DamerauLevenshtein("hello", "holla");
```
</details>

<details>
<summary>Wording</summary>

String utility for linguistic text processing:

```
// "This is..."
string trimmedStr = Wording.TrimText("This is a very long sentence.", 10);
```

```
// "head, shoulders, knees and toes"
string joined = Wording.JoinStrings(", ", " and ", "head", "shoulders", "knees", "toes");
```

```
// Text where each line does not exceed 80 characters:
string wrappedTo80chars = Wording.WrapText("A whole paragraph with 1000 words [...]", 80, false);
```
</details>

## Changelog

### 4.0.1 (27.10.2025)

* **change:** Added `ParseExact` parameter to JSON converters

### 4.0.0 (15.09.2025)

* **change:** Targeting .NET 9.0
* **new:** `StringLengthJsonConverter` class
* **new:** `BytecodeApi.Data.ObservableTuple` class
* **new:** `BytecodeApi.Threading.CriticalSection` class
* **removed:** `CSharp.GetHashCode` method
* **removed:** `MathEx.Random` property
* **removed:** `Network.DisableCertificateValidation` and `EnableAllSecurityProtocols` methods.

### 3.0.6 (28.11.2024)

* **new:** `CSharp.RunTask` method
* **new:** `EnumerableExtensions.Union` method
* **new:** `DateTimeEx`, `DateTimeExtensions` and `DateOnlyExtensions` methods

### 3.0.5 (27.07.2024)

* **new:** `CliCommand.ExecuteAsync` method
* **removed:** `StringExtensions.ReplaceLineBreaks`
* **new:** `DateTimeEx.IsValidDate` and `GetMonthsDifference` method

### 3.0.4 (10.12.2023)

* **new:** `BytecodeApi.Data.TreeNode` class
* **new:** `DateOnlyExtensions` class
* **new:** `ReflectionExtensions.GetValue` method overloads
* **new:** `CliCommand.Execute` method overload
* **new:** `MathEx.Min` and `Max` method overloads for `DateOnly` and `TimeOnly`
* **new:** `DateOnlyJsonConverter` constructor with format parameter
* **new:** `TimeOnlyJsonConverter` constructor with format parameter
* **bugfix:** `MathEx.Interpolate` mapToValueRange parameter did not work correcly
* **removed:** `EnumerableExtensions.Sort` and `SortDescending` method
* **removed:** `EnumEx.GetValues` method overload

### 3.0.3 (30.09.2023)

* **new:** `RandomExtensions.NextEnumValue` method
* **new:** `RandomNumberGeneratorExtensions.GetEnumValue` method
* **new:** `RegistryExtensions.GetExpandStringValue` and `SetExpandStringValue` method

### 3.0.2 (27.09.2023)

* **new:** `SevenBitInteger` class
* **removed:** `ConvertEx.To7BitEncodedInt` and `From7BitEncodedInt`

### 3.0.1 (08.09.2023)

* Initial release