using BytecodeApi.Wpf.Cui.Controls;
using BytecodeApi.Wpf.Extensions;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace BytecodeApi.Wpf.Cui.Converters;

/// <summary>
/// Represents the converter that converts <see cref="GroupItem" /> values to <see cref="bool" /> values representing whether the group is expanded or collapsed.
/// </summary>
public sealed class ListViewGroupIsExpandedConverter : ConverterBase<GroupItem>
{
	/// <summary>
	/// Converts a <see cref="GroupItem" /> value to a <see cref="bool" /> value representing whether the group is expanded or collapsed.
	/// </summary>
	/// <param name="value">The <see cref="GroupItem" /> value to convert.</param>
	/// <returns>
	/// <see langword="true" />, if the group is expanded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override object? Convert(GroupItem? value)
	{
		if (value?.FindParent<UiListView>(UITreeType.Visual) is UiListView listView)
		{
			object? group = (value.DataContext as CollectionViewGroup)?.Name;
			return listView.GroupIsExpandedConverter?.Convert(group, typeof(bool), listView.GroupIsExpandedConverterParameter, CultureInfo.InvariantCulture) as bool? ?? true;
		}
		else
		{
			return true;
		}
	}
}