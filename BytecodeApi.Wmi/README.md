# BytecodeApi.Wmi

Library for efficient WMI querying.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Wmi

Retrieve list of WMI objects by reading entire table:

```
WmiObject[] processes = WmiContext.Root
	.GetNamespace("CIMV2")
	.GetClass("Win32_Process")
	.ToArray();

foreach (WmiObject process in processes)
{
	// Retrieve properties for each WmiObject
	Console.WriteLine(process.Properties["Name"].Value);
	Console.WriteLine(process.Properties["CommandLine"].Value);
}
```

Retrieve one WMI object:

```
WmiObject? process = WmiContext.Root
	.GetNamespace("CIMV2")
	.GetClass("Win32_Process")
	.Where("ProcessId = 4")
	.FirstOrDefault();

string? caption = process?.Properties["Caption"].GetValue<string>();
```

Retrieve all classes from a namespace:

```
WmiClass[] classes = WmiContext.Root
	.GetNamespace("CIMV2")
	.GetClasses();
```

Retrieve all namespaces from a namespace:

```
WmiNamespace[] namespaces = WmiContext.Root
	.GetNamespaces();
```

## Changelog

### 5.0.0 (15.02.2026)

* **change:** Targeting .NET 10.0

### 4.0.0 (15.09.2025)

* **change:** Targeting .NET 9.0

### 3.0.0 (08.09.2023)

* Initial release