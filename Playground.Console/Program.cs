using BytecodeApi;
using BytecodeApi.Extensions;
using BytecodeApi.IO;
using BytecodeApi.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
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
			Stopwatch stopwatch = ThreadFactory.StartStopwatch();



			stopwatch.Stop();
			WriteLine("Time elapsed: " + stopwatch.ElapsedMilliseconds);
			ReadKey();
		}
	}
}