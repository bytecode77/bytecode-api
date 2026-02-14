using BytecodeApi.Data;

namespace Playground.Wpf.Cui.Model;

public sealed class Product : ObservableObject
{
	public int ProductId { get; set => Set(ref field, value); }
	public string ProductName { get; set => Set(ref field, value); } = "";
	public int SupplierId { get; set => Set(ref field, value); }
	public int CategoryId { get; set => Set(ref field, value); }
	public string QuantityPerUnit { get; set => Set(ref field, value); } = "";
	public decimal UnitPrice { get; set => Set(ref field, value); }
	public int UnitsInStock { get; set => Set(ref field, value); }
	public int UnitsOnOrder { get; set => Set(ref field, value); }
	public int ReorderLevel { get; set => Set(ref field, value); }
	public bool Discontinued { get; set => Set(ref field, value); }
}