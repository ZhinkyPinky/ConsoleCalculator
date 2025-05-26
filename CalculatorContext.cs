using ConsoleCalculator.Interfaces;
using ConsoleCalculator.Operations;

namespace ConsoleCalculator;

public class CalculatorContext(OperationSelector operationSelector)
{
    private readonly OperationSelector operationSelector = operationSelector;

    private IOperation? operation;

    public double Calculate(char oprtr, double a, double b)
    {
        operation = operationSelector(oprtr);
        if (operation == null) throw new NullReferenceException();
        Console.WriteLine($"{a} {oprtr} {b}");
        return operation.Operate(a, b);
    }
}
