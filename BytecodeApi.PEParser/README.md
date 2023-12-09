# BytecodeApi.PEParser

Library for PE file parsing.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.PEParser

<details>
<summary>PEImage</summary>

`PEImage` represents a parsed executable containing information about the file:

```
PEImage exe = PEImage.FromFile(@"C:\Windows\explorer.exe");

// Optional header
uint entryPoint = exe.OptionalHeader.AddressOfEntryPoint;

// Sections
foreach (ImageSection section in exe.Sections)
{
	if (section.Header.Name == ".text")
	{
		byte[] code = section.Data;
	}
}
```
</details>

## Changelog

### 3.0.0 (08.09.2023)

* Initial release