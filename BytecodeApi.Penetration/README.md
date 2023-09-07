# BytecodeApi.Penetration

Basic implementations of certain penetration testing routines, such as code injection.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Penetration

<details>
<summary>Shellcode</summary>

The `Shellcode` class handles compiled assembly that is typically position independent.

```
byte[] compiledInstructions = ...;
Shellcode.Execute(compiledInstructions);
```

To extract the code section from an executable file, use `ExtractFromExecutable`:

```
byte[] exeFile = File.ReadAllBytes(@"C:\Windows\explorer.exe");
byte[] textSection = Shellcode.ExtractFromExecutable(exeFile);
```
</details>

<details>
<summary>DllInjection</summary>

To inject a running process with a DLL, use `DllInjection.Inject`:

```
using Process process = Process.GetProcessesByName("explorer")[0];
DllInjection.Inject(process, @"C:\path\to\library.dll");
```
</details>

<details>
<summary>ExecutableInjection</summary>

To perform process hollowing, use the `RunPE` method. An optional parameter enables parent process spoofing.

```
byte[] exeFile = ...;
int spoofedParentProcessId = ...;
ExecutableInjection.RunPE(@"C:\Windows\System32\svchost.exe", null, exeFile, spoofedParentProcessId);
```

To load and invoke a .NET executable, use `ExecuteDotNetAssembly`:

```
byte[] dotNetExecutable = ...;
ExecutableInjection.ExecuteDotNetAssembly(dotNetExecutable, new[] { "arg1", "arg2" });
```
</details>