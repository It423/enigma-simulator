// RotorTurnedEventArgs.cs
// <copyright file="RotorTurnedEventArgs.cs"> This code is protected under the MIT License. </copyright>
namespace EnigmaUtilities.Components.EventArgs
{
    /// <summary>
    /// The event arguments for when a rotor rotates.
    /// </summary>
    public class RotorTurnedEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RotorTurnedEventArgs" /> class.
        /// </summary>
        /// <param name="newPos"> The new position of the rotor. </param>
        /// <param name="rotorNumber"> The rotor number. </param>
        public RotorTurnedEventArgs(char newPos, int rotorNumber)
        {
            this.NewPosition = newPos;
            this.RotorNumber = rotorNumber;
        }

        /// <summary>
        /// Gets or sets the rotor identification number.
        /// </summary>
        public int RotorNumber { get; set; }

        /// <summary>
        /// Gets or sets the position of the rotor.
        /// </summary>
        public char NewPosition { get; set; }
    }
}
