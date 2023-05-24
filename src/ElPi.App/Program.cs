using ElPi.Interpreter;
using ElPi.Interpreter.Visitors;

namespace ElPi.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string code;
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: elpi [script]");
                return;
            }
            else if (args.Length == 1)
            {
                code = FileReader.ReadFile(args[0]);
            }
            else
            {
                code = InputReader.ReadInput();
            }

            var scanner = new Scanner(code);
            var tokens = scanner.ScanTokens();

            var parser = new Parser(tokens);
            var statements =  parser.ParseStatements();

            //var printer = new AstPrinter();
            //var expressionTree = printer.Print(expression);
          //  Console.WriteLine(expressionTree);

            var interpreter = new ProgramInterpreter();
            interpreter.Interpret(statements);

            //Console.WriteLine($"Result: {result}");    
        }
    }
}