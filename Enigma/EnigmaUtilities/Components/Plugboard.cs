// Plugboard.cs
// <copyright file="Plugboard.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using System.Linq;

namespace EnigmaUtilities.Components
{
    /// <summary>
    /// An implementation of the plug board component for the enigma machine.
    /// </summary>
    public class Plugboard : Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Plugboard" /> class.
        /// </summary>
        /// <param name="plugs"> The plugs to be used. </param>
        public Plugboard(string[] plugs)
        {
            // Turn each plug connection into an encryption element in the dictionary 
            this.EncryptionKeys = new Dictionary<char, char>();
            foreach (string plug in plugs)
            {
                // Add both ways round so its not required to look backwards across the plugboard during the encryption
                this.EncryptionKeys.Add(plug[0], plug[1]);
                this.EncryptionKeys.Add(plug[1], plug[0]);
            }
        }

        /// <summary>
        /// Encrypts a letter with the current plug board settings.
        /// </summary>
        /// <param name="c"> The character to encrypt. </param>
        /// <returns> The encrypted character. </returns>
        public override char Encrypt(char c)
        {
            return this.EncryptionKeys.Keys.Contains(c) ? this.EncryptionKeys[c] : c;
        }
    }
}
