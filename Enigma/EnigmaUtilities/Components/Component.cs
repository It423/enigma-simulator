// Component.cs
// <copyright file="Component.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;

namespace EnigmaUtilities.Components
{
    /// <summary>
    /// An abstract super class for each component in the enigma machine.
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// Gets or sets a dictionary all letters and what they get encrypted to.
        /// </summary>
        protected Dictionary<char, char> EncryptionKeys { get; set; }

        /// <summary>
        /// Encrypts a letter with the current encryption keys.
        /// </summary>
        /// <param name="c"> The character to encrypt. </param>
        /// <returns> The encrypted character. </returns>
        public abstract char Encrypt(char c);
    }
}
