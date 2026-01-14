/// <summary>
/// This method will be executed. The method must have either no parameters, or a <see cref="string" />[] parameter.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class ExecuteAttribute : Attribute
{
}