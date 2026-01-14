using System.ComponentModel;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Specifies the known web browsers.
/// </summary>
public enum KnownBrowser
{
	/// <summary>
	/// Specifies the Internet Explorer browser.
	/// </summary>
	[Description("Internet Explorer")]
	InternetExplorer,
	/// <summary>
	/// Specifies the Microsoft Edge browser.
	/// </summary>
	[Description("Microsoft Edge")]
	Edge,
	/// <summary>
	/// Specifies the Google Chrome browser.
	/// </summary>
	[Description("Google Chrome")]
	Chrome,
	/// <summary>
	/// Specifies the Mozilla Firefox browser.
	/// </summary>
	[Description("Mozilla Firefox")]
	Firefox,
	/// <summary>
	/// Specifies the Opera browser.
	/// </summary>
	[Description("Opera")]
	Opera,
	/// <summary>
	/// Specifies the Safari browser.
	/// </summary>
	[Description("Safari")]
	Safari
}