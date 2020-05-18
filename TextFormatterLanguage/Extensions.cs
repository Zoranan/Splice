using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormatterLanguage
{
    /// <summary>
    /// Allows the user to apply splice directly to string objects
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Format this string using the spliceFormat string
        /// </summary>
        /// <param name="input">The string to format</param>
        /// <param name="spliceFormat">The splice string specifying the format</param>
        /// <returns>The formatted string</returns>
        public static string Splice(this string input, string spliceFormat)
        {
            return Splicer.Format(spliceFormat, input);
        }
    }
}
