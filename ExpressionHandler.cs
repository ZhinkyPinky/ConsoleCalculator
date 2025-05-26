using System.Text.RegularExpressions;
using ConsoleCalculator.Interfaces;
using ConsoleCalculator.Operations;

namespace ConsoleCalculator;

public class ExpressionHandler(string pattern = @"\d+|\D") : IExpressionHandler
{
    private readonly CalculatorContext calculatorContext = new(new(SelectOperation));

    private readonly string pattern = pattern;
    private string[]? symbols;

    public double CalculateExpression(string expression)
    {
        symbols = [.. Regex.Matches(expression, pattern).Cast<Match>().Select(m => m.Value)];
        if (symbols == null) throw new NullReferenceException();

        if (symbols[0][0] == '(') return HandleOpeningParenthesis();
        if (symbols[1][0] == '*') return MultiDiv();
        return AddSub();
    }

    private double AddSub(int startIndex = 0)
    {
        Console.WriteLine($"AddSub: {startIndex}");
        if (symbols == null) throw new NullReferenceException();
        if (symbols[startIndex][0] == '(') return HandleOpeningParenthesis(start: startIndex);

        char oprtr;
        string operand = symbols[startIndex];
        double value = double.Parse(operand);
        for (int i = startIndex + 1; i < symbols.Length; i++)
        {
            oprtr = symbols[i++][0];

            if (symbols[i][0] == '(')
            {
                value = calculatorContext.Calculate(oprtr, value, HandleOpeningParenthesis(oprtr, value, i));
                break;
            }

            if ((i + 1) < symbols.Length)
            { //Check for upcoming multiplication/division
                if (symbols[i + 1][0] == '*' || symbols[i + 1][0] == '/')
                {
                    value = calculatorContext.Calculate(oprtr, value, MultiDiv(i));
                    break;
                }
            }

            operand = symbols[i];
            Console.WriteLine(operand);
            Console.WriteLine(oprtr);
            value = calculatorContext.Calculate(oprtr, value, double.Parse(operand));

            if ((i + 1) < symbols.Length && symbols[i + 1][0] == ')')
            {
                value = HandleClosingParenthesis(value, i + 1);
                break;
            }
        }

        Console.WriteLine($"AddSub value: {value}");
        return value;
    }

    private double MultiDiv(int startIndex = 0)
    {
        Console.WriteLine($"MultiDiv: {startIndex}");
        if (symbols == null) throw new NullReferenceException();
        if (symbols[startIndex][0] == '(') return HandleOpeningParenthesis(start: startIndex);

        char oprtr;
        string operand = symbols[startIndex];
        double value = double.Parse(operand);
        for (int i = startIndex + 1; i < symbols.Length; i++)
        {
            oprtr = symbols[i++][0];

            if (symbols[i][0] == '(')
            {
                value = HandleOpeningParenthesis(oprtr, value, i);
                break;
            }

            //Check if there is upcoming multiplication/division after current addition/subtraction
            if ((i + 1) < symbols.Length && (oprtr != '+' || oprtr != '-'))
            {
                char nextOprtr = symbols[i + 1][0];
                if (nextOprtr == '*' || nextOprtr == '/')
                {
                    value = calculatorContext.Calculate(oprtr, value, MultiDiv(i));
                    break;
                }
            }

            operand = symbols[i];
            value = calculatorContext.Calculate(oprtr, value, double.Parse(operand));

            if (i + 1 < symbols.Length && symbols[i + 1][0] == ')')
            {
                value = HandleClosingParenthesis(value, i + 1);
                break;
            }
        }

        Console.WriteLine($"MultiDiv value: {value}");
        return value;
    }

    private double HandleOpeningParenthesis(char prefixOperator = '+', double value = 0, int start = 0)
    {
        Console.WriteLine($"Handling opening parenthesis on index: {start}");

        if (symbols == null) throw new NullReferenceException();
        while (start < symbols.Length && symbols[start][0] == '(') start++;

        //As we skip consecutive ( the first symbol will always be a number followed by an operator.
        int operatorIndex = start + 1;
        if (operatorIndex < symbols.Length)
        {
            char op = symbols[operatorIndex][0];
            if (op == '+' || op == '-')
            {
                value = calculatorContext.Calculate(prefixOperator, value, AddSub(start));
            }
            else if (op == '*' || op == '/')
            {
                value = calculatorContext.Calculate(prefixOperator, value, MultiDiv(start));
            }
        }

        Console.WriteLine($"Operning value: {value}");
        return value;
    }

    //TODO: Is this needed?
    private double HandleClosingParenthesis(double value, int start)
    {
        Console.WriteLine($"Handling closing parenthesis on index: {start}");
        if (symbols == null) throw new NullReferenceException();
        while (start < symbols.Length && symbols[start][0] == ')') start++;

        //As we skip over consecutive ) the first symbol, it it exists, will always be an operator.
        char oprtr;
        string operand;
        for (int i = start; i < symbols.Length; i++)
        {
            oprtr = symbols[i++][0]; //Add operation. In the first loop this is directly after )

            if (symbols[i][0] == '(')
            {
                value = HandleOpeningParenthesis(oprtr, value, i);
                break;
            }

            if (symbols[i + 1][0] == '*' || symbols[i + 1][0] == '/')
            {
                Console.WriteLine($"boop: {i}");
                value = calculatorContext.Calculate(oprtr, value, MultiDiv(i));
                break;
            }

            operand = symbols[i];
            value = calculatorContext.Calculate(oprtr, value, double.Parse(operand));

            if (symbols[i + 1][0] == ')')
            {
                value = HandleClosingParenthesis(value, i);
                break;
            }
        }

        Console.WriteLine($"Closing value: {value}");
        return value;
    }

    private static IOperation? SelectOperation(char oprtr)
    {
        return oprtr switch
        {
            '+' => new Add(),
            '-' => new Subtract(),
            '*' => new Multiply(),
            '/' => new Divide(),
            '%' => throw new NotImplementedException(),
            _ => null,
        };
    }
}

