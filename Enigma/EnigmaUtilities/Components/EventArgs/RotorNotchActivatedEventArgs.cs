// RotorNotchActivatedEventArgs.cs
// <copyright file="RotorNotchActivatedEventArgs.cs"> This code is protected under the MIT License. </copyright>
namespace EnigmaUtilities.Components.EventArgs
{
    /// <summary>
    /// The event arguments for when a rotor reaches its notch to turn the next rotor.
    /// </summary>
    public class RotorNotchActivatedEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RotorNotchActivatedEventArgs" /> class.
        /// </summary>
        /// <param name="rotorNumber"> The identification number of the rotor. </param>
        public RotorNotchActivatedEventArgs(int rotorNumber)
        {
            this.RotorNumber = rotorNumber;
        }

        /// <summary>
        /// Gets or sets the rotor identification number.
        /// </summary>
        public int RotorNumber { get; set; }
    }
}
