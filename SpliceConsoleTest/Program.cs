using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFormatterLanguage;

namespace SpliceConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            char k;
            do
            {
                string input = "yvfgf.5432";
                string format = Console.ReadLine();

                var f = new CompiledFormatter(format);
                Console.WriteLine(f.Format(input));

                k = Console.ReadKey().KeyChar;
            } while (k != 'e');

        }
    }
}
