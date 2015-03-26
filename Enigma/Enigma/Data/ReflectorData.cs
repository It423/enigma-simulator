// ReflectorData.cs
// <copyright file="ReflectorData.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Data
{
    /// <summary>
    /// A class that holds data about reflectors to be used.
    /// </summary>
    public class ReflectorData
    {        
        /// <summary>
        /// Initailizes a new instance of the <see cref="ReflectorData" /> class.
        /// </summary>
        /// <param name="name"> The name of the reflector. </param>
        /// <param name="wiring"> The wiring of the reflector. </param>
        public ReflectorData(string name, string wiring)
        {
            this.Name = name;
            this.Wiring = wiring;
        }

        /// <summary>
        /// Gets or sets the name of the reflector.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the wiring to be used for this reflector.
        /// </summary>
        public string Wiring { get; set; }
    }
}
