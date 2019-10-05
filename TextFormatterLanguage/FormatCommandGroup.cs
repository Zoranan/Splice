using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextFormatterLanguage.InternalCommands;

namespace TextFormatterLanguage
{
    class FormatCommandGroup
    {
        private List<FormattingAction> _formattingActions = new List<FormattingAction>();
        private int _currentIndex = 0;

        public FormatCommandGroup (string groupString)
        {
            //If this is a command, we need to split it and qork out each command separately
            if (groupString[0] == '[')
            {
                //Cut the brackets off the ends
                groupString = groupString.Substring(1, groupString.Length - 2);

                //Split up the different commands
                var parts = groupString.Split(';');

                foreach (var p in parts)
                {
                    FormattingAction fa = null;

                    //Character skipping
                    if (p[0] == '>')
                    {
                        fa = new SkipCharacterFormatAction(p);
                    }
                    //Character range substring
                    else if (Regex.IsMatch(p, "^(_|[^-]+)-(_|.+)$"))
                    {
                        fa = new SubstringRangeFormatAction(p);
                    }

                    //Traditional substring (start, length optionally)
                    else if (Regex.IsMatch(p, "^(_|[^,]+)(,.+)?$"))
                    {
                        fa = new SubstringFormatAction(p);
                    }

                    _formattingActions.Add(fa);
                }

            }

            //If this is not a command block, it is a literal
            else
            {
                _formattingActions.Add(new StringLiteralAction(groupString));
            }
        }

        public void AddCommand(FormattingAction fa)
        {
            _formattingActions.Add(fa);
        }

        //Get the value of all command block together in this 
        public string GetValue(string input)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _formattingActions.Count; i++)
            {
                sb.Append(_formattingActions[i].GetValue(input, ref _currentIndex));
            }

            _currentIndex = 0;

            return sb.ToString();
        }
    }
}
