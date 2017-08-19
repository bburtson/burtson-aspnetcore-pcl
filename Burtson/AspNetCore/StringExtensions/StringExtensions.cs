using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes a substring from specified collection of occurences
        /// if none are found in the sequence, the original string is returned,
        /// ignores null or empty elements in the values array
        /// </summary>
        /// <param name="source">string to remove from</param>
        /// <param name="values">an array of occurences to remove from string </param>
        /// <returns> A new string without the found values[i], or the original if none found</returns>
        public static string RemoveAny(this string source, string[] values)
        {
            foreach (var unwanted in values.Where(o => !string.IsNullOrEmpty(o)))
            {
                if (source.Contains(unwanted))
                {
                    source = source.Replace(unwanted, string.Empty);
                }
            }
            return source;
        }
        
        /// <summary>
        /// Removes a substring from specified collection of occurences
        /// if none are found in the sequence, the original string is returned,
        /// ignores null or empty elements in the values array with comparison options
        /// </summary>
        /// <param name="source">string to remove from</param>
        /// <param name="values">an array of occurences to remove from string </param>
        /// <param name="comparisonType"></param>
        /// <returns> A new string without the found values[i], or the original if none found</returns>

        public static string RemoveAny(this string source, 
                                       string[] values, 
                                       StringComparison comparisonType)
        {
            foreach (var value in values)
            {
                var startIndex = source.IndexOf(value, StringComparison.CurrentCultureIgnoreCase);

                source = source.Remove(startIndex, value.Length);
            }
            return source;
        }



        public static bool Contains(this string source, string value,
            StringComparison comparisonType)
        {
            return source.IndexOf(value, comparisonType) >= 0;
        }




        /// <summary>
        /// Replaces a string with a newValue 
        /// if the current string contains any strings 
        /// specified in oldValues
        /// </summary>
        /// <param name="text"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValues">values to omit from source</param>
        /// <returns></returns>
        public static string ReplaceAny(this string source, string newValue, string[] values)
        {
            if(values.Any(s => !source.Contains(s))) return source;

            foreach (var value in values.Where(v => !string.IsNullOrEmpty(v)))
            {
                source = source.Replace(value, newValue);
            }

            return source;
        }

        /// <summary>
        /// Replaces a string with a newValue 
        /// if the current string contains any strings 
        /// specified in oldValues
        /// </summary>
        /// <param name="text"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValues">values to omit from source</param>
        /// <returns>new string with replaced values if any</returns>

        public static string ReplaceAny(this string source, 
                                        string newValue, 
                                        string[] values, 
                                        StringComparison comparisonType)
        {

            foreach (var value in values.Where(v => !string.IsNullOrEmpty(v)))
            {
                var startIndex = source.IndexOf(value, comparisonType);

                var sourceSub = source.Substring(startIndex, value.Length);

                source = source.Replace(sourceSub, newValue);
            }

            return source;
        }

        /// <summary>
        /// Splits supporting escape characters
        /// </summary>
        /// <param name="text"></param>
        /// <param name="separator"></param>
        /// <param name="escapeCharacter"></param>
        /// <param name="removeEmptyEntries"></param>
        /// <returns></returns>
        public static IEnumerable<string> Split(this string text, char separator, char escapeCharacter, bool removeEmptyEntries)
        {
            string buffer = string.Empty;
            bool escape = false;

            foreach (var c in text)
            {
                if (!escape && c == separator)
                {
                    if (!removeEmptyEntries || buffer.Length > 0)
                    {
                        yield return buffer;
                    }

                    buffer = string.Empty;
                }
                else
                {
                    if (c == escapeCharacter)
                    {
                        escape = !escape;

                        if (!escape)
                        {
                            buffer = string.Concat(buffer, c);
                        }
                    }
                    else
                    {
                        if (!escape)
                        {
                            buffer = string.Concat(buffer, c);
                        }

                        escape = false;
                    }
                }
            }

            if (buffer.Length != 0)
            {
                yield return buffer;
            }
        }
    }
}
