// CreateXML.cs
// <copyright file="CreateXML.cs"> This code is protected under the MIT License. </copyright>
using System.Xml.Linq;

namespace EnigmaUtilities.Data.XML
{
    /// <summary>
    /// A static class used for constructing xml data about components.
    /// </summary>
    public static class CreateXML
    {
        /// <summary>
        /// Coverts a <see cref="ReflectorData" /> instance into a <see cref="XElement" /> instance.
        /// </summary>
        /// <param name="rd"> The rotor data. </param>
        /// <returns> The <see cref="XElement" /> value for the current rotor data. </returns>
        public static XElement ToXElement(this ReflectorData rd)
        {
            return new XElement(
                "ReflectorData",
                new XAttribute("Name", rd.Name),
                new XAttribute("Wiring", rd.Wiring));
        }
        
        /// <summary>
        /// Coverts a <see cref="RotorData" /> instance into a <see cref="XElement" /> instance.
        /// </summary>
        /// <param name="rd"> The rotor data. </param>
        /// <returns> The <see cref="XElement" /> value for the current rotor data. </returns>
        public static XElement ToXElement(this RotorData rd)
        {
            return new XElement(
                "RotorData",
                new XAttribute("Name", rd.Name),
                new XAttribute("Wiring", rd.Wiring),
                new XAttribute("TunringNotches", rd.TunringNotches));
        }
    }
}
