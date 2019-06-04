# BytecodeApi

BytecodeApi implements lots of extensions and classes for general purpose use.
In addition, specific classes implement more complex logic for both general app
development as well as for WPF apps.

Especially, boilerplate code that is known to be part of any Core DLL in a C#
project is likely to be already here. In fact, I use this library in many of my
own projects. For this reason, each class and method has been reviewed numerous
times.

BytecodeApi is highly consistent, particularly in terms of structure, naming
conventions, patterns, etc. The entire code style resembles the patterns used in
the .NET Framework itself. You will find it intuitive to understand.

Documentation can be taken for granted! <b>All</b> classes and members have XML
tags. The [online documentation](https://docs.bytecode77.com/bytecode-api/)
makes it easy to find the class or method you are looking for.

## Examples

Following examples are a very brief overview over what features you can come to
expect. However, the entire framework covers a lot more. Please review the
[online documentation](https://docs.bytecode77.com/bytecode-api/) to get a
complete overview.

### Example: Advanced conversion

```
// Additional conversion methods:

byte[] structBytes = ConvertEx.StructureToArray(myStruct);
MyStruct myStruct = ConvertEx.ArrayToStructure<MyStruct>(structBytes);
			
string excelColumn = ConvertEx.ToExcelColumnString(1); // <-- "A"

string hexString = ConvertEx.ToHexadecimalString(byteArray);
```

### Example: Thread-safe access (Atomic)

```
Atomic<MyClass> ThreadSafeObj = new Atomic<MyClass>();

// Access an object in a thread-safe manner:
ThreadSafeObj.Value.Foo = 123;

// Or exchange the object:
ThreadSafeObj.Exchange((oldObject) => // <-- The current MyClass instance
{
	return new MyClass(); // <-- the new MyClass instance to replace "oldObject".
});

// Call a method, while the object is locked:
int result = ThreadSafeObj.Lock((obj) => obj.GetInteger());

// ...

public class MyClass
{
	public int Foo;

	public int GetInteger()
	{
		return 0;
	}
}
```

### Example: CSV parsing

```
CsvFile file = CsvFile.FromFile(path: @"C:\test.csv", delimiter: null, hasHeaderRow: true);

// delimiter: null means auto-detect!

foreach (CsvRow row in file.Rows)
{
	int? id = row["A"].Int32Value;
	string name = row["B"].Value;
}

// And for the fans or victims of "CSV extreme databasing"
// This IEnumerable will iterate over the CSV file
IEnumerable<CsvRow> largeCsv = CsvFile.EnumerateFile(@"C:\very_large_file.csv", null, true);
```

### Example: Advanced File I/O

```
// Find a string in a file
long index = FileEx.FindSequence(@"C:\test.txt", "search_me".ToUTF8Bytes());
if (index != -1)
{
	// ...
}

// FileEx extends the System.IO.File class.
```

### Example: HTTP GET & POST request handling

```
// HttpClient implements GET, POST and multipart requests
string str = new HttpClient()
	.CreatePostRequest("http://example.com/")
	.AddQueryParameter("id", "123")             // GET parameter for URL
	.AddPostValue("submit", "on")               // POST value (e.g.: The Submit button)
	.ReadString();
//              ^- Complete the request and get the result as string or byte[]
```

### Example: WMI wrapper class

```
// Read WMI objects using the BytecodeApi.IO.Wmi namespace:
WmiObject[] processes = new WmiNamespace("CIMV2")
	.GetClass("Win32_Process")
	.GetObjects()
	.ToArray();
```

### Example: Compression

```
byte[] file = File.ReadAllBytes("C:\\test.txt");

// Easily compress and decompress binary data:
byte[] compressed = Compression.Compress(file);
byte[] decompressed = Compression.Decompress(compressed);
```

### Example: Cryptography

```
byte[] file = File.ReadAllBytes(@"C:\test.txt");

// Encrypt and decrypt binary content using a password.
byte[] encrypted = ContentEncryption.Encrypt(file, "123456");
byte[] decrypted = ContentEncryption.Decrypt(encrypted, "123456");

// Test using a the byte[] extension method:
if (!encrypted.Compare(decrypted)) throw new Exception();

// The "ContentEncryption" class uses
//     byte[] Encryption.Encrypt(byte[] data, byte[] iv, byte[] key)
// in conjunction with the SHA-256 converted password.

// Hash examples:
string passwordHash = Hashes.Compute("123456", HashType.SHA256);
string fileHash = Hashes.Compute(file, HashType.SHA256);
```

### Example: DLL injection

```
Process process = ProcessEx.GetSessionProcessesByName("TaskMgr").First();

// Inject my wonderful ring-3 rootkit into your task manager.
process.LoadLibrary(@"C:\r77.dll");

// Project r77: https://bytecode77.com/hacking/payloads/r77-rootkit
```

### Example: ViewModel base class (INotifyPropertyChanged)

```
public class MyViewModel : ObservableObject
{
	// An MVVM property for the ViewModel.
	// Strongly typed and as simple as it gets:
	public string Foo
	{
		get => Get(() => Foo);
		set => Set(() => Foo, value);
		// INotifyPropertyChanged -^
	}
}
```

## Online Documentation

BytecodeApi is fully documented using XML tags. The
[online documentation](https://docs.bytecode77.com/bytecode-api/) is an
up-to-date reference.

## Downloads

[![](https://bytecode77.com/images/shared/fileicons/zip.png) BytecodeApi 1.3.3 Binaries.zip](https://bytecode77.com/downloads/framework/bytecode-api/BytecodeApi%201.3.3%20Binaries.zip)

## Project Page

[![](https://bytecode77.com/images/shared/favicon16.png) bytecode77.com/framework/bytecode-api](https://bytecode77.com/framework/bytecode-api)