# BytecodeApi

Framework for C# development.

## Examples

See: [Examples](https://github.com/bytecode77/bytecode-api/blob/master/BytecodeApi/README.md)

## Changelog

### 5.0.0 (15.02.2026)

* **change:** Targeting .NET 10.0
* **new:** `EnumerableExtensions.UnorderedEqual` method
* **new:** `RandomNumberGenerator.Shared` extension member
* **new:** `DateOnly.Today` extension member
* **new:** `TimeOnly.Now` and `UtcNow` extension member
* **change:** `BitArrayExtensions` methods changed to extension members: `CountTrue`, `CountFalse`, `AllTrue`, `AllFalse`
* **change:** `BitCalculator` methods changed to extension members of `BitOperations`
* **change:** `ConvertEx` methods changed to extension members of `Convert`
* **change:** `DateTimeEx` methods changed to extension members of `DateTime`
* **change:** `DateOnlyEx` methods changed to extension members of `DateOnly`
* **change:** `TimeOnlyEx` methods changed to extension members of `TimeOnly`
* **change:** `DirectoryEx` methods changed to extension members of `Directory`
* **change:** `FileEx` methods changed to extension members of `File`
* **change:** `PathEx` methods changed to extension members of `Path`
* **change:** `MathEx` methods changed to extension members of `Math`
* **change:** `EnumExtensions` methods changed to extension members of `Enum`
* **change:** `ExceptionExtensions` methods changed to extension members: `FullStackTrace`, `ChildExceptionCount`
* **change:** `ReflectionExtensions` methods changed to extension members: `NestedName`, `NestedFullName`
* **change:** `Create.HexadecimalString` method renamed to `HexString` to match .net naming conventions
* **removed:** `ConvertEx.ToHexadecimalString` and `FromHexadecimalString`

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