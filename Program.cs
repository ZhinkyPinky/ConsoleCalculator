// See https://aka.ms/new-console-template for more information
using ConsoleCalculator.Interfaces;

namespace ConsoleCalculator;

class Calculator(IExpressionHandler expressionHandler)
{
    private readonly IExpressionHandler expressionHandler = expressionHandler;

    public void MainMenu()
    {
        string? userInput = "";
        //while (userInput != "q")
        //{
        Console.WriteLine("Please input something to calculate (q to quit):\n");
        userInput = "15+2*2+6-5*10";
        userInput = "12+3*5-1+(2412+2+1)*3/2+(1+1)";
        Console.WriteLine(userInput);
        double result = expressionHandler.CalculateExpression(userInput);
        Console.WriteLine($"result: {result}");
        //Console.ReadLine();

        //if (userInput == null) continue;

        //}
    }


}


class Program
{
    static void Main()
    {
        IExpressionHandler expressionHandler = new ExpressionHandler();
        Calculator calculator = new(expressionHandler);
        calculator.MainMenu();
    }

}
