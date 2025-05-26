using ConsoleCalculator.Interfaces;

namespace ConsoleCalculator.Operations;

public class Add : IOperation
{
    public double Operate(double a, double b) => a + b;
}
