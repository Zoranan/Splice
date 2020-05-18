using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormatterLanguage
{
    public class CompiledFormatter
    {
        private const char ESCAPE_CHAR = '\\';
        private List<FormatCommandGroup> _formattingGroups = new List<FormatCommandGroup>();

        #region Contructor Stuff
        //Constructor
        public CompiledFormatter(string format)
        {
            ParseFormat(format);
        }

        //Parsing
        public void ParseFormat(string format)
        {
            StringBuilder currentPart = new StringBuilder();
            bool inCmdBlock = false;
            bool escaping = false;

            for (int i = 0; i < format.Length; i++)
            {
                var c = format[i];

                if (escaping)
                {
                    currentPart.Append(c);
                    escaping = false;
                }

                else if (c == ESCAPE_CHAR)
                {
                    escaping = true;
                }

                //Start a new command block
                else if (c == '[')
                {
                    if (!inCmdBlock)
                    {
                        inCmdBlock = true;

                        if (currentPart.Length > 0)
                        {
                            _formattingGroups.Add(new FormatCommandGroup(currentPart.ToString()));
                            currentPart.Clear();
                        }

                        currentPart.Append(c);
                    }
                    else
                    {
                        ThrowFormatStringException(c, i, "You can not start a new command block until the current command block ends (])");
                    }
                }

                //End the current command block
                else if (c == ']')
                {
                    currentPart.Append(c);

                    if (inCmdBlock)
                    {
                        inCmdBlock = false;

                        _formattingGroups.Add(new FormatCommandGroup(currentPart.ToString()));
                        currentPart.Clear();
                    }
                    else
                    {
                        ThrowFormatStringException(c, i, "No command was started, so you can not end a command");
                    }
                }

                //Add the current char if nothing else matched
                else
                {
                    currentPart.Append(c);
                }
            }

            //Check the current string builder at the end for our last action
            if (currentPart.Length > 0)
            {
                _formattingGroups.Add(new FormatCommandGroup(currentPart.ToString()));
            }
        }
        #endregion

        private static void ThrowFormatStringException(char c, int i, string message = "")
        {
            throw new ArgumentException("Bad command '" + c + " at index " + i + "." + Environment.NewLine + message);
        }

        public string Format(string input)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _formattingGroups.Count; i++)
            {
                _formattingGroups[i].ResetPosition();
                sb.Append(_formattingGroups[i].GetValue(input));
            }

            return sb.ToString();
        }
        
    }
}
