namespace BytecodeApi.Wmi;

/// <summary>
/// Class to query the Windows Management Instrumentation.
/// </summary>
public static class WmiContext
{
	/// <summary>
	/// Gets the root WMI namespace.
	/// </summary>
	public static WmiNamespace Root => new("");
}