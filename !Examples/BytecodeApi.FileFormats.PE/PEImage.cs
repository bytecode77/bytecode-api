using BytecodeApi.FileFormats.PE;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Read explorer.exe
		PEImage pe = PEImage.FromFile(@"C:\Windows\explorer.exe");

		// Boring stuff
		ImageDosHeader dosHeader = pe.DosHeader;
		byte[] dosStub = pe.DosStub;

		Console.WriteLine("explorer.exe");
		Console.WriteLine();

		Console.WriteLine(pe.CoffHeader.NumberOfSections + " Sections:");
		foreach (ImageSection section in pe.Sections)
		{
			Console.Write(" * " + section.Header.Name.PadRight(15));
			Console.Write(" (raw data = " + section.Header.SizeOfRawData + " bytes, ");
			Console.WriteLine("virtual size = " + section.Header.VirtualSize + " bytes)");
		}
		Console.WriteLine();

		if (pe.OptionalHeader is ImageOptionalHeader32)
		{
			Console.WriteLine("Executable is 32-bit");
		}
		else if (pe.OptionalHeader is ImageOptionalHeader64)
		{
			Console.WriteLine("Executable is 64-bit");
		}

		// If you are dissecting PE files, you are probably familiar with the rest of the properties...

		Console.ReadKey();
	}
}