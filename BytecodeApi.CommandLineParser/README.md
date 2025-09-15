# BytecodeApi.CommandLineParser

Library for commandline parsing and handling.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.CommandLineParser

<details>
<summary>OptionSet</summary>

An `OptionSet` is the definition of all possible commandline options.

```
new OptionSet("-", "--")
	.Add(new[] { "c" }, new[] { "command" })
	.Add(new[] { "h" }, new[] { "help" })
```

`-` is the prefix and `--` is the alternative prefix. In this example, following options are configured:

- `-c`
- `--command`
- `-h`
- `--help`

Each option can have multiple parameters. Example:

```
-f file1.txt file2.txt -o output.txt
```

</details>

<details>
<summary>ParsedOptionSet</summary>

After the `OptionSet.Parse` method comes the handling of user provided commandline arguments.

The handler executes the `Action<string[]>` with the arguments after the commandline, i.e.

> CommandLine: **-c arg1 arg2**  
Output: **-c executed with arg1 arg2**

```
new OptionSet("-", "--")
	.Add(new[] { "c" }, new[] { "command" })
	.Add(new[] { "h" }, new[] { "help" })
	.Parse(args) // From Main() method
	.Handle("c", arguments =>
	{
		Console.WriteLine("-c executed with " + string.Join(" ", arguments));
	});
```

</details>

<details>
<summary>ParsedOptionSet.Validate</summary>

`Validate` provides a callback that is invoked, if a specific validation rule is not met.

If none of the existing validation rules satisfy your requirements, use `Validate.Custom` to implement a custom validation rule.

```
new OptionSet("-", "--")
	.Add(new[] { "c" }, new[] { "command" })
	.Add(new[] { "h" }, new[] { "help" })
	.Parse(args)
	.Validate.OptionRequired("c", () =>
	{
		Console.WriteLine("-c is mandatory");
	})
	.Handle(
		// ...
	);
```

</details>

<details>
<summary>ParsedOptionSet.Assert</summary>

`Assert` works similar to `Validate`, however instead of callbacks, a `CommandLineParserException` is thrown.

Use assertion, if an exception is sufficient and manual error handling is not required.

```
new OptionSet("-", "--")
	.Add(new[] { "c" }, new[] { "command" })
	.Add(new[] { "h" }, new[] { "help" })
	.Parse(args)
	.Assert.OptionRequired("c")
	.Handle(
		// ...
	);
```

</details>

## Changelog

### 4.0.0 (15.09.2025)

* **change:** Targeting .NET 9.0

### 3.0.0 (08.09.2023)

* Initial release