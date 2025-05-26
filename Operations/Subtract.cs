using ConsoleCalculator.Interfaces;

namespace ConsoleCalculator.Operations;

public class Subtract : IOperation
{
    public double Operate(double a, double b) => a - b;
}
