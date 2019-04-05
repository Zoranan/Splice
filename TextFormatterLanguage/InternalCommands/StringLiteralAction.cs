using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormatterLanguage.InternalCommands
{
    class StringLiteralAction : FormattingAction
    {
        private string _literal;

        public StringLiteralAction(string val)
        {
            _literal = val;
        }

        public override string GetValue(string input, ref int start)
        {
            return _literal;
        }
    }
}
