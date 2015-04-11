// CheckXML.cs
// <copyright file="CheckXML.cs"> This code is public under the MIT License. </copyright>
using System.Collections.Generic;
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
            if (!ContainsAttributes(x, new string[] { "Name", "Wiring" }))
            {
                // Does not contain attributes so return false
                return false;
            }

            // Return the validity of wiring
            return ValidWiring(x, true);
        }

        /// <summary>
        /// Checks the validity of rotor data xml.
        /// </summary>
        /// <param name="x"> The xml to check. </param>
        /// <returns> Whether the xml was valid. </returns>
        public static bool ValidRotorXML(XElement x)
        {
            if (!ContainsAttributes(x, new string[] { "Name", "Wiring", "TunringNotches" }))
            {
                // Does not contain attributes so return false
                return false;
            }

            // Return validity of turning notches and wiring
            return ValidWiring(x) && ValidTurningNotches(x);
        }

        /// <summary>
        /// Checks whether a <see cref="XElement" /> contains certain attributes.
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

        /// <summary>
        /// Checks the validity of wiring contained in an xml element.
        /// </summary>
        /// <param name="x"> The xml element. </param>
        /// <param name="reflector"> Whether the wiring is for reflectors. </param>
        /// <returns> Whether the wiring is valid. </returns>
        private static bool ValidWiring(XElement x, bool reflector = false)
        {
            // Get wiring
            string wiring = x.Attribute("Wiring").Value.ToLower();

            // If its the incorrect length then return false
            if (wiring.Length != 26)
            {
                return false;
            }

            // Make sure the wiring only contains one of each letter
            for (int i = 0; i < 26; i++)
            {
                // If the letter is not in the wiring its invalid
                if (!wiring.Contains(Resources.Alphabet[i].ToString()))
                {
                    return false;
                }
            }

            if (reflector)
            {
                // Return the reflector validity if need be
                return ValidReflectorWiring(x);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks the validity of reflector wiring.
        /// </summary>
        /// <param name="x"> The xml element. </param>
        /// <returns> Whether the wiring was valid. </returns>
        private static bool ValidReflectorWiring(XElement x)
        {
            // Get wiring
            string wiring = x.Attribute("Wiring").Value.ToLower();

            // Create a dictionary of the wiring
            Dictionary<char, char> wiringDictionary = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                wiringDictionary.Add(i.ToChar(), wiring[i]);
            }

            // Check each character has a loop 
            for (int i = 0; i < 26; i++)
            {
                // Get what the character leads to
                char leadsTo = wiringDictionary[i.ToChar()];

                // Check if that leads back to the ith letter in the alphabet
                if (wiringDictionary[leadsTo] != i.ToChar())
                {
                    // No loop was formed so the wiring is invalid
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the validity of turning notches contained in an xml element.
        /// </summary>
        /// <param name="x"> The xml element. </param>
        /// <returns> Whether the turning notches is valid. </returns>
        private static bool ValidTurningNotches(XElement x)
        {
            // Get the turning notches
            string turningNotches = x.Attribute("TunringNotches").Value.ToLower();

            // If the letter is not in the alphabet the turning notches are invalid
            foreach (char c in turningNotches)
            {
                if (!Resources.Alphabet.Contains(c.ToString()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
