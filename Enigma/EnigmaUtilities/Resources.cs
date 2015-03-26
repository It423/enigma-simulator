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
        /// <returns> The character value of the integer. </returns>
        public static char ToChar(this int i)
        {
            return Alphabet[Mod(i, 26)];
        }

        /// <summary>
        /// Gets the index in the alphabet of a character.
        /// </summary>
        /// <param name="c"> The character to convert to an integer. </param>
        /// <returns> The integer value of the character. </returns>
        /// <remarks> Will return -1 if not in the alphabet. </remarks>
        public static int ToInt(this char c)
        {
            return Alphabet.Contains(c) ? Alphabet.IndexOf(c) : -1;
        }
    }
}
