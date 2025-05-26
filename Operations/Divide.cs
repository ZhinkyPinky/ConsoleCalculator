using ConsoleCalculator.Interfaces;

namespace ConsoleCalculator.Operations;

public class Divide : IOperation
{
    public double Operate(double a, double b)
    {
        if (b == 0) throw new DivideByZeroException();
        return a / b;
    }
}
