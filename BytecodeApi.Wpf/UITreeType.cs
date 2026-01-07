using System.Windows;
using System.Windows.Media;

namespace BytecodeApi.Wpf;

/// <summary>
/// Specifies the type of the WPF tree.
/// </summary>
public enum UITreeType
{
	/// <summary>
	/// Specifies the WPF logical tree. For traversing operations, <see cref="LogicalTreeHelper" /> is used.
	/// </summary>
	Logical,
	/// <summary>
	/// Specifies the WPF visual tree. For traversing operations, <see cref="VisualTreeHelper" /> is used.
	/// </summary>
	Visual
}