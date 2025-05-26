using ConsoleCalculator.Interfaces;

namespace ConsoleCalculator.Operations;

public class Multiply : IOperation
{
    public double Operate(double a, double b) => a * b;
}

