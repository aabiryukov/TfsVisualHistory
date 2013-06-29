using System;
using System.Text.RegularExpressions;

namespace Sitronics.TfsVisualHistory.VSExtension.Utility
{
    /// <summary>
    /// Represents a wildcard running on the
    /// <see cref="System.Text.RegularExpressions"/> engine.
    /// </summary>
    public class Wildcard : Regex
    {
        /// <summary>
        /// Initializes a wildcard with the given search pattern.
        /// </summary>
        /// <param name="pattern">The wildcard pattern to match.</param>
        public Wildcard(string pattern)
            : base(WildcardToRegex(pattern))
        {
        }

        /// <summary>
        /// Initializes a wildcard with the given search pattern and options.
        /// </summary>
        /// <param name="pattern">The wildcard pattern to match.</param>
        /// <param name="options">A combination of one or more
        /// <see cref="RegexOptions"/>.</param>
        public Wildcard(string pattern, RegexOptions options)
            : base(WildcardToRegex(pattern), options)
        {
        }

        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(pattern); }
        }

        /// <summary>
        /// Converts a wildcard to a regex.
        /// </summary>
        /// <param name="pattern">The wildcard pattern to convert.</param>
        /// <returns>A regex equivalent of the given wildcard.</returns>
        public static string WildcardToRegex(string pattern)
        {
            if (pattern == null) throw new ArgumentNullException("pattern");

            // Remove whitespaces
            var items = pattern.Split(new []{';'}, StringSplitOptions.RemoveEmptyEntries);
            for(var i=0; i<items.Length; i++)
            {
                items[i] = "^" + Escape(items[i].Trim()).
                 Replace("\\*", ".*").
                 Replace("\\?", ".") + "$";
            }

            return string.Join("|", items);
        }
    }
}
