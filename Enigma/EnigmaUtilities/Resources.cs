// Resources.cs
// <copyright file="Resources.cs"> This code is protected under the MIT License. </copyright>
using System.Linq;

namespace EnigmaUtilities
{
    /// <summary>
    /// A static class with commonly used functions throughout multiple classes.
    /// </summary>
    public static class Resources
    {        
        /// <summary>
        /// Gets the alphabet as a string.
        /// </summary>
        public static string Alphabet
        {
            get
            {
                return "abcdefghijklmnopqrstuvwxyz";
            }
        }

        /// <summary>
        /// Performs modulus operations on an integer.
        /// </summary>
        /// <param name="i"> The integer to modulus. </param>
        /// <param name="j"> The integer to modulus by. </param>
        /// <returns> The integer after modulus operations. </returns>
        /// <remarks> Performs negative modulus python style. </remarks>
        public static int Mod(int i, int j)
        {
            return ((i % j) + j) % j;
        }

        /// <summary>
        /// Gets the character value of an integer.
        /// </summary>
        /// <param name="i"> The integer to convert to a character. </param>
        /// <param name="lowerCase"> Whether the character needs to be lower case. </param>
        /// <returns> The character value of the integer. </returns>
        public static char ToChar(this int i, bool lowerCase = true)
        {
            return lowerCase ? Alphabet[Mod(i, 26)] : Alphabet.ToUpper()[Mod(i, 26)];
        }

        /// <summary>
        /// Gets the index in the alphabet of a character.
        /// </summary>
        /// <param name="c"> The character to convert to an integer. </param>
        /// <returns> The integer value of the character. </returns>
        /// <remarks> Will return -1 if not in the alphabet. </remarks>
        public static int ToInt(this char c)
        {
            // Make sure c is lower case
            c = c.ToString().ToLower()[0];

            return Alphabet.Contains(c) ? Alphabet.IndexOf(c) : -1;
        }

        /// <summary>
        /// Removes duplicate characters from a string.
        /// </summary>
        /// <param name="s"> The string. </param>
        /// <returns> The string without duplicates. </returns>
        public static string RemoveDuplicateCharacters(string s)
        {
            // The string without duplicates
            string result = string.Empty;

            // Add each new character to the string
            foreach (char c in s)
            {
                if (!result.Contains(c.ToString()))
                {
                    result += c;
                }
            }

            return result;
        }
    }
}
