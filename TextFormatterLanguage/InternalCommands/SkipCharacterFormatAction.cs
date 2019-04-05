using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormatterLanguage.InternalCommands
{
    class SkipCharacterFormatAction : FormattingAction
    {
        private readonly char _charToSkip;
        private readonly bool _negated;

        //Constructors
        public SkipCharacterFormatAction(char skip)
        {
            _charToSkip = skip;
        }

        public SkipCharacterFormatAction(string skip)
        {
            //Make sure the input command for skip is valid!
            //Skip the following character
            if (skip[0] == '>' && skip.Length == 2)
            {
                _negated = false;
                _charToSkip = skip[1];
            }
            else if (skip[0] == '>' && skip.Length == 3 && skip[1] == '!')
            {
                _negated = true;
                _charToSkip = skip[2];
            }
            else
            {
                throw new ArgumentException("Bad input command for skip: " + skip);
            }
        }

        //Skip does not return real a value. Instead it updates the start value
        public override string GetValue(string input, ref int start)
        {
            while (start < input.Length && 
                (input[start] == _charToSkip) != _negated)
            {
                start++;
            }

            return string.Empty;
        }
    }
}
