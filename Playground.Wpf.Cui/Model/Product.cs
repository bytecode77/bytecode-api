using BytecodeApi.Data;

namespace Playground.Wpf.Cui.Model;

public sealed class Product : ObservableObject
{
	private int _ProductId;
	private string _ProductName;
	private int _SupplierId;
	private int _CategoryId;
	private string _QuantityPerUnit;
	private decimal _UnitPrice;
	private int _UnitsInStock;
	private int _UnitsOnOrder;
	private int _ReorderLevel;
	private bool _Discontinued;
	public int ProductId
	{
		get => _ProductId;
		set => Set(ref _ProductId, value);
	}
	public string ProductName
	{
		get => _ProductName;
		set => Set(ref _ProductName, value);
	}
	public int SupplierId
	{
		get => _SupplierId;
		set => Set(ref _SupplierId, value);
	}
	public int CategoryId
	{
		get => _CategoryId;
		set => Set(ref _CategoryId, value);
	}
	public string QuantityPerUnit
	{
		get => _QuantityPerUnit;
		set => Set(ref _QuantityPerUnit, value);
	}
	public decimal UnitPrice
	{
		get => _UnitPrice;
		set => Set(ref _UnitPrice, value);
	}
	public int UnitsInStock
	{
		get => _UnitsInStock;
		set => Set(ref _UnitsInStock, value);
	}
	public int UnitsOnOrder
	{
		get => _UnitsOnOrder;
		set => Set(ref _UnitsOnOrder, value);
	}
	public int ReorderLevel
	{
		get => _ReorderLevel;
		set => Set(ref _ReorderLevel, value);
	}
	public bool Discontinued
	{
		get => _Discontinued;
		set => Set(ref _Discontinued, value);
	}
}