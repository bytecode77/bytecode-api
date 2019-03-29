using System.ComponentModel;

namespace BytecodeApi
{
	/// <summary>
	/// Specifies the known web browsers.
	/// </summary>
	public enum KnownBrowser
	{
		/// <summary>
		/// The browser is Internet Explorer.
		/// </summary>
		[Description("Internet Explorer")]
		InternetExplorer,
		/// <summary>
		/// The browser is Microsoft Edge.
		/// </summary>
		[Description("Microsoft Edge")]
		Edge,
		/// <summary>
		/// The browser is Google Chrome.
		/// </summary>
		[Description("Google Chrome")]
		Chrome,
		/// <summary>
		/// The browser is Mozilla Firefox.
		/// </summary>
		[Description("Mozilla Firefox")]
		Firefox,
		/// <summary>
		/// The browser is Opera.
		/// </summary>
		[Description("Opera")]
		Opera,
		/// <summary>
		/// The browser is Safari.
		/// </summary>
		[Description("Safari")]
		Safari
	}
}