// ReflectorData.cs
// <copyright file="ReflectorData.cs"> This code is protected under the MIT License. </copyright>
namespace EnigmaUtilities.Data
{
    /// <summary>
    /// A class that holds data about reflectors to be used.
    /// </summary>
    public class ReflectorData : ComponentData
    {        
        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectorData" /> class.
        /// </summary>
        /// <param name="name"> The name of the reflector. </param>
        /// <param name="wiring"> The wiring of the reflector. </param>
        public ReflectorData(string name, string wiring)
        {
            this.Name = name;
            this.Wiring = wiring;
        }
    }
}
