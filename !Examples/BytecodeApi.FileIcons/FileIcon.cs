using BytecodeApi.FileIcons;
using System;
using System.Drawing;
using System.Windows.Media.Imaging;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Get the file icon for the .csproj extension
		FileIcon csprojIcon = KnownFileIcons.Csproj;

		// Get as bitmap
		// Available sizes are: 16, 32, 48
		Bitmap csproj16 = csprojIcon.Icon16;
		// or use the shortcut
		csproj16 = KnownFileIcons.Csproj.Icon16;

		// Get the bitmap source to use in WPF projects
		BitmapSource csproj16Source = csprojIcon.Icon16ImageSource;
		// or use the shortcut (can be used with x:Static markup extension and directly bound in a WPF <Image />
		csproj16Source = KnownFileIconImages.Csproj16;

		// Get the icon from an extension string
		FileIcon exeIcon = FileIcon.FromExtension("exe");

		// Special file icons
		FileIcon directory = SpecialFileIcons.Directory;
		FileIcon unknwonFileType = SpecialFileIcons.Unknown;
	}
}