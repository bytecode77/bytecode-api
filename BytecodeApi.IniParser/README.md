# BytecodeApi.IniParser

Library for INI file reading and writing.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.IniParser

This example INI file is used in the examples.

```
global_prop1 = hello
global_prop2 = world

[section1]
key1 = value1
key2 = value2

[section2]
key3 = value3

; A section that occurs multiple times
[section2]
key4 = value 4
```

<details>
<summary>Read INI file</summary>

Load INI file and retrieve a property:

The `Section` and `Property` methods are accessors; The collections of sections and properties can also be queried using LINQ.

```
// Load INI file:
IniFile iniFile = IniFile.FromFile(@"C:\path\to\file.ini");

// Retrieve "section1"
if (iniFile.Section("section1", true) is IniSection section1) // true = ignore case
{
	// Retrieve property "key1"
	IniProperty? property = section1.Property("key1", true); // true = ignore case
			
	if (property != null)
	{
		// Property value:
		string value = property.Value;
	}
}
```
</details>

<details>
<summary>Loop through all sections & properties</summary>

Loop through the entire contents of an INI file:

```
// Load INI file:
IniFile iniFile = IniFile.FromFile(@"C:\path\to\file.ini");

// "global properties" are properties without a section. They are on top of the INI file
foreach (IniProperty globalProperty in iniFile.GlobalProperties.Properties)
{
	Console.WriteLine($"Name: {globalProperty.Name}, Value: {globalProperty.Value}");
}

// Loop through all sections:
foreach (IniSection section in iniFile.Sections)
{
	// Loop through section properties:
	foreach (IniProperty property in section.Properties)
	{
		Console.WriteLine($"Section: {section.Name}, Name: {property.Name}, Value: {property.Value}");
	}
}
```
</details>

<details>
<summary>IniFileParsingOptions</summary>

This class is a set of rules that is applied when reading an INI file.

```
IniFileParsingOptions parsingOptions = new()
{
	PropertyDelimiter = IniPropertyDelimiter.Colon,
	DuplicatePropertyNameBehavior = IniDuplicatePropertyNameBehavior.Overwrite

	// See XML comments for a documentation of each property
};

// Load INI file with custom parsing options:
IniFile iniFile = IniFile.FromFile(@"C:\path\to\file.ini", Encoding.UTF8, parsingOptions);
```
</details>

<details>
<summary>Write INI file</summary>

Either create an `IniFile`, or load an existing to modify it. Call `IniFile.Save` to save the new or modified INI file.

```
IniFile iniFile = new();

iniFile.GlobalProperties.Properties.Add("global_prop1", "hello");
iniFile.GlobalProperties.Properties.Add("global_prop2", "world");

IniSection setion1 = iniFile.Sections.Add("setion1");
setion1.Properties.Add("key1", "value1");
setion1.Properties.Add("key2", "value2");

iniFile.Save(@"C:\path\to\file.ini");
```
</details>

<details>
<summary>IniFileFormattingOptions</summary>

This class is a set of rules that is applied when writing an INI file.

```
IniFileFormattingOptions formattingOptions = new()
{
	PropertyDelimiter = IniPropertyDelimiter.EqualSign,
	UseNewLineBetweenSections = true
};

iniFile.Save(@"C:\path\to\file.ini", Encoding.UTF8, formattingOptions);
```
</details>

## Changelog

### 4.0.0 (15.09.2025)

* **change:** Targeting .NET 9.0

### 3.0.0 (08.09.2023)

* Initial release