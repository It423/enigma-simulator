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
        /// <param name="rotor1"> The first rotor to be used by the machine. </param>
        /// <param name="rotor2"> The second rotor to be used by the machine. </param>
        /// <param name="rotor3"> The third rotor to be used by the machine. </param>
        /// <param name="plugboard"> The plug board to be used by the machine. </param>
        public EnigmaMachine(Reflector reflector, Rotor rotor1, Rotor rotor2, Rotor rotor3, Plugboard plugboard)
        {
            // Set up components
            this.Reflector = reflector;
            this.Plugboard = plugboard;
            this.Rotors = new Rotor[3];
            this.Rotors[0] = rotor1;
            this.Rotors[1] = rotor2;
            this.Rotors[2] = rotor3;

            // Apply to ring event handlers
            this.ApplyToRotorEvents();
        }

        /// <summary>
        /// Gets or sets the reflector.
        /// </summary>
        public Reflector Reflector { get; set; }

        /// <summary>
        /// Gets or sets the rotors.
        /// </summary>
        public Rotor[] Rotors { get; set; }

        /// <summary>
        /// Gets or sets the plug board.
        /// </summary>
        public Plugboard Plugboard { get; set; }

        /// <summary>
        /// Applies to the rotor events.
        /// </summary>
        public void ApplyToRotorEvents()
        {
            for (int i = 0; i < 3; i++)
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
                // Rotate dail
                this.Rotors[2].Rotate();

                // Send the signal through the plugboard
                char newChar = this.Plugboard.Encrypt(c);
                
                // Run the signal down the rotors, through the reflector then back down the rotors
                int nextRotorAddition = -1;
                for (int nextRotor = 2; nextRotor < 3; nextRotor += nextRotorAddition)
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

                    // Run the rotor
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
        /// Handles the turning notch activating on the rotors.
        /// </summary>
        /// <param name="origin"> The origin of the event. </param>
        /// <param name="eventArgs"> The event arguments. </param>
        public void HandleNotchActivate(object origin, RotorNotchActivatedEventArgs eventArgs)
        {
            // Advance the next rotor
            if (eventArgs.RotorNumber > 0)
            {
                this.Rotors[eventArgs.RotorNumber - 1].Rotate();
            }
        }
    }
}
