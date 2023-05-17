using ElPi.Interpreter;

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


            IScanner scanner = new Scanner();

            scanner.Scan(code);
            
        }
    }
}