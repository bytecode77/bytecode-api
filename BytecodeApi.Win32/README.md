# BytecodeApi.Win32

Library for querying Windows specific operating system data.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Win32.AtomTable

<details>
<summary>Atom & GlobalAtom</summary>

`Atom` and `GlobalAtom` both have equivalent methods and properties, operating on either atoms or global atoms.

Find atom by name:

```
Atom atom = Atom.Find("MyAtom");
ushort value = atom.Value;
```

Add new atom:
```
Atom newAtom = Atom.Add("MyNewAtom");
```

Find atom by value:
```
Atom foundAtombyValue = new(123);
```
</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Win32.Interop

<details>
<summary>CommandPrompt</summary>

The `CommandPrompt` wraps up a **cmd.exe** process to write to and read from.

```
CommandPrompt cmd = new();

cmd.Start();
cmd.WriteLine("dir");

cmd.MessageReceived += Prompt_MessageReceived;

static void Prompt_MessageReceived(object? sender, CommandPromptEventArgs e)
{
	Console.WriteLine(e.Message);
}
```
</details>

<details>
<summary>GlobalKeyboardHook</summary>

The `GlobalKeyboardHook` class listens for keystrokes and raises an event, each time the user pressed a key:

```
GlobalKeyboardHook hook = new();
hook.KeyPressed += Hook_KeyPressed;

static void Hook_KeyPressed(object sender, KeyboardHookEventArgs e)
{
	// Retrieve properties about the key press event:
	Keys key = e.KeyCode;
	int scanCode = e.ScanCode;
	char keyChar = e.KeyChar;

	// Abort key press:
	e.Handled = true;
}
```
</details>

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Win32.SystemInfo

<details>
<summary>DeviceManager</summary>

The `DeviceManager` retrieves information about installed devices. The returned data structure closely resembles what you can see in the Windows Device Manager.

```
DeviceManager devices = DeviceManager.Create();

// Iterate all device types:
foreach (DeviceTypeInfo deviceType in devices.DeviceTypes)
{
	string? className = deviceType.ClassName;

	// Iterate all devices of that device type:
	foreach (DeviceInfo device in deviceType.Devices)
	{
		// Name, description and attributes of the device:
		string? deviceName = device.Name;
		string? deviceDescription = device.Description;
		ReadOnlyDictionary<string, object> deviceAttributes = device.Attributes;
	}
}
```
</details>

<details>
<summary>HardwareInfo</summary>

`HardwareInfo` retrieves information about system hardware:

```
HardwareInfo.ProcessorNames;
HardwareInfo.VideoControllerNames;
HardwareInfo.TotalMemory;
HardwareInfo.AvailableMemory;
```
</details>

<details>
<summary>HostsFile</summary>

An `HostsFile` instance provides a snapshot of the current hosts file:

```
HostsFile hosts = HostsFile.Load();

foreach(HostsFileEntry entry in hosts.Entries)
{
	string hostName = entry.HostName;
	string ipAddress = entry.IPAddress;
}
```
</details>

<details>
<summary>InstalledSoftware</summary>

An `InstalledSoftware` instance provides a snapshot of currently installed programs:

```
InstalledSoftware software = InstalledSoftware.Load();

foreach (InstalledSoftwareInfo program in software.Software)
{
	string? name = program.Name;
	string? publisher = program.Publisher;
	string? version = program.Version;
	// ...
}
```
</details>

<details>
<summary>OperatingSystemInfo</summary>

`OperatingSystemInfo` retrieves information about the operating system:

```
OperatingSystemInfo.Name;
OperatingSystemInfo.InstallDate;
OperatingSystemInfo.InstalledAntiVirusSoftware;
OperatingSystemInfo.DefaultBrowser;
OperatingSystemInfo.FrameworkVersions;
```
</details>

<details>
<summary>ProtocolMapping</summary>

A `ProtocolMapping` instance provides a snapshot of currently configured protocols.

This class is used by the `TcpView` class to resolve port names (e.g. 443 -> "https").

```
ProtocolMapping protocols = ProtocolMapping.Load();

foreach (ProtocolMappingEntry entry in protocols.Entries)
{
	TransportProtocol protocol = entry.Protocol;
	int port = entry.Port;
	string name = entry.Name;

	// e.g.: Udp, 443, "https"
}
```
</details>

<details>
<summary>TcpView</summary>

A `TcpView` instance provides a snapshot of the TCPView table. It contains information about all current TCP and UDP connections.

In addition, port names are resolved by using the `ProtocolMapping` class.

```
TcpView tcp = TcpView.Load();

foreach(TcpViewEntry entry in tcp.Entries)
{
	// ...
}
```
</details>

## Changelog

### 3.0.2 (28.11.2024)

* **new:** Several methods in `Desktop` class

### 3.0.1 (27.09.2023)

* **new:** `HardwareInfo.TotalMemory` and `AvailableMemory`
* **removed:** `HardwareInfo.Memory`

### 3.0.0 (08.09.2023)

* Initial release