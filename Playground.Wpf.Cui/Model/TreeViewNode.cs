using BytecodeApi.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Playground.Wpf.Cui.Model;

public sealed class TreeViewNode : ObservableObject
{
	public string Header { get; set => Set(ref field, value); }
	public string? Icon { get; set => Set(ref field, value); }
	public bool IsExpanded { get; set => Set(ref field, value); }
	public ObservableCollection<TreeViewNode> Children { get; set => Set(ref field, value); }

	public TreeViewNode(string header, string? icon)
	{
		Header = header;
		Icon = icon;
		Children = [];
	}
	public TreeViewNode(string header, string? icon, bool isExpanded, IEnumerable<TreeViewNode> children) : this(header, icon)
	{
		IsExpanded = isExpanded;
		Children = new(children);
	}
}