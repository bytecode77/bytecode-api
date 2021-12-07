using BytecodeApi.Extensions;
using BytecodeApi.IO.SystemInfo;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Get list of TCP & UDP connections

		foreach (TcpViewEntry entry in new TcpView(true).GetEntries())
		{
			Console.Write(entry.Protocol.GetDescription() + " ");
			Console.Write(entry.LocalAddress + ":" + (entry.LocalProtocolName ?? entry.LocalPort.ToString()));
			Console.Write(" - ");
			Console.Write(entry.RemoteAddress + ":" + (entry.RemoteProtocolName ?? entry.RemotePort.ToString()));
			Console.Write(" " + entry.TcpState);
			Console.WriteLine(" PID: " + entry.ProcessId);
		}

		Console.ReadKey();
	}
}