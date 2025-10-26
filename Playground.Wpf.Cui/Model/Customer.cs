using BytecodeApi.Data;

namespace Playground.Wpf.Cui.Model;

public sealed class Customer : ObservableObject
{
	private string? _CustomerId;
	private string? _CompanyName;
	private string? _ContactName;
	private string? _ContactTitle;
	private string? _Address;
	private string? _City;
	private string? _Region;
	private string? _PostalCode;
	private string? _Country;
	private string? _Phone;
	private string? _Fax;
	public string? CustomerId
	{
		get => _CustomerId;
		set => Set(ref _CustomerId, value);
	}
	public string? CompanyName
	{
		get => _CompanyName;
		set => Set(ref _CompanyName, value);
	}
	public string? ContactName
	{
		get => _ContactName;
		set => Set(ref _ContactName, value);
	}
	public string? ContactTitle
	{
		get => _ContactTitle;
		set => Set(ref _ContactTitle, value);
	}
	public string? Address
	{
		get => _Address;
		set => Set(ref _Address, value);
	}
	public string? City
	{
		get => _City;
		set => Set(ref _City, value);
	}
	public string? Region
	{
		get => _Region;
		set => Set(ref _Region, value);
	}
	public string? PostalCode
	{
		get => _PostalCode;
		set => Set(ref _PostalCode, value);
	}
	public string? Country
	{
		get => _Country;
		set => Set(ref _Country, value);
	}
	public string? Phone
	{
		get => _Phone;
		set => Set(ref _Phone, value);
	}
	public string? Fax
	{
		get => _Fax;
		set => Set(ref _Fax, value);
	}
}