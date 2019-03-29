using Microsoft.Win32;

namespace BytecodeApi.IO
{
	/// <summary>
	/// Container class for <see cref="CurrentUser" /> and <see cref="LocalMachine" /> classes.
	/// </summary>
	public static class KnownRegistryKeys
	{
		/// <summary>
		/// Represents a <see langword="static" /> set of <see cref="RegistryKey" /> objects to common locations in the Windows registry in the HKEY_CURRENT_USER hive.
		/// </summary>
		public static class CurrentUser
		{
			/// <summary>
			/// Creates a new <see cref="RegistryKey" /> object pointing to HKEY_CURRENT_USER\Software.
			/// </summary>
			/// <param name="writable"><see langword="true" /> to create this <see cref="RegistryKey" /> object with write permissions.</param>
			/// <returns>
			/// A new <see cref="RegistryKey" /> object pointing to HKEY_CURRENT_USER\Software.
			/// </returns>
			public static RegistryKey Software(bool writable)
			{
				return Registry.CurrentUser.OpenSubKey("Software", writable);
			}
			/// <summary>
			/// Creates a new <see cref="RegistryKey" /> object pointing to HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion.
			/// </summary>
			/// <param name="writable"><see langword="true" /> to create this <see cref="RegistryKey" /> object with write permissions.</param>
			/// <returns>
			/// A new <see cref="RegistryKey" /> object pointing to HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion.
			/// </returns>
			public static RegistryKey WindowsCurrentVersion(bool writable)
			{
				using (RegistryKey key = Software(writable))
				{
					return key.OpenSubKey(@"Microsoft\Windows\CurrentVersion", writable);
				}
			}
			/// <summary>
			/// Creates a new <see cref="RegistryKey" /> object pointing to HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run.
			/// </summary>
			/// <param name="writable"><see langword="true" /> to create this <see cref="RegistryKey" /> object with write permissions.</param>
			/// <returns>
			/// A new <see cref="RegistryKey" /> object pointing to HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run.
			/// </returns>
			public static RegistryKey Run(bool writable)
			{
				using (RegistryKey key = WindowsCurrentVersion(writable))
				{
					return key.OpenSubKey("Run", writable);
				}
			}
		}
		/// <summary>
		/// Represents a <see langword="static" /> set of <see cref="RegistryKey" /> objects to common locations in the Windows registry in the HKEY_LOCAL_MACHINE hive.
		/// </summary>
		public static class LocalMachine
		{
			/// <summary>
			/// Creates a new <see cref="RegistryKey" /> object pointing to HKEY_LOCAL_MACHINE\Software.
			/// </summary>
			/// <param name="writable"><see langword="true" /> to create this <see cref="RegistryKey" /> object with write permissions.</param>
			/// <returns>
			/// A new <see cref="RegistryKey" /> object pointing to HKEY_LOCAL_MACHINE\Software.
			/// </returns>
			public static RegistryKey Software(bool writable)
			{
				return Registry.LocalMachine.OpenSubKey("SOFTWARE", writable);
			}
			/// <summary>
			/// Creates a new <see cref="RegistryKey" /> object pointing to HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion.
			/// </summary>
			/// <param name="writable"><see langword="true" /> to create this <see cref="RegistryKey" /> object with write permissions.</param>
			/// <returns>
			/// A new <see cref="RegistryKey" /> object pointing to HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion.
			/// </returns>
			public static RegistryKey WindowsCurrentVersion(bool writable)
			{
				using (RegistryKey key = Software(writable))
				{
					return key.OpenSubKey(@"Microsoft\Windows\CurrentVersion", writable);
				}
			}
			/// <summary>
			/// Creates a new <see cref="RegistryKey" /> object pointing to HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run.
			/// </summary>
			/// <param name="writable"><see langword="true" /> to create this <see cref="RegistryKey" /> object with write permissions.</param>
			/// <returns>
			/// A new <see cref="RegistryKey" /> object pointing to HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run.
			/// </returns>
			public static RegistryKey Run(bool writable)
			{
				using (RegistryKey key = WindowsCurrentVersion(writable))
				{
					return key.OpenSubKey("Run", writable);
				}
			}
		}
	}
}