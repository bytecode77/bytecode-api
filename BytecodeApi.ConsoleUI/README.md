# BytecodeApi.ConsoleUI

Console input & output library.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.ConsoleUI

<details>
<summary>ConsoleWriter</summary>

The `ConsoleWriter` is a class that writes formatted text to the Console.

```
ConsoleWriter.WriteLine("Welcome to my interactive command line tool!");

ConsoleWriter.Success("Config loaded");
ConsoleWriter.Warning("Minimum screen resolution of 320x240 not met");
ConsoleWriter.Error("Windows NT 4.0 is not supported");
```

**Output:**

> Welcome to my interactive command line tool!  
20:19:11 [+] Config loaded  
20:19:11 [!] Minimum screen resolution of 320x240 not met  
20:19:11 [-] Windows NT 4.0 is not supported

</details>

<details>
<summary>ConsoleWriter.Preview</summary>

The `Preview` method displays a message that is erased from the console, after a successive write.

This helps to declutter console output, by erasing progress messages and only leaving success/failure messages on the screen.

```
ConsoleWriter.Preview("Loading file");
// Load file here
ConsoleWriter.Success("File loaded");
```

**Output:**

> File loaded

*(The initial message "Loading file" was erased)*

To display a progress bar and erase it when done, use the `Preview` method with additional progress parameters:

```
for (int i = 0; i < 100; i++)
{
	ConsoleWriter.Preview("Loading file", i, $"Progress: {i} %");
	Thread.Sleep(10);
}

ConsoleWriter.Success("File loaded");
```

**Output:**

>20:22:19 ... Loading file  
64 % [████████████████████░░░░░░░░]

</details>

<details>
<summary>ConsoleInput.Text</summary>

Write a question and input a `string`:

```
string name = ConsoleInput.Text("What's your name?");
```

</details>

<details>
<summary>ConsoleInput.Confirmation</summary>

Write a question and request a `y` or `n` from the user:

```
bool confirmed = ConsoleInput.Confirmation("Do you want to delete the file?");
```

**Output:**

> Do you want to delete the file? [y/n]

If the `defaultChoice` parameter is true, `[Y/n]` is displayed *(upper case `Y`)*. Pressing return confirms the question positively.

Likewise, false shows a `[y/N]` and pressing return confirms the question negatively.

</details>

<details>
<summary>ConsoleInput.Select</summary>

To prompt for a selection, use the `Select` method.

```
int selection = ConsoleInput.Select(
	"Place your order, sir",
	"Pork",
	"Beef",
	"Veggie");

// selection == 1    -> Pork
// selection == 2    -> Beef
// selection == 3    -> Veggie
```

**Output:**

> Place your order, sir  
&nbsp;&nbsp;[1] Pork  
&nbsp;&nbsp;[2] Beef  
&nbsp;&nbsp;[3] Veggie  
Select: _

</details>

<details>
<summary>ConsoleWriterTheme & ConsoleInputTheme</summary>

Change the `ConsoleWriter.Theme` property to configure Console output:

```
ConsoleWriter.Theme.ErrorStyle = new ConsoleStyle(ConsoleColor.Red, ConsoleColor.Black);
ConsoleWriter.Theme.SuccessIcon = "[+]";
ConsoleWriter.Theme.ErrorIcon = "[-]";
```

Change the `ConsoleInput.Theme` property to configure Console input:

```
ConsoleInput.Theme.ConfirmationAnswerNo = "n";
ConsoleInput.Theme.ConfirmationAnswerYes = "y";
ConsoleInput.Theme.SelectOptionsPromptText = "Select:";
```

</details>