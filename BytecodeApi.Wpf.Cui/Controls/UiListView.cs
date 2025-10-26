using BytecodeApi.Extensions;
using BytecodeApi.Wpf.Extensions;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiListView : ListView
{
	public static readonly DependencyProperty ShowHeaderRowProperty = DependencyPropertyEx.Register(nameof(ShowHeaderRow), new(true));
	public static readonly DependencyProperty CanResizeColumnsProperty = DependencyPropertyEx.Register(nameof(CanResizeColumns), new(true));
	public static readonly DependencyProperty CanSelectProperty = DependencyPropertyEx.Register(nameof(CanSelect), new(true));
	public static readonly DependencyProperty SelectedItemListProperty = DependencyPropertyEx.Register(nameof(SelectedItemList), new(SelectedItemList_Changed));
	public static readonly DependencyProperty SortColumnProperty = DependencyPropertyEx.Register(nameof(SortColumn));
	public static readonly DependencyProperty SortDirectionProperty = DependencyPropertyEx.Register(nameof(SortDirection), new(ListSortDirection.Ascending));
	public static readonly DependencyProperty SortColumn2Property = DependencyPropertyEx.Register(nameof(SortColumn2));
	public static readonly DependencyProperty SortDirection2Property = DependencyPropertyEx.Register(nameof(SortDirection2), new(ListSortDirection.Ascending));
	public static readonly DependencyProperty GroupSortDirectionProperty = DependencyPropertyEx.Register(nameof(GroupSortDirection), new(ListSortDirection.Ascending));
	public static readonly DependencyProperty GroupIsExpandedConverterProperty = DependencyPropertyEx.Register(nameof(GroupIsExpandedConverter));
	public static readonly DependencyProperty GroupIsExpandedConverterParameterProperty = DependencyPropertyEx.Register(nameof(GroupIsExpandedConverterParameter));
	public static readonly DependencyProperty ItemDoubleClickCommandProperty = DependencyPropertyEx.Register(nameof(ItemDoubleClickCommand));
	public bool ShowHeaderRow
	{
		get => this.GetValue<bool>(ShowHeaderRowProperty);
		set => SetValue(ShowHeaderRowProperty, value);
	}
	public bool CanResizeColumns
	{
		get => this.GetValue<bool>(CanResizeColumnsProperty);
		set => SetValue(CanResizeColumnsProperty, value);
	}
	public bool CanSelect
	{
		get => this.GetValue<bool>(CanSelectProperty);
		set => SetValue(CanSelectProperty, value);
	}
	public IList? SelectedItemList
	{
		get => this.GetValue<IList?>(SelectedItemListProperty);
		set => SetValue(SelectedItemListProperty, value);
	}
	public string? SortColumn
	{
		get => this.GetValue<string?>(SortColumnProperty);
		set => SetValue(SortColumnProperty, value);
	}
	public ListSortDirection SortDirection
	{
		get => this.GetValue<ListSortDirection>(SortDirectionProperty);
		set => SetValue(SortDirectionProperty, value);
	}
	public string? SortColumn2
	{
		get => this.GetValue<string?>(SortColumn2Property);
		set => SetValue(SortColumn2Property, value);
	}
	public ListSortDirection SortDirection2
	{
		get => this.GetValue<ListSortDirection>(SortDirection2Property);
		set => SetValue(SortDirection2Property, value);
	}
	public ListSortDirection GroupSortDirection
	{
		get => this.GetValue<ListSortDirection>(GroupSortDirectionProperty);
		set => SetValue(GroupSortDirectionProperty, value);
	}
	public IValueConverter? GroupIsExpandedConverter
	{
		get => this.GetValue<IValueConverter?>(GroupIsExpandedConverterProperty);
		set => SetValue(GroupIsExpandedConverterProperty, value);
	}
	public object? GroupIsExpandedConverterParameter
	{
		get => GetValue(GroupIsExpandedConverterParameterProperty);
		set => SetValue(GroupIsExpandedConverterParameterProperty, value);
	}
	public ICommand? ItemDoubleClickCommand
	{
		get => this.GetValue<ICommand?>(ItemDoubleClickCommandProperty);
		set => SetValue(ItemDoubleClickCommandProperty, value);
	}

	private GridViewColumnHeader[] ColumnHeaders => this
		.FindChildren<GridViewColumnHeader>(UITreeType.Visual)
		.Where(column => column.Role == GridViewColumnHeaderRole.Normal)
		.ToArray();
	private string? CurrentSortProperty;
	private SortAdorner? SortAdorner;

	public UiListView()
	{
		AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(UiListView_Click));
		Loaded += UiListView_Loaded;
		IsVisibleChanged += UiListView_IsVisibleChanged;
	}

	private void UiListView_Loaded(object sender, RoutedEventArgs e)
	{
		if (Sort(SortColumn, SortDirection))
		{
			Loaded -= UiListView_Loaded;
			IsVisibleChanged -= UiListView_IsVisibleChanged;
		}
	}
	private void UiListView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (Sort(SortColumn, SortDirection))
		{
			Loaded -= UiListView_Loaded;
			IsVisibleChanged -= UiListView_IsVisibleChanged;
		}
	}
	private static void SelectedItemList_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		UiListView listView = (UiListView)dependencyObject;

		if (listView.SelectedItemList == null)
		{
			listView.UnselectAll();
		}
		else
		{
			listView.SetSelectedItems(listView.SelectedItemList);
		}
	}
	protected override void OnPreviewMouseDoubleClick(MouseButtonEventArgs e)
	{
		base.OnPreviewMouseDoubleClick(e);

		if (e.Source is DependencyObject dependencyObject)
		{
			if (dependencyObject is TextElement textElement)
			{
				dependencyObject = textElement.Parent;
			}

			if (dependencyObject.FindParent<ListViewItem>(UITreeType.Visual) != null)
			{
				ItemDoubleClickCommand?.Execute(UIContext.Find<object>(e.Source));
			}
		}
	}
	protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
	{
		base.OnItemsSourceChanged(oldValue, newValue);

		UnselectAll();
	}
	protected override void OnSelectionChanged(SelectionChangedEventArgs e)
	{
		base.OnSelectionChanged(e);

		if (!CanSelect)
		{
			UnselectAll();
		}

		// Use reflection to support binding to generic source (IList -> List<T>)
		if (GetBindingExpression(SelectedItemListProperty) is BindingExpression binding &&
			(UIContext.Find<object>(binding.ResolvedSource) ?? binding.ResolvedSource) is object dataContext &&
			dataContext.GetType().GetProperty(binding.ResolvedSourcePropertyName) is PropertyInfo property &&
			property.PropertyType.GetGenericArguments().FirstOrDefault() is Type boundType)
		{
			Type returnType = property.PropertyType.GetGenericTypeDefinition().MakeGenericType(boundType);
			IList list = (IList)Activator.CreateInstance(returnType)!;

			if (SelectedItems != null)
			{
				foreach (object itm in SelectedItems)
				{
					list.Add(Convert.ChangeType(itm, boundType));
				}
			}

			SelectedItemList = list;
		}
	}
	private static void UiListView_Click(object sender, RoutedEventArgs e)
	{
		if (sender is UiListView listView &&
			e.OriginalSource is GridViewColumnHeader columnHeader &&
			columnHeader.Column is UiGridViewColumn column &&
			column.SortProperty is string propertyName)
		{
			listView.Sort(propertyName, null);
		}
	}

	private bool Sort(string? propertyName, ListSortDirection? direction)
	{
		if (ColumnHeaders.FirstOrDefault(column => (column.Column as UiGridViewColumn)?.SortProperty == propertyName) is GridViewColumnHeader columnHeader)
		{
			if (direction == null)
			{
				if (Items.SortDescriptions.FirstOrDefault(sortDescription => sortDescription.PropertyName == CurrentSortProperty) is SortDescription currentSort)
				{
					if (currentSort.PropertyName == propertyName)
					{
						direction = currentSort.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
					}
					else
					{
						direction = ListSortDirection.Ascending;
					}
				}
				else
				{
					direction = ListSortDirection.Ascending;
				}
			}

			Items.SortDescriptions.Clear();

			if (SortAdorner != null)
			{
				AdornerLayer.GetAdornerLayer(columnHeader).Remove(SortAdorner);
				SortAdorner = null;
			}

			if (direction != null)
			{
				// 1.) Sort by groups
				foreach (PropertyGroupDescription groupDescription in Items.GroupDescriptions.OfType<PropertyGroupDescription>())
				{
					if (groupDescription.Converter != null)
					{
						throw Throw.InvalidOperation("Converters are not supported in sorted groups.");
					}
					else
					{
						Items.SortDescriptions.Add(new(groupDescription.PropertyName, GroupSortDirection));
					}
				}

				// 2.) Sort by columns
				Items.SortDescriptions.Add(new(propertyName, direction.Value));
				SortAdorner = new(columnHeader, direction.Value);
				CurrentSortProperty = propertyName;
				AdornerLayer.GetAdornerLayer(columnHeader).Add(SortAdorner);

				// 3.) Sort by 2nd sort column
				if (SortColumn2 != null && SortColumn2 != CurrentSortProperty)
				{
					Items.SortDescriptions.Add(new(SortColumn2, SortDirection2));
				}
			}

			TextSearch.SetTextPath(this, propertyName);

			return true;
		}
		else
		{
			TextSearch.SetTextPath(this, null);

			// This happens when a UiListView is not part of the visual tree, yet (TabPage, or Visibility is set to Collapsed, etc...)
			// -> Sort() must be called again, once the headers are loaded.
			return false;
		}
	}
}