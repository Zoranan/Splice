using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormatterLanguage
{
    /// <summary>
    /// A Splicer object contains a series of string formatting commands. Its like Regex, for chopping up strings
    /// </summary>
    public class Splicer
    {
        private readonly List<FormatCommandGroup> _formattingGroups = new List<FormatCommandGroup>();

        #region Constructor Stuff
        //Constructor
        /// <summary>
        /// Create a new Splicer object with the give splice format string
        /// </summary>
        /// <param name="format">The splice string used to generate the formatting commands for this splice object</param>
        public Splicer(string format)
        {
            ParseFormat(format);
        }

        //Parsing
        internal void ParseFormat(string format)
        {
            StringBuilder currentPart = new StringBuilder();
            bool inCmdBlock = false;

            for (int i = 0; i < format.Length; i++)
            {
                var c = format[i];

                //Start a new command block
                if (c == '[')
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

                //Always add the current char
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

        /// <summary>
        /// Formats a string with this splice object
        /// </summary>
        /// <param name="input">The string to format</param>
        /// <returns>The spliced string</returns>
        public string Format(string input)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _formattingGroups.Count; i++)
            {
                sb.Append(_formattingGroups[i].GetValue(input));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Formats a string with the specified splice string
        /// </summary>
        /// <param name="splice">The splice string used to perform formatting</param>
        /// <param name="input">The string to format</param>
        /// <returns>The spliced string</returns>
        public static string Format(string splice, string input)
        {
            return new Splicer(splice).Format(input);
        }
        
    }
}
