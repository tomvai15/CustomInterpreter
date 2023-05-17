using System.Text;

namespace ElPi.App
{
    public static class InputReader
    {
        public static string ReadInput()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Console.WriteLine("Supply code:");

            string line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                stringBuilder.AppendLine(line);
                line = Console.ReadLine();
            }

            return stringBuilder.ToString();
        }
    }
}
