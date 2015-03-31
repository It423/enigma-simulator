// ETW.cs
// <copyright file="ETW.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using System.Linq;

namespace EnigmaUtilities.Components
{
    /// <summary>
    /// An implementation of the ETW component for the enigma machine.
    /// </summary>
    public class ETW : Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ETW" /> class.
        /// </summary>
        /// <param name="plugs"> The corrosponding values for the alphabet. </param>
        public ETW(string alphabeticValues)
        {
            // Make sure alphabetic values are lower case
            alphabeticValues = alphabeticValues.ToLower();

            // Turn each plug connection into an encryption element in the dictionary 
            this.EncryptionKeys = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                this.EncryptionKeys.Add(i.ToChar(), alphabeticValues[i]);
            }
        }

        /// <summary>
        /// Turns a letter into another using the current ETW settings.
        /// </summary>
        /// <param name="c"> The character to change. </param>
        /// <returns> The changed character. </returns>
        public override char Encrypt(char c)
        {
            return this.EncryptionKeys[c];
        }
    }
}
