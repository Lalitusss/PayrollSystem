namespace Payroll.Engine.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class PayrollGroupAttribute : Attribute
{
    public string Name { get; }
    public PayrollGroupAttribute(string name) => Name = name;
}