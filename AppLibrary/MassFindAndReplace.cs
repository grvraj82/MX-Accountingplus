using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace DataManager
{
    public class MassFindAndReplace
    {
        #region Fields
            private string outputDirectory = string.Empty;
            private string findWhatString = string.Empty;
            private string replaceWithText = string.Empty;
            private bool isMatchWholeWord = false;
            private bool isMatchCase = false;
            private bool isWildcard = false;
        #endregion

        /// <summary>
        /// Performs the find and replace operation on a file.
        /// </summary>
        /// <param name="file">The path of the file to operate on.</param>
        /// <returns>A value indicating if the file has changed.</returns>
        public bool FindAndReplace(string file, string findString, string replaceWithString)
        {
            //holds the content of the file
            string content = string.Empty;

            //Create a new object to read a file	
            using (StreamReader sr = new StreamReader(file))
            {
                //Read the file into the string variable.
                content = sr.ReadToEnd();
            }

            //Get search text
            string searchText = GetSearchText(findString);

            //Look for a match
            if (Regex.IsMatch(content, searchText, GetRegExOptions()))
            {
                //Replace the text
                string newText = Regex.Replace
                    (content, searchText, replaceWithString, GetRegExOptions());

                //Create a new object to write a file
                using (StreamWriter sw = new StreamWriter(file, false, Encoding.Unicode))
                {
                    //Write the updated file
                    sw.Write(newText);
                }

                //A match was found and replaced
                return true;
            }

            //No match found and replaced
            return false;
        }

        /// <summary>
        /// Gets the text to find based on the selected options.
        /// </summary>
        /// <param name="textToFind">The text to find in the file.</param>
        /// <returns>The text to search for.</returns>
        private string GetSearchText(string textToFind)
        {
            //Copy the text to find into the search text variable
            //Make the text regex safe
            string searchText = Regex.Escape(textToFind);

            //Is the match whole word box checked
            if (isMatchWholeWord)
            {
                //Update the search string to find whole words only
                searchText = string.Format("{0}{1}{0}", @"\b", textToFind);
            }

            //Is the wildcard box checked
            if (isWildcard)
            {
                searchText = searchText.Replace(@"\*", ".*").Replace(@"\?", ".");
            }

            return searchText;
        }


        /// <summary>
        /// Adds options to the expression.
        /// </summary>
        /// <returns>A Regex options object.</returns>
        private RegexOptions GetRegExOptions()
        {
            //Create a new option
            RegexOptions options = new RegexOptions();

            //Is the match case check box checked
            if (isMatchCase == false)
                options |= RegexOptions.IgnoreCase;

            //Return the options
            return options;
        }
    }
    

}
