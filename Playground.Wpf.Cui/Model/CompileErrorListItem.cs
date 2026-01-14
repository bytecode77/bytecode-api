using BytecodeApi.Data;

namespace Playground.Wpf.Cui.Model;

public sealed class CompileErrorListItem : ObservableObject
{
	public string? Icon { get; set => Set(ref field, value); }
	public string? Code { get; set => Set(ref field, value); }
	public string? Description { get; set => Set(ref field, value); }
	public string? Project { get; set => Set(ref field, value); }
	public string? File { get; set => Set(ref field, value); }
	public int? Line { get; set => Set(ref field, value); }

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