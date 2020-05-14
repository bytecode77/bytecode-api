using BytecodeApi;
using BytecodeApi.Comparers;
using BytecodeApi.Cryptography;
using BytecodeApi.Data;
using BytecodeApi.Extensions;
using BytecodeApi.FileFormats;
using BytecodeApi.FileFormats.Coff;
using BytecodeApi.FileFormats.Csv;
using BytecodeApi.FileFormats.Ini;
using BytecodeApi.FileFormats.ResourceFile;
using BytecodeApi.FileIcons;
using BytecodeApi.GeoIP;
using BytecodeApi.GeoIP.ASN;
using BytecodeApi.GeoIP.City;
using BytecodeApi.IO;
using BytecodeApi.IO.Cli;
using BytecodeApi.IO.Debugging;
using BytecodeApi.IO.FileSystem;
using BytecodeApi.IO.Http;
using BytecodeApi.IO.Interop;
using BytecodeApi.IO.SystemInfo;
using BytecodeApi.IO.Wmi;
using BytecodeApi.Mathematics;
using BytecodeApi.Text;
using BytecodeApi.Threading;
using BytecodeApi.UI;
using BytecodeApi.UI.Controls;
using BytecodeApi.UI.Converters;
using BytecodeApi.UI.Data;
using BytecodeApi.UI.Dialogs;
using BytecodeApi.UI.Effects;
using BytecodeApi.UI.Extensions;
using BytecodeApi.UI.Markup;
using BytecodeApi.UI.Mathematics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using static System.Console;

namespace Playground.Console
{
	/// <summary>
	/// Playground project for development and case testing of class libraries.
	/// </summary>
	public static class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			Action test = Test1;

			WriteLine("Time elapsed: " + (int)CSharp.MeasureTime(test).TotalMilliseconds);
			ReadKey();
		}

		public static void Test1()
		{
		}
		public static void Test2()
		{
		}
		public static void Test3()
		{
		}
	}
}