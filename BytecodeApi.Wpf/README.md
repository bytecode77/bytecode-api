# BytecodeApi.Wpf

WPF & MVVM library and converter engine.

## Examples

For brevity, only core components are described here.

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Wpf

<details>
<summary>MVVM (ObservableObject & DelegateCommand)</summary>

The `ObservableObject` class from the **BytecodeApi** package can be used as a base class for ViewModels.

A `DelegateCommand` can be created as a property and then bound to the view. It has a handler for `Execute` and `CanExecute`.

```
public class MainWindowViewModel : ObservableObject
{
	private DelegateCommand<string>? _TestCommand;
	public DelegateCommand<string> TestCommand => _TestCommand ??= new(TestCommand_Execute);

	private bool _TestProperty;
	public bool TestProperty
	{
		get => _TestProperty;
		set => Set(ref _TestProperty, value);
	}

	private void TestCommand_Execute(string? parameter)
	{
		// ...
	}
}
```
</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Wpf.Converters

<details>
<summary>ConverterBase</summary>

This class combines `IValueConverter` and `MarkupExtension`.

The `BytecodeApi.Wpf.Converters` namespace has lots of converters. For example, the `BooleanConverter` converts `bool` values to other types.

**Implementation:**

```
public class BooleanConverter : ConverterBase<bool?>
{
	public BooleanConverterMethod Method { get; set; }

	public BooleanConverter(BooleanConverterMethod method)
	{
		Method = method;
	}

	public override object? Convert(bool? value)
	{
		return Method switch
		{
			BooleanConverterMethod.Default => value,
			BooleanConverterMethod.Inverse => value != true,
			BooleanConverterMethod.Visibility => (value == true).ToVisibility(),
			// ...
		};
	}
}
```

**Usage:**

We use the `BooleanConverter`, because the bound value is a `bool`. Then we specify to what this value should be converted: `Visibility`.

```
<Button Visibility="{Binding ShowThis, Converter={ui:BooleanConverter Visibility}}">
```

Some converts have additional parameters in their constructors. Depending on the converter and its conversion method, a `ConverterParameter` may be used.

In the following example, `Price` is bound. If `Price > 0`, then `Visibility.Visible` should be returned, otherwise `Visibility.Collapsed`:

```
Visibility="{Binding Price, Converter={ui:EqualityConverter Greater, Visibility}, ConverterParameter={ui:Int32 0}}"
```

All converters in this namespace follow the same pattern. Additional converters can be implemented by inheriting the `ConverterBase` class.

</details>

<details>
<summary>More Converter examples</summary>

`If`, the XAML way:

```
Title="{Binding IsCreate, Converter={ui:IfConverter 'Create Entry', 'Edit Entry'}}"
```

Display the `[Description("...")]` attribute of an enum value:

```
{Binding SomeEnumValue, Converter={ui:EnumConverter Description}}
```

Display the first 3 digits of a `Version`:

```
{Binding Source={x:Static ApplicationVersion}, Converter={ui:VersionConverter 3}}
```

Convert a `DateTime` value using `Format` as the conversion method:

```
{Binding LastModified, Converter={ui:DateTimeConverter Format}, ConverterParameter='yyyy-MM-dd HH:mm:ss'}
```

... And many many more. Please review the documentation on each converter and the *ConverterMethod class.

</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Wpf.Markup

<details>
<summary>Markup Extensions</summary>

This namespace has markup extensions for all built in types:

```
"{ui:Int32 123}"
"{ui:DateTime '2023-01-01', 'yyyy-MM-dd'}"
"{ui:Thickness 10, 5, 10, 5}"
...
```

`event` to `ICommand` extension:

```
Closed="{ui:EventBinding WindowClosedCommand}"
```

</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Wpf.Interop

<details>
<summary>SingleInstance</summary>

The `SingleInstance` can detect an already running instance and notify it:

```
public partial class App : Application
{
	public static SingleInstance SingleInstance { get; private set; }

	public App()
	{
		SingleInstance = new SingleInstance("MY_APPLICATION_NAME_SINGLE_INSTANCE");
		if (SingleInstance.CheckInstanceRunning())
		{
			SingleInstance.SendActivationMessage();
			Shutdown();
		}
	}
}

public partial class MainWindow
{
	private void MainWindow_Loaded(object sender, RoutedEventArgs e)
	{
		App.SingleInstance.RegisterWindow(this);
		App.SingleInstance.Activated += delegate
		{
			Show();
			if (WindowState == WindowState.Minimized) WindowState = WindowState.Normal;
			Activate();
		};
	}
}
```
</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Wpf.Dialogs

<details>
<summary>Task Dialog</summary>

The `Dialog` class is a fluent wrapper around the Windows Task Dialog interface. It is internally using `System.Windows.Forms.TaskDialog`.

```
DialogResult result = Dialog
	.Title("My Task Dialog")
	.Text("I am a Task Dialog")
	.Icon(DialogIcon.ShieldBlueBar)
	.Expander("Additional info...")
	.Button(DialogResult.OK)
	.Button(DialogResult.Cancel, "Later")
	.Show(owner);

if (result == DialogResult.OK)
{
	// ...
}
```

The `DialogMessageBoxes` class offers some shorthand methods for common message boxes:

```
if (DialogMessageBoxes.OkCancel(owner, "title", "text", isWarning: false, "Text in expander"))
{
	// ...
}
```

</details>

<details>
<summary>FileDialogs</summary>

`FileDialogs` is a shorthand class to access various file dialogs.

Additionally, it retrieves information about a file extension from the operating system to display a string like

> *.txt|Text files

```
if (FileDialogs.Open("txt") is string path)
{
	// ...
}
```

```
if (FileDialogs.OpenFolder(@"C:\path\to\directory") is string directory)
{
	// ...
}
```

**See also:**

- `FileDialogs.OpenMultiple`
- `FileDialogs.Save`
- `FileDialogs.SelectIcon`

</details>

## Changelog

### 3.0.0 (08.09.2023)

* Initial release

### 3.0.1 (27.09.2023)

* **new:** `ApplicationExtensions.Dispatch` method

### 3.0.2 (30.09.2023)

* **new:** `DateOnlyConverterMethod.Quarter`
* **new:** `DateTimeConverterMethod.Quarter`

### 3.0.3 (10.12.2023)

* **new:** `ObservableUserControl.Shown` and `ShownOnce` events
* **new:** `ObservableItemsControl.Shown` and `ShownOnce` events
* **change:** `BindingErrorTraceListener` trace level changed from `Information` to `Error`