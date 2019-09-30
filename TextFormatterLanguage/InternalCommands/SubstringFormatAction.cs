using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormatterLanguage.InternalCommands
{
    class SubstringFormatAction : FormattingAction
    {
        readonly bool _endBasedIndex = false;
        readonly int _start = 0;
        readonly int _length = -1;

        //Constructors
        internal SubstringFormatAction(int s, int l = -1, bool endBasedIndex = false)
        {
            _start = s;
            _length = l;
            _endBasedIndex = endBasedIndex;
        }

        internal SubstringFormatAction(string args)
        {
            var parts = args.Split(',');

            //Check for "Current Position" argument
            if (parts[0] == "_")
            {
                _start = -1;
            }
            else
            {

                //Check for end based index
                if (parts[0][0] == 'e')
                {
                    _endBasedIndex = true;
                    parts[0] = parts[0].Substring(1);
                }

                //Parse the start index
                if (!int.TryParse(parts[0], out _start))
                {
                    throw new ArgumentException("Could not parse start index: " + parts[0]);
                }

            }
            
            //Parse the length argument
            if (parts.Length > 1)
            {
                if (!int.TryParse(parts[1], out _length))
                {
                    throw new ArgumentException("Could not parse length: " + parts[1]);
                }
            }

            if (parts.Length > 2)
            {
                throw new ArgumentException("Invalid number of arguments for substring: " + parts.Length);
            }
        }

        //Get value
        internal override string GetValue(string input, ref int start)
        {
            string sub = "";
            int s;

            //If the start index is supposed to be the current position, use that
            if (_start == -1)
            {
                s = start;
            }

            //Otherwise, 
            else
            {
                //Use our stored value to start
                s = _start;

                //Calculate the normal index if using an end based index
                if (_endBasedIndex)
                {
                    s = ConvertEndBasedIndex(input, s);
                }
            }

            //Get the substring using the correct number of arguments
            if (s >= 0 && s < input.Length)
            {
                if (_length > -1 && s + _length <= input.Length)
                {
                    sub = input.Substring(s, _length);
                }

                else
                    sub = input.Substring(s);
            }

            start = s + sub.Length;

            return sub;

        }
    }
}
