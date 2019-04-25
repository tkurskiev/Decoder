using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Decoding
{
    public class MyDecoder
    {
        #region Private Properties

        // Encoded string to decode.
        private string StringToDecode { get; set; }

        // Regular expression with the basic pattern
        private static Regex Regex { get; set; } = new Regex(@"\d+\[([a-zA-Z]+)\]([a-zA-Z]*)");

        #endregion

        #region Public Constructor

        /// <summary>
        /// Custom constructor, takes string parameter and assigns it to StringToDecode property of the object
        /// </summary>
        /// <param name="encodedString">The string to decode</param>
        public MyDecoder(string value)
        {
            StringToDecode = value;            
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Takes the string argument, assigns it to StringToDecode property of the object, and decodes it
        /// </summary>
        /// <param name="inputString">The string to decode</param>
        /// <returns>The resulting decoded string</returns>
        public string Decode(string inputString)
        {
            // If the given string parameter is empty...
            if (string.IsNullOrEmpty(inputString))
                return this.StringToDecode = "The string is empty";
                        
            this.StringToDecode = inputString;

            // Check if the StringToDecode is a correct string "n[s]", where "s" - is a string,
            // consisting of latin alphabet letters and nested "n[s]" expressions; and "n" - is a number
            // of times "s" should be repeated
            // Also, if "s" doesn't needs to be repeated, it comes with no number "n" and brackets
            string check = ValidityChecker(this.StringToDecode);

            // if Validity checker didn't return the original value the StringToDecode
            if (this.StringToDecode != check)
                return check;

            // Find the initial match in the given string
            Match match = Regex.Match(this.StringToDecode);

            do
            {
                // Unfold the found match value
                string toReplace = StringUnfolder(match.Value);

                // Replace the found match in the given string with its unfolded value
                this.StringToDecode = Replace(this.StringToDecode, toReplace, match.Index, match.Length);

                // Find the next match in the processed given string
                match = Regex.Match(this.StringToDecode);

            } while (match.Success); // While any new match can be found

            // Return the decoded string
            return this.StringToDecode;
        }

        /// <summary>
        /// Decodes the string, which was given to the constructor
        /// </summary>
        /// <returns></returns>
        public string Decode()
        {
            // If the string value, consumed by constructor, is empty
            if (string.IsNullOrEmpty(this.StringToDecode))
                return this.StringToDecode = "The string is empty";

            // Else, decode the string
            return Decode(this.StringToDecode);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Unfolds the encoded string in the found match; each match is string with a pattern: \d+\[([a-zA-Z]+)\]([a-zA-Z]*)
        /// </summary>
        /// <param name="currentMatch">The value of the current match</param>
        /// <returns></returns>
        private string StringUnfolder(string currentMatch)
        {
            // If there is no numbers in a match
            if (!Regex.Match(currentMatch, @"\d+").Success)
                return currentMatch;

            // Select the numbers in the current match
            int number = Int32.Parse(Regex.Match(currentMatch, @"\d+").Value);
                        
            // Select first ocurrence of a sequence of letters (which is the text in square brackets)
            string subString = Regex.Match(currentMatch, @"([a-zA-Z]+)").Value;

            // Remove the numbers, brackets and letters inside brackets, in order to get the remaining letters
            string remainingString = Regex.Replace(currentMatch, @"\d|\[([a-zA-Z]+)\]", @"");

            // Get the unfolded string, which is substring repeated *numbers in match* times  PLUS the remaingString
            string unfoldedString = Repeat(subString, number, remainer: remainingString);

            return unfoldedString;
        }

        /// <summary>
        /// Repeats a string given number of times and adds the remainer to it.
        /// </summary>
        /// <param name="s">The string to repeat</param>
        /// <param name="n">The amount of times to repeat the string</param>
        /// <param name="remainer">The string to append to the end of the obtained string</param>
        /// <returns></returns>
        private string Repeat(string s, int n, string remainer)
            => new StringBuilder(s.Length * n).Insert(0, s, n).Append(remainer).ToString();

        /// <summary>
        /// Replaces a match by its unfolded value
        /// </summary>
        /// <param name="inputString">The given string</param>
        /// <param name="toReplace">The string, representing the unfolded match value</param>
        /// <param name="index">Index where match occurs in the given string</param>
        /// <param name="length">The length of the match value</param>
        /// <returns></returns>
        private string Replace(string inputString, string toReplace, int index, int length)
            => new StringBuilder(inputString).Remove(index, length).Insert(index, toReplace).ToString();

        /// <summary>
        ///  Checks if the StringToDecode value corresponds to the global given task condition
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns></returns>
        private string ValidityChecker(string value)
        {
            // First check if the string contains any symbols other than latin alphabet letters or digits

            // MatchCollection matchCollection = Regex.Matches(value, @"[^a-zA-Z0-9-\[-\]]");  - wrong, because (\[-\])  - includes "\" sign.
            MatchCollection matchCollection = Regex.Matches(value, @"[^a-zA-Z0-9-\[\]]");
            if (matchCollection.Count != 0)
            {
                string s = "Unacceptable symbol is used:";
                foreach (Match match in matchCollection)
                {
                    s += $"\nSymbol: \"{match.Value}\", at: {match.Index}";
                }
                return s;
            }

            // Check if anywhere in a string there is number coming before text or before "]"
            // or if the string ends with a number
            if (Regex.Match(value, @"\d+([a-zA-Z])|\d+\]|\d+$").Success)
            {
                return "Неправильное выражение";
            }

            // Check if anywhere in the string there is a
            // text before starting brackets ("[") occurs
            if (Regex.Match(value, @"\D+\[").Success)                 // if (Regex.Match(value, @"^\D+|\D+\[").Success)
            {
                return "Неправильное выражение";
            }

            // If string consists of only opening and closing brackets and latin alphabet letters
            // inside them
            if (Regex.Match(value, @"^\[([a-zA-Z])\]*").Success)
            {
                return "Неправильное выражение";
            }

            // Check if there is opening and closing brackets without any text inside them
            // anywhere in the string
            if (Regex.Match(value, @"\[\]").Success)
            {
                return "Неправильное выражение";
            }

            // Check if the amount of opening brackets equals the amount of closing ones
            matchCollection = Regex.Matches(value, @"\[");
            if (matchCollection.Count != Regex.Matches(value, @"\]").Count)
            {
                return "Число открывающих скобок не равно числу закрывающих";
            }

            // If all checks passed, just return the original value
            return value;
        }

        #endregion
    }
}
