// ReadXML.cs
// <copyright file="ReadXML.cs"> This code is protected under the MIT License. </copyright>
using System.Xml.Linq;

namespace EnigmaUtilities.Data.XML
{
    /// <summary>
    /// A static class used for reading xml data about components.
    /// </summary>
    public static class ReadXML
    {
        /// <summary>
        /// Converts an instance of the <see cref="XElement "/> class to a <see cref="ReflectorData" /> instance.
        /// </summary>
        /// <param name="x"> The <see cref="XElement" /> instance. </param>
        /// <returns> The equivalent reflector data from the xml element. </returns>
        public static ReflectorData ToReflectorData(this XElement x)
        {
            // Check validity
            if (!CheckXML.ValidReflectorXML(x))
            {
                // Not valid so return nothing
                return null;
            }

            // Convert the x element into reflector data
            string name = x.Attribute("Name").Value;
            string wiring = x.Attribute("Wiring").Value.ToLower();
            return new ReflectorData(name, wiring);
        }

        /// <summary>
        /// Converts an instance of the <see cref="XElement "/> class to a <see cref="RotorData" /> instance.
        /// </summary>
        /// <param name="x"> The <see cref="XElement" /> instance. </param>
        /// <returns> The equivalent rotor data from the xml element. </returns>
        public static RotorData ToRotorData(this XElement x)
        {
            // Check validity
            if (!CheckXML.ValidRotorXML(x))
            {
                // Not valid so return nothing
                return null;
            }

            // Convert the x element into rotor data
            string name = x.Attribute("Name").Value;
            string wiring = x.Attribute("Wiring").Value.ToLower();
            string turningNotches = x.Attribute("TurningNotches").Value.ToLower();
            return new RotorData(name, wiring, turningNotches);
        }
    }
}
