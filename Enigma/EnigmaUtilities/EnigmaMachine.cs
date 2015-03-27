// EnigmaMachine.cs
// <copyright file="EnigmaMachine.cs"> This code is protected under the MIT License. </copyright>
using System.Linq;
using EnigmaUtilities.Components;
using EnigmaUtilities.Components.EventArgs;

namespace EnigmaUtilities
{
    /// <summary>
    /// An implementation of the enigma machine.
    /// </summary>
    public class EnigmaMachine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnigmaMachine" /> class.
        /// </summary>
        /// <param name="reflector"> The reflector to be used by the machine. </param>
        /// <param name="rotors"> The rotors to be used by the machine. </param>
        /// <param name="plugboard"> The plug board to be used by the machine. </param>
        public EnigmaMachine(Reflector reflector, Rotor[] rotors, Plugboard plugboard)
        {
            // Set up components
            this.Reflector = reflector;
            this.Plugboard = plugboard;
            this.Rotors = rotors;

            // Apply to ring event handlers
            this.ApplyToRotorEvents();
        }

        /// <summary>
        /// Gets or sets the reflector.
        /// </summary>
        public Reflector Reflector { get; protected set; }

        /// <summary>
        /// Gets or sets the rotors.
        /// </summary>
        public Rotor[] Rotors { get; protected set; }

        /// <summary>
        /// Gets or sets the plug board.
        /// </summary>
        public Plugboard Plugboard { get; protected set; }

        /// <summary>
        /// Applies to the rotor events.
        /// </summary>
        public void ApplyToRotorEvents()
        {
            for (int i = 0; i < this.Rotors.Length; i++)
            {
                this.Rotors[i].RotorNotchActivated += this.HandleNotchActivate;
            }
        }

        /// <summary>
        /// Encrypts a letter with the enigma machine.
        /// </summary>
        /// <param name="c"> The letter to encrypt. </param>
        /// <returns> The encrypted letter. </returns>
        public char Encrypt(char c)
        {
            // Make the letter lower case
            c = c.ToString().ToLower()[0];

            // Only encrypt if its a letter in the alphabet
            if (Resources.Alphabet.Contains(c))
            {
                // Rotate last rotor
                this.Rotors[this.Rotors.Length - 1].Rotate();

                // Send the signal through the plugboard
                char newChar = this.Plugboard.Encrypt(c);
                
                // Run the signal down the rotors, through the reflector then back down the rotors
                int nextRotorAddition = -1;
                for (int nextRotor = this.Rotors.Length - 1; nextRotor < this.Rotors.Length; nextRotor += nextRotorAddition)
                {
                    // Check if the rotor has reached the end of the run
                    if (nextRotor == -1)
                    {
                        // Run through the reflector then run back the other way
                        newChar = this.Reflector.Encrypt(newChar);
                        nextRotorAddition = 1;
                        continue;
                    }

                    // Make the rotor run the correct way
                    this.Rotors[nextRotor].RightToLeft = nextRotorAddition < 0 ? true : false;

                    // Run the signal through the rotor
                    newChar = this.Rotors[nextRotor].Encrypt(newChar);
                }

                // Send the signal through the plugboard again
                newChar = this.Plugboard.Encrypt(newChar);

                return newChar;
            }
            else
            {
                // Otherwise return nothing
                return '\0';
            }
        }

        /// <summary>
        /// Resets the rotors to their starting positions.
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < this.Rotors.Length; i++)
            {
                this.Rotors[i].Reset();
            }
        }

        /// <summary>
        /// Handles the turning notch activating on the rotors.
        /// </summary>
        /// <param name="sender"> The origin of the event. </param>
        /// <param name="eventArgs"> The event arguments. </param>
        protected void HandleNotchActivate(object sender, RotorNotchActivatedEventArgs eventArgs)
        {
            // Advance the next rotor
            if (eventArgs.RotorNumber > 0)
            {
                this.Rotors[eventArgs.RotorNumber - 1].Rotate();
            }
        }
    }
}
