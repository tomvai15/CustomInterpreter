using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPi.Interpreter
{
    public static class Extensions
    {
        public static string Cut(this string text, int start, int end)
        {
            return text.Substring(start, end-start);
        }
    }
}
