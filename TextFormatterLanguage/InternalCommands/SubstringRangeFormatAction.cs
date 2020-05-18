using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormatterLanguage.InternalCommands
{
    class SubstringRangeFormatAction : FormattingAction
    {
        readonly bool _endBasedStartIndex = false;
        readonly bool _endBasedEndIndex = false;
        readonly int _start = -1;
        readonly int _end = -1;

        internal SubstringRangeFormatAction(string args)
        {
            var parts = args.Split('-');

            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments for substring range: " + parts.Length + ". Requires 2 index values");
            }

            //Do the start index first
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
                    _endBasedStartIndex = true;
                    parts[0] = parts[0].Substring(1);
                }

                //Parse the start index
                if (!int.TryParse(parts[0], out _start))
                {
                    throw new ArgumentException("Could not parse start index: " + parts[0]);
                }

            }

            //Now the end index
            //Check for "Current Position" argument
            if (parts[1] == "_")
            {
                _end = -1;
            }
            else
            {

                //Check for end based index
                if (parts[1][0] == 'e')
                {
                    _endBasedEndIndex = true;
                    parts[1] = parts[1].Substring(1);
                }

                //Parse the start index
                if (!int.TryParse(parts[1], out _end))
                {
                    throw new ArgumentException("Could not parse start index: " + parts[1]);
                }

            }
        }

        internal override string GetValue(string input, ref int start)
        {
            string sub = "";
            int s;

            //Get START Index
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
                if (_endBasedStartIndex)
                {
                    s = ConvertEndBasedIndex(input, s);
                }
            }

            //Get end index
            int e;
            //If the start index is supposed to be the current position, use that
            if (_end == -1)
            {
                e = start;
            }

            //Otherwise, 
            else
            {
                //Use our stored value for start as end
                e = _end;

                //Calculate the normal index if using an end based index
                if (_endBasedEndIndex)
                {
                    e = ConvertEndBasedIndex(input, e);
                }
            }

            //Now calculate length
            int l;
            if (e >= s)
            {
                l = e - s + 1;
            }
            else
            {
                l = 0;
            }

            //Get the substring using the correct number of arguments
            if (s >= 0 && s < input.Length && e >= 0 && e < input.Length)
            {
                sub = input.Substring(s, l);
            }

            start = s + sub.Length;

            return sub;
        }
    }
}
