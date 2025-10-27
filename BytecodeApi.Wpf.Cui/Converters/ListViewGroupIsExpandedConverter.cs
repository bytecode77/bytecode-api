using BytecodeApi.Wpf.Cui.Controls;
using BytecodeApi.Wpf.Extensions;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace BytecodeApi.Wpf.Cui.Converters;

public sealed class ListViewGroupIsExpandedConverter : ConverterBase<GroupItem>
{
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