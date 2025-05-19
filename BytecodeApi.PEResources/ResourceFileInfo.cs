using BytecodeApi.Extensions;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace BytecodeApi.PEResources;

/// <summary>
/// Provides properties and instance methods for native resources in portable executables, typically EXE and DLL files.
/// </summary>
public class ResourceFileInfo
{
	/// <summary>
	/// Gets the path of the file.
	/// </summary>
	public string Path { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ResourceFileInfo" /> class using the specified filename.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path of a PE file.</param>
	public ResourceFileInfo(string path)
	{
		Check.ArgumentNull(path);
		Check.ArgumentEx.StringNotEmpty(path);

		Path = path;
	}

	/// <summary>
	/// Extracts a resource from the file, specified by a name.
	/// </summary>
	/// <param name="type">A <see cref="ResourceType" /> specifying the type of the resource.</param>
	/// <param name="name">A <see cref="int" /> value specifying the name of the resource.</param>
	/// <returns>
	/// The extracted <see cref="byte" />[] resource.
	/// </returns>
	public byte[] GetResource(ResourceType type, int name)
	{
		Check.FileNotFound(Path);
		Check.ArgumentOutOfRangeEx.Greater0(name);

		nint module = 0;

		try
		{
			module = Native.LoadLibraryEx(Path, 0, 2);
			if (module == 0) throw Throw.Win32();

			return GetData(module, type, name);
		}
		finally
		{
			if (module != 0) Native.FreeLibrary(module);
		}
	}
	/// <summary>
	/// Extracts all group icon resources from the file and returns their names.
	/// </summary>
	/// <returns>
	/// A new <see cref="int" />[] with all group icon resource names.
	/// </returns>
	public int[] GetGroupIconResourceNames()
	{
		Check.FileNotFound(Path);

		List<int> icons = [];
		nint module = 0;

		try
		{
			module = Native.LoadLibraryEx(Path, 0, 2);
			if (module == 0) throw Throw.Win32();

			Native.EnumResourceNames(module, (nint)ResourceType.GroupIcon, Callback, 0);
		}
		finally
		{
			if (module != 0) Native.FreeLibrary(module);
		}

		return icons.ToArray();

		bool Callback(nint hModuleCallback, nint type, nint name, nint lParam)
		{
			icons.Add((int)name);
			return true;
		}
	}
	/// <summary>
	/// Extracts a group icon resource from the file, specified by a name.
	/// </summary>
	/// <param name="name">A <see cref="int" /> value specifying the name of the resource.</param>
	/// <returns>
	/// The extracted <see cref="Icon" /> resource.
	/// </returns>
	public Icon GetGroupIconResource(int name)
	{
		Check.FileNotFound(Path);
		Check.ArgumentOutOfRangeEx.Greater0(name);

		nint module = 0;

		try
		{
			module = Native.LoadLibraryEx(Path, 0, 2);
			if (module == 0) throw Throw.Win32();

			return GetGroupIconData(module, name);
		}
		finally
		{
			if (module != 0) Native.FreeLibrary(module);
		}
	}
	/// <summary>
	/// Changes the icon of the file to the specified icon file that is a valid ICO file.
	/// </summary>
	/// <param name="iconPath">A <see cref="string" /> specifying the path of a valid ICO file.</param>
	public void ChangeIcon(string iconPath)
	{
		Check.FileNotFound(Path);
		Check.ArgumentNull(iconPath);
		Check.FileNotFound(iconPath);

		ChangeIcon(new Icon(iconPath));
	}
	/// <summary>
	/// Changes the icon of the file to the specified icon.
	/// </summary>
	/// <param name="icon">The <see cref="Icon" /> to be applied to the file.</param>
	public unsafe void ChangeIcon(Icon icon)
	{
		Check.FileNotFound(Path);
		Check.ArgumentNull(icon);

		Native.IconDir iconDir = new();
		List<Native.IconDirEntry> iconEntry = [];
		List<byte[]> iconData = [];

		using (BinaryReader reader = new(new MemoryStream(icon.ToArray())))
		{
			Marshal.Copy(reader.ReadBytes(sizeof(Native.IconDir)), 0, (nint)(&iconDir), sizeof(Native.IconDir));

			for (int i = 0; i < iconDir.Count; i++)
			{
				Native.IconDirEntry entry = new();
				Marshal.Copy(reader.ReadBytes(sizeof(Native.IconDirEntry)), 0, (nint)(&entry), sizeof(Native.IconDirEntry));
				iconEntry.Add(entry);
			}

			for (int i = 0; i < iconDir.Count; i++)
			{
				iconData.Add(reader.ReadBytes((int)iconEntry[i].BytesInRes));
			}
		}

		nint update = Native.BeginUpdateResource(Path, false);
		byte[] data = new byte[sizeof(Native.IconDir) + sizeof(Native.GroupIconDirEntry) * iconDir.Count];
		Marshal.Copy((nint)(&iconDir), data, 0, sizeof(Native.IconDir));

		for (int i = 0, offset = sizeof(Native.IconDir); i < iconDir.Count; i++, offset += sizeof(Native.GroupIconDirEntry))
		{
			Native.BitmapInfoHeader header = new();
			Marshal.Copy(iconData[i], 0, (nint)(&header), sizeof(Native.BitmapInfoHeader));

			Native.GroupIconDirEntry groupEntry = new()
			{
				Width = iconEntry[i].Width,
				Height = iconEntry[i].Height,
				ColorCount = iconEntry[i].ColorCount,
				Reserved = iconEntry[i].Reserved,
				Planes = header.Planes,
				BitCount = header.BitCount,
				BytesInRes = iconEntry[i].BytesInRes,
				Id = (ushort)(i + 10)
			};

			Marshal.Copy((nint)(&groupEntry), data, offset, Marshal.SizeOf(groupEntry));
		}

		Native.UpdateResource(update, (uint)ResourceType.GroupIcon, 1, 0, data, (uint)data.Length);

		for (int i = 0; i < iconDir.Count; i++)
		{
			Native.UpdateResource(update, (uint)ResourceType.Icon, (uint)(10 + i), 0, iconData[i], (uint)iconData[i].Length);
		}

		Native.EndUpdateResource(update, false);
	}
	/// <summary>
	/// Strips all resources from the file.
	/// </summary>
	public void DeleteResources()
	{
		Check.FileNotFound(Path);

		nint handle = Native.BeginUpdateResource(Path, true);
		if (handle == 0) throw Throw.Win32();

		Native.EndUpdateResource(handle, false);
	}

	private static Icon GetGroupIconData(nint module, int name)
	{
		byte[] data = GetData(module, ResourceType.GroupIcon, name);

		int count = BitConverter.ToUInt16(data, 4);
		int size = 6 + 16 * count + Enumerable.Range(0, count).Sum(i => BitConverter.ToInt32(data, 14 + 14 * i));

		using MemoryStream memoryStream = new(size);
		using (BinaryWriter writer = new(memoryStream))
		{
			writer.Write(data, 0, 6);

			for (int i = 0, offset = 6 + 16 * count; i < count; i++)
			{
				byte[] icon = GetData(module, ResourceType.Icon, BitConverter.ToUInt16(data, 18 + 14 * i));

				memoryStream.Seek(6 + 16 * i, SeekOrigin.Begin);
				writer.Write(data, 6 + 14 * i, 8);
				writer.Write(icon.Length);
				writer.Write(offset);

				memoryStream.Seek(offset, SeekOrigin.Begin);
				writer.Write(icon);
				offset += icon.Length;
			}
		}

		return ConvertEx.ToIcon(memoryStream.ToArray());
	}
	private static byte[] GetData(nint module, ResourceType type, int name)
	{
		nint resource = Native.FindResource(module, name, (nint)type);
		if (resource == 0) throw Throw.Win32();

		nint data = Native.LoadResource(module, resource);
		if (data == 0) throw Throw.Win32();

		nint dataPtr = Native.LockResource(data);
		if (dataPtr == 0) throw Throw.Win32();

		uint size = Native.SizeofResource(module, resource);
		if (size == 0) throw Throw.Win32();

		byte[] buffer = new byte[size];
		Marshal.Copy(dataPtr, buffer, 0, buffer.Length);
		return buffer;
	}
}

file static class Native
{
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	[SuppressUnmanagedCodeSecurity]
	public static extern nint LoadLibraryEx(string fileName, nint file, uint flags);
	[DllImport("kernel32.dll", SetLastError = true)]
	[SuppressUnmanagedCodeSecurity]
	public static extern bool FreeLibrary(nint module);
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	[SuppressUnmanagedCodeSecurity]
	public static extern bool EnumResourceNames(nint module, nint type, EnumerateResourceNames enumFunc, nint param);
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	[SuppressUnmanagedCodeSecurity]
	public static extern nint FindResource(nint module, nint name, nint type);
	[DllImport("kernel32.dll", SetLastError = true)]
	[SuppressUnmanagedCodeSecurity]
	public static extern nint LoadResource(nint module, nint resInfo);
	[DllImport("kernel32.dll", SetLastError = true)]
	[SuppressUnmanagedCodeSecurity]
	public static extern nint LockResource(nint resData);
	[DllImport("kernel32.dll", SetLastError = true)]
	[SuppressUnmanagedCodeSecurity]
	public static extern uint SizeofResource(nint module, nint resInfo);
	[DllImport("kernel32.dll")]
	public static extern nint BeginUpdateResource(string fileName, [MarshalAs(UnmanagedType.Bool)] bool deleteExistingResources);
	[DllImport("kernel32.dll")]
	public static extern bool EndUpdateResource(nint update, bool discard);
	[DllImport("kernel32.dll")]
	public static extern int UpdateResource(nint update, uint type, uint name, ushort language, byte[] data, uint dateLength);

	[StructLayout(LayoutKind.Sequential)]
	public struct IconDir
	{
		public ushort Reserved;
		public ushort Type;
		public ushort Count;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct IconDirEntry
	{
		public byte Width;
		public byte Height;
		public byte ColorCount;
		public byte Reserved;
		public ushort Planes;
		public ushort BitCount;
		public uint BytesInRes;
		public uint ImageOffset;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct BitmapInfoHeader
	{
		public uint Size;
		public int Width;
		public int Height;
		public ushort Planes;
		public ushort BitCount;
		public uint Compression;
		public uint SizeImage;
		public int PixelsPerMeterX;
		public int PixelsPerMeterY;
		public uint ClrUsed;
		public uint ClrImportant;
	}
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public struct GroupIconDirEntry
	{
		public byte Width;
		public byte Height;
		public byte ColorCount;
		public byte Reserved;
		public ushort Planes;
		public ushort BitCount;
		public uint BytesInRes;
		public ushort Id;
	}
	[UnmanagedFunctionPointer(CallingConvention.Winapi, SetLastError = true, CharSet = CharSet.Unicode)]
	[SuppressUnmanagedCodeSecurity]
	public delegate bool EnumerateResourceNames(nint module, nint type, nint name, nint lParam);
}