using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormatterLanguage.InternalCommands
{
    internal class StringLiteralAction : FormattingAction
    {
        private string _literal;

        internal StringLiteralAction(string val)
        {
            _literal = val;
        }

        internal override string GetValue(string input, ref int start)
        {
            return _literal;
        }
    }
}
