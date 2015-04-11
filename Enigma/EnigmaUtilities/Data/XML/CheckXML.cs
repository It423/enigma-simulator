// CheckXML.cs
// <copyright file="CheckXML.cs"> This code is protected under the MIT License. </copyright>
using System.Xml.Linq;

namespace EnigmaUtilities.Data.XML
{
    /// <summary>
    /// A static class used for checking the validity of xml data about components.
    /// </summary>
    public static class CheckXML
    {
        /// <summary>
        /// Checks the validity of reflector data xml.
        /// </summary>
        /// <param name="x"> The xml to check. </param>
        /// <returns> Whether the xml was valid. </returns>
        public static bool ValidReflectorXML(XElement x)
        {
            // TODO: implement
            return true;
        }

        /// <summary>
        /// Checks the validity of rotor data xml.
        /// </summary>
        /// <param name="x"> The xml to check. </param>
        /// <returns> Whether the xml was valid. </returns>
        public static bool ValidRotorXML(XElement x)
        {
            // TODO: implement
            return true;
        }

        /// <summary>
        /// Checks whether a <see cref="XElement" /> contians certain attributes.
        /// </summary>
        /// <param name="x"> The xml to check. </param>
        /// <param name="attributeNames"> The list of attribute names. </param>
        /// <returns> Whether the xml contains all of the attributes. </returns>
        public static bool ContainsAttributes(XElement x, string[] attributeNames)
        {
            // Check over all the attributes
            foreach (string attribute in attributeNames)
            {
                if (x.Attribute(attribute) == null)
                {
                    // Return false if the attribute does not exist
                    return false;
                }
            }

            return true;
        }
    }
}
