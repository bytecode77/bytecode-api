using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Management;
using System.Security;

namespace BytecodeApi
{
	internal static class Throw
	{
		public static Exception Argument(string parameterName, string message)
		{
			return new ArgumentException(message, parameterName);
		}
		public static Exception InvalidOperation(string message)
		{
			return new InvalidOperationException(message);
		}
		public static Exception Format(string message)
		{
			return new FormatException(message);
		}
		public static Exception FileFormat(string message)
		{
			return new FileFormatException(message);
		}
		public static Exception Management(string message)
		{
			return new ManagementException(message);
		}
		public static Exception InvalidEnumArgument<TEnum>(string parameterName, TEnum enumValue) where TEnum : Enum
		{
			return new InvalidEnumArgumentException(parameterName, Convert.ToInt32(enumValue), typeof(TEnum)); //REFACTOR: Can (int) cast be used?
		}
		public static Exception UnsupportedType(string parameterName)
		{
			return new NotSupportedException(string.Format(CultureInfo.InvariantCulture, ExceptionMessages.UnsupportedType, parameterName));
		}
		public static Exception WrongPassword()
		{
			return new SecurityException(ExceptionMessages.Security.WrongPassword);
		}
		public static Exception Win32()
		{
			return new Win32Exception(ExceptionMessages.Win32);
		}
		public static Exception Win32(string message)
		{
			return new Win32Exception(message);
		}
		public static Exception Win32(int error)
		{
			return new Win32Exception(error);
		}
	}
}