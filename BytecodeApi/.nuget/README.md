# BytecodeApi

Framework for C# development.

## Examples

See: [Examples](https://github.com/bytecode77/bytecode-api/blob/master/BytecodeApi/README.md)

## Changelog

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