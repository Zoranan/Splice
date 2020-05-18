using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFormatterLanguage;

namespace TestSpliceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter text to process: ");
                var text = Console.ReadLine();

                Console.Write("  Enter splice format: ");
                var splice = Console.ReadLine();

                Console.WriteLine(Splicer.Format(splice, text));
            }
        }
    }
}
