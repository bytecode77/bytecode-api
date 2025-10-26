using BytecodeApi.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Playground.Wpf.Cui.Model;

public sealed class TreeViewNode : ObservableObject
{
	private string _Header = "";
	private string? _Icon;
	private bool _IsExpanded;
	private ObservableCollection<TreeViewNode> _Children = [];
	public string Header
	{
		get => _Header;
		set => Set(ref _Header, value);
	}
	public string? Icon
	{
		get => _Icon;
		set => Set(ref _Icon, value);
	}
	public bool IsExpanded
	{
		get => _IsExpanded;
		set => Set(ref _IsExpanded, value);
	}
	public ObservableCollection<TreeViewNode> Children
	{
		get => _Children;
		set => Set(ref _Children, value);
	}

	public TreeViewNode(string header, string? icon)
	{
		Header = header;
		Icon = icon;
	}
	public TreeViewNode(string header, string? icon, bool isExpanded, IEnumerable<TreeViewNode> children) : this(header, icon)
	{
		IsExpanded = isExpanded;
		Children = new(children);
	}
}