using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormatterLanguage.InternalCommands
{
    abstract class FormattingAction
    {
        //Gets the specified value, updating the start index if necessary
        public abstract string GetValue(string input, ref int start);

        //Calculate an end based index
        protected int ConvertEndBasedIndex(string input, int i)
        {
            return input.Length - 1 - i;
        }
    }
}
