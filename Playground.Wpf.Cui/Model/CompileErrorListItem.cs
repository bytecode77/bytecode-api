using BytecodeApi.Data;

namespace Playground.Wpf.Cui.Model;

public sealed class CompileErrorListItem : ObservableObject
{
	private string? _Icon;
	private string? _Code;
	private string? _Description;
	private string? _Project;
	private string? _File;
	private int? _Line;
	public string? Icon
	{
		get => _Icon;
		set => Set(ref _Icon, value);
	}
	public string? Code
	{
		get => _Code;
		set => Set(ref _Code, value);
	}
	public string? Description
	{
		get => _Description;
		set => Set(ref _Description, value);
	}
	public string? Project
	{
		get => _Project;
		set => Set(ref _Project, value);
	}
	public string? File
	{
		get => _File;
		set => Set(ref _File, value);
	}
	public int? Line
	{
		get => _Line;
		set => Set(ref _Line, value);
	}

	public CompileErrorListItem(string? icon, string? code, string? description, string? project, string? file, int? line)
	{
		Icon = icon;
		Code = code;
		Description = description;
		Project = project;
		File = file;
		Line = line;
	}
}