using ConsoleCalculator.Interfaces;

namespace ConsoleCalculator.Operations;

public class Modulus : IOperation
{
    public double Operate(double a, double b) => a % b;
}
