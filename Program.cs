// See https://aka.ms/new-console-template for more information
using System.Data;
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

// interface IExpressionGraphBuilder
// {
//     ExpressionGraph BuildGraph(string expression);
// }
//
// class ExpressionGraphBuilder : IExpressionGraphBuilder
// {
//     private readonly string pattern = @"\d+|\D";
//
//     public ExpressionGraph BuildGraph(string expression) => new(
//             AddSubNode
//             (
//                 node: new(),
//                 symbols: [.. Regex.Matches(expression, pattern).Cast<Match>().Select(m => m.Value)],
//                 start: 0
//             ));
//
//     private Node MultiDivNode(Node node, string[] symbols, int start)
//     {
//         Console.WriteLine("MultiDivNode");
//         for (int i = start; i < symbols.Length; i++)
//         {
//             Console.WriteLine($"booop: {symbols[i]}");
//             if (symbols[i][0] == '(') return HandleOpeningParenthesis(node, symbols, i);
//             if (symbols[i][0] == ')') return HandleClosingParenthesis(node, symbols, i);
//
//             node.Symbols.Add(symbols[i]);
//         }
//
//         return node;
//     }
//
//     private Node AddSubNode(Node node, string[] symbols, int start)
//     {
//         Console.WriteLine("AddSubNode");
//         for (int i = start; i < symbols.Length; i++)
//         {
//             Console.WriteLine($"booop: {symbols[i]}");
//             if (symbols[i][0] == '(') return HandleOpeningParenthesis(node, symbols, i);
//             if (symbols[i][0] == ')') return HandleClosingParenthesis(node, symbols, i);
//
//             if ((i + 1) < symbols.Length && (symbols[i + 1][0] == '*' || symbols[i + 1][0] == '/'))
//             {
//                 node.Next = MultiDivNode(new(), symbols, i);
//                 return node;
//             }
//
//             node.Symbols.Add(symbols[i]);
//         }
//
//         return node;
//     }
//
//     private Node HandleOpeningParenthesis(Node currentNode, string[] symbols, int start)
//     {
//         Console.WriteLine($"Handling parenthesis on index: {start}");
//         //Skip consecutive (
//         while (start < symbols.Length && symbols[start][0] == '(') start++;
//
//         //As we skip consecutive ( the first symbol will always be a number followed by an operator.
//         int operatorIndex = start + 1;
//         if (operatorIndex < symbols.Length)
//         {
//             char op = symbols[operatorIndex][0];
//             if (op == '+' || op == '-')
//             {
//                 currentNode.Next = AddSubNode(new(), symbols, start);
//             }
//             else if (op == '*' || op == '/')
//             {
//                 currentNode.Next = MultiDivNode(new(), symbols, start);
//             }
//         }
//
//         return currentNode;
//     }
//
//
//     private Node HandleClosingParenthesis(Node node, string[] symbols, int start)
//     {
//         while (start < symbols.Length && symbols[start][0] == ')') start++;
//
//         //As we skip over consecutive ) the first symbol, it it exists, will always be an operator.
//         for (int i = start; i < symbols.Length; i++)
//         {
//             node.Symbols.Add(symbols[i++]); //Add operation. In the first loop this is directly after )
//
//             if (symbols[i][0] == '(') return HandleOpeningParenthesis(node, symbols, i);
//
//             if (symbols[i + 1][0] == '*' || symbols[i + 1][0] == '/')
//             {
//                 node.Next = MultiDivNode(new(), symbols, i);
//                 return node;
//             }
//
//             node.Symbols.Add(symbols[i]); //Add number.
//         }
//
//         return node;
//     }
//
// }

// class ExpressionGraph(Node startNode)
// {
//     private readonly CalculatorContext calculatorContext = new();
//
//     private readonly Node startNode = startNode;
//     internal Node StartNode => startNode;
//
//
//     public double CalculateGraphValue() => CalculateNodeValue(startNode);
//
//     private double CalculateNodeValue(Node node)
//     {
//         //TODO: Convert to while-loop in CalculateGraphValue???
//         List<string> nodeSymbols = node.Symbols;
//
//         double a, b, value = 0;
//         for (int i = 0; (i + 3) < nodeSymbols.Count; i += 3)
//         {
//             a = double.Parse(nodeSymbols.ElementAt(i));
//             b = double.Parse(nodeSymbols.ElementAt(i + 2));
//
//             calculatorContext.Operation = SelectOperation(nodeSymbols.ElementAt(i + 1));
//             value += calculatorContext.Calculate(a, b);
//         }
//
//         Console.WriteLine($"Last symbol: {nodeSymbols.Last()}");
//         calculatorContext.Operation = SelectOperation(nodeSymbols.Last());
//         if (node.Next != null && calculatorContext.Operation != null) return calculatorContext.Calculate(value, CalculateNodeValue(node.Next));
//         return value;
//     }
//
//     private static IOperation? SelectOperation(string symbol)
//     {
//         return symbol switch
//         {
//             "+" => new Add(),
//             "-" => new Subtract(),
//             "*" => new Multiply(),
//             "/" => new Divide(),
//             _ => null,
//         };
//     }
// }
//
//
// class Node
// {
//     private readonly List<string> symbols = [];
//     internal List<string> Symbols => symbols;
//
//     private Node? next;
//     internal Node? Next
//     {
//         get => next;
//         set => next = value;
//     }
// }
