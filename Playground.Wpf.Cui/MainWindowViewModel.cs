using BytecodeApi.CsvParser;
using BytecodeApi.Data;
using BytecodeApi.Extensions;
using BytecodeApi.Wpf;
using Playground.Wpf.Cui.Model;
using Playground.Wpf.Cui.Properties;
using System;
using System.Data;
using System.Linq;

namespace Playground.Wpf.Cui;

public sealed class MainWindowViewModel : ObservableObject
{
	public MainWindow View { get; set; }

	private DelegateCommand<bool>? _ShowDialogWindowCommand;
	public DelegateCommand<bool> ShowDialogWindowCommand => _ShowDialogWindowCommand ??= new(ShowDialogWindowCommand_Execute);

	private string[] _ListBoxItems = ["Item 1", "Item 2", "Item 3", "Item 4", "Item 5"];
	private string[] _TextBoxAutoCompleteItems = ["James", "Robert", "John", "Michael", "David", "William", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", "Kenneth", "Kevin", "Brian", "George", "Timothy", "Ronald", "Edward", "Jason", "Jeffrey", "Ryan", "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen", "Larry", "Justin", "Scott", "Brandon", "Benjamin", "Samuel", "Gregory", "Alexander", "Frank", "Patrick", "Raymond", "Jack", "Dennis", "Jerry", "Tyler", "Aaron", "Jose", "Adam", "Nathan", "Henry", "Douglas", "Zachary", "Peter", "Kyle", "Ethan", "Walter", "Noah", "Jeremy", "Christian", "Keith", "Roger", "Terry", "Gerald", "Harold", "Sean", "Austin", "Carl", "Arthur", "Lawrence", "Dylan", "Jesse", "Jordan", "Bryan", "Billy", "Joe", "Bruce", "Gabriel", "Logan", "Albert", "Willie", "Alan", "Juan", "Wayne", "Elijah", "Randy", "Roy", "Vincent", "Ralph", "Eugene", "Russell", "Bobby", "Mason", "Philip", "Louis"];
	private TreeViewNode[] _TreeNodes =
	[
		new("Solution 'MyProject' (1 of 1 project)", "/Playground.Wpf.Cui;component/Resources/Icons/Application.svg", true,
		[
			new("MyProject", "/Playground.Wpf.Cui;component/Resources/Icons/CSProjectNode.svg", true,
			[
				new("Depemndencies", "/Playground.Wpf.Cui;component/Resources/Icons/ReferenceGroup.svg", false,
				[
					new("Analyzers", "/Playground.Wpf.Cui;component/Resources/Icons/CodeInformation.svg"),
					new("Frameworks", "/Playground.Wpf.Cui;component/Resources/Icons/Framework.svg", false,
					[
						new("Microsoft.NETCore.App", "/Playground.Wpf.Cui;component/Resources/Icons/FrameworkPrivate.svg"),
						new("Microsoft.WindowsDesktop.App.WPF", "/Playground.Wpf.Cui;component/Resources/Icons/FrameworkPrivate.svg")
					])
				]),
				new("Properties", "/Playground.Wpf.Cui;component/Resources/Icons/PropertiesFolderClosed.svg", false,
				[
					new("AssemblyInfo.cs", "/Playground.Wpf.Cui;component/Resources/Icons/CSFileNode.svg")
				]),
				new("App.xaml", "/Playground.Wpf.Cui;component/Resources/Icons/WPFFile.svg", false,
				[
					new("App.xaml.cs", "/Playground.Wpf.Cui;component/Resources/Icons/CSFileNode.svg")
				]),
				new("MainWindow.xaml", "/Playground.Wpf.Cui;component/Resources/Icons/WPFFile.svg", true,
				[
					new("MainWindow.xaml.cs", "/Playground.Wpf.Cui;component/Resources/Icons/CSFileNode.svg")
				])
			])
		])
	];
	private Customer[] _Customers = [];
	private Product[] _Products = [];
	private CompileErrorListItem[] _CompileErrorListItems =
	[
		new("/Playground.Wpf.Cui;component/Resources/Icons/StatusError.svg", "CS1585", "Member modifier 'private' must precede the member type and name", "MyProject", "MainWindow.xaml.cs", 7),
		new("/Playground.Wpf.Cui;component/Resources/Icons/StatusWarning.svg", "CS0162", "Unreachable code detected", "MyProject", "MainWindow.xaml.cs", 42)
	];
	private DataTable _CustomersDataTable = new();
	public string[] ListBoxItems
	{
		get => _ListBoxItems;
		set => Set(ref _ListBoxItems, value);
	}
	public string[] TextBoxAutoCompleteItems
	{
		get => _TextBoxAutoCompleteItems;
		set => Set(ref _TextBoxAutoCompleteItems, value);
	}
	public TreeViewNode[] TreeNodes
	{
		get => _TreeNodes;
		set => Set(ref _TreeNodes, value);
	}
	public Customer[] Customers
	{
		get => _Customers;
		set => Set(ref _Customers, value);
	}
	public Product[] Products
	{
		get => _Products;
		set => Set(ref _Products, value);
	}
	public CompileErrorListItem[] CompileErrorListItems
	{
		get => _CompileErrorListItems;
		set => Set(ref _CompileErrorListItems, value);
	}
	public DataTable CustomersDataTable
	{
		get => _CustomersDataTable;
		set => Set(ref _CustomersDataTable, value);
	}

	public MainWindowViewModel(MainWindow view)
	{
		View = view;

		CsvFile customers = CsvFile.FromString(Resources.Customers, true);

		Customers = customers.Rows
			.Select(row => new Customer
			{
				CustomerId = row[0].Value,
				CompanyName = row[1].Value,
				ContactName = row[2].Value,
				ContactTitle = row[3].Value,
				Address = row[4].Value,
				City = row[5].Value,
				Region = row[6].Value,
				PostalCode = row[7].Value,
				Country = row[8].Value,
				Phone = row[9].Value,
				Fax = row[10].Value
			})
			.ToArray();

		if (customers.Headers != null)
		{
			foreach (string header in customers.Headers)
			{
				CustomersDataTable.Columns.Add(header, typeof(string));
			}
		}

		foreach (CsvRow row in customers.Rows)
		{
			DataRow dataRow = CustomersDataTable.NewRow();
			for (int i = 0; i < row.Count && i < CustomersDataTable.Columns.Count; i++)
			{
				dataRow[i] = string.IsNullOrEmpty(row[i].Value) || row[i].Value == "NULL" ? DBNull.Value : row[i].Value;
			}

			CustomersDataTable.Rows.Add(dataRow);
		}

		Products = CsvFile.FromString(Resources.Products, true).Rows
			.Select(row => new Product
			{
				ProductId = row[0].Int32Value!.Value,
				ProductName = row[1].Value,
				SupplierId = row[2].Int32Value!.Value,
				CategoryId = row[3].Int32Value!.Value,
				QuantityPerUnit = row[4].Value,
				UnitPrice = row[5].Value.ToDecimalOrNull()!.Value,
				UnitsInStock = row[6].Int32Value!.Value,
				UnitsOnOrder = row[7].Int32Value!.Value,
				ReorderLevel = row[8].Int32Value!.Value,
				Discontinued = row[9].Int32Value == 1
			})
			.ToArray();
	}

	private void ShowDialogWindowCommand_Execute(bool useApplicationWindow)
	{
		if (useApplicationWindow)
		{
			new DialogApplicationWindow(View).ShowDialog();
		}
		else
		{
			new DialogWindow(View).ShowDialog();
		}
	}
}