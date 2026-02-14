namespace BytecodeApi.PEResources;

/// <summary>
/// Specifies the type of a PE file resource.
/// </summary>
public enum ResourceType
{
	/// <summary>
	/// The resource type is RT_CURSOR.
	/// </summary>
	Cursor = 1,
	/// <summary>
	/// The resource type is RT_BITMAP.
	/// </summary>
	Bitmap = 2,
	/// <summary>
	/// The resource type is RT_ICON.
	/// </summary>
	Icon = 3,
	/// <summary>
	/// The resource type is RT_MENU.
	/// </summary>
	Menu = 4,
	/// <summary>
	/// The resource type is RT_DIALOG.
	/// </summary>
	Dialog = 5,
	/// <summary>
	/// The resource type is RT_STRING.
	/// </summary>
	String = 6,
	/// <summary>
	/// The resource type is RT_FONTDIR.
	/// </summary>
	FontDir = 7,
	/// <summary>
	/// The resource type is RT_FONT.
	/// </summary>
	Font = 8,
	/// <summary>
	/// The resource type is RT_ACCELERATOR.
	/// </summary>
	Accelerator = 9,
	/// <summary>
	/// The resource type is RT_RCDATA.
	/// </summary>
	RCData = 10,
	/// <summary>
	/// The resource type is RT_MESSAGETABLE.
	/// </summary>
	MessageTable = 11,
	/// <summary>
	/// The resource type is RT_GROUP_CURSOR.
	/// </summary>
	GroupCursor = 12,
	/// <summary>
	/// The resource type is RT_GROUP_ICON.
	/// </summary>
	GroupIcon = 14,
	/// <summary>
	/// The resource type is RT_VERSION.
	/// </summary>
	Version = 16,
	/// <summary>
	/// The resource type is RT_DLGINCLUDE.
	/// </summary>
	DlgInclude = 17,
	/// <summary>
	/// The resource type is RT_PLUGPLAY.
	/// </summary>
	PlugPlay = 19,
	/// <summary>
	/// The resource type is RT_VXD.
	/// </summary>
	Vxd = 20,
	/// <summary>
	/// The resource type is RT_ANICURSOR.
	/// </summary>
	AniCursor = 21,
	/// <summary>
	/// The resource type is RT_ANIICON.
	/// </summary>
	AniIcon = 22,
	/// <summary>
	/// The resource type is RT_HTML.
	/// </summary>
	Html = 23,
	/// <summary>
	/// The resource type is RT_MANIFEST.
	/// </summary>
	Manifest = 24
}