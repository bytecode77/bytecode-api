using BytecodeApi.Data;

namespace Playground.Wpf.Cui.Model;

public sealed class Customer : ObservableObject
{
	public string? CustomerId { get; set => Set(ref field, value); }
	public string? CompanyName { get; set => Set(ref field, value); }
	public string? ContactName { get; set => Set(ref field, value); }
	public string? ContactTitle { get; set => Set(ref field, value); }
	public string? Address { get; set => Set(ref field, value); }
	public string? City { get; set => Set(ref field, value); }
	public string? Region { get; set => Set(ref field, value); }
	public string? PostalCode { get; set => Set(ref field, value); }
	public string? Country { get; set => Set(ref field, value); }
	public string? Phone { get; set => Set(ref field, value); }
	public string? Fax { get; set => Set(ref field, value); }
}