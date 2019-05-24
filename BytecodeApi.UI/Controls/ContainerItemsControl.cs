using System.Windows.Controls;

namespace BytecodeApi.UI.Controls
{
	/// <summary>
	/// Represents an <see cref="ItemsControl" /> that applies the <see cref="ItemsControl.ItemTemplate" />, even if the item is its own container (e.g. a UserControl).
	/// </summary>
	public class ContainerItemsControl : ItemsControl
	{
		/// <summary>
		/// Overwrites the IsItemItsOwnContainer property by always returning <see langword="false" />. This allows the <see cref="ItemsControl.ItemTemplate" /> to be also used with user controls.
		/// </summary>
		/// <param name="item">The item to check.</param>
		/// <returns>
		/// Always returns <see langword="false" />.
		/// </returns>
		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return false;
		}
	}
}