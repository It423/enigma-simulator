// RotorData.cs
// <copyright file="RotorData.cs"> This code is protected under the MIT License. </copyright>
namespace EnigmaUtilities.Data
{
    /// <summary>
    /// A class that holds data about rotors to be used.
    /// </summary>
    public class RotorData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RotorData" /> class.
        /// </summary>
        /// <param name="name"> The name of the rotor. </param>
        /// <param name="wiring"> The wiring of the rotor. </param>
        /// <param name="turningNotches"> The letters that will turn the next rotor. </param>
        public RotorData(string name, string wiring, string turningNotches)
        {
            this.Name = name;
            this.Wiring = wiring;
            this.TunringNotches = turningNotches;
        }

        /// <summary>
        /// Gets or sets the name of the rotor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the wiring to be used for this rotor.
        /// </summary>
        public string Wiring { get; set; }

        /// <summary>
        /// Gets or sets the letter that will turn the rotor.
        /// </summary>
        public string TunringNotches { get; set; }
    }
}
