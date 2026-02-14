# BytecodeApi.PEResources

Library for reading of native resources of executables & DLL files.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.PEResources

<details>
<summary>ResourceFileInfo</summary>

A `ResourceFileInfo` represents an executable or DLL file that contains resources.

```
ResourceFileInfo resourceFile = new(@"C:\Windows\explorer.exe");

// Retrieve resource as byte[]:
byte[] rcData1 = resourceFile.GetResource(ResourceType.RCData, 101);

// Retrieve icon groups:
int[] iconNames = resourceFile.GetGroupIconResourceNames();
Icon[] icons = iconNames.Select(name => resourceFile.GetGroupIconResource(name)).ToArray();
```

Modify the resources of the file:

```
// Change executable icon:
resourceFile.ChangeIcon(new Icon(@"C:\path\to\icon.ico"));

// Strip all resources:
resourceFile.DeleteResources();
```
</details>

## Changelog

### 5.0.0 (15.02.2026)

* **change:** Targeting .NET 10.0

### 4.0.0 (15.09.2025)

* **change:** Targeting .NET 9.0
* **change:** Bugfix: `ResourceFileInfo` methods did not accept name "0".

### 3.0.0 (08.09.2023)

* Initial release