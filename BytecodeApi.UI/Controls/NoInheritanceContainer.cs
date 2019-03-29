using System.Windows;

namespace BytecodeApi.UI.Controls
{
	/// <summary>
	/// Represents an <see cref="ObservableUserControl" /> that does not inherit style (<see cref="FrameworkElement.InheritanceBehavior" /> is set to <see cref="InheritanceBehavior.SkipToAppNext" />).
	/// </summary>
	public class NoInheritanceContainer : ObservableUserControl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NoInheritanceContainer" /> class.
		/// </summary>
		public NoInheritanceContainer()
		{
			InheritanceBehavior = InheritanceBehavior.SkipToAppNext;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="NoInheritanceContainer" /> class with the specified content.
		/// </summary>
		/// <param name="content">An <see cref="object" /> representing the content of this <see cref="NoInheritanceContainer" />.</param>
		public NoInheritanceContainer(object content) : this()
		{
			Content = content;
		}
	}
}