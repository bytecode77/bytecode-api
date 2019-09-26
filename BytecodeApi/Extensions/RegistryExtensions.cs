using BytecodeApi.IO;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="RegistryKey" /> objects, specifically for easy data access of strongly typed values.
	/// </summary>
	public static class RegistryExtensions
	{
		/// <summary>
		/// Retrieves a <see cref="bool" /> value from this <see cref="RegistryKey" /> that is represented as a REG_DWORD value. Returns <see langword="null" />, if the value does not exist in the registry, is not a REG_DWORD value, or is not equal to 0 or 1.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <returns>
		/// <see langword="true" />, if the value is a REG_DWORD and is equal to 1;
		/// <see langword="false" />, if the value is a REG_DWORD and is equal to 0;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static bool? GetBooleanValue(this RegistryKey key, string name)
		{
			Check.ArgumentNull(key, nameof(key));

			return key.GetInt32Value(name) switch
			{
				0 => false,
				1 => true,
				_ => (bool?)null
			};
		}
		/// <summary>
		/// Retrieves a <see cref="bool" /> value from this <see cref="RegistryKey" /> that is represented as a REG_DWORD value. Returns a default value, if the value does not exist in the registry, is not a REG_DWORD value, or is not equal to 0 or 1.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <param name="defaultValue">The value that is used if retrieving or conversion failed.</param>
		/// <returns>
		/// <see langword="true" />, if the value is a REG_DWORD and is equal to 1;
		/// <see langword="false" />, if the value is a REG_DWORD and is equal to 0;
		/// otherwise, <paramref name="defaultValue" />.
		/// </returns>
		public static bool GetBooleanValue(this RegistryKey key, string name, bool defaultValue)
		{
			return key.GetBooleanValue(name) ?? defaultValue;
		}
		/// <summary>
		/// Retrieves a <see cref="int" /> value from this <see cref="RegistryKey" /> that is represented as a REG_DWORD value. Returns <see langword="null" />, if the value does not exist in the registry or is not a REG_DWORD value.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static int? GetInt32Value(this RegistryKey key, string name)
		{
			Check.ArgumentNull(key, nameof(key));

			return key.GetValue(name) as int?;
		}
		/// <summary>
		/// Retrieves a <see cref="int" /> value from this <see cref="RegistryKey" /> that is represented as a REG_DWORD value. Returns a default value, if the value does not exist in the registry or is not a REG_DWORD value.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <param name="defaultValue">The value that is used if retrieving or conversion failed.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <paramref name="defaultValue" />.
		/// </returns>
		public static int GetInt32Value(this RegistryKey key, string name, int defaultValue)
		{
			return key.GetInt32Value(name) ?? defaultValue;
		}
		/// <summary>
		/// Retrieves a <see cref="long" /> value from this <see cref="RegistryKey" /> that is represented as a REG_QWORD value. Returns <see langword="null" />, if the value does not exist in the registry or is not a REG_QWORD value.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static long? GetInt64Value(this RegistryKey key, string name)
		{
			Check.ArgumentNull(key, nameof(key));

			return key.GetValue(name) as long?;
		}
		/// <summary>
		/// Retrieves a <see cref="long" /> value from this <see cref="RegistryKey" /> that is represented as a REG_QWORD value. Returns a default value, if the value does not exist in the registry or is not a REG_QWORD value.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <param name="defaultValue">The value that is used if retrieving or conversion failed.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <paramref name="defaultValue" />.
		/// </returns>
		public static long GetInt64Value(this RegistryKey key, string name, long defaultValue)
		{
			return key.GetInt64Value(name) ?? defaultValue;
		}
		/// <summary>
		/// Retrieves a <see cref="string" /> value from this <see cref="RegistryKey" /> that is represented as a REG_SZ value. Returns <see langword="null" />, if the value does not exist in the registry or is not a REG_SZ value.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static string GetStringValue(this RegistryKey key, string name)
		{
			Check.ArgumentNull(key, nameof(key));

			return key.GetValue(name) as string;
		}
		/// <summary>
		/// Retrieves a <see cref="string" /> value from this <see cref="RegistryKey" /> that is represented as a REG_SZ value. Returns a default value, if the value does not exist in the registry or is not a REG_SZ value.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <param name="defaultValue">The value that is used if retrieving or conversion failed.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <paramref name="defaultValue" />.
		/// </returns>
		public static string GetStringValue(this RegistryKey key, string name, string defaultValue)
		{
			return key.GetStringValue(name) ?? defaultValue;
		}
		/// <summary>
		/// Retrieves a <see cref="DateTime" /> value from this <see cref="RegistryKey" /> that is represented as a REG_SZ value with the format "yyyy-MM-dd HH:mm:ss". Returns <see langword="null" />, if the value does not exist in the registry, is not a REG_SZ value, or does not match the format.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static DateTime? GetDateTimeValue(this RegistryKey key, string name)
		{
			Check.ArgumentNull(key, nameof(key));

			return key.GetStringValue(name).ToDateTime("yyyy-MM-dd HH:mm:ss");
		}
		/// <summary>
		/// Retrieves a <see cref="DateTime" /> value from this <see cref="RegistryKey" /> that is represented as a REG_SZ value with the format "yyyy-MM-dd HH:mm:ss". Returns a default value, if the value does not exist in the registry, is not a REG_SZ value, or does not match the format.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <param name="defaultValue">The value that is used if retrieving or conversion failed.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <paramref name="defaultValue" />.
		/// </returns>
		public static DateTime GetDateTimeValue(this RegistryKey key, string name, DateTime defaultValue)
		{
			return key.GetDateTimeValue(name) ?? defaultValue;
		}
		/// <summary>
		/// Retrieves an <see cref="Enum" /> value from this <see cref="RegistryKey" /> that is represented as a REG_DWORD value. Returns <see langword="null" />, if the value does not exist in the registry or is not a REG_DWORD value.
		/// </summary>
		/// <typeparam name="T">The type of the returned <see cref="Enum" /> value.</typeparam>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static T? GetEnumValue<T>(this RegistryKey key, string name) where T : struct, Enum
		{
			Check.ArgumentNull(key, nameof(key));

			return key.GetInt32Value(name) is int value ? (T?)Enum.ToObject(typeof(T), value) : null;
		}
		/// <summary>
		/// Retrieves an <see cref="Enum" /> value from this <see cref="RegistryKey" /> that is represented as a REG_DWORD value. Returns a default value, if the value does not exist in the registry or is not a REG_DWORD value.
		/// </summary>
		/// <typeparam name="T">The type of the returned <see cref="Enum" /> value.</typeparam>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <param name="defaultValue">The value that is used if retrieving or conversion failed.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <paramref name="defaultValue" />.
		/// </returns>
		public static T GetEnumValue<T>(this RegistryKey key, string name, T defaultValue) where T : struct, Enum
		{
			return key.GetEnumValue<T>(name) ?? defaultValue;
		}
		/// <summary>
		/// Retrieves a <see cref="byte" />[] value from this <see cref="RegistryKey" /> that is represented as a REG_BINARY value. Returns <see langword="null" />, if the value does not exist in the registry or is not a REG_BINARY value.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static byte[] GetByteArrayValue(this RegistryKey key, string name)
		{
			Check.ArgumentNull(key, nameof(key));

			return key.GetValue(name) as byte[];
		}
		/// <summary>
		/// Retrieves a <see cref="byte" />[] value from this <see cref="RegistryKey" /> that is represented as a REG_BINARY value. Returns a default value, if the value does not exist in the registry or is not a REG_BINARY value.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <param name="defaultValue">The value that is used if retrieving or conversion failed.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <paramref name="defaultValue" />.
		/// </returns>
		public static byte[] GetByteArrayValue(this RegistryKey key, string name, byte[] defaultValue)
		{
			return key.GetByteArrayValue(name) ?? defaultValue;
		}
		/// <summary>
		/// Retrieves a <see cref="string" />[] value from this <see cref="RegistryKey" /> that is represented as a REG_MULTI_SZ value. Returns <see langword="null" />, if the value does not exist in the registry or is not a REG_MULTI_SZ value.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <see langword="null" />.
		/// </returns>
		public static string[] GetStringArrayValue(this RegistryKey key, string name)
		{
			Check.ArgumentNull(key, nameof(key));

			return key.GetValue(name) as string[];
		}
		/// <summary>
		/// Retrieves a <see cref="string" />[] value from this <see cref="RegistryKey" /> that is represented as a REG_MULTI_SZ value. Returns a default value, if the value does not exist in the registry or is not a REG_MULTI_SZ value.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to read the value from.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to read.</param>
		/// <param name="defaultValue">The value that is used if retrieving or conversion failed.</param>
		/// <returns>
		/// The converted value, if it exists and conversion is possible;
		/// otherwise, <paramref name="defaultValue" />.
		/// </returns>
		public static string[] GetStringArrayValue(this RegistryKey key, string name, string[] defaultValue)
		{
			return key.GetStringArrayValue(name) ?? defaultValue;
		}

		/// <summary>
		/// Writes a <see cref="bool" /> value to this <see cref="RegistryKey" /> that is represented as a REG_DWORD value. If <see langword="null" /> is provided, the value will be deleted.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to write the value to.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to write to.</param>
		/// <param name="value">The <see cref="bool" /> value to be written to. If <see langword="null" /> is provided, the value will be deleted.</param>
		public static void SetBooleanValue(this RegistryKey key, string name, bool? value)
		{
			Check.ArgumentNull(key, nameof(key));

			key.SetInt32Value(name, value == null ? (int?)null : value.Value ? 1 : 0);
		}
		/// <summary>
		/// Writes a <see cref="int" /> value to this <see cref="RegistryKey" /> that is represented as a REG_DWORD value. If <see langword="null" /> is provided, the value will be deleted.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to write the value to.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to write to.</param>
		/// <param name="value">The <see cref="int" /> value to be written to. If <see langword="null" /> is provided, the value will be deleted.</param>
		public static void SetInt32Value(this RegistryKey key, string name, int? value)
		{
			Check.ArgumentNull(key, nameof(key));

			if (value == null) key.DeleteValue(name, false);
			else key.SetValue(name, value.Value, RegistryValueKind.DWord);
		}
		/// <summary>
		/// Writes a <see cref="long" /> value to this <see cref="RegistryKey" /> that is represented as a REG_QWORD value. If <see langword="null" /> is provided, the value will be deleted.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to write the value to.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to write to.</param>
		/// <param name="value">The <see cref="long" /> value to be written to. If <see langword="null" /> is provided, the value will be deleted.</param>
		public static void SetInt64Value(this RegistryKey key, string name, long? value)
		{
			Check.ArgumentNull(key, nameof(key));

			if (value == null) key.DeleteValue(name, false);
			else key.SetValue(name, value.Value, RegistryValueKind.QWord);
		}
		/// <summary>
		/// Writes a <see cref="string" /> value to this <see cref="RegistryKey" /> that is represented as a REG_SZ value. If <see langword="null" /> is provided, the value will be deleted.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to write the value to.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to write to.</param>
		/// <param name="value">The <see cref="string" /> value to be written to. If <see langword="null" /> is provided, the value will be deleted.</param>
		public static void SetStringValue(this RegistryKey key, string name, string value)
		{
			Check.ArgumentNull(key, nameof(key));

			if (value == null) key.DeleteValue(name, false);
			else key.SetValue(name, value, RegistryValueKind.String);
		}
		/// <summary>
		/// Writes a <see cref="DateTime" /> value to this <see cref="RegistryKey" /> that is represented as a REG_SZ value with the format "yyyy-MM-dd HH:mm:ss". If <see langword="null" /> is provided, the value will be deleted.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to write the value to.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to write to.</param>
		/// <param name="value">The <see cref="DateTime" /> value to be written in the format "yyyy-MM-dd HH:mm:ss". If <see langword="null" /> is provided, the value will be deleted.</param>
		public static void SetDateTimeValue(this RegistryKey key, string name, DateTime? value)
		{
			Check.ArgumentNull(key, nameof(key));

			key.SetStringValue(name, value?.ToStringInvariant("yyyy-MM-dd HH:mm:ss"));
		}
		/// <summary>
		/// Writes an <see cref="Enum" /> value to this <see cref="RegistryKey" /> that is represented as a REG_dword value. If <see langword="null" /> is provided, the value will be deleted.
		/// </summary>
		/// <typeparam name="T">The type of the <see cref="Enum" /> to write.</typeparam>
		/// <param name="key">The <see cref="RegistryKey" /> to write the value to.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to write to.</param>
		/// <param name="value">The <see cref="Enum" /> value to be written. If <see langword="null" /> is provided, the value will be deleted.</param>
		public static void SetEnumValue<T>(this RegistryKey key, string name, T? value) where T : struct, Enum
		{
			Check.ArgumentNull(key, nameof(key));

			if (value == null) key.DeleteValue(name, false);
			else key.SetInt32Value(name, Convert.ToInt32(value.Value));
		}
		/// <summary>
		/// Writes a <see cref="byte" />[] value to this <see cref="RegistryKey" /> that is represented as a REG_BINARY value. If <see langword="null" /> is provided, the value will be deleted.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to write the value to.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to write to.</param>
		/// <param name="value">The <see cref="byte" />[] value to be written to. If <see langword="null" /> is provided, the value will be deleted.</param>
		public static void SetByteArrayValue(this RegistryKey key, string name, byte[] value)
		{
			Check.ArgumentNull(key, nameof(key));

			if (value == null) key.DeleteValue(name, false);
			else key.SetValue(name, value, RegistryValueKind.Binary);
		}
		/// <summary>
		/// Writes a <see cref="string" />[] value to this <see cref="RegistryKey" /> that is represented as a REG_MULTI_SZ value. If <see langword="null" /> is provided, the value will be deleted.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> to write the value to.</param>
		/// <param name="name">A <see cref="string" /> value specifying the name of the value to write to.</param>
		/// <param name="value">The <see cref="string" />[] value to be written to. If <see langword="null" /> is provided, the value will be deleted.</param>
		public static void SetStringArrayValue(this RegistryKey key, string name, string[] value)
		{
			Check.ArgumentNull(key, nameof(key));

			if (value == null) key.DeleteValue(name, false);
			else key.SetValue(name, value, RegistryValueKind.MultiString);
		}

		/// <summary>
		/// Starts regedit.exe and navigates to the location specified by this <see cref="RegistryKey" />. If regedit.exe is already running, the <see cref="Process" /> will be terminated.
		/// </summary>
		/// <param name="key">The <see cref="RegistryKey" /> that will be navigates to.</param>
		public static void OpenInRegedit(this RegistryKey key)
		{
			Check.ArgumentNull(key, nameof(key));

			using (Process process = ProcessEx.GetSessionProcessesByName("regedit").FirstOrDefault())
			{
				process?.Kill();
			}

			using (RegistryKey currentVersionKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion", true))
			using (RegistryKey appletKey = currentVersionKey.CreateSubKey("Applets"))
			using (RegistryKey regeditAppletKey = appletKey.CreateSubKey("Regedit"))
			{
				regeditAppletKey.SetStringValue("LastKey", key.Name);
			}

			CSharp.Try(() => Process.Start("regedit"));
		}
	}
}