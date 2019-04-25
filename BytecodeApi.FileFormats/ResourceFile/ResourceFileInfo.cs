using BytecodeApi.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace BytecodeApi.FileFormats.ResourceFile
{
	/// <summary>
	/// Provides properties and instance methods for native resources in portable executables, typically EXE and DLL files.
	/// </summary>
	public class ResourceFileInfo
	{
		/// <summary>
		/// Gets the path of the file.
		/// </summary>
		public string Path { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ResourceFileInfo" /> class using the specified filename.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a PE file.</param>
		public ResourceFileInfo(string path)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.ArgumentEx.StringNotEmpty(path, nameof(path));

			Path = path;
		}

		/// <summary>
		/// Extracts a resource from the file, specified by a name.
		/// </summary>
		/// <param name="type">A <see cref="ResourceType" /> specifying the type of the resource.</param>
		/// <param name="name">A <see cref="int" /> value specifying the name of the resource.</param>
		/// <returns>
		/// A new <see cref="ResourceEntry{TData}" /> of type <see cref="byte" />[] with the resource from the file.
		/// </returns>
		public ResourceEntry<byte[]> GetResource(ResourceType type, int name)
		{
			Check.FileNotFound(Path);
			Check.ArgumentOutOfRangeEx.Greater0(name, nameof(name));

			IntPtr module = IntPtr.Zero;

			try
			{
				module = Native.LoadLibraryEx(Path, IntPtr.Zero, 2);
				return module != IntPtr.Zero ? new ResourceEntry<byte[]>(type, name, GetData(module, type, name)) : throw Throw.Win32();
			}
			finally
			{
				if (module != IntPtr.Zero) Native.FreeLibrary(module);
			}
		}
		/// <summary>
		/// Extracts all group icon resources from the file.
		/// </summary>
		/// <returns>
		/// A new collection of <see cref="ResourceEntry{TData}" /> of type <see cref="Icon" /> with all group icon resources from the file.
		/// </returns>
		public ResourceEntry<Icon>[] GetGroupIconResources()
		{
			Check.FileNotFound(Path);

			List<ResourceEntry<Icon>> icons = new List<ResourceEntry<Icon>>();
			IntPtr module = IntPtr.Zero;

			try
			{
				module = Native.LoadLibraryEx(Path, IntPtr.Zero, 2);
				if (module == IntPtr.Zero) throw Throw.Win32();

				Native.EnumResourceNames(module, (IntPtr)ResourceType.GroupIcon, Callback, IntPtr.Zero);
			}
			finally
			{
				if (module != IntPtr.Zero) Native.FreeLibrary(module);
			}

			return icons.ToArray();

			bool Callback(IntPtr hModuleCallback, IntPtr type, IntPtr name, IntPtr lParam)
			{
				icons.Add(new ResourceEntry<Icon>(ResourceType.GroupIcon, (int)name, GetGroupIconData(module, (int)name)));
				return true;
			}
		}
		/// <summary>
		/// Extracts a group icon resource from the file, specified by a name.
		/// </summary>
		/// <param name="name">A <see cref="int" /> value specifying the name of the resource.</param>
		/// <returns>
		/// A new <see cref="ResourceEntry{TData}" /> of type <see cref="Icon" /> with the group icon resource from the file.
		/// </returns>
		public ResourceEntry<Icon> GetGroupIconResource(int name)
		{
			Check.FileNotFound(Path);
			Check.ArgumentOutOfRangeEx.Greater0(name, nameof(name));

			IntPtr module = IntPtr.Zero;

			try
			{
				module = Native.LoadLibraryEx(Path, IntPtr.Zero, 2);
				return module != IntPtr.Zero ? new ResourceEntry<Icon>(ResourceType.GroupIcon, name, GetGroupIconData(module, name)) : throw Throw.Win32();
			}
			finally
			{
				if (module != IntPtr.Zero) Native.FreeLibrary(module);
			}
		}
		/// <summary>
		/// Changes the icon of the file to the specified icon file that is a valid ICO file.
		/// </summary>
		/// <param name="iconPath">A <see cref="string" /> specifying the path of a valid ICO file.</param>
		public void ChangeIcon(string iconPath)
		{
			Check.FileNotFound(Path);
			Check.ArgumentNull(iconPath, nameof(iconPath));
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
			Check.ArgumentNull(icon, nameof(icon));

			Native.IconDir iconDir = new Native.IconDir();
			List<Native.IconDirEntry> iconEntry = new List<Native.IconDirEntry>();
			List<byte[]> iconData = new List<byte[]>();

			using (BinaryReader reader = new BinaryReader(new MemoryStream(icon.ToArray())))
			{
				Marshal.Copy(reader.ReadBytes(sizeof(Native.IconDir)), 0, (IntPtr)(&iconDir), sizeof(Native.IconDir));

				for (int i = 0; i < iconDir.Count; i++)
				{
					Native.IconDirEntry entry = new Native.IconDirEntry();
					Marshal.Copy(reader.ReadBytes(sizeof(Native.IconDirEntry)), 0, (IntPtr)(&entry), sizeof(Native.IconDirEntry));
					iconEntry.Add(entry);
				}
				for (int i = 0; i < iconDir.Count; i++)
				{
					iconData.Add(reader.ReadBytes((int)iconEntry[i].BytesInRes));
				}
			}

			IntPtr update = Native.BeginUpdateResource(Path, false);
			byte[] data = new byte[sizeof(Native.IconDir) + sizeof(Native.GroupIconDirEntry) * iconDir.Count];
			Marshal.Copy((IntPtr)(&iconDir), data, 0, sizeof(Native.IconDir));

			for (int i = 0, offset = sizeof(Native.IconDir); i < iconDir.Count; i++, offset += sizeof(Native.GroupIconDirEntry))
			{
				Native.BitmapInfoHeader header = new Native.BitmapInfoHeader();
				Marshal.Copy(iconData[i], 0, (IntPtr)(&header), sizeof(Native.BitmapInfoHeader));

				Native.GroupIconDirEntry groupEntry = new Native.GroupIconDirEntry
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

				Marshal.Copy((IntPtr)(&groupEntry), data, offset, Marshal.SizeOf(groupEntry));
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

			IntPtr handle = Native.BeginUpdateResource(Path, true);
			if (handle == IntPtr.Zero) throw Throw.Win32();

			Native.EndUpdateResource(handle, false);
		}

		private static Icon GetGroupIconData(IntPtr module, int name)
		{
			byte[] data = GetData(module, ResourceType.GroupIcon, name);

			int count = BitConverter.ToUInt16(data, 4);
			int size = 6 + 16 * count + Enumerable.Range(0, count).Sum(i => BitConverter.ToInt32(data, 14 + 14 * i));

			using (MemoryStream memoryStream = new MemoryStream(size))
			{
				using (BinaryWriter writer = new BinaryWriter(memoryStream))
				{
					writer.Write(data, 0, 6);

					for (int i = 0, offset = 6 + 16 * count; i < count; i++)
					{
						byte[] icon = GetData(module, ResourceType.Icon, BitConverter.ToUInt16(data, 18 + 14 * i));

						memoryStream.Seek(6 + 16 * i);
						writer.Write(data, 6 + 14 * i, 8);
						writer.Write(icon.Length);
						writer.Write(offset);

						memoryStream.Seek(offset);
						writer.Write(icon);
						offset += icon.Length;
					}
				}

				return ConvertEx.ToIcon(memoryStream.ToArray());
			}
		}
		private static byte[] GetData(IntPtr module, ResourceType type, int name)
		{
			IntPtr resource = Native.FindResource(module, (IntPtr)name, (IntPtr)type);
			if (resource == IntPtr.Zero) throw Throw.Win32();

			IntPtr data = Native.LoadResource(module, resource);
			if (data == IntPtr.Zero) throw Throw.Win32();

			IntPtr dataPtr = Native.LockResource(data);
			if (dataPtr == IntPtr.Zero) throw Throw.Win32();

			uint size = Native.SizeofResource(module, resource);
			if (size == 0) throw Throw.Win32();

			byte[] buffer = new byte[size];
			Marshal.Copy(dataPtr, buffer, 0, buffer.Length);
			return buffer;
		}
	}
}