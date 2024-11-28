# BytecodeApi.Wpf

WPF & MVVM library and converter engine.

## Examples

See: [Examples](https://github.com/bytecode77/bytecode-api/blob/master/BytecodeApi.Wpf/README.md)

## Changelog

### 3.0.5 (28.11.2024)

* **new:** `Dialog.Icon` method
* **new:** `TabItemConverter` class

### 3.0.4 (27.07.2024)

* **change:** `BooleanConverter` now implements the `TwoWayConverterBase`
* **new:** `EnumerableConverterMethod.Contains`
* **new:** `StringConverterMethod.ConcatIfNotEmpty` and `ConcatBeforeIfNotEmpty`
* **new:** `MathConverterMethod.Abs`
* **new:** `ThicknessConverter`
* **new:** `TextBlockService.TextWrapping` property
* **new:** `Dialog` class adds support for checkboxes
* **new:** `FileDialogs` method overloads with `initialDirectory` parameter
* **new:** `TextBoxExtensions.GetSelectionStart` and `GetSelectionLength` methods

### 3.0.3 (10.12.2023)

* **new:** `ObservableUserControl.Shown` and `ShownOnce` events
* **new:** `ObservableItemsControl.Shown` and `ShownOnce` events
* **change:** `BindingErrorTraceListener` trace level changed from `Information` to `Error`

### 3.0.2 (30.09.2023)

* **new:** `DateOnlyConverterMethod.Quarter`
* **new:** `DateTimeConverterMethod.Quarter`

### 3.0.1 (27.09.2023)

* **new:** `ApplicationExtensions.Dispatch` method

### 3.0.0 (08.09.2023)

* Initial release